(function () {
    'use strict';

    angular
      .module('myapp')
      .controller('ErrorController', ErrorController);

    ErrorController.$inject = ['$scope', '$stateParams', 'ErrorPages'];

    function ErrorController($scope, $stateParams, ErrorPages) {
        $scope.Err = ErrorPages[$scope.$stateParams.err];
    }

})();
