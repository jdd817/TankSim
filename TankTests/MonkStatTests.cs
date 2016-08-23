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
    public class MonkStatTests
    {
        private Monk player;

        public MonkStatTests()
        {
            player = new Monk()
            {
                Name = "Monk",
                MasteryRating = 1689,
                CritRating = 1623,
                HasteRating = 946,
                VersatilityRating = 1271,
                Leech = 0,
                Agility = 4670,
                Stamina = 8782,
                MaxHealth = 526920,
                Armor = 1170,
                Weapons =
                {
                    new Weapon()
                    {
                        LowDamage = 7413,
                        HighDamage = 8140,
                        Speed = 3.02m
                    }
                }
            };
        }

        [Test]
        public void MasteryCorrect()
        {
            player.Mastery.Should().BeApproximately(0.2335m, 0.0001m);
        }

        [Test]
        public void CritCorrect()
        {
            player.CritChance.Should().BeApproximately(0.2975m, 0.0001m);
        }

        [Test]
        public void HasteCorrect()
        {
            player.Haste.Should().BeApproximately(0.0946m, 0.0001m);
        }

        [Test]
        public void AttackPowerCorrect()
        {
            player.AttackPower.Should().Be(4670);
        }

        [Test]
        public void VersatilityDamageIncreaseCorrect()
        {
            player.VersatilityDamageIncrease.Should().BeApproximately(0.1089m, 0.0001m);
        }

        [Test]
        public void VersatilityDamageReductionCorrect()
        {
            player.VersatilityDamageReduction.Should().BeApproximately(0.0545m, 0.0001m);
        }

        [Test]
        public void LeechCorrect()
        {
            player.LeechPercentage.Should().BeApproximately(0.0m, 0.0001m);
        }

        [Test]
        public void DodgeCorrect()
        {
            player.DodgeChance.Should().BeApproximately(0.1207m, 0.0001m);
        }

        [Test]
        public void ParryCorrect()
        {
            player.ParryChance.Should().BeApproximately(0.0300m, 0.0001m);
        }

        [Test]
        public void BlockCorrect()
        {
            player.BlockChance.Should().BeApproximately(0, 0.0001m);
        }
    }
}
