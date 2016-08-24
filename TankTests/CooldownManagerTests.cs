using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank;
using Tank.Abilities;
using Tank.Abilities.Warrior;

namespace TankTests
{
    [TestFixture]
    public class CooldownManagerTests
    {
        [Test]
        public void GCDTest()
        {
            var cd = new CooldownManager();

            cd.OffGCD.Should().BeTrue();

            var ability = new Devastate(Substitute.For<IRng>());
            var result = new AbilityResult();

            cd.AbilityUsed(ability, result);

            cd.OffGCD.Should().BeFalse();

            cd.UpdateTimers(0.75m);

            cd.OffGCD.Should().BeFalse();

            cd.UpdateTimers(0.50m);

            cd.OffGCD.Should().BeFalse();

            cd.UpdateTimers(0.50m);

            cd.OffGCD.Should().BeTrue();
        }

        [Test]
        public void BasicCooldownTest()
        {
            var cd = new CooldownManager();

            cd.AbilityReady<ShieldSlam>().Should().BeTrue();

            var ability = new ShieldSlam();
            var result = new AbilityResult();

            cd.AbilityUsed(ability, result);

            cd.AbilityReady<ShieldSlam>().Should().BeFalse();

            cd.UpdateTimers(3);

            cd.AbilityReady<ShieldSlam>().Should().BeFalse();

            cd.UpdateTimers(5);

            cd.AbilityReady<ShieldSlam>().Should().BeFalse();

            cd.UpdateTimers(2);

            cd.AbilityReady<ShieldSlam>().Should().BeTrue();
        }

        [Test]
        public void ChargedCooldownTest()
        {
            var cd = new CooldownManager();

            cd.AbilityReady<ShieldBlock>().Should().BeTrue();
            cd.ChargesAvailable<ShieldBlock>().Should().Be(2);

            var ability = new ShieldBlock();
            var result = new AbilityResult();

            cd.AbilityUsed(ability, result);

            cd.AbilityReady<ShieldBlock>().Should().BeTrue();
            cd.ChargesAvailable<ShieldBlock>().Should().Be(1);

            cd.UpdateTimers(3);

            cd.AbilityReady<ShieldBlock>().Should().BeTrue();
            cd.ChargesAvailable<ShieldBlock>().Should().Be(1);

            cd.UpdateTimers(15);

            cd.AbilityReady<ShieldBlock>().Should().BeTrue();
            cd.ChargesAvailable<ShieldBlock>().Should().Be(2);

            cd.AbilityUsed(ability, result);

            cd.UpdateTimers(10);

            cd.AbilityReady<ShieldBlock>().Should().BeTrue();
            cd.ChargesAvailable<ShieldBlock>().Should().Be(1);

            cd.AbilityUsed(ability, result);

            cd.AbilityReady<ShieldBlock>().Should().BeFalse();
            cd.ChargesAvailable<ShieldBlock>().Should().Be(0);

            cd.UpdateTimers(3);

            cd.AbilityReady<ShieldBlock>().Should().BeTrue();
            cd.ChargesAvailable<ShieldBlock>().Should().Be(1);

            cd.UpdateTimers(13);

            cd.AbilityReady<ShieldBlock>().Should().BeTrue();
            cd.ChargesAvailable<ShieldBlock>().Should().Be(2);

            cd.UpdateTimers(100);

            cd.AbilityReady<ShieldBlock>().Should().BeTrue();
            cd.ChargesAvailable<ShieldBlock>().Should().Be(2);
        }

        [Test]
        public void MultipleCooldownsTests()
        {
            var cd = new CooldownManager();

            var slam = new ShieldSlam();
            var revenge = new Revenge();
            var block = new ShieldBlock();
            var result = new AbilityResult();

            cd.AbilityReady<ShieldSlam>().Should().BeTrue();
            cd.AbilityReady<Revenge>().Should().BeTrue();
            cd.AbilityReady<ShieldBlock>().Should().BeTrue();
            cd.ChargesAvailable<ShieldBlock>().Should().Be(2);

            cd.AbilityUsed(slam, result);

            cd.AbilityReady<ShieldSlam>().Should().BeFalse();
            cd.AbilityReady<Revenge>().Should().BeTrue();
            cd.AbilityReady<ShieldBlock>().Should().BeTrue();
            cd.ChargesAvailable<ShieldBlock>().Should().Be(2);

            cd.UpdateTimers(1.5m);
            cd.AbilityUsed(revenge, result);

            cd.AbilityReady<ShieldSlam>().Should().BeFalse();
            cd.AbilityReady<Revenge>().Should().BeFalse();
            cd.AbilityReady<ShieldBlock>().Should().BeTrue();
            cd.ChargesAvailable<ShieldBlock>().Should().Be(2);

            cd.UpdateTimers(3);
            cd.AbilityUsed(block, result);

            cd.AbilityReady<ShieldSlam>().Should().BeFalse();
            cd.AbilityReady<Revenge>().Should().BeFalse();
            cd.AbilityReady<ShieldBlock>().Should().BeTrue();
            cd.ChargesAvailable<ShieldBlock>().Should().Be(1);

            cd.UpdateTimers(4.5m);

            cd.AbilityReady<ShieldSlam>().Should().BeTrue();
            cd.AbilityReady<Revenge>().Should().BeFalse();
            cd.AbilityReady<ShieldBlock>().Should().BeTrue();
            cd.ChargesAvailable<ShieldBlock>().Should().Be(1);

            cd.AbilityUsed(slam, result);

            cd.UpdateTimers(4.5m);

            cd.AbilityReady<ShieldSlam>().Should().BeFalse();
            cd.AbilityReady<Revenge>().Should().BeTrue();
            cd.AbilityReady<ShieldBlock>().Should().BeTrue();
            cd.ChargesAvailable<ShieldBlock>().Should().Be(1);

            cd.AbilityUsed(block, result);
            cd.AbilityUsed(revenge, result);

            cd.AbilityReady<ShieldSlam>().Should().BeFalse();
            cd.AbilityReady<Revenge>().Should().BeFalse();
            cd.AbilityReady<ShieldBlock>().Should().BeFalse();
            cd.ChargesAvailable<ShieldBlock>().Should().Be(0);

            cd.UpdateTimers(4.5m);

            cd.AbilityReady<ShieldSlam>().Should().BeTrue();
            cd.AbilityReady<Revenge>().Should().BeFalse();
            cd.AbilityReady<ShieldBlock>().Should().BeTrue();
            cd.ChargesAvailable<ShieldBlock>().Should().Be(1);
        }

        [Test]
        public void RemovesCooldown()
        {
            var cd = new CooldownManager();

            var ability = new ShieldSlam();
            var result = new AbilityResult();

            cd.AbilityUsed(ability, result);

            cd.AbilityReady<ShieldSlam>().Should().BeFalse();

            cd.ReduceTimers(new CooldownReduction { Ability = typeof(ShieldSlam), Amount = 0, ReductionType = ReductionType.To });

            cd.AbilityReady<ShieldSlam>().Should().BeTrue();
        }

        [Test]
        public void ReducesCooldown()
        {
            var cd = new CooldownManager();

            var ability = new ShieldSlam();
            var result = new AbilityResult();

            cd.AbilityUsed(ability, result);

            cd.AbilityReady<ShieldSlam>().Should().BeFalse();

            var initialCooldown = cd.CooldownRemaining<ShieldSlam>();

            cd.ReduceTimers(new CooldownReduction { Ability = typeof(ShieldSlam), Amount = 2, ReductionType = ReductionType.By });

            cd.AbilityReady<ShieldSlam>().Should().BeFalse();
            cd.CooldownRemaining<ShieldSlam>().Should().Be(initialCooldown - 2);
        }

        [Test]
        public void ResetFunctions()
        {
            var cd = new CooldownManager();

            var ability = new ShieldSlam();
            var result = new AbilityResult();

            cd.AbilityUsed(ability, result);

            cd.AbilityReady<ShieldSlam>().Should().BeFalse();

            cd.Reset();

            cd.AbilityReady<ShieldSlam>().Should().BeTrue();
        }
    }
}
