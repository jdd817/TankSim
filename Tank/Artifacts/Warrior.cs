using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs;
using ArtifactBuffs = Tank.Buffs.Warrior.Artifact;

namespace Tank.Artifacts
{
    public class Warrior:IArtifact
    {
        private IRng _rng;

        public Warrior(IRng rng)
        {
            _rng = rng;
        }

        public int StrengthOfTheEarthAspect { get; set; }
        public int ShatterTheBones { get; set; }
        public int Intolerance { get; set; }
        public int DragonSkin { get; set; }
        public int ThunderCrash { get; set; }
        public int ReflectivePlating { get; set; }
        public int DragonScales { get; set; }
        public int WallOfSteel { get; set; }
        public int LeapingGiants { get; set; }
        public int ScalesOfEarth { get; set; }
        public int RageOfTheFallen { get; set; }
        public int VrykulShieldTraining { get; set; }
        public int WillToSurvive { get; set; }
        public int Toughness { get; set; }
        public int RumblingVoice { get; set; }
        public int MightOfTheVrykul { get; set; }
        public int ProtectionOfTheValarjar { get; set; }
        public int BastionOfTheAspects { get; set; }
        public int GleamingScales { get; set; }
        public int NeltharionsThunder { get; set; }
        public int Concordance { get; set; }

        public IEnumerable<Buff> GetArtifactBuffs()
        {
            if (BastionOfTheAspects > 0)
                yield return new ArtifactBuffs.BastionOfTheAspects { Stacks = BastionOfTheAspects };
            if (DragonScales > 0)
                yield return new ArtifactBuffs.DragonScales(_rng) { Stacks = DragonScales };
            if (DragonSkin > 0)
                yield return new ArtifactBuffs.DragonSkin { Stacks = DragonSkin };
            if (Intolerance > 0)
                yield return new ArtifactBuffs.Intolerance { Stacks = Intolerance };
            if (MightOfTheVrykul > 0)
                yield return new ArtifactBuffs.MightOfTheVrykul { Stacks = MightOfTheVrykul };
            if (NeltharionsThunder > 0)
                yield return new ArtifactBuffs.NeltharionsThunder { Stacks = NeltharionsThunder };
            if (RumblingVoice > 0)
                yield return new ArtifactBuffs.RumblingVoice { Stacks = RumblingVoice };
            if (ScalesOfEarth > 0)
                yield return new ArtifactBuffs.ScalesOfEarth(_rng) { Stacks = ScalesOfEarth };
            if (VrykulShieldTraining > 0)
                yield return new ArtifactBuffs.VrykulShieldTraining { Stacks = VrykulShieldTraining };
            if (Concordance > 0)
                yield return new Buffs.Common.Concordance(_rng) { Stacks = Concordance };
        }
    }
}
