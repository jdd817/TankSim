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
    public class DruidStatTests:TestBase<Druid>
    {
        public DruidStatTests()
        {
            Service.Name = "Druid";
            Service.MasteryRating = 4257;
            Service.CritRating = 4116;
            Service.HasteRating = 5032;
            Service.VersatilityRating = 715;
            Service.Leech = 0;
            Service.Agility = 17368;
            Service.Stamina = 20712;
            Service.MaxHealth = (int)(1367963 * 1.55m);
            Service.Armor = 1821 * 2;
            Service.Weapons = new List<Weapon>
                {
                    new Weapon()
                    {
                        LowDamage = 2010,
                        HighDamage = 3735,
                        Speed = 2.60m
                    }
                };
        }

        [Test]
        public void MasteryCorrect()
        {
            Service.Mastery.Should().BeApproximately(0.1008m, 0.0001m);
        }

        [Test]
        public void CritCorrect()
        {
            Service.CritChance.Should().BeApproximately(0.2776m, 0.0001m);
        }

        [Test]
        public void HasteCorrect()
        {
            Service.Haste.Should().BeApproximately(0.1548m, 0.0001m);
        }

        [Test]
        public void AttackPowerCorrect()
        {
            Service.AttackPower.Should().Be(17368);
        }

        [Test]
        public void VersatilityDamageIncreaseCorrect()
        {
            Service.VersatilityDamageIncrease.Should().BeApproximately(0.0179m, 0.0001m);
        }

        [Test]
        public void VersatilityDamageReductionCorrect()
        {
            Service.VersatilityDamageReduction.Should().BeApproximately(0.0089m, 0.0001m);
        }

        [Test]
        public void LeechCorrect()
        {
            Service.LeechPercentage.Should().BeApproximately(0m, 0.0001m);
        }

        [Test]
        public void DodgeCorrect()
        {
            Service.DodgeChance.Should().BeApproximately(0.1613m, 0.0001m);
        }

        [Test]
        public void ParryCorrect()
        {
            Service.ParryChance.Should().BeApproximately(0, 0.0002m);
        }

        [Test]
        public void BlockCorrect()
        {
            Service.BlockChance.Should().BeApproximately(0, 0.0002m);
        }
    }
}
