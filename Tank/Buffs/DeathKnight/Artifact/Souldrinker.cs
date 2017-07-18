using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.DeathKnight.Artifact
{
    public class Souldrinker : Buff
    {
        private List<decimal[]> _healthIncreases;
        private int _maxHealth;

        public Souldrinker(decimal overhealAmount, int maxHealth)
        {
            _healthIncreases = new List<decimal[]>();
            _healthIncreases.Add(new[] { 15m, overhealAmount / 2 });
            _maxHealth = maxHealth;
        }

        public override decimal Durration
        {
            get
            {
                return 15.0m;
            }
        }

        public override int MaxStacks
        {
            get
            {
                return 1;
            }
        }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            return 0;
        }

        public override int GetRatingModifier(StatType RatingType)
        {
            if (RatingType == StatType.MaxHealth)
                return HealthIncrease;
            return 0;
        }

        private int HealthIncrease
        {
            get { return (int)Math.Min(_healthIncreases.DefaultIfEmpty(new[] { 0m, 0m }).Sum(x => x[1]), _maxHealth * 0.30m); }
        }

        public override void TimerUpdated(decimal delta)
        {
            base.TimerUpdated(delta);
            for (var i = 0; i < _healthIncreases.Count; i++)
            {
                _healthIncreases[i][0] -= delta;
                if (_healthIncreases[i][0] <= 0)
                {
                    _healthIncreases.RemoveAt(i);
                    i--;
                }
            }
        }

        public override void Refresh(Buff NewBuff)
        {
            base.Refresh(NewBuff);
            _healthIncreases.AddRange((NewBuff as Souldrinker)._healthIncreases);
        }

        public override string ToString()
        {
            return String.Format("{0}<{1}> ({2:0.00})",
                    Name,
                    HealthIncrease,
                    TimeRemaining);
        }
    }
}
