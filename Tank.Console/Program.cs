using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;

namespace Tank.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            /*DataLogging.DataLogManager.Loggers.Add(new DataLogging.ConsoleLogger());
            DataLogging.SummaryLogger Summary= new DataLogging.SummaryLogger();
            DataLogging.DataLogManager.Loggers.Add(Summary);
            */


            Classes.DeathKnight Bart = new Classes.DeathKnight()
            {
                Name = "DK",
                Armor=2350,
                MasteryRating = 1466,
                CritRating = 1400,
                HasteRating = 1418,
                Strength = 4625,
                Stamina = 8897,
                RunicPowerCap = 125,
                MaxHealth = 533280,
                Weapons =
                {
                    new Weapon()
                    {
                        LowDamage = 7720,
                        HighDamage = 8584,
                        Speed = 3.15m
                    }
                }
            };

            Classes.Warrior Dul = new Classes.Warrior()
            {
                Name = "War",
                Armor=3110,
                MasteryRating = 1736,
                CritRating = 1210,
                HasteRating = 800,
                Strength = 4691,
                Stamina = 8243,
                RageCap = 120,
                MaxHealth = 494580,
                Weapons =
                {
                    new Weapon()
                    {
                        LowDamage = 5823,
                        HighDamage = 6715,
                        Speed = 2.41m
                    }
                }
            };

            Classes.DemonHunter Gut = new Classes.DemonHunter()
            {
                Name = "DHunter",
                Armor=2605,
                MasteryRating = 1486,
                CritRating = 2077,
                HasteRating = 1951,
                Agility = 5840,
                Stamina = 8454,
                PainCap = 100,
                MaxHealth = 507240,
                Weapons =
                {
                    new Weapon()
                    {
                        LowDamage = 6469,
                        HighDamage = 7377,
                        Speed = 2.18m
                    }
                }
            };

            Classes.Monk Gut2 = new Classes.Monk()
            {
                Name = "Monk",
                Armor=1170,
                MasteryRating = 1689,
                CritRating = 1623,
                HasteRating = 946,
                Agility = 4670,
                Stamina = 8454,
                MaxHealth = 526920,
                Weapons =
                {
                    new Weapon()
                    {
                        LowDamage = 7413,
                        HighDamage = 8140,
                        Speed = 3.02m
                    }
                }
            };

            var tanks = new Player[] { Bart, Dul, Gut, Gut2 };

            var dps = 125000m;

            var Mobs = new[]
            {
                new Mob()
                {
                    Name="Slow",
                    Weapons =
                    {
                        new Weapon()
                        {
                            LowDamage=(int)(dps * 3.2m),
                            HighDamage=(int)(dps * 3.2m),
                            Speed=3.2m
                        }
                    }
                },
                new Mob()
                {
                    Name="Medium",
                    Weapons =
                    {
                        new Weapon()
                        {
                            LowDamage=(int)(dps * 1.6m),
                            HighDamage=(int)(dps * 1.6m),
                            Speed=1.6m
                        }
                    }
                },

                new Mob()
                {
                    Name="Fast",
                    Weapons =
                    {
                        new Weapon()
                        {
                            LowDamage=(int)(dps * 0.8m),
                            HighDamage=(int)(dps * 0.8m),
                            Speed=0.8m
                        }
                    }
                },

                new Mob()
                {
                    Name="Combo",
                    Weapons =
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
                    }
                },
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
                    RNG.Reseed(12500);
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
            CombatEngine Engine = new CombatEngine();

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
    }
}
