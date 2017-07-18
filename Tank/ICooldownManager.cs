using System;
using Tank.Abilities;

namespace Tank
{
    public interface ICooldownManager
    {
        Player Player { get; set; }

        decimal GCDLength { get; set; }
        decimal GCDTimer { get; }
        bool OffGCD { get; }

        bool AbilityReady(Type Ability);
        bool AbilityReady<T>() where T : Ability;
        void AbilityUsed(Ability ability, AbilityResult result);
        int ChargesAvailable(Type Ability);
        int ChargesAvailable<T>() where T : Ability;
        decimal CooldownRemaining(Type Ability);
        decimal CooldownRemaining<T>() where T : Ability;
        void ReduceTimers(params CooldownReduction[] reductions);
        void Reset();
        void UpdateTimers(decimal DeltaTime);
    }
}