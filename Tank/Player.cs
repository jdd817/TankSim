using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;
using Tank.Abilities;

namespace Tank
{
    /// <summary>
    /// Base class for all players.  Derived class is responsible for tracking any class-specific state.
    /// </summary>
    public abstract class Player : Actor
    {
        public Player()
        {
            Buffs = new BuffManager();
            Cooldowns = new CooldownManager();
            Weapons = new List<Weapon>();
            GCDLength = 1.5m;
        }

        #region constants

        protected decimal BaseDodge;
        protected decimal BaseParry;
        protected decimal BaseBlock;

        protected decimal V = 0.018m;

        #endregion

        #region directly editable attributes

        private int _maxHealth;

        public virtual int MaxHealth
        {
            get
            {
                return (int)(_maxHealth * (1.0m + Buffs.GetPercentageAdjustment(StatType.Stamina)));
            }
            set { _maxHealth = value; }
        }

        private int _currentHealth;
        public int CurrentHealth
        {
            get { return _currentHealth; }
            set
            {
                _currentHealth = value;
                if (_currentHealth > MaxHealth)
                    _currentHealth = MaxHealth;
            }
        }

        public decimal HealthPercentage
        {
            get
            {
                return (CurrentHealth * 1m) / MaxHealth;
            }
        }

        public string Name
        { get; set; }

        public int DodgeRating
        { get; set; }

        public int ParryRating
        { get; set; }

        public int MasteryRating
        { get; set; }

        public int HitRating
        { get; set; }

        public int ExpertiseRating
        { get; set; }

        public int HasteRating
        { get; set; }

        public int CritRating
        { get; set; }

        public int VersatilityRating
        { get; set; }

        public int Leech
        { get; set; }

        public int Stamina
        { get; set; }

        public int Strength
        { get; set; }

        public int Agility
        { get; set; }

        public int Armor
        { get; set; }

        #endregion

        #region calculated properties        

        [XmlIgnore]
        public virtual decimal DodgeChance
        {
            get
            {
                return BaseDodge
                    + GetDiminishedAvoidance(
                        RatingConverter.GetRating(StatType.Dodge, DodgeRating + Buffs.GetRatingAdjustment(StatType.Dodge))
                            + Buffs.GetPercentageAdjustment(StatType.Dodge));
            }
        }

        [XmlIgnore]
        public virtual decimal ParryChance
        {
            get
            {
                return BaseParry
                    + GetDiminishedAvoidance(
                        RatingConverter.GetRating(StatType.Parry, ParryRating + Buffs.GetRatingAdjustment(StatType.Parry))
                        + Buffs.GetPercentageAdjustment(StatType.Parry));
            }
        }

        [XmlIgnore]
        public virtual decimal BlockChance
        {
            get
            {
                return 0;
            }
        }

        [XmlIgnore]
        public virtual decimal MissChance
        { get { return Math.Max(0.0m, 0.075m - RatingConverter.GetRating(StatType.Hit, HitRating)); } }

        [XmlIgnore]
        public virtual decimal CritChance
        { get { return 0.05m + RatingConverter.GetRating(StatType.Crit, CritRating); } }

        [XmlIgnore]
        public virtual decimal Mastery { get { return 0.12m + RatingConverter.GetRating(StatType.Mastery, MasteryRating); } }

        [XmlIgnore]
        public virtual decimal Haste
        { get { return RatingConverter.GetRating(StatType.Haste, HasteRating) + Buffs.GetPercentageAdjustment(StatType.Haste);} }

        public abstract int AttackPower
        { get; }

        [XmlIgnore]
        public decimal VersatilityDamageIncrease
        { get { return RatingConverter.GetRating(StatType.Versatility, VersatilityRating) + Buffs.GetPercentageAdjustment(StatType.Versatility); } }

        [XmlIgnore]
        public decimal VersatilityDamageReduction
        { get { return (RatingConverter.GetRating(StatType.Versatility, VersatilityRating) + Buffs.GetPercentageAdjustment(StatType.Versatility)) / 2m; } }

        [XmlIgnore]
        public decimal LeechPercentage
        { get { return RatingConverter.GetRating(StatType.Leech, Leech) + Buffs.GetPercentageAdjustment(StatType.Leech); } }

        [XmlIgnore]
        public decimal ArmorDamageReduction
        { get { return RatingConverter.GetRating(StatType.Armor, (int)((Armor + Buffs.GetRatingAdjustment(StatType.Armor)) * (1 + Buffs.GetPercentageAdjustment(StatType.Armor)))); } }

        #endregion

        #region state properties

        [XmlIgnore]
        public IBuffManager Buffs
        { get; set; }

        [XmlIgnore]
        public ICooldownManager Cooldowns
        { get; set; }

        public List<Weapon> Weapons
        { get; set; }

        public decimal GCDLength { get; set; }

        #endregion

        #region public methods
        
        protected decimal GetDiminishedAvoidance(decimal Avoidance)
        {
            /*
                BLUE REGARDING DIMINISHING RETURNS:

                An update for tank theorycrafters:

                The diminishing returns equations for avoidance should be updated in an upcoming build. It is generally simplified and unified across all of the classes, and also adjusted to make sure it prevents avoidance from getting excessively high during the expansion.

                For people familiar with avoidance DR, the basic setup is unchanged: 
                    it applies to dodge or parry from Agility, Strength, rating, or crit-rating passives, but not to the base 3%. 
    
                The new DR curve is:

                A = P/(PV+H)

                A is the final % [dodge or parry], after DR
                P is the % [dodge or parry] from the above sources, before DR
                V is 0.0315 for Demon Hunters, 0.018 for everyone else
                H is 1/0.97 for Druids, 1/0.94 for everyone else

                (For people who remember that there used to be another variable F that varied by class and avoidance type--in this data, F is 1 in all cases).
            */
            return ((Avoidance * 100m) / ((Avoidance * 100m) * V + (1.0m / 0.94m))) / 100m;
        }

        public virtual int ApplyHealing(int healingAmount)
        {
            CurrentHealth += healingAmount;
            return healingAmount;
        }

        #endregion

        #region methods to be implimented in class model

        /// <summary>
        /// Gets the next ability that the player uses
        /// </summary>
        /// <returns>An ability if one is used, null otherwise</returns>
        public abstract Abilities.Ability GetAbilityUsed(IBuffManager MobBuffs);

        /// <summary>
        /// Update state based on the result of an ability
        /// </summary>
        /// <param name="Ability"></param>
        /// <param name="Result"></param>
        public abstract void UpdateAbilityResults(decimal CurrentTime, Abilities.Ability Ability, AbilityResult Result);

        /// <summary>
        /// Updates the player when the tick is done
        /// </summary>
        /// <param name="DeltaTime"></param>
        public virtual void UpdateTimeElapsed(decimal DeltaTime)
        {
            UpdateFromTickingBuffs(Buffs.DecrementBuffTimers(DeltaTime));
            Cooldowns.UpdateTimers(DeltaTime);
            foreach (Weapon W in Weapons)
                W.SwingTimer -= DeltaTime;
        }

        /// <summary>
        /// Called when the player is attacked
        /// </summary>
        /// <param name="MobAttack"></param>
        /// <param name="Result"></param>
        public abstract void UpdateFromMobAttack(decimal CurrentTime, Abilities.Attack MobAttack, AttackResult Result);

        public virtual void UpdateFromTickingBuffs(IEnumerable<Buffs.Buff> TickingBuffs)

        {
            foreach (var hot in TickingBuffs.OfBaseType<Buffs.HealOverTime>())
            {
                ApplyHealing(hot.HealingPerTick);
                DataLogging.DataLogManager.LogEvent(new DataLogging.DamageEvent
                {
                    Time = DataLogging.DataLogManager.CurrentTime,
                    DamageHealed = hot.HealingPerTick
                });
            }
        }

        public abstract void Reset();

        #endregion

        #region protected mangement methods

        /// <summary>
        /// Gets the white damange attack for the player
        /// </summary>
        /// <returns></returns>
        public virtual Abilities.Attack GetAttack()
        {
            foreach (Weapon W in Weapons)
            {
                if (W.SwingTimer <= 0)
                {
                    W.SwingTimer += W.Speed;
                    return new Abilities.Attack(W.Damage);
                }
            }
            return null;
        }

        #endregion
    }
}
