angular.module("Tank.Web")
    .factory("simulationApi", ["$http", "$q", "loadingIndicator",
        function ($http, $q, loadingIndicator) {

            function endload(data) {
                loadingIndicator.endLoad();
                return data;
            }

            function handleError(ex) {
                loadingIndicator.endLoad();
                alert(ex.ExceptionMessage);
                return ex;
            }

            return {
                runSimulation: function (parameters) {
                    loadingIndicator.startLoad();
                    var deferred = $q.defer();
                    $http.post($.url("api/Simulation"), parameters)
                        .success(deferred.resolve)
                        .error(deferred.reject);
                    return deferred.promise.then(endload, handleError);
                },
                getWeights: function (parameters) {
                    loadingIndicator.startLoad();
                    var deferred = $q.defer();
                    $http.post($.url("api/Simulation/Weights"), parameters)
                        .success(deferred.resolve)
                        .error(deferred.reject);
                    return deferred.promise.then(endload, handleError);
                },
                getAvailableEffects: function (className) {
                    loadingIndicator.startLoad();
                    var deferred = $q.defer();
                    var url = "api/effects";
                    if (className != null)
                        url += "/" + className.replace(" ", "");
                    $http.get($.url(url))
                        .success(deferred.resolve)
                        .error(deferred.reject);
                    return deferred.promise.then(endload, handleError);
                },
                getAvailableTalents: function (className) {
                    loadingIndicator.startLoad();
                    var deferred = $q.defer();
                    var url = "api/talents";
                    if (className != null)
                        url += "/" + className.replace(" ","");
                    $http.get($.url(url))
                        .success(deferred.resolve)
                        .error(deferred.reject);
                    return deferred.promise.then(endload, handleError);
                }
            };
        }]);