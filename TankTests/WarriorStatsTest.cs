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
    public class WarriorStatsTest
    {
        private Warrior player;

        public WarriorStatsTest()
        {
            player = new Warrior()
            {
                Name = "Warrior",
                MasteryRating = 1736,
                CritRating = 1210,
                HasteRating = 800,
                VersatilityRating = 1421,
                Leech= 2,
                Strength = 4681,
                Stamina = 8243,
                MaxHealth = 494580,
                Weapons =
                {
                    new Weapon()
                    {
                        LowDamage = 5823,
                        HighDamage = 6715,
                        Speed = 2.41m
                    }
                }
            };
        }

        [Test]
        public void MasteryCorrect()
        {
            player.Mastery.Should().BeApproximately(0.3567m, 0.0001m);
        }

        [Test]
        public void CritCorrect()
        {
            player.CritChance.Should().BeApproximately(0.1600m, 0.0001m);
        }

        [Test]
        public void HasteCorrect()
        {
            player.Haste.Should().BeApproximately(0.0800m, 0.0001m);
        }

        [Test]
        public void AttackPowerCorrect()
        {
            player.AttackPower.Should().Be(4681);
        }

        [Test]
        public void VersatilityDamageIncreaseCorrect()
        {
            player.VersatilityDamageIncrease.Should().BeApproximately(0.1093m, 0.0001m);
        }

        [Test]
        public void VersatilityDamageReductionCorrect()
        {
            player.VersatilityDamageReduction.Should().BeApproximately(0.0547m, 0.0001m);
        }

        [Test]
        public void LeechCorrect()
        {
            player.LeechPercentage.Should().BeApproximately(0.0251m,0.0001m);
        }

        [Test]
        public void DodgeCorrect()
        {
            player.DodgeChance.Should().BeApproximately(0.0300m, 0.0001m);
        }

        [Test]
        public void ParryCorrect()
        {
            player.ParryChance.Should().BeApproximately(0.1623m, 0.0001m);
        }

        [Test]
        public void BlockCorrect()
        {
            player.BlockChance.Should().BeApproximately(0.2731m, 0.0001m);
        }

        [Test]
        public void CritBlockCorrect()
        {
            player.CritBlockChance.Should().BeApproximately(0.3567m, 0.0001m);
        }
    }
}
