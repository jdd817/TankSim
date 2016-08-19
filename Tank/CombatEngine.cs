using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Tank.Abilities;
using Tank.Buffs;

namespace Tank
{
    public class CombatEngine
    {
        public CombatEngine()
        {
            TimeIncrement = 0.1m;
        }

        public decimal TimeIncrement
        { get; set; }

        /// <summary>
        /// Simulates combat between a tank and a mob.  Assumes neither dies.
        /// </summary>
        /// <param name="Tank">The tank</param>
        /// <param name="Mob">Mob being tanked</param>
        /// <param name="Durration">Durration of the simulation, in seconds</param>
        /// <remarks>
        /// Each tick, the following may occur:
        /// Active player ability (ie, shield slam)
        /// Passive player ability (ie, white damage)
        /// Mob ability (ie, attack)
        /// Buff action (ie, damage from shadow word: pain)
        /// 
        /// The player abilities will originate with the player object passed in.
        /// The mob abilities will originate with the mob object passed in.
        /// Buff actions will originate from the buff object maintained on each the mob and the player
        /// The engine will be responsible for asking the mob and player what the do, communicating that action
        /// to the other actor, and processing the buffs
        /// </remarks>
        public void DoCombat(Player Tank, Mob Mob, decimal Durration, bool EndAtDeath = false, Healer[] Healers = null)
        {
            if (Healers == null)
                Healers = new Healer[0];
            foreach (var healer in Healers)
                healer.HealTimer = 0;            
            Tank.Reset();
            Mob.Reset();
            Mob.GetAttack(); //start mobs on the swing timer to simulate players always getting the jump
            for (decimal Time = 0; Time < Durration; Time += TimeIncrement)
            {
                Ability PlayerAction = Tank.GetAbilityUsed(Mob.Buffs);

                if (PlayerAction != null)
                {
                    if (PlayerAction.OnGCD)
                        Tank.GCD += Tank.GCDLength;

                    var modifiers = PlayerAction.GetModifiers(Tank.Buffs, Mob.Buffs);

                    AttackResult Result = CombatTable.GetAttackResult(Tank, Mob,
                        modifiers);

                    var actionResult = PlayerAction.GetAbilityResult(Result, Tank, Mob);
                    actionResult.AttackResult = Result;
                    actionResult.Time = Time;

                    DataLogging.DataLogManager.UsedAbility(Time, PlayerAction.GetType().Name, actionResult);

                    Tank.UpdateAbilityResults(Time, PlayerAction, actionResult);
                    foreach (Buff B in actionResult.CasterBuffsApplied)
                        Tank.Buffs.AddBuff(B);
                    foreach (Buff B in actionResult.TargetBuffsApplied)
                        Mob.Buffs.AddBuff(B);
                }

                Attack PlayerAttack = Tank.GetAttack();
                if (PlayerAttack != null)
                {
                    var modifiers = PlayerAttack.GetModifiers(Tank.Buffs, Mob.Buffs);
                    AttackResult Result = CombatTable.GetAttackResult(Tank, Mob,
                        modifiers);

                    var actionResult = PlayerAttack.GetAbilityResult(Result, Tank, Mob);
                    actionResult.AttackResult = Result;
                    actionResult.Time = Time;

                    Tank.UpdateAbilityResults(Time, PlayerAttack, actionResult);
                }

                Attack MobAttack = Mob.GetAttack();
                if (MobAttack != null)
                {
                    var modifiers = MobAttack.GetModifiers(Mob.Buffs, Tank.Buffs);
                    AttackResult Result = CombatTable.GetAttackResult(Mob, Tank,
                        modifiers);
                    
                    Tank.UpdateFromMobAttack(Time, MobAttack, Result);
                }

                //healers
                foreach(var healer in Healers)
                {
                    healer.HealTimer -= TimeIncrement;
                    if (healer.HealTimer <= 0)
                    {
                        Tank.ApplyHealing(healer.HealAmount);
                        healer.HealTimer += healer.HealPeriod;
                    }
                }

                DataLogging.DataLogManager.LogHealth(Time, Tank.CurrentHealth);

                Tank.UpdateFromTickingBuffs(Tank.Buffs.DecrementBuffTimers(TimeIncrement));
                Mob.Buffs.DecrementBuffTimers(TimeIncrement);

                Tank.UpdateTimeElapsed(TimeIncrement);
                Mob.UpdateTimeElapsed(TimeIncrement);

                if (EndAtDeath && Tank.CurrentHealth <= 0)
                    break;
            }
        }

        public void DoNCombats(Player Tank, Mob Mob, decimal CombatDurration, int N)
        {
            int i;
            for (i = 0; i < N; i++)
                DoCombat(Tank, Mob, CombatDurration);
        }
    }
}
