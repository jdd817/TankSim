using BattleNetApi.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleNetApi.ApiSegments
{
    public interface IBattleNetCharacter
    {
        Character CharacterProfile(string realm, string characterName, params CharacterFields[] fields);
        Character Achievements(string realm, string characterName);
        Character Appearance(string realm, string characterName);
        Character Feed(string realm, string characterName);
        Character Guild(string realm, string characterName);
        Character Items(string realm, string characterName);
        Character Stats(string realm, string characterName);
    }
}

namespace BattleNetApi
{
    public enum CharacterFields
    {
        achievements,
        appearance,
        feed,
        guild,
        hunterPets,
        items,
        mounts,
        pets,
        petSlots,
        professions,
        progression,
        pvp,
        quests,
        reputation,
        statistics,
        stats
    }
}
