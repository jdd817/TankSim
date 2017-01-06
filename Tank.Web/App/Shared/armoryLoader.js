angular.module("Tank.Web")
    .factory("armoryLoader", ["$http", "$q", "loadingIndicator", function ($http, $q, loadingIndicator) {
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
            getCharacter: function (character, realm) {
                loadingIndicator.startLoad();
                var deferred = $q.defer();
                $http.get($.url("BattleNet/Character/" + realm + "/" + character))
                    .success(deferred.resolve)
                    .error(deferred.reject);
                return deferred.promise.then(endload, handleError);
            }
        };
    }]);