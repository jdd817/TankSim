using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs;

namespace Tank.Artifacts
{
    public class Druid
    {
        private IRng _rng;

        public Druid(IRng rng)
        {
            _rng = rng;
        }

        public int RageOfTheSleeper { get; set; }
        public int IronClaws { get; set; }
        public int ViciousBites { get; set; }
        public int Wildflesh { get; set; }
        public int BearHug { get; set; }
        public int PerpetualSpring { get; set; }
        public int BloodyPaws { get; set; }
        public int JaggedClaws { get; set; }
        public int EmbraceOfTheNightmare { get; set; }
        public int ReinforcedFur { get; set; }
        public int AdaptiveFur { get; set; }
        public int SharpenedInstincts { get; set; }
        public int UrsocsEndurance { get; set; }
        public int GoryFur { get; set; }
        public int BestialFortitude { get; set; }
        public int Mauler { get; set; }
        public int RoarOfTheCrowd { get; set; }
        public int FortitudeOfTheCenarionCircle { get; set; }
        public int ScintillatingMoonlight { get; set; }
        public int Fleshknitting { get; set; }
        public int PawsitiveOutlook { get; set; }
        public int Concordance { get; set; }

        private IEnumerable<string> GetAvailableTraits(string Class)
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var nameSpace = String.Format("Tank.Buffs.{0}.Artifact", Class);
            return assembly.GetTypes()
                .Where(t => t.Namespace == nameSpace)
                .Where(t => typeof(Buffs.PermanentBuff).IsAssignableFrom(t))
                .Select(t => t.FullName)
                .Concat(new[] { "Tank.Buffs.Common.Concordance" })
                .ToList();
        }

        public IEnumerable<Buff> GetArtifactBuffs()
        {
            var traits = GetAvailableTraits("Druid");
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            return traits.Select(t =>
            {
                var traitType = assembly.GetType(t);
                Buff buff;
                if (traitType.GetConstructor(new Type[] { typeof(IRng) }) != null)
                    buff = Activator.CreateInstance(traitType, _rng) as Buff;
                else
                    buff = Activator.CreateInstance(traitType) as Buff;
                var prop = this.GetType().GetProperty(t.Split('.').Last());
                if (prop != null)
                    buff.Stacks = (int)prop.GetValue(this);

                return buff;
            }
            ).ToList();
        }
    }
}
