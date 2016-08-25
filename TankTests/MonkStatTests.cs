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
    public class MonkStatTests:TestBase<Monk>
    {
        public MonkStatTests()
        {
            Service.Name = "Monk";
            Service.MasteryRating = 1689;
            Service.CritRating = 1623;
            Service.HasteRating = 946;
            Service.VersatilityRating = 1271;
            Service.Leech = 0;
            Service.Agility = 4670;
            Service.Stamina = 8782;
            Service.MaxHealth = 526920;
            Service.Armor = 1170;
            Service.Weapons = new List<Weapon>
                {
                    new Weapon()
                    {
                        LowDamage = 7413,
                        HighDamage = 8140,
                        Speed = 3.02m
                    }
                };
        }

        [Test]
        public void MasteryCorrect()
        {
            Service.Mastery.Should().BeApproximately(0.2335m, 0.0001m);
        }

        [Test]
        public void CritCorrect()
        {
            Service.CritChance.Should().BeApproximately(0.2975m, 0.0001m);
        }

        [Test]
        public void HasteCorrect()
        {
            Service.Haste.Should().BeApproximately(0.0946m, 0.0001m);
        }

        [Test]
        public void AttackPowerCorrect()
        {
            Service.AttackPower.Should().Be(4670);
        }

        [Test]
        public void VersatilityDamageIncreaseCorrect()
        {
            Service.VersatilityDamageIncrease.Should().BeApproximately(0.1089m, 0.0001m);
        }

        [Test]
        public void VersatilityDamageReductionCorrect()
        {
            Service.VersatilityDamageReduction.Should().BeApproximately(0.0545m, 0.0001m);
        }

        [Test]
        public void LeechCorrect()
        {
            Service.LeechPercentage.Should().BeApproximately(0.0m, 0.0001m);
        }

        [Test]
        public void DodgeCorrect()
        {
            Service.DodgeChance.Should().BeApproximately(0.1207m, 0.0001m);
        }

        [Test]
        public void ParryCorrect()
        {
            Service.ParryChance.Should().BeApproximately(0.0300m, 0.0001m);
        }

        [Test]
        public void BlockCorrect()
        {
            Service.BlockChance.Should().BeApproximately(0, 0.0001m);
        }
    }
}
