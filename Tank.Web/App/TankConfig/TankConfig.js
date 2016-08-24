angular.module("Tank.Web")
    .directive("tankConfig", [
        function () {
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

                    if ($scope.tank && $scope.tank.Class)
                    {
                        angular.forEach($scope.tankClasses, function (tankClass) {
                            if (tankClass.Name == $scope.tank.Class)
                                $scope.Class = tankClass;
                        })
                    }

                    $scope.$watch("Class", function () {
                        if (!$scope.tank.Name || $scope.tank.Name == "")
                            $scope.tank.Name = $scope.Class.Name;
                        $scope.tank.Class = $scope.Class.Name;
                    })
                }
            };
        }]);