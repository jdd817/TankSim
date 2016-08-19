using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank
{
    public enum AttackResult
    {
        Dodge, Parry, Miss, Hit, Crit, Block
    }

    /// <summary>
    /// Teh combat table. as this is not for dps and tanks are uncritable, ignoring crits
    /// </summary>
    public class CombatTable
    {
        public static AttackResult GetAttackResult(Actor Attacker, Actor Defender,
            HitTableModifiers modifiers)
        {
            decimal AttackRoll = (decimal)RNG.NextDouble();

            if (AttackRoll <= Attacker.MissChance - modifiers.HitModifiers)
                return AttackResult.Miss;
            AttackRoll -= Attacker.MissChance - modifiers.HitModifiers;

            if (AttackRoll <= Defender.DodgeChance - Attacker.GetDodgedReduction - modifiers.DodgeModifiers)
                return AttackResult.Dodge;
            AttackRoll -= Defender.DodgeChance - Attacker.GetDodgedReduction - modifiers.DodgeModifiers;

            if (AttackRoll <= Defender.ParryChance - Attacker.GetParriedReduction - modifiers.ParryModifiers)
                return AttackResult.Parry;
            AttackRoll -= Defender.ParryChance - Attacker.GetParriedReduction - modifiers.ParryModifiers;

            decimal BlockRoll = (decimal)RNG.NextDouble(); //seperate roll for block
            if (BlockRoll < Defender.BlockChance || Defender.Buffs.GetBuff(typeof(Buffs.Warrior.ShieldBlock)) != null)
                return AttackResult.Block;
            else
                return AttackResult.Hit;
        }
    }
}
