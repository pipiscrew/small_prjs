(function () {
    'use strict';

    angular
      .module('myapp')
      .controller('LoginController', LoginController);

    LoginController.$inject = ['$http', '$state', '$rootScope', '$window'];

    function LoginController($http, $state, $rootScope, $window) {

        var vm = this;

        vm.User;
		vm.Password;
        vm.Save = Save;
		
		console.log($state);
      
        activate();

        function activate() {
			console.log("activate");
        }
		
		
		function Login() {
			console.log("Login");
		}

    }
}());
