﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.CombatEngine
{
    public interface ICombatTable
    {
        AttackResult GetAttackResult(Actor Attacker, Actor Defender, HitTableModifiers modifiers);
    }

    /// <summary>
    /// Teh combat table. as this is not for dps and tanks are uncritable, ignoring crits
    /// </summary>
    public class CombatTable:ICombatTable
    {
        private IRng _rng;

        public CombatTable(IRng rng)
        {
            _rng = rng;
        }

        public AttackResult GetAttackResult(Actor Attacker, Actor Defender,
            HitTableModifiers modifiers)
        {
            decimal AttackRoll = (decimal)_rng.NextDouble();

            if (AttackRoll <= Attacker.MissChance - modifiers.HitModifiers)
                return AttackResult.Miss;
            AttackRoll -= Attacker.MissChance - modifiers.HitModifiers;

            if (AttackRoll <= Defender.DodgeChance - modifiers.DodgeModifiers)
                return AttackResult.Dodge;
            AttackRoll -= Defender.DodgeChance - modifiers.DodgeModifiers;

            if (AttackRoll <= Defender.ParryChance - modifiers.ParryModifiers)
                return AttackResult.Parry;
            AttackRoll -= Defender.ParryChance - modifiers.ParryModifiers;

            if (AttackRoll <= Attacker.CritChance - modifiers.CritModifiers)
                return AttackResult.Crit;
            AttackRoll -= Attacker.CritChance - modifiers.CritModifiers;

            decimal BlockRoll = (decimal)_rng.NextDouble(); //seperate roll for block
            if (BlockRoll < Defender.BlockChance)
                return AttackResult.Block;
            
            return AttackResult.Hit;
        }
    }
}
