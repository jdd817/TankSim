using BattleNetApi.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleNetApi.ApiSegments
{
    public interface IBattleNetList
    {
        IEnumerable<BattleGroup> BattleGroups();
        IEnumerable<CharacterClass> Classes();
        IEnumerable<CharacterRace> Races();
        IEnumerable<AchievementCategory> CharacterAchievements();
        IEnumerable<GuildReward> GuildRewards();
        IEnumerable<GuildPerk> GuildPerks();
        IEnumerable<AchievementCategory> GuildAchievements();
        IEnumerable<ItemClass> ItemClasses();
        Dictionary<int,List<Talent>> Talents();
        List<PetType> PetTypes { get; set; }
    }
}
