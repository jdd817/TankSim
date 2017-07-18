angular.module("Tank.Web")
    .directive("tankConfig", ["simulationApi",
        function (simulationApi) {
            return {
                restrict: 'E',
                templateUrl: $.url('app/TankConfig/TankConfig.html'),
                scope: {
                    tank: "="
                },
                link: function ($scope) {
                    $scope.tankClasses = [
                        { Name: "Death Knight", PrimaryStat: "Strength" },
                        { Name: "Demon Hunter", PrimaryStat: "Agility" },
                        { Name: "Warrior", PrimaryStat: "Strength" },
                        { Name: "Monk", PrimaryStat: "Agility" },
                        { Name: "Druid", PrimaryStat: "Agility" }
                    ];

                    simulationApi.getAvailableEffects($scope.tank.Class).then(function (data) { $scope.availableEffects = data; });

                    if ($scope.tank && $scope.tank.Class)
                    {
                        angular.forEach($scope.tankClasses, function (tankClass) {
                            if (tankClass.Name == $scope.tank.Class)
                                $scope.Class = tankClass;
                        })
                    }

                    $scope.newEffect = null;

                    $scope.$watch("Class", function () {
                        if (!$scope.tank.Name || $scope.tank.Name == "")
                            $scope.tank.Name = $scope.Class.Name;
                        $scope.tank.Class = $scope.Class.Name;
                        simulationApi.getAvailableEffects($scope.tank.Class).then(function (data) { $scope.availableEffects = data; });
                    });

                    $scope.addEffect = function (ef) {
                        if (!$scope.tank.Effects)
                            $scope.tank.Effects = [];
                        $scope.tank.Effects.push(angular.copy($scope.newEffect));
                        $scope.newEffect = null;
                    }

                    $scope.removeEffect = function (effectIndex) {
                        $scope.tank.Effects.splice(effectIndex, 1);
                    }
                }
            };
        }]);