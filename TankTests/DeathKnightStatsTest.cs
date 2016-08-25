using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank;
using Tank.Classes;

namespace TankTests
{
    [TestFixture]
    public class DeathKnightStatsTest:TestBase<DeathKnight>
    {
        private DeathKnight DK;

        public DeathKnightStatsTest()
        {
            Service.Name = "DK";
            Service.MasteryRating = 979;
            Service.CritRating = 1961;
            Service.HasteRating = 1384;
            Service.VersatilityRating = 841;
            Service.Leech = 0;
            Service.Strength = 4917;
            Service.Stamina = 9588;
            Service.RunicPowerCap = 125;
            Service.MaxHealth = 575280;
            Service.Weapons = new List<Weapon>
                {
                    new Weapon()
                    {
                        LowDamage = 8423,
                        HighDamage = 9489,
                        Speed = 3.16m
                    }
                };
        }

        [Test]
        public void MasteryCorrect()
        {
            Service.Mastery.Should().BeApproximately(0.2535m, 0.0001m);
        }

        [Test]
        public void CritCorrect()
        {
            Service.CritChance.Should().BeApproximately(0.2283m, 0.0001m);
        }

        [Test]
        public void HasteCorrect()
        {
            Service.Haste.Should().BeApproximately(0.1384m, 0.0001m);
        }

        [Test]
        public void AttackPowerCorrect()
        {
            Service.AttackPower.Should().Be(4917);
        }

        [Test]
        public void VersatilityDamageIncreaseCorrect()
        {
            Service.VersatilityDamageIncrease.Should().BeApproximately(0.0647m, 0.0001m);
        }

        [Test]
        public void VersatilityDamageReductionCorrect()
        {
            Service.VersatilityDamageReduction.Should().BeApproximately(0.0323m, 0.0001m);
        }

        [Test]
        public void LeechCorrect()
        {
            Service.LeechPercentage.Should().BeApproximately(0m, 0.0001m);
        }

        [Test]
        public void DodgeCorrect()
        {
            Service.DodgeChance.Should().BeApproximately(0.0300m, 0.0001m);
        }

        [Test]
        public void ParryCorrect()
        {
            Service.ParryChance.Should().BeApproximately(0.1898m, 0.0001m);
        }
    }
}
