angular.module("Tank.Web")
    .factory("modals", ["$uibModal", function ($uibModal) {
        return {
            logView: function (log) {
                return $uibModal.open({
                    template: '<div class="modal-body"><log-view log="log"></log-view></div>',
                    resolve: {
                        log: function () {
                            return log;
                        }
                    },
                    controller: function ($scope) {
                        $scope.log = log;
                    }
                });
            }
        };
    }])