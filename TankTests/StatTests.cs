using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank;

namespace TankTests
{
    public abstract class StatTests<T>:TestBase<T> where T :Player
    {
        protected StatTestDescriptor stats;

        protected void Init()
        {
            Service.Name = Service.GetType().Name;
            Service.MasteryRating = stats.Mastery.Rating;
            Service.CritRating = stats.Crit.Rating;
            Service.HasteRating = stats.Haste.Rating;
            Service.VersatilityRating = stats.Versatility.Rating;
            Service.Leech = stats.Leech.Rating;
            Service.Strength = stats.PrimaryStat;
            Service.Agility = stats.PrimaryStat;
            Service.Weapons = new List<Weapon>();
            if (stats.Weapon != null)
                Service.Weapons.Add(stats.Weapon);
        }

        [Test]
        public void MasteryCorrect()
        {
            Service.Mastery.Should().BeApproximately(stats.Mastery.Expected, 0.0001m);
        }

        [Test]
        public void CritCorrect()
        {
            Service.CritChance.Should().BeApproximately(stats.Crit.Expected, 0.0001m);
        }

        [Test]
        public void HasteCorrect()
        {
            Service.Haste.Should().BeApproximately(stats.Haste.Expected, 0.0001m);
        }

        [Test]
        public void AttackPowerCorrect()
        {
            Service.AttackPower.Should().Be(stats.PrimaryStat);
        }

        [Test]
        public void VersatilityDamageIncreaseCorrect()
        {
            Service.VersatilityDamageIncrease.Should().BeApproximately(stats.Versatility.Expected, 0.0001m);
        }

        [Test]
        public void VersatilityDamageReductionCorrect()
        {
            Service.VersatilityDamageReduction.Should().BeApproximately(stats.Versatility.Expected / 2, 0.0001m);
        }

        [Test]
        public void LeechCorrect()
        {
            Service.LeechPercentage.Should().BeApproximately(stats.Leech.Expected, 0.0001m);
        }

        [Test]
        public void DodgeCorrect()
        {
            Service.DodgeChance.Should().BeApproximately(stats.Dodge, 0.0001m);
        }

        [Test]
        public void ParryCorrect()
        {
            Service.ParryChance.Should().BeApproximately(stats.Parry, 0.0001m);  //0.0878 before DR
        }

        [Test]
        public void BlockCorrect()
        {
            Service.BlockChance.Should().BeApproximately(stats.Block, 0.0001m);
        }

        [Test]
        public void WeaponDamageCorrect()
        {
            Service.WeaponDamage.Should().BeApproximately(stats.WeaponDamage, 50m);
        }

        public class StatTestDescriptor
        {
            public StatDescriptor Crit { get; set; }
            public StatDescriptor Haste { get; set; }
            public StatDescriptor Mastery { get; set; }
            public StatDescriptor Leech { get; set; }
            public StatDescriptor Versatility { get; set; }

            public decimal Dodge { get; set; }
            public decimal Parry { get; set; }
            public decimal Block { get; set; }

            public int PrimaryStat { get; set; }

            public Weapon Weapon { get; set; }
            public int WeaponDamage { get; set; }
        }

        public class StatDescriptor
        {
            public StatDescriptor() { }
            public StatDescriptor(int rating, decimal expected)
            {
                Rating = rating;
                Expected = expected;
            }

            public int Rating { get; set; }
            public decimal Expected { get; set; }
        }
    }
}
