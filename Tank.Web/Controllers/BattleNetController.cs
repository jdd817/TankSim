using BattleNetApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Tank.Web.Controllers
{
    public class BattleNetController:ApiController
    {
        private IBattleNetClient _battleNet;

        public BattleNetController(IBattleNetClient battleNet)
        {
            _battleNet = battleNet;
        }

        [Route("BattleNet/Character/{realm}/{characterName}")]
        [HttpGet]
        public Models.Tank Character(string realm, string characterName)
        {
            var armoryChar = _battleNet.Character.CharacterProfile(realm, characterName, CharacterFields.stats, CharacterFields.talents);

            return new Models.Tank()
            {
                Name = armoryChar.name,
                Class = getClass(armoryChar.@class),
                Armor = armoryChar.stats.armor,
                Crit = armoryChar.stats.critRating,
                Haste = armoryChar.stats.hasteRating,
                Versatility = armoryChar.stats.versatility,
                Mastery = armoryChar.stats.masteryRating,
                Leech = (int)(armoryChar.stats.leechRating * 230),
                Strength = armoryChar.stats.str,
                Agility = armoryChar.stats.agi,
                MaxHealth = armoryChar.stats.health,
                WeaponHighDamage = (int)armoryChar.stats.mainHandDmgMax,
                WeaponLowDamage = (int)armoryChar.stats.mainHandDmgMin,
                WeaponSpeed = armoryChar.stats.mainHandSpeed,
                Talents = armoryChar.talents.Where(t=>t.selected).SelectMany(t=>t.talents).Select(t => new Models.Talent
                {
                    Class = getClass(armoryChar.@class),
                    Column = t.column + 1,
                    Row = t.tier + 1,
                    Name = t.spell.name.Replace(" ", ""),
                    FullName = String.Format("Tank.Talents.{0}.{1}", getClass(armoryChar.@class).Replace(" ", ""), t.spell.name.Replace(" ", ""))
                }).ToArray()
            };
        }

        private string getClass(int classId)
        {
            switch(classId)
            {
                case 8:return "Mage";
                case 6:return "Death Knight";
                case 1:return "Warrior";
                case 11:return "Druid";
                case 10:return "Monk";
                case 12:return "Demon Hunter";
                case 2:return "Paladin";
                default:return "Unknown";
            }
        }
    }
}