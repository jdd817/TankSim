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
    public class WarriorStatsTest:TestBase<Warrior>
    {
        public WarriorStatsTest()
        {
            Service.Name = "Warrior";
            Service.MasteryRating = 1736;
            Service.CritRating = 1210;
            Service.HasteRating = 800;
            Service.VersatilityRating = 1421;
            Service.Leech = 2;
            Service.Strength = 4681;
            Service.Stamina = 8243;
            Service.MaxHealth = 494580;
            Service.Weapons = new List<Weapon>
                {
                    new Weapon()
                    {
                        LowDamage = 5823,
                        HighDamage = 6715,
                        Speed = 2.41m
                    }
                };
        }

        [Test]
        public void MasteryCorrect()
        {
            Service.Mastery.Should().BeApproximately(0.3567m, 0.0001m);
        }

        [Test]
        public void CritCorrect()
        {
            Service.CritChance.Should().BeApproximately(0.1600m, 0.0001m);
        }

        [Test]
        public void HasteCorrect()
        {
            Service.Haste.Should().BeApproximately(0.0800m, 0.0001m);
        }

        [Test]
        public void AttackPowerCorrect()
        {
            Service.AttackPower.Should().Be(4681);
        }

        [Test]
        public void VersatilityDamageIncreaseCorrect()
        {
            Service.VersatilityDamageIncrease.Should().BeApproximately(0.1093m, 0.0001m);
        }

        [Test]
        public void VersatilityDamageReductionCorrect()
        {
            Service.VersatilityDamageReduction.Should().BeApproximately(0.0547m, 0.0001m);
        }

        [Test]
        public void LeechCorrect()
        {
            Service.LeechPercentage.Should().BeApproximately(0.0251m,0.0001m);
        }

        [Test]
        public void DodgeCorrect()
        {
            Service.DodgeChance.Should().BeApproximately(0.0300m, 0.0001m);
        }

        [Test]
        public void ParryCorrect()
        {
            Service.ParryChance.Should().BeApproximately(0.1623m, 0.0001m);
        }

        [Test]
        public void BlockCorrect()
        {
            Service.BlockChance.Should().BeApproximately(0.2731m, 0.0001m);
        }

        [Test]
        public void CritBlockCorrect()
        {
            Service.CritBlockChance.Should().BeApproximately(0.3567m, 0.0001m);
        }
    }
}
