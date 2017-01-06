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
            var armoryChar = _battleNet.Character.Stats(realm, characterName);

            return new Models.Tank()
            {
                Name = armoryChar.name,
                Class = getClass(armoryChar.@class),
                Armor = armoryChar.stats.armor,
                Crit = armoryChar.stats.critRating,
                Haste = armoryChar.stats.hasteRating,
                Versatility = armoryChar.stats.versatility,
                Mastery = armoryChar.stats.masteryRating,
                Strength = armoryChar.stats.str,
                Agility = armoryChar.stats.agi,
                MaxHealth = armoryChar.stats.health,
                WeaponHighDamage = (int)armoryChar.stats.mainHandDmgMax,
                WeaponLowDamage = (int)armoryChar.stats.mainHandDmgMin,
                WeaponSpeed = armoryChar.stats.mainHandSpeed,
            };
        }

        private string getClass(int classId)
        {
            switch(classId)
            {
                case 8:return "Mage";
                case 6:return "Death Knight";
                default:return "Unknown";
            }
        }
    }
}