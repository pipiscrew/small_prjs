(function () {
    'use strict';

    angular
      .module('myapp')
      .controller('AppController', AppController);

    AppController.$inject = ['$rootScope', '$http', '$state', '$stateParams'];

    function AppController($rootScope, $http, $state, $stateParams) {
        var vm = this;
        vm.$state = $state;
        vm.menuUrl = APP.RootFolder+'App/_common/fullmenu.html?' + APP.VersionCode;
		console.log(vm.menuUrl);
        activate();

        function activate() {

            vm.UserName = "test";

            //Load with expanded menu
            //angular.element(document.body).removeClass('sidebar-collapse');
        }


    }

})();
