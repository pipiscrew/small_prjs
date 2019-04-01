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
	  
	 
	 
    var names = ['Homer', 'Marge', 'Bart', 'Lisa', 'Mo'];
    function createSubTree(level, width, prefix) {
        if (level > 0) {
            var res = [];
            for (var i=1; i <= width; i++)
                res.push({ "label" : "Node " + prefix + i, "id" : "id"+prefix + i, "i": i, "children": createSubTree(level-1, width, prefix + i +"."), "name": names[i%names.length] });
            return res;
        }
        else
            return [];
    }
	
	
   $scope.treedata=[
       { "label" : "Vegetables", "children" : [
         { "label": "Vegetable:Carrot (Orange)",  "name" : "Carrot",  "color" : "Orange", "class" : "Vegetable", "children" : [] },
         { "label": "Vegetable:Cabbbage (Green)", "name" : "Cabbage", "color" : "Green",  "class" : "Vegetable", "children" : [] },
         { "label": "Vegetable:Potato (Brown)",   "name" : "Potato",  "color" : "Brown",  "class" : "Vegetable", "children" : [] } 
       ]},
       { "label" : "Fruit", "children" : [
         { "label": "Fruit:Apricot (Orange)", "name" : "Apricot", "color" : "Orange", "class" : "Fruit", "children" : [] },
         { "label": "Fruit:Apple (Red)",      "name" : "Apple",   "color" : "Red",    "class" : "Fruit", "children" : [] },
         { "label": "Fruit:Grape (Green)",    "name" : "Grape",   "color" : "Green",  "class" : "Fruit", "children" : [] }
       ]}
   ];
   
  $scope.lots = [
        {
        'apt_no': 'P0106',
        'lot_no': '44',
        'aspect': 'P/E',
        'bedroom': '1',
        'car': '0',
        'internal_size': '61',
        'external_size': '23',
        'price': '$580,000'},
    {
        'apt_no': 'P0104',
        'lot_no': '2',
        'aspect': 'P/E',
        'bedroom': '1',
        'car': '0',
        'internal_size': '54',
        'external_size': '26',
        'price': '$576,000'},
    {
        'apt_no': 'P0108',
        'lot_no': '46',
        'aspect': 'P/N+W',
        'bedroom': '3',
        'car': '2',
        'internal_size': '98',
        'external_size': '46',
        'price': '$1,156,000'},
    {
        'apt_no': 'P0111',
        'lot_no': '49',
        'aspect': 'P/W',
        'bedroom': '2',
        'car': '1',
        'internal_size': '76',
        'external_size': '20',
        'price': '$700,000'},
    {
        'apt_no': 'P0113',
        'lot_no': '129',
        'aspect': 'P/W',
        'bedroom': '3',
        'car': '2',
        'internal_size': '110',
        'external_size': '24',
        'price': '$1,076,000'}
    ];
	
    $scope.predicate = 'apt_no';
	
	
    $scope.clearFilter = function() {
      console.log("xxx");
      $scope.query = {};
    };

	
     $scope.expandedNodes = [$scope.treedata[1],
         $scope.treedata[3],
         $scope.treedata[3],
         $scope.treedata[3]];
     $scope.setExpanded = function() {
         $scope.expandedNodes = [$scope.treedata[1],
             $scope.treedata[2],
             $scope.treedata[2].children[2]
         ];
     };


});


app.controller('ShowDisplayController', function($scope, $http, $location) {
});
