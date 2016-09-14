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
        public DeathKnightStatsTest()
        {
            Service.Name = "DK";
            Service.MasteryRating = 5706;
            Service.CritRating = 4520;
            Service.HasteRating = 5347;
            Service.VersatilityRating = 518;
            Service.Leech = 0;
            Service.Strength = 18382;
            Service.Stamina = 35371;
            Service.RunicPowerCap = 125;
            Service.MaxHealth = 2122260;
            Service.Weapons = new List<Weapon>
                {
                    new Weapon()
                    {
                        LowDamage = 4753,
                        HighDamage = 7131,
                        Speed = 3.60m
                    }
                };
        }

        [Test]
        public void MasteryCorrect()
        {
            var x = Service.Mastery;
            x.Should().BeApproximately(0.3645m, 0.0001m);
        }

        [Test]
        public void CritCorrect()
        {
            Service.CritChance.Should().BeApproximately(0.1791m, 0.0001m);
        }

        [Test]
        public void HasteCorrect()
        {
            Service.Haste.Should().BeApproximately(0.1645m, 0.0001m);
        }

        [Test]
        public void AttackPowerCorrect()
        {
            Service.AttackPower.Should().Be(18382);
        }

        [Test]
        public void VersatilityDamageIncreaseCorrect()
        {
            Service.VersatilityDamageIncrease.Should().BeApproximately(0.0129m, 0.0001m);
        }

        [Test]
        public void VersatilityDamageReductionCorrect()
        {
            Service.VersatilityDamageReduction.Should().BeApproximately(0.0065m, 0.0001m);
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
            Service.ParryChance.Should().BeApproximately(0.1624m, 0.0001m);  //0.0878 before DR
        }

        [Test]
        public void WeaponDamageCorrect()
        {
            Service.WeaponDamage.Should().BeApproximately(11144, 50m);
        }
    }

    [TestFixture]
    public class DeathKnightStatsTest2 : StatTests<DeathKnight>
    {
        public DeathKnightStatsTest2()
        {
            stats = new StatTestDescriptor
            {
                Crit = new StatDescriptor(2736, 0.1282m),
                Haste = new StatDescriptor(3551, 0.1093m),
                Mastery = new StatDescriptor(5769, 0.3672m),
                Leech = new StatDescriptor(0, 0),
                Versatility = new StatDescriptor(3614, 0.0903m),
                Dodge = 0.0300m,
                Parry = 0.1515m, //.0531 before DR
                Block = 0m,
                PrimaryStat = 18536,
                Weapon = new Weapon
                {
                    LowDamage = 4935,
                    HighDamage = 7405,
                    Speed = 3.60m
                },
                WeaponDamage = 38000
            };

            Init();
        }
    }
}
