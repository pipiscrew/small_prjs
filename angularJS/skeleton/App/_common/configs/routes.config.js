(function () {
    'use strict';

    angular
      .module('myapp')
      .run(RoutesConfig);

    RoutesConfig.$inject = ['routerHelper'];

    function RoutesConfig(routerHelper) {

        var _states = getStates();
        routerHelper.configureStates(_states, APP.RootFolder == "" ? "" : "/" + APP.RootFolder);

    }

    function getStates() {
        return [{
            state: 'app',
            config: {
                url: APP.RootFolder == "" ? "" : '/' + APP.RootFolder,
                templateUrl: APP.RootFolder+"App/_common/_layout.html?" + APP.VersionCode,
                controller: "AppController",
            }
        }, {
            state: 'error',
            config: {
                url: APP.RootFolder == "" ? '/error?err' : '/' + APP.RootFolder + '/error?err',
                templateUrl: APP.RootFolder+"App/_common/error.html?" + APP.VersionCode,
                params: {
                    err: "GENERIC"
                },
                controller: "ErrorController",
            }
        }];
    }

}());
