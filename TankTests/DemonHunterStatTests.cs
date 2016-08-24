using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank;
using Tank.Classes;

namespace TankTests
{
    [TestFixture]
    public class DemonHunterStatTests
    {
        private DemonHunter player;

        public DemonHunterStatTests()
        {
            player = new DemonHunter()
            {
                Name = "DemonHunter",
                MasteryRating = 1520,
                CritRating = 2093,
                HasteRating = 1969,
                VersatilityRating = 351,
                Leech = 0,
                Agility = 5892,
                Stamina = 8571,
                MaxHealth = 514260,
                Armor = 2605,
                Weapons =
                {
                    new Weapon()
                    {
                        LowDamage = 6484,
                        HighDamage = 7350,
                        Speed = 2.15m
                    }
                }
            };
        }

        [Test]
        public void MasteryCorrect()
        {
            player.Mastery.Should().BeApproximately(0.3273m, 0.0001m);
        }

        [Test]
        public void CritCorrect()
        {
            player.CritChance.Should().BeApproximately(0.3403m, 0.0001m);
        }

        [Test]
        public void HasteCorrect()
        {
            player.Haste.Should().BeApproximately(0.2089m, 0.0001m);
        }

        [Test]
        public void AttackPowerCorrect()
        {
            player.AttackPower.Should().Be(5892);
        }

        [Test]
        public void VersatilityDamageIncreaseCorrect()
        {
            player.VersatilityDamageIncrease.Should().BeApproximately(0.0270m, 0.0001m);
        }

        [Test]
        public void VersatilityDamageReductionCorrect()
        {
            player.VersatilityDamageReduction.Should().BeApproximately(0.0135m, 0.0001m);
        }

        [Test]
        public void LeechCorrect()
        {
            player.LeechPercentage.Should().BeApproximately(0.0m, 0.0001m);
        }

        [Test]
        public void DodgeCorrect()
        {
            player.DodgeChance.Should().BeApproximately(0.1791m, 0.0001m);
        }

        [Test]
        public void ParryCorrect()
        {
            player.ParryChance.Should().BeApproximately(0.1178m, 0.0001m);
        }

        [Test]
        public void BlockCorrect()
        {
            player.BlockChance.Should().BeApproximately(0, 0.0001m);
        }
    }
}
