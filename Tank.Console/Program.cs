using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;

using Ninject;
using Tank.ItemOptimization;

namespace Tank.Console
{
    class Program
    {
        static IKernel kernel;

        static void Main(string[] args)
        {
            ILvlMetrics();return;
            TestItemOptimizer();return;

            /*DataLogging.DataLogManager.Loggers.Add(new DataLogging.ConsoleLogger());
            DataLogging.SummaryLogger Summary= new DataLogging.SummaryLogger();
            DataLogging.DataLogManager.Loggers.Add(Summary);
            */

            kernel = new Ninject.StandardKernel();
            kernel.Load<Tank.CompositionRoot>();

            Classes.DeathKnight Bart = kernel.Get<Classes.DeathKnight>();
            Bart.Name = "DK";
            Bart.Armor = 2350;
            Bart.MasteryRating = 1466;
            Bart.CritRating = 1400;
            Bart.HasteRating = 1418;
            Bart.Strength = 4625;
            Bart.Stamina = 8897;
            Bart.RunicPowerCap = 125;
            Bart.MaxHealth = 533280;
            Bart.Weapons =new List<Weapon>
            {
                new Weapon()
                {
                    LowDamage = 7720,
                    HighDamage = 8584,
                    Speed = 3.15m
                }
            };

            Classes.Warrior Dul = kernel.Get<Classes.Warrior>();
            Dul.Name = "War";
            Dul.Armor = 3110;
            Dul.MasteryRating = 1736;
            Dul.CritRating = 1210;
            Dul.HasteRating = 800;
            Dul.Strength = 4691;
            Dul.Stamina = 8243;
            Dul.RageCap = 120;
            Dul.MaxHealth = 494580;
            Dul.Weapons = new List<Weapon>
            {
                new Weapon()
                {
                    LowDamage = 5823,
                    HighDamage = 6715,
                    Speed = 2.41m
                }
            };

            Classes.DemonHunter Gut = kernel.Get<Classes.DemonHunter>();
            Gut.Name = "DHunter";
            Gut.Armor = 2605;
            Gut.MasteryRating = 1486;
            Gut.CritRating = 2077;
            Gut.HasteRating = 1951;
            Gut.Agility = 5840;
            Gut.Stamina = 8454;
            Gut.PainCap = 100;
            Gut.MaxHealth = 507240;
            Gut.Weapons = new List<Weapon>
            {
                new Weapon()
                {
                    LowDamage = 6469,
                    HighDamage = 7377,
                    Speed = 2.18m
                }
            };

            Classes.Monk Gut2 = kernel.Get<Classes.Monk>();
            Gut2.Name = "Monk";
            Gut2.Armor = 1170;
            Gut2.MasteryRating = 1689;
            Gut2.CritRating = 1623;
            Gut2.HasteRating = 946;
            Gut2.Agility = 4670;
            Gut2.Stamina = 8454;
            Gut2.MaxHealth = 526920;
            Gut2.Weapons =new List<Weapon>
            {
                new Weapon()
                {
                    LowDamage = 7413,
                    HighDamage = 8140,
                    Speed = 3.02m
                }
            };

            Classes.Druid Val = kernel.Get<Classes.Druid>();
            Val.Name = "Druid";
            Val.MaxHealth = 711166;
            Val.Armor = 2458;
            Val.Agility = 4619;
            Val.MasteryRating = 2196;
            Val.CritRating = 2024;
            Val.HasteRating = 1627;
            Val.VersatilityRating = 291;
            Val.Weapons = new List<Weapon>
            {
                new Weapon()
                {
                    LowDamage=7992,
                    HighDamage=8883,
                    Speed=3.1m
                }
            };

            var tanks = new Player[] { Bart, Dul, Gut, Gut2, Val };

            var dps = 125000m;

            var Mobs = new List<Mob>();

            Mobs.Add(kernel.Get<Mob>());

            Mobs[0].Name = "Slow";
            Mobs[0].Weapons = new List<Weapon>
                    {
                        new Weapon()
                        {
                            LowDamage = (int)(dps * 3.2m),
                            HighDamage = (int)(dps * 3.2m),
                            Speed = 3.2m
                        }
                    };

            Mobs.Add(kernel.Get<Mob>());

            Mobs[1].Name = "Medium";
            Mobs[1].Weapons = new List<Weapon>
                    {
                        new Weapon()
                        {
                            LowDamage=(int)(dps * 1.6m),
                            HighDamage=(int)(dps * 1.6m),
                            Speed=1.6m
                        }
                    };

            Mobs.Add(kernel.Get<Mob>());

            Mobs[2].Name = "Fast";
            Mobs[2].Weapons = new List<Weapon>
                    {
                        new Weapon()
                        {
                            LowDamage=(int)(dps * 0.8m),
                            HighDamage=(int)(dps * 0.8m),
                            Speed=0.8m
                        }
                    };

            Mobs.Add(kernel.Get<Mob>());

            Mobs[3].Name = "Combo";
            Mobs[3].Weapons = new List<Weapon>
                    {
                        new Weapon()
                        {
                            LowDamage=(int)(dps/2m * 2.4m),
                            HighDamage=(int)(dps/2m * 2.4m),
                            Speed=2.4m
                        },
                        new Weapon()
                        {
                            LowDamage=(int)(dps/2m * 1.2m),
                            HighDamage=(int)(dps/2m * 1.2m),
                            Speed=1.2m
                        }
                    };

            //Bart.Buffs.AddBuff(new Buffs.DeathKnight.Artifact.BloodFeast());
            //Bart.Buffs.AddBuff(new Buffs.DeathKnight.Artifact.SkeletalShattering());

            var healers = new[]
            {
                new Healer
                {
                    HealAmount=80000,
                    HealPeriod=1
                }
            };

            var grapher = new DataLogging.GraphLogger();
            DataLogging.DataLogManager.Loggers.Add(grapher);

            foreach (var tank in tanks)
            {
                foreach (var mob in Mobs)
                {
                    var rng = kernel.Get<IRng>();
                    rng.Reseed(12500);
                    grapher.RunName = String.Format("{0} - {1}", tank.Name, mob.Name);
                    System.Console.WriteLine("Running {0}", grapher.RunName);
                    var results = DoRun(tank, mob, grapher.RunName, healers);

                    System.Console.WriteLine("Damage Taken:  {0}\r\nDamage Absorbed:  {1}\r\nDamage Healed:  {2}",
                        results.DamageTaken,
                        results.DamageAbsorbed,
                        results.DamageHealed);
                    System.Console.WriteLine("Average Hit: {0}", results.Average);
                    System.Console.WriteLine("Lasted: {0} min {1:0.00} sec  ({2:0.00} secs)", (int)(DataLogging.DataLogManager.CurrentTime / 60.0m), DataLogging.DataLogManager.CurrentTime % 60, DataLogging.DataLogManager.CurrentTime);
                    System.Console.WriteLine();
                }
            }

            using (var writer = new StreamWriter("c:\\tank\\health.csv"))
            {
                writer.Write(", ");
                foreach (var run in grapher.Runs)
                    writer.Write(run + ",");
                writer.WriteLine();

                foreach (var time in grapher.Lines.Keys)
                {
                    writer.Write(time + ",");
                    foreach (var run in grapher.Runs)
                        if (grapher.Lines[time].ContainsKey(run))
                            writer.Write(grapher.Lines[time][run] + ",");
                        else
                            writer.Write(",");
                    writer.WriteLine();
                }
            }


            return;
        }

        static DataLogging.SummaryLogger DoRun(Player Tank, Mob Mob, string Name, Healer[] healers)
        {
            var Engine = kernel.Get<ICombatEngine>();

            DataLogging.DataLogManager.Reset();
            var Logger = new DataLogging.StreamLogger(new StreamWriter(String.Format("c:\\tank\\{0}log.txt", Name)));
            var Aggregator = new DataLogging.SummaryLogger();
            //var Grapher = new DataLogging.CsvLogger(String.Format("c:\\{0}hplog.csv", Name));
            //new DataLogging.DatabaseLogger("data source=localhost\\sql2k8;initial catalog=tank;integrated security=true",
            //new DataLogging.DatabaseLogger("data source=loki\\sqlexpress;initial catalog=tank;integrated security=true",
            // Name))
            {
                DataLogging.DataLogManager.Loggers.Add(Logger);
                DataLogging.DataLogManager.Loggers.Add(Aggregator);
                //DataLogging.DataLogManager.Loggers.Add(Grapher);
                Engine.DoCombat(Tank, Mob, 200.0m, true, healers);
                DataLogging.DataLogManager.Loggers.Remove(Logger);
                DataLogging.DataLogManager.Loggers.Remove(Aggregator);
                //DataLogging.DataLogManager.Loggers.Remove(Grapher);
            }

            //Grapher.Dispose();
            Logger.Dispose();

            return Aggregator;
        }

        static void SaveXml(Classes.Warrior Tank, Mob Mob)
        {
            XmlSerializer PlayerSerializer = new XmlSerializer(typeof(Classes.Warrior));
            XmlSerializer MobSerializer = new XmlSerializer(typeof(Mob));

            XDocument Doc = new XDocument();
            using (XmlWriter writer = Doc.CreateWriter())
            {
                writer.WriteStartElement("root");
                PlayerSerializer.Serialize(writer, Tank);
                MobSerializer.Serialize(writer, Mob);
                writer.WriteEndElement();
            }

            Doc.Save("c:\\tank.xml");
        }

        static void TestItemOptimizer()
        {
            kernel = new Ninject.StandardKernel();
            kernel.Load<Tank.ItemOptimization.ItemOptimizationModule>();
            kernel.Bind<BattleNetApi.IBattleNetConfiguration>().To<BattleNetConfig>();
            kernel.Load<BattleNetApi.BattleNetModule>();


            var bloodDk = Presets.BloodDk;
            var loader = kernel.Get<ArmoryLoader>();
            var optimizer = kernel.Get<ItemOptimizer>();


            /*
            Character toon;
            using (var reader = new Newtonsoft.Json.JsonTextReader(new StreamReader("c:\\dk.json")))
                toon = new Newtonsoft.Json.JsonSerializer().Deserialize<Character>(reader);
            
            /*/
            var toon = loader.LoadCharacter("Bartenderizr", "Doomhammer");

            var serializer = new Newtonsoft.Json.JsonSerializer();
            serializer.Formatting = Newtonsoft.Json.Formatting.Indented;
            using (var writer = new StreamWriter("c:\\dk.json"))
                serializer.Serialize(writer, toon);
            /**/

            foreach (var item in toon.EquippedItems)
            {
                System.Console.WriteLine("{0}: {1}", item.Key, item.Value.Name);
            }

            System.Console.WriteLine("\r\n\r\n");

            optimizer.OptimizeGear(toon, bloodDk);

            foreach(var item in toon.EquippedItems)
            {
                System.Console.WriteLine("{0}: {1}", item.Key, item.Value.Name);
            }

            System.Console.WriteLine("\r\nInventory:");
            foreach (var item in toon.Inventory)
                System.Console.WriteLine(item.Name);

            System.Console.ReadKey();  
        }

        static void ILvlMetrics()
        {
            kernel = new Ninject.StandardKernel();
            kernel.Load<Tank.ItemOptimization.ItemOptimizationModule>();
            kernel.Bind<BattleNetApi.IBattleNetConfiguration>().To<BattleNetConfig>();
            kernel.Load<BattleNetApi.BattleNetModule>();

            var characters = new List<string>
            {
                "Bartenderizr",
                "Chinlaau",
                //"Destros",
                "Diàblós",
                "Donya",
                "Galdaviz",
                "Griek",
                "Jhorlia",
                "Lerrick",
                "Lothis",
                "Mahlona",
                "Nepty",
                "Prôfessôrôdd",
                "Sisterkrisi",
                "Tìnkybell",
                "Treetugger",
                "Whisperos",
                "Winterborne",
                "Xiall"
            };

            var bnet = kernel.Get<BattleNetApi.IBattleNetClient>();
            var loader = kernel.Get<ArmoryLoader>();

            var avgILevelList = new List<double>();
            using (var writer = new StreamWriter("ilvl.txt"))
            {
                System.Console.SetOut(writer);
                foreach (var character in characters)
                {
                    var toon = bnet.Character.Items(character == "Sisterkrisi" ? "Baelgun" : "Doomhammer", character);
                    var items = GetEquippedItems(toon.items);

                    var ilvl = items.Where(i => i.quality < 5)
                        .Concat(toon.items.mainHand.relics.Select(r => bnet.Item.Item(r.itemId, r.bonusLists.ToArray())))
                        .Average(i => i.itemLevel);
                    System.Console.WriteLine("{0}\t{1:0.0000}", character, ilvl);

                    avgILevelList.Add(ilvl);
                }

                System.Console.WriteLine("Avg item level excluding legendaries, subbing relics for artifacts {0:0.0000}", avgILevelList.Average());
                //System.Console.ReadKey();
            }
        }

        static IEnumerable<BattleNetApi.DataObjects.Item> GetEquippedItems(BattleNetApi.DataObjects.ItemList items)
        {
            if(items.head!=null)
                yield return items.head;
            if (items.neck != null)
                yield return items.neck;
            if (items.shoulder != null)
                yield return items.shoulder;
            if (items.back != null)
                yield return items.back;
            if (items.chest != null)
                yield return items.chest;
            if (items.wrist != null)
                yield return items.wrist;
            if (items.hands != null)
                yield return items.hands;
            if (items.waist != null)
                yield return items.waist;
            if (items.legs != null)
                yield return items.legs;
            if (items.feet != null)
                yield return items.feet;
            if (items.finger1 != null)
                yield return items.finger1;
            if (items.finger2 != null)
                yield return items.finger2;
            if (items.trinket1 != null)
                yield return items.trinket1;
            if (items.trinket2 != null)
                yield return items.trinket2;
            if (items.mainHand != null)
                yield return items.mainHand;
            if (items.mainHand != null)
                yield return items.mainHand;
        }
    }

    public class BattleNetConfig : BattleNetApi.IBattleNetConfiguration
    {
        public string ApiKey { get { return "nryb77x56ta2dmvpykhdwdc8kf56mym8"; } set { } }

        public string ApiUrl { get { return "https://us.api.battle.net/wow"; } set { } }

        public string Locale { get { return "en_us"; } set { } }
    }
}
