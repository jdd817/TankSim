angular.module("Tank.Web")
    .directive("talentConfig", ["simulationApi",
        function (simulationApi) {
            return {
                restrict: 'E',
                templateUrl: $.url('app/TankConfig/TalentConfig.html'),
                scope: {
                    talents: "=",
                    className:"="
                },
                link: function ($scope) {
                    $scope.talentGrid = [
                        [false, false, false],
                        [false, false, false],
                        [false, false, false],
                        [false, false, false],
                        [false, false, false],
                        [false, false, false],
                        [false, false, false],
                    ];

                    $scope.talentNames = [
                        ["", "", ""],
                        ["", "", ""],
                        ["", "", ""],
                        ["", "", ""],
                        ["", "", ""],
                        ["", "", ""],
                        ["", "", ""],
                    ];

                    angular.forEach($scope.talents, function (talent) {
                        $scope.talentGrid[talent.Row - 1][talent.Column - 1] = true;
                    });

                    $scope.selectTalent = function (tier, column) {
                        var i;
                        for (i = 0; i < 3; i++)
                            $scope.talentGrid[tier - 1][i] = false;
                        $scope.talentGrid[tier - 1][column - 1] = true;

                        $scope.talents=[];

                        for(i=0;i<7;i++){
                            var j;
                            for (j = 0; j < 3; j++) {
                                if ($scope.talentGrid[i][j]) {
                                    $scope.talents.push({ Row: i + 1, Column: j + 1 });
                                }
                            }
                        }
                    }

                    $scope.$watch("className", function () {
                        if ($scope.className) {
                            simulationApi.getAvailableTalents($scope.className).then(function (data) {
                                angular.forEach(data, function (talent) {
                                    $scope.talentNames[talent.Row - 1][talent.Column - 1] = talent.Name;
                                })
                            });
                        }
                    });
                }
            };
        }]);