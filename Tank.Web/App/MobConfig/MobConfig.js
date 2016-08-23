angular.module("Tank.Web")
    .directive("mobConfig", [
        function () {
            return {
                restrict: 'E',
                templateUrl: $.url('app/MobConfig/MobConfig.html'),
                scope: {
                    mob:"="
                },
                link: function ($scope) {
                    $scope.addAttack = function () {
                        $scope.mob.Attacks.push({ Damage: 100000, Period: 2.0 });
                    }

                    $scope.removeAttack = function (idx) {
                        $scope.mob.Attacks.splice(idx, 1);
                    }

                    if (!$scope.mob.Attacks) {
                        $scope.mob.Name = "Mob" + parseInt(Math.random() * 10000);
                        $scope.mob.Attacks = [];
                        $scope.addAttack();
                    }
                }
            };
        }]);