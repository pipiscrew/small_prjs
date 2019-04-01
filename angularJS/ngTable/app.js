var app = angular.module('plunker', ["ngTable", "ngResource"]);


app.controller('UnitListCtrl', function(NgTableParams, $resource) {
    vm = this;
    // tip: to debug, open chrome dev tools and uncomment the following line 
    //debugger;

    var Api = $resource('/services/api.php?id=takis');
    vm.tableParams = new NgTableParams({
      page: 1, // show first page
      count: 10 // count per page
    }, {
      filterDelay: 300,
      getData: function(params) {
        // ajax request to api
        return Api.get(params.url()).$promise.then(function(data) {
          params.total(data.inlineCount);
          return data.results;
        });
      }
    });

});


app.controller('ShowDisplayController', function($scope, $http, $location) {
});
