angular.module("Tank.Web")
    .directive("healsConfig", [
        function () {
            return {
                restrict: 'E',
                templateUrl: $.url('app/HealsConfig/HealsConfig.html'),
                scope: {
                    healers:"="
                },
                link: function ($scope) {
                    if (!$scope.healers)
                        $scope.healers = [];

                    $scope.addHealer = function () {
                        $scope.healers.push({ HealAmount: 0, HealPeriod: 0 });
                    }

                    $scope.removeHealer = function (idx) {
                        $scope.healers.splice(idx, 1);
                    }
                }
            };
        }]);