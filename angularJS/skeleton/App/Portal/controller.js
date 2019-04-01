(function () {
    'use strict';

    angular
      .module('myapp')
      .controller('PortalController', PortalController);

    PortalController.$inject = ['$http', '$state', '$rootScope', '$window', '$location'];

    function PortalController($http, $state, $rootScope, $window, $location) {

        var vm = this;

        vm.Companies = [];
		
		console.log($location);
      
        activate();

        function activate() {
			console.log("activate");
			vm.Companies = {AB : ' Άλφα Βήτα', SK : 'Σκλαβενίτης'};
        }
		
		$scope.go = function ( path ) {
			$location.path( path );
		};

    }
}());
