﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Tank.Abilities;
using Tank.Buffs;

namespace Tank
{
    public class CombatEngine : ICombatEngine
    {
        private ICombatTable _combatTable;

        public CombatEngine(ICombatTable combatTable, IRng rng)
        {
            TimeIncrement = 0.1m;
            _combatTable = combatTable;
            Rng = rng;
        }

        public IRng Rng { get; set; }

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
                    ProcessPlayerAction(Tank, Mob, Time, PlayerAction);

                Attack PlayerAttack = Tank.GetAttack();
                if (PlayerAttack != null)
                    ProcessPlayerAttack(Tank, Mob, Time, PlayerAttack);

                Attack MobAttack = Mob.GetAttack();
                if (MobAttack != null)
                    ProcessMobAttack(Tank, Mob, Time, MobAttack);

                //healers
                foreach (var healer in Healers)
                {
                    healer.HealTimer -= TimeIncrement;
                    if (healer.HealTimer <= 0)
                    {
                        Tank.ApplyHealing(healer.HealAmount);
                        healer.HealTimer += healer.HealPeriod;
                    }
                }

                DataLogging.DataLogManager.LogHealth(Time, Tank.CurrentHealth);
                
                Tank.UpdateTimeElapsed(TimeIncrement);
                Mob.UpdateTimeElapsed(TimeIncrement);

                if (EndAtDeath && Tank.CurrentHealth <= 0)
                    break;
            }
        }

        private void ProcessMobAttack(Player Tank, Mob Mob, decimal Time, Attack MobAttack)
        {
            var modifiers = MobAttack.GetModifiers(Mob.Buffs, Tank.Buffs);
            AttackResult Result = _combatTable.GetAttackResult(Mob, Tank,
                modifiers);

            if (Tank.Armor > 0)
                MobAttack.Damage = (int)(MobAttack.Damage * (1m - Tank.ArmorDamageReduction));

            Tank.UpdateFromMobAttack(Time, MobAttack, Result);
        }

        private void ProcessPlayerAttack(Player Tank, Mob Mob, decimal Time, Attack PlayerAttack)
        {
            var modifiers = PlayerAttack.GetModifiers(Tank.Buffs, Mob.Buffs);
            AttackResult Result = _combatTable.GetAttackResult(Tank, Mob,
                modifiers);

            var actionResult = PlayerAttack.GetAbilityResult(Result, Tank, Mob);
            actionResult.AttackResult = Result;
            actionResult.Time = Time;

            Tank.UpdateAbilityResults(Time, PlayerAttack, actionResult);
        }

        private void ProcessPlayerAction(Player Tank, Mob Mob, decimal Time, Ability PlayerAction)
        {
            var modifiers = PlayerAction.GetModifiers(Tank.Buffs, Mob.Buffs);

            AttackResult Result = _combatTable.GetAttackResult(Tank, Mob,
                modifiers);

            var actionResult = PlayerAction.GetAbilityResult(Result, Tank, Mob);
            actionResult.AttackResult = Result;
            actionResult.Time = Time;

            Tank.Cooldowns.AbilityUsed(PlayerAction, actionResult);

            DataLogging.DataLogManager.UsedAbility(Time, PlayerAction.GetType().Name, actionResult);

            Tank.UpdateAbilityResults(Time, PlayerAction, actionResult);
            foreach (Buff B in actionResult.CasterBuffsApplied)
                Tank.Buffs.AddBuff(B);
            foreach (Buff B in actionResult.TargetBuffsApplied)
                Mob.Buffs.AddBuff(B);
        }
    }
}
