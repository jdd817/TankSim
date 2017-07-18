using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs;

using ArtifactBuffs = Tank.Buffs.DeathKnight.Artifact;

namespace Tank.Artifacts
{
    public class DeathKnight:IArtifact
    {
        public int Consumption { get; set; }
        public int SanguinaryAffinity { get; set; }
        public int VampiricFangs { get; set; }
        public int RattlingBones { get; set; }
        public int GrimPerseverance { get; set; }
        public int SkeletalShattering { get; set; }
        public int Coagulopathy { get; set; }
        public int Bonebreaker { get; set; }
        public int BloodFeast { get; set; }
        public int AllConsumingRot { get; set; }
        public int UnendingThirst { get; set; }
        public int IronHeart { get; set; }
        public int MouthOfHell { get; set; }
        public int DanceOfDarkness { get; set; }
        public int MeatShield { get; set; }
        public int UmbilicusEternus { get; set; }
        public int Veinrender { get; set; }
        public int FortitudeOfTheEbonBlade { get; set; }
        public int CarrionFeast { get; set; }
        public int VampiricAura { get; set; }
        public int Souldrinker { get; set; }

        public IEnumerable<Buff> GetArtifactBuffs()
        {
            if (AllConsumingRot > 0)
                yield return new ArtifactBuffs.AllConsumingRot() { Stacks = AllConsumingRot };
            if (BloodFeast > 0)
                yield return new ArtifactBuffs.BloodFeast() { Stacks = BloodFeast };
            if (Bonebreaker>0)
                yield return new ArtifactBuffs.Bonebreaker() { Stacks = Bonebreaker };
            if (Consumption > 0)
                yield return new ArtifactBuffs.Consumption() { Stacks = Consumption };
            if (MeatShield > 0)
                yield return new ArtifactBuffs.MeatShield() { Stacks = MeatShield };
            if (RattlingBones > 0)
                yield return new ArtifactBuffs.RattlingBones() { Stacks = RattlingBones };
            if (SanguinaryAffinity > 0)
                yield return new ArtifactBuffs.SanguinaryAffinity() { Stacks = SanguinaryAffinity };
            if (SkeletalShattering > 0)
                yield return new ArtifactBuffs.SkeletalShattering() { Stacks = SkeletalShattering };
            if (UnendingThirst > 0)
                yield return new ArtifactBuffs.UnendingThirst() { Stacks = UnendingThirst };
            if (VampiricFangs > 0)
                yield return new ArtifactBuffs.VampiricFangs() { Stacks = VampiricFangs };
            if (CarrionFeast > 0)
                yield return new ArtifactBuffs.CarrionFeast() { Stacks = CarrionFeast };
            if (Souldrinker > 0)
                yield return new ArtifactBuffs.SouldrinkerActive { Stacks = Souldrinker };
            if (VampiricAura > 0)
                yield return new ArtifactBuffs.VampiricAura { Stacks = VampiricAura };
        }
    }
}
