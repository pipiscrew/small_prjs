var app = angular.module('plunker', ["ngRoute","treeControl"]);

app.config(['$routeProvider', '$locationProvider', function($routeProvider, $locationProvider){
    $routeProvider.
    when("/NewEvent",{
        templateUrl : "add_event.html",
        controller: "AddEventController"
    }).
    when("/DisplayEvent", {
        templateUrl: "show_event.html",
        controller: "ShowDisplayController"
    })

    $locationProvider.html5Mode(true);
}]);

app.controller('UnitListCtrl', function($scope, $http, $location) {
	
	$scope.doSelect = doSelect;
    
    //init roots
    getRecords(0)
    .then(function (result) {
        //var x = result.data;
       // x = x.replace('"[]"', '[]'); //replaceAll('"[]"', '[]', result.data);
        $scope.treedata = result.data;
        console.log("1",result.data);
    }, function errorCallback(err) {
        console.log("*error*",err);
    }).finally(function () {
        console.log("*finally");
    });
    


    //node selected
	function doSelect(node) {
        if (node.children && node.children.length >0)
         return;
         console.log(node);
        // return;
        getRecords(node.id)
        .then(function (result) {
            var log = [];
            node.children = [];
            angular.forEach(result.data, function(value, key) {
                value.children = [];
                node.children.push(value);
              }, log);
            console.log("1",result.data);
           // $scope.expandedNodes=node;
           $scope.expandedNodes.push(node);
        }, function errorCallback(err) {
            console.log("*error*",err);
        }).finally(function () {
            console.log("*finally");
        });
        // console.log(d);
		 //$scope.treedata[1].children.push({label: "New Child", id:"some id", children: []})
		console.log("1");
  
        $location.path('/DisplayEvent')
      console.log("2");
    }
    
    //when is 4 is node, folder is when < 4
    $scope.opts = {
        isLeaf: function(node) {
            return node.IsFolder==4;
        },
        dirSelectable: true
    };
    
    function getRecords(node_id) {
        return $http
            .get('/services/api.php?id='+node_id);
    }
	  
	 


});


app.controller('ShowDisplayController', function($scope, $http, $location) {
});
