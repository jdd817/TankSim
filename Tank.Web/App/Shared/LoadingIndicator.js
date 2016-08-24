angular.module("Tank.Web")
    .factory("loadingIndicator", ["$rootScope", function ($rootScope) {
        var loadingDepth = 0;
        return {
            startLoad: function () {
                if (loadingDepth <= 0) {
                    $rootScope.$broadcast('showLoadingIndicator');
                }
                loadingDepth++;
            },
            endLoad: function () {
                loadingDepth--;
                if (loadingDepth <= 0) {
                    loadingDepth = 0;
                    $rootScope.$broadcast('hideLoadingIndicator');
                }
            }
        };
    }])
    .directive("loadingIndicator", [
        function () {
            return {
                restrict: 'E',
                template: "<div class='loadingIndicator' ng-show='display'>Loading</div>",
                scope: {},
                link: function (scope) {
                    scope.display = false;

                    scope.$on('showLoadingIndicator', function () {
                        scope.display = true;
                    });

                    scope.$on('hideLoadingIndicator', function () {
                        scope.display = false;
                    });
                }
            };
        }]);