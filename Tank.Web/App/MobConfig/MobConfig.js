angular.module("Tank.Web")
    .directive("mobConfig", [
        function () {
            return {
                restrict: 'E',
                transclude: true,
                templateUrl: $.url('app/MobConfig/MobConfig.html'),
                scope: {
                    mob:"="
                },
                link: function ($scope) {
                    if (!$scope.mob.Attacks)
                        $scope.mob.Attacks = [];

                    $scope.addAttack = function () {
                        $scope.mob.Attacks.push({ Damage: 100000, Period: 2.0 });
                    }

                    $scope.removeAttack = function (idx) {
                        $scope.mob.Attacks.splice(idx, 1);
                    }
                }
            };
        }]);