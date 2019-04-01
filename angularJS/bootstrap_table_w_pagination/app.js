var app = angular.module('plunker', ["ngRoute",'ui.bootstrap']);


// app.config(['$routeProvider', '$locationProvider', function($routeProvider, $locationProvider){
//     $routeProvider.
//     when("/NewEvent",{
//         templateUrl : "add_event.html",
//         controller: "AddEventController"
//     }).
//     when("/DisplayEvent", {
//         templateUrl: "show_event.html",
//         controller: "ShowDisplayController"
//     })

//     $locationProvider.html5Mode(true);
// }]);

app.controller('UnitListCtrl', function($scope, $http, $location) {
    $scope.maxSize = 5;     // Limit number for pagination display number.
    $scope.totalCount = 0;  // Total number of items in all pages. initialize as a zero
    $scope.pageIndex = 1;   // Current page number. First page is 1.-->
    $scope.pageSizeSelected = 5; // Maximum number of items per page.

    $scope.getEmployeeList = function () {
        $http.get("/services/api.php?id=takis&pageIndex=" + $scope.pageIndex + "&pageSize=" + $scope.pageSizeSelected).then(
                       function (response) {
                           console.log(response.data);
                           $scope.employees = response.data.employees;
                           $scope.totalCount = response.data.totalCount;
                       },
                       function (err) {
                           var error = err;
                       });
    }

    //Loading employees list on first time
    $scope.getEmployeeList();

    //This method is calling from pagination number
    $scope.pageChanged = function () {
        $scope.getEmployeeList();
    };

    //This method is calling from dropDown
    $scope.changePageSize = function () {
        $scope.pageIndex = 1;
        $scope.getEmployeeList();
    };

});


app.controller('ShowDisplayController', function($scope, $http, $location) {
});
