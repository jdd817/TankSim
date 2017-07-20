using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Tank.Abilities;
using Tank.Buffs;

namespace Tank.CombatEngine
{
    public class CombatEngine : ICombatEngine
    {
        private ICombatTable _combatTable;
        private IAttackHandler _playerAttackHandler, _mobAttackHandler;
        private IAbilityHandler _playerAbilityHandler;

        public CombatEngine(ICombatTable combatTable, 
            IAttackHandler playerAttackHandler,
            IAttackHandler mobAttackHandler,
            IAbilityHandler playerAbilityHandler,
            IRng rng)
        {
            TimeIncrement = 0.1m;
            _combatTable = combatTable;
            _playerAttackHandler = playerAttackHandler;
            _mobAttackHandler = mobAttackHandler;
            _playerAbilityHandler = playerAbilityHandler;
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
                    _playerAbilityHandler.ProcessAction(Tank, Mob, Time, PlayerAction);

                Attack PlayerAttack = Tank.GetAttack();
                if (PlayerAttack != null)
                    _playerAttackHandler.ProcessAttack(Tank, Mob, Time, PlayerAttack);

                Attack MobAttack = Mob.GetAttack();
                if (MobAttack != null)
                    _mobAttackHandler.ProcessAttack(Tank, Mob, Time, MobAttack);

                var healingEffectStack = Tank.Buffs.GetEffectStack<IHealingReceivedEffectStack>();
                //healers
                foreach (var healer in Healers)
                {
                    healer.HealTimer -= TimeIncrement;
                    if (healer.HealTimer <= 0)
                    {
                        var healingEvent = new DataLogging.HealingEvent
                        {
                            Name = "Healer",
                            Amount = healer.HealAmount,
                            Time = Time
                        };
                        foreach (var effect in healingEffectStack)
                            effect.HealingReceived(healingEvent, Tank, null);
                        Tank.ApplyHealing(healingEvent.Amount);
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
    }
}
