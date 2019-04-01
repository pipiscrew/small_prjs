(function () {
    'use strict';

    angular
      .module('myapp')
      .provider('routerHelper', routerHelperProvider);

    routerHelperProvider.$inject = ['$locationProvider', '$stateProvider', '$urlRouterProvider'];

    function routerHelperProvider($locationProvider, $stateProvider, $urlRouterProvider) {

        this.$get = RouterHelper;

        RouterHelper.$inject = ['$state'];
		
		console.log($locationProvider);
		
		<!-- https://scotch.io/tutorials/angular-routing-using-ui-router -->
		
		$urlRouterProvider.otherwise('/home'); // this will send any non-valid url back to "/home"

        function RouterHelper($state) {
            var hasOtherwise = false;

            var service = {
                configureStates: configureStates,
                getStates: getStates
            };

            return service;

            function configureStates(states, otherwisePath) {
                states.forEach(function (state) {
console.log(state);
                    var config = {
                        controllerAs: 'vm',
                    };

                    angular.extend(config, state.config);
                    
                    if (config.views) {
                        _.each(config.views, function (item) {
                            item.controllerAs = 'vm';
                        });
                    }

                    $stateProvider.state(state.state, config);
                });
                if (otherwisePath && !hasOtherwise) {
                    hasOtherwise = true;
                    $urlRouterProvider.otherwise(otherwisePath);
                }
            }

            function getStates() {
                return $state.get();
            }
        }
    }

}());
