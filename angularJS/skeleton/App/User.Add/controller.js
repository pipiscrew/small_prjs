(function () {
    'use strict';

    angular
      .module('myapp')
      .controller('UserEditController', UserEditController);

    UserEditController.$inject = ['$http', '$state', '$rootScope', '$window'];

    function UserEditController($http, $state, $rootScope, $window) {

        var vm = this;

        vm.User;
		vm.Password;
		vm.Telephone;
		vm.Address;
        vm.Save = Save;
		
		console.log($state);
      
        activate();

        function activate() {
			console.log("activate");
        }
		
		
		function SaveUser() {
			console.log("SaveUser");
		}

    }
}());
