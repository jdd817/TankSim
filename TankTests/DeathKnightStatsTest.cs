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
    public class DeathKnightStatsTest
    {
        private DeathKnight DK;

        public DeathKnightStatsTest()
        {
            DK = new DeathKnight()
            {
                Name = "DK",
                MasteryRating = 979,
                CritRating = 1961,
                HasteRating = 1384,
                VersatilityRating = 841,
                Leech = 0,
                Strength = 4917,
                Stamina = 9588,
                RunicPowerCap = 125,
                MaxHealth = 575280,
                Weapons =
                {
                    new Weapon()
                    {
                        LowDamage = 8423,
                        HighDamage = 9489,
                        Speed = 3.16m
                    }
                }
            };
        }

        [Test]
        public void MasteryCorrect()
        {
            DK.Mastery.Should().BeApproximately(0.2535m, 0.0001m);
        }

        [Test]
        public void CritCorrect()
        {
            DK.CritChance.Should().BeApproximately(0.2283m, 0.0001m);
        }

        [Test]
        public void HasteCorrect()
        {
            DK.Haste.Should().BeApproximately(0.1384m, 0.0001m);
        }

        [Test]
        public void AttackPowerCorrect()
        {
            DK.AttackPower.Should().Be(4917);
        }

        [Test]
        public void VersatilityDamageIncreaseCorrect()
        {
            DK.VersatilityDamageIncrease.Should().BeApproximately(0.0647m, 0.0001m);
        }

        [Test]
        public void VersatilityDamageReductionCorrect()
        {
            DK.VersatilityDamageReduction.Should().BeApproximately(0.0323m, 0.0001m);
        }

        [Test]
        public void LeechCorrect()
        {
            DK.LeechPercentage.Should().BeApproximately(0m, 0.0001m);
        }

        [Test]
        public void DodgeCorrect()
        {
            DK.DodgeChance.Should().BeApproximately(0.0300m, 0.0001m);
        }

        [Test]
        public void ParryCorrect()
        {
            DK.ParryChance.Should().BeApproximately(0.1898m, 0.0001m);
        }
    }
}
