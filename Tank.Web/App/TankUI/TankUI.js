angular.module("Tank.Web")
    .directive("tankUi", [ "simulationApi", "storage", "modals", "armoryLoader",
        function (simulationApi, storage, modals, armoryLoader) {
            return {
                restrict: 'E',
                templateUrl: $.url('app/TankUI/TankUI.html'),
                scope: {
                },
                link: function ($scope) {
                    var paramStorage = "tankSimParams";

                    $scope.parameters = {
                        Seed: 12500,
                        RunCount:1,
                        Tanks: [],
                        Healers: [],
                        Mobs: []
                    };

                    var storedParams = storage.get(paramStorage);
                    if(storedParams)
                        $scope.parameters=storedParams;

                    $scope.addTank = function () {
                        $scope.parameters.Tanks.push({});
                    };

                    $scope.removeTank = function (idx) {
                        $scope.parameters.Tanks.splice(idx, 1);
                    };

                    $scope.addMob = function () {
                        $scope.parameters.Mobs.push({});
                    };

                    $scope.removeMob = function (idx) {
                        $scope.parameters.Mobs.splice(idx, 1);
                    };

                    $scope.loadJson = function () {
                        if (confirm("Replace data?"))
                            $scope.parameters = angular.fromJson($scope.jsonData);
                    };

                    $scope.getJson = function () {
                        $scope.jsonData = angular.toJson($scope.parameters);
                    };

                    $scope.getBaseline = function () {
                        $scope.jsonData = '{"Seed":42013,"RunCount":"100","Tanks":[{"Name":"Death Knight","Class":"Death Knight","Strength":"4625","MaxHealth":"533280","Mastery":"1466","Crit":"1400","Haste":"1418","WeaponLowDamage":"7720","WeaponHighDamage":"8584","WeaponSpeed":"3.15","Versatility":"841"},{"Name":"Warrior","Class":"Warrior","Strength":"4691","MaxHealth":"494580","Mastery":"1736","Crit":"1210","Haste":"800","WeaponLowDamage":"5823","WeaponHighDamage":"6715","WeaponSpeed":"2.41","Versatility":"1421"},{"Name":"Demon Hunter","Class":"Demon Hunter","Agility":"5840","MaxHealth":"507240","Mastery":"1486","Crit":"2077","Haste":"1951","WeaponLowDamage":"6469","WeaponHighDamage":"7377","WeaponSpeed":"2.18","Versatility":"351"},{"Name":"Monk","Class":"Monk","Agility":"4670","MaxHealth":"526920","Mastery":"1689","Crit":"1623","Haste":"946","Versatility":"1271","WeaponLowDamage":"7413","WeaponHighDamage":"8140","WeaponSpeed":"3.02"}],"Healers":[],"Mobs":[{"Attacks":[{"Damage":"50000","Period":"3.2"}],"Name":"Slow"},{"Attacks":[{"Damage":"25000","Period":"1.6"}],"Name":"Medium"},{"Attacks":[{"Damage":"12500","Period":"0.8"}],"Name":"Fast"},{"Attacks":[{"Damage":"7500","Period":"0.96"},{"Damage":"30000","Period":"3.84"}],"Name":"Combo"}]}';
                    }

                    $scope.randomizeSeed = function () {
                        $scope.parameters.Seed = parseInt(Math.random() * 50000);
                    }

                    $scope.runSimulation = function () {
                        storage.set(paramStorage, $scope.parameters);
                        simulationApi.runSimulation($scope.parameters)
                            .then(displayResults);
                    };

                    $scope.getWeights = function () {
                        storage.set(paramStorage, $scope.parameters);
                        simulationApi.getWeights($scope.parameters)
                            .then(function (data) { $scope.weights = data; });
                    };

                    $scope.showLog = function (log) {
                        modals.logView(log);
                    };

                    $scope.armoryLoad = function () {
                        var character = prompt("Enter Character Name");

                        armoryLoader.getCharacter(character, "Doomhammer")
                        .then(function (tank) {
                            $scope.parameters.Tanks.push(tank);
                        });
                    };

                    function displayResults(results) {
                        $scope.rawResults = results.Results;
                        $scope.results = [];

                        var classes = {};
                        var mobs = {};

                        
                        for(var i=0;i<results.Results.length;i++)
                        {
                            var res=results.Results[i];

                            var Class = res.label.split('-')[0];
                            var Mob = res.label.split('-')[1];

                            if (!classes[Class])
                                classes[Class] = [];
                            classes[Class].push(res);

                            if (!mobs[Mob])
                                mobs[Mob] = [];
                            mobs[Mob].push(res);
                        }

                        angular.forEach(classes, function (res) { $scope.results.push(res) });
                        angular.forEach(mobs, function (res) { $scope.results.push(res) });

                        //angular.forEach(results.Results, function (res) { $scope.results.push([res]); });

                        var x = 0;
                    };

                    $scope.chartOptions = {
                        series: {
                            lines: { show: true },
                            points: {
                                show: false,
                                radius:0.75
                            }
                        }
                    };
                }
            };
        }]);