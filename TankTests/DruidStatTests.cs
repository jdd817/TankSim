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
    public class DruidStatTests
    {
        private Druid player;

        public DruidStatTests()
        {
            player = new Druid()
            {
                Name = "Druid",
                MasteryRating = 1515,
                CritRating = 1346,
                HasteRating = 386,
                VersatilityRating = 245,
                Leech = 131,
                Agility = 4413,
                Stamina = 5177,
                MaxHealth = (int)(310620*1.55m),
                Armor=2130*2,
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
            player.Mastery.Should().BeApproximately(0.3266m, 0.0001m);
        }

        [Test]
        public void CritCorrect()
        {
            player.CritChance.Should().BeApproximately(0.2724m, 0.0001m);
        }

        [Test]
        public void HasteCorrect()
        {
            player.Haste.Should().BeApproximately(0.0429m, 0.0001m);
        }

        [Test]
        public void AttackPowerCorrect()
        {
            player.AttackPower.Should().Be(4413);
        }

        [Test]
        public void VersatilityDamageIncreaseCorrect()
        {
            player.VersatilityDamageIncrease.Should().BeApproximately(0.0188m, 0.0001m);
        }

        [Test]
        public void VersatilityDamageReductionCorrect()
        {
            player.VersatilityDamageReduction.Should().BeApproximately(0.0094m, 0.0001m);
        }

        [Test]
        public void LeechCorrect()
        {
            player.LeechPercentage.Should().BeApproximately(0.0187m, 0.0001m);
        }

        [Test]
        public void DodgeCorrect()
        {
            player.DodgeChance.Should().BeApproximately(0.1825m, 0.0001m);
        }

        [Test]
        public void ParryCorrect()
        {
            player.ParryChance.Should().BeApproximately(0, 0.0002m);
        }

        [Test]
        public void BlockCorrect()
        {
            player.BlockChance.Should().BeApproximately(0, 0.0002m);
        }
    }
}
