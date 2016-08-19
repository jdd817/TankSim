using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Tank.Web.Models;
using Tank.Web.Controllers;

namespace TankTests
{
    [TestFixture]
    public class ApiTests
    {
        public SimulationParameters parameters;
        private SimulationController Service;

        public ApiTests()
        {
            Service = new SimulationController();
            parameters = new SimulationParameters
            {
                Tanks = new List<Tank.Web.Models.Tank>
                {
                    new Tank.Web.Models.Tank()
                    {
                        Name = "DK",
                        Class="Death Knight",
                        Mastery = 1466,
                        Crit = 1400,
                        Haste = 1418,
                        Strength = 4625,
                        MaxHealth = 533280,
                        WeaponLowDamage = 7720,
                        WeaponHighDamage = 8584,
                        WeaponSpeed = 3.15m
                    }
                },
                Healers = new List<Healer>()
                {
                    new Healer
                    {
                        HealAmount=20000,
                        HealPeriod=1
                    }
                },
                Mobs = new List<Mob>()
                {
                    new Mob
                    {
                        Name="test",
                        Attacks=new List<MobAttack>()
                        {
                            new MobAttack
                            {
                                Damage=80000,
                                Period=1.6m
                            }
                        }
                    }
                }
            };
        }

        [Test]
        public void TestValuesFromMultiple()
        {
            var singleRuns = new List<SimulationResult>();

            parameters.RunCount = 1;
            parameters.Seed = 3115;
            singleRuns.Add(Service.RunSimulation(parameters));
            parameters.Seed = 445;
            singleRuns.Add(Service.RunSimulation(parameters));

            parameters.RunCount = 2;
            parameters.Seed = 49001;
            var multipleRun = Service.RunMultipleSimulations(parameters);


            var singleDatasets = singleRuns.SelectMany(r => r.Results).Select(r => r.data).ToList();
            var multipleDataset = multipleRun.Results.First().data;

            for(var i=0m;i<200m;i+=0.1m)
            {
                var multipleDatapoint = multipleDataset
                    .Where(dp => dp[0] == i)
                    .DefaultIfEmpty(new[] { 0m, 0m })
                    .First()[1];

                var singleDataPoints = singleDatasets.Select(ds => ds
                    .Where(dp => dp[0] == i)
                    .DefaultIfEmpty(new[] { 0m, 0m })
                    .First()[1]).ToList();

                multipleDatapoint.Should().Be(singleDataPoints.Sum() / 2m, "Time index " + i);
            }
        }
    }
}
