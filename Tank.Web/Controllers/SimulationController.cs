﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tank.Web.Models;

namespace Tank.Web.Controllers
{
    public class SimulationController : ApiController
    {
        private ICombatEngine _combatEngine;

        public SimulationController(ICombatEngine combatEngine)
        {
            _combatEngine = combatEngine;
        }

        [HttpPost]
        [Route("api/simulation")]
        public SimulationResult SimulationStartPoint(SimulationParameters parameters)
        {
            var maxRuns = Math.Max(1, 250 / (parameters.Mobs.Count * parameters.Tanks.Count));
            parameters.RunCount = Math.Min(parameters.RunCount, maxRuns);
            if (parameters.RunCount > 1)
                return SmoothResult(RunMultipleSimulations(parameters));
            else
                return SmoothResult(RunSimulation(parameters));
        }

        public SimulationResult RunSimulation(SimulationParameters parameters)
        {
            var tanks = GetTanks(parameters).ToArray();
            var healers = GetHealers(parameters).ToArray();
            var mobs = GetMobs(parameters).ToArray();

            return GetSingleResult(parameters.Seed, tanks, healers, mobs);
        }

        public SimulationResult SmoothResult(SimulationResult result)
        {
            var newResults = new List<Plot>();

            foreach(var plot in result.Results)
            {
                var dataPoints = new List<decimal[]>();
                var min = plot.data.Select(d => d[0]).Min();
                var max = plot.data.Select(d => d[0]).Max();
                for (var i = min; i < max; i += 0.1m)
                    dataPoints.Add(new[] { i, plot.data.Where(d => d[0] >= i - 2.5m && d[0] <= i + 2.5m).Select(d => d[1]).Average() });

                newResults.Add(new Plot { label = plot.label, data = dataPoints.ToArray(), Summary = plot.Summary });
            }

            return new SimulationResult { Results = newResults };
        }
        
        public SimulationResult RunMultipleSimulations(SimulationParameters parameters)
        {
            var tanks = GetTanks(parameters).ToArray();
            var healers = GetHealers(parameters).ToArray();
            var mobs = GetMobs(parameters).ToArray();

            var results = new List<SimulationResult>();

            var random = new Random(parameters.Seed);

            for (var i = 0; i < parameters.RunCount; i++)
                results.Add(GetSingleResult(random.Next(5000), tanks, healers, mobs));

            return new SimulationResult
            {
                Results =
                      results.SelectMany(r => r.Results)
                      .GroupBy(p => p.label)
                      .Select(r => new Plot
                      {
                          label = r.Key,
                          data = r.SelectMany(p => p.data)
                              .GroupBy(d => d[0])
                                .Select(d =>
                                new[] {
                                    d.Key,
                                    d.Select(p=>p[1]).Sum() / parameters.RunCount
                                }).ToArray(),
                          Summary = new SimulationSummary
                          {
                              DamageTaken = (int)r.Select(p => p.Summary.DamageTaken).Average(),
                              DamageHealed = (int)r.Select(p => p.Summary.DamageHealed).Average(),
                              AverageHealthOverall = (int)r.Select(p => p.Summary.AverageHealthOverall).Average()
                          }
                      }).ToList()
            };
        }

        private SimulationResult GetSingleResult(int seed, Player[] tanks, Healer[] healers, Mob[] mobs)
        {
            var grapher = new DataLogging.GraphLogger();
            DataLogging.DataLogManager.Loggers.Add(grapher);
            var summaries = new Dictionary<string, SimulationSummary>();

            foreach (var tank in tanks)
            {
                foreach (var mob in mobs)
                {
                    _combatEngine.Rng.Reseed(seed);
                    var runName= String.Format("{0} - {1}", tank.Name, mob.Name);
                    grapher.RunName = runName;

                    var summaryLogger = new DataLogging.SummaryLogger();
                    var streamLogger = new DataLogging.StreamLogger(new System.IO.StreamWriter(System.Web.HttpRuntime.AppDomainAppPath + "/" + runName + ".txt", true));
                    DataLogging.DataLogManager.Loggers.Add(summaryLogger);
                    DataLogging.DataLogManager.Loggers.Add(streamLogger);
                    
                    DataLogging.DataLogManager.Reset();
                    _combatEngine.DoCombat(tank, mob, 200.0m, true, healers);

                    DataLogging.DataLogManager.Loggers.Remove(summaryLogger);
                    DataLogging.DataLogManager.Loggers.Remove(streamLogger);
                    streamLogger.Dispose();
                    summaries.Add(runName, new SimulationSummary
                    {
                        DamageTaken = summaryLogger.DamageTaken,
                        DamageHealed = summaryLogger.DamageHealed
                    });
                }
            }
            DataLogging.DataLogManager.Loggers.Remove(grapher);

            var plots = new Dictionary<string, List<decimal[]>>();

            foreach (var run in grapher.Runs)
                plots.Add(run, new List<decimal[]>());

            foreach (var time in grapher.Lines.Keys)
            {
                foreach (var run in grapher.Runs)
                    if (grapher.Lines[time].ContainsKey(run))
                        plots[run].Add(new[] { time, grapher.Lines[time][run] });

            }
            foreach (var run in grapher.Runs)
                summaries[run].AverageHealthOverall = (int)(plots[run].Select(d => d[1]).Sum() / 2000);

            return new SimulationResult
            {
                Results = plots.Select(p =>
                  new Plot
                  {
                      label = p.Key,
                      data = p.Value.ToArray(),
                      Summary = summaries[p.Key]
                  }).ToList()
            };
        }

        private List<Mob> GetMobs(SimulationParameters parameters)
        {
            return parameters.Mobs.Select(m =>
            {
                var Mob = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(Tank.Mob)) as Tank.Mob;
                Mob.Name = m.Name;
                Mob.Weapons = m.Attacks.Select(a =>
                      new Weapon
                      {
                          LowDamage = a.Damage,
                          HighDamage = a.Damage,
                          Speed = a.Period
                      })
                      .ToList();
                return Mob;
                })
                .ToList();
        }

        private List<Healer> GetHealers(SimulationParameters parameters)
        {
            return parameters.Healers.Select(h =>
                new Healer
                {
                    HealAmount = h.HealAmount,
                    HealPeriod = h.HealPeriod
                })
                .ToList();
        }

        private List<Player> GetTanks(SimulationParameters parameters)
        {
            return parameters.Tanks.Select(GetTank).ToList();
        }

        private Player GetTank(Models.Tank tankData)
        {
            var tank = GetBaseTank(tankData);

            tank.Name = tankData.Name;
            tank.Strength = tankData.Strength;
            tank.Agility = tankData.Agility;
            tank.MaxHealth = tankData.MaxHealth;
            tank.Armor = tankData.Armor;
            tank.MasteryRating = tankData.Mastery;
            tank.CritRating = tankData.Crit;
            tank.HasteRating = tankData.Haste;
            tank.VersatilityRating = tankData.Versatility;
            tank.Weapons = new List<Weapon>
            {
                new Weapon
                {
                    LowDamage=tankData.WeaponLowDamage,
                    HighDamage=tankData.WeaponHighDamage,
                    Speed=tankData.WeaponSpeed
                }
            };

            return tank;
        }

        private Player GetBaseTank(Models.Tank tankData)
        {
            switch(tankData.Class)
            {
                case "Death Knight":
                    return System.Web.Mvc.DependencyResolver.Current.GetService(typeof(Classes.DeathKnight)) as Player;
                case "Demon Hunter":
                    return System.Web.Mvc.DependencyResolver.Current.GetService(typeof(Classes.DemonHunter)) as Player;
                case "Warrior":
                    return System.Web.Mvc.DependencyResolver.Current.GetService(typeof(Classes.Warrior)) as Player;
                case "Monk":
                    return System.Web.Mvc.DependencyResolver.Current.GetService(typeof(Classes.Monk)) as Player;
                case "Druid":
                    return System.Web.Mvc.DependencyResolver.Current.GetService(typeof(Classes.Druid)) as Player;
                default:
                    throw new InvalidOperationException("Unkown tank class");
            }
        }
    }
}
