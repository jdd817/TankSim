angular.module("Tank.Web")
    .directive("effectConfig", [
        function () {
            return {
                restrict: 'E',
                templateUrl: $.url('app/TankConfig/EffectConfig.html'),
                scope: {
                    effect: "=",
                    availableEffects: "=",
                    selected: "&",
                    remove: "&"
                },
                link: function ($scope) {
                    $scope.$watch("effect", function () {
                        if ($scope.selected && $scope.effect != null)
                            $scope.selected($scope.effect);
                    });
                }
            };
        }]);