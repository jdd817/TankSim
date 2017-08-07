using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs;

using ArtifactBuffs = Tank.Buffs.DemonHunter.Artifact;

namespace Tank.Artifacts
{
    public class DemonHunter : IArtifact
    {
        private IRng _rng;

        public DemonHunter(IRng rng)
        {
            _rng = rng;
        }

        public int SoulCarver { get; set; }
        public int TormentedSouls { get; set; }
        public int ShatterTheSouls { get; set; }
        public int SiphonPower { get; set; }
        public int FieryDemise { get; set; }
        public int FueledByPain { get; set; }
        public int InternalForce { get; set; }
        public int DefensiveSpikes { get; set; }
        public int EmbraceThePain { get; set; }
        public int Painbringer { get; set; }
        public int AldrachiDesign { get; set; }
        public int DemonicFlames { get; set; }
        public int AuraOfPain { get; set; }
        public int CharredWarblades { get; set; }
        public int DevourSouls { get; set; }
        public int WillOfTheIllidari { get; set; }
        public int HonedWarblades { get; set; }
        public int Soulgorger { get; set; }
        public int EruptingSouls { get; set; }
        public int LingeringOrdeal { get; set; }
        public int FlamingSoul { get; set; }
        public int Concordance { get; set; }

        public IEnumerable<Buff> GetArtifactBuffs()
        {
            if (Painbringer > 0)
                yield return new ArtifactBuffs.Painbringer() { Stacks = Painbringer };
            if (EmbraceThePain > 0)
                yield return new ArtifactBuffs.EmbraceThePain() { Stacks = EmbraceThePain };
            if (DefensiveSpikes > 0)
                yield return new ArtifactBuffs.DefensiveSpikes() { Stacks = DefensiveSpikes };
            if (DevourSouls > 0)
                yield return new ArtifactBuffs.DevourSouls() { Stacks = DevourSouls };
            if (TormentedSouls > 0)
                yield return new ArtifactBuffs.TormentedSouls() { Stacks = TormentedSouls };
            if (FueledByPain > 0)
                yield return new ArtifactBuffs.FueledByPain(_rng) { Stacks = FueledByPain };
            if (AldrachiDesign > 0)
                yield return new ArtifactBuffs.AldrachiDesign() { Stacks = AldrachiDesign };
            if (ShatterTheSouls > 0)
                yield return new ArtifactBuffs.ShatterTheSouls() { Stacks = ShatterTheSouls };
            if (FlamingSoul > 0)
                yield return new ArtifactBuffs.FlamingSoul { Stacks = FlamingSoul };
            if (LingeringOrdeal > 0)
                yield return new ArtifactBuffs.LingeringOrdeal { Stacks = LingeringOrdeal };
            if (Concordance > 0)
                yield return new Buffs.Common.Concordance(_rng) { Stacks = Concordance };
            if (WillOfTheIllidari > 0)
                yield return new ArtifactBuffs.WillOfTheIllidari { Stacks = WillOfTheIllidari };
            if (DemonicFlames > 0)
                yield return new ArtifactBuffs.DemonicFlames { Stacks = DemonicFlames };
            if (HonedWarblades > 0)
                yield return new ArtifactBuffs.HonedWarblades { Stacks = HonedWarblades };
            if (CharredWarblades > 0)
                yield return new ArtifactBuffs.CharredWarblades { Stacks = CharredWarblades };
            if (FieryDemise > 0)
                yield return new ArtifactBuffs.FieryDemise { Stacks = FieryDemise };
        }
    }
}
