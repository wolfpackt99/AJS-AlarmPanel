angular.module('alarmApp', ['alarmApp.services', 'firebase']).
    controller('paneCtrl', ['$scope', 'panelService', 'angularFire', function ($scope, panelService, angularFire) {
        
        var promise = angularFire('https://tdj-notification-panel.firebaseio.com/Panel', $scope, "firePanel", {});

        $scope.panelService = panelService;
        $scope.panelService.get(function (data) {
            $scope.panel = data;
        });
        console.log($scope.panel);
    }]);

angular.module('alarmApp.services', []).
    factory('panelService', function ($rootScope, $http) {
        var server = "http://localhost:6619/api/service";
        return {
            get: function (successCallback) {
                var res;
                $http({ method: 'GET', url: server })
                    .success(function (data, status, headers, config) {
                        res = data;
                        successCallback(data);
                    })
                    .error(function (data, status, headers, config) {
                        console.log(data);
                    });
                return res;
            }
        };
    });

app.run(['$templateCache', function ($templateCache) {
    $templateCache.put('switcher.html');
}]);

app.directive('btnSwitch', function () {

    return {
        restrict: 'A',
        require: 'ngModel',
        templateUrl: 'switcher.html',
        replace: true,
        link: function (scope, element, attrs, ngModel) {

            // Specify how UI should be updated
            ngModel.$render = function () {
                render();
            };

            var render = function () {
                var val = ngModel.$viewValue;

                var open = angular.element(element.children()[0]);
                open.removeClass(val ? 'hide' : 'show');
                open.addClass(val ? 'show' : 'hide');

                var closed = angular.element(element.children()[1]);
                closed.removeClass(val ? 'show' : 'hide');
                closed.addClass(val ? 'hide' : 'show');
            };

            // Listen for the button click event to enable binding
            element.bind('click', function () {
                scope.$apply(toggle);
            });

            // Toggle the model value
            function toggle() {
                var val = ngModel.$viewValue;
                ngModel.$setViewValue(!val);
                render();
            }

            if (!ngModel) {
                //TODO: Indicate that something is missing!
                return;
            }  // do nothing if no ng-model

            // Initial render
            render();
        }
    };
});