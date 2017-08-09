using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs;
using ArtifactBuffs = Tank.Buffs.Monk.Artifact;

namespace Tank.Artifacts
{
    public class Monk : IArtifact
    {
        private IRng _rng;

        public Monk(IRng rng)
        {
            _rng = rng;
        }

        public int ExplodingKeg { get; set; }
        public int FullKeg { get; set; }
        public int PotentKick { get; set; }
        public int HotBlooded { get; set; }
        public int FacePalm { get; set; }
        public int ObstinateDetermination { get; set; }
        public int DarkSideOfTheMoon { get; set; }
        public int Smashed { get; set; }
        public int ObsidianFists { get; set; }
        public int Overflow { get; set; }
        public int BrewStache { get; set; }
        public int DragonfireBrew { get; set; }
        public int StaggeringAround { get; set; }
        public int Fortification { get; set; }
        public int SwiftAsACoursingRiver { get; set; }
        public int GiftedStudent { get; set; }
        public int HealthyAppetite { get; set; }
        public int EnduranceOfTheBrokenTemple { get; set; }
        public int DraughtOfDarkness { get; set; }
        public int StaveOff { get; set; }
        public int QuickSip { get; set; }
        public int Concordance { get; set; }

        public IEnumerable<Buff> GetArtifactBuffs()
        {
            if (Overflow > 0)
                yield return new ArtifactBuffs.Overflow(_rng) { Stacks = Overflow };
            if (HotBlooded > 0)
                yield return new ArtifactBuffs.HotBlooded() { Stacks = HotBlooded };
            if (FullKeg > 0)
                yield return new ArtifactBuffs.FullKeg() { Stacks = FullKeg };
            if (ObstinateDetermination > 0)
                yield return new ArtifactBuffs.ObstinateDetermination() { Stacks = ObstinateDetermination };
            if (BrewStache > 0)
                yield return new ArtifactBuffs.BrewStache() { Stacks = BrewStache };
            if (PotentKick > 0)
                yield return new ArtifactBuffs.PotentKick() { Stacks = PotentKick };
            if (DraughtOfDarkness > 0)
                yield return new ArtifactBuffs.DraughtOfDarkness() { Stacks = DraughtOfDarkness };
            if (QuickSip > 0)
                yield return new ArtifactBuffs.QuickSip { Stacks = QuickSip };
            if (StaveOff > 0)
                yield return new ArtifactBuffs.StaveOff(_rng) { Stacks = StaveOff };
            if (Concordance > 0)
                yield return new Buffs.Common.Concordance(_rng) { Stacks = Concordance };
            if (GiftedStudent > 0)
                yield return new ArtifactBuffs.GiftedStudent { Stacks = GiftedStudent };
            if (DarkSideOfTheMoon > 0)
                yield return new ArtifactBuffs.DarkSideOfTheMoon { Stacks = DarkSideOfTheMoon };
            if (FacePalm > 0)
                yield return new ArtifactBuffs.FacePalm(_rng) { Stacks = FacePalm };
            if (Fortification > 0)
                yield return new ArtifactBuffs.Fortification() { Stacks = Fortification };
            if (StaggeringAround > 0)
                yield return new ArtifactBuffs.StaggeringAround() { Stacks = StaggeringAround };
            if (DragonfireBrew > 0)
                yield return new ArtifactBuffs.DragonfireBrew() { Stacks = DragonfireBrew };
        }
    }
}
