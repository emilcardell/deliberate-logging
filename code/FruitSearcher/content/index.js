var app = angular.module('FruitApp', []);

app.controller('MainController', ['$scope', '$http', function ($scope, $http) {

	$http.get("/search").success(function (data, status, headers, config) {
		console.log(data);
		$scope.FruitTypes = data.aggregations.nameAggs.buckets;
		$scope.Quantities = data.aggregations.quantityAggs.buckets;
		$scope.Result = data.hits.hits.map(function (result) {
			return result._source;
		});

		console.log($scope.Result);
	});

	var loadResult = function () {
		var name = '';
		if ($scope.SelectedFruit) {
			name = $scope.SelectedFruit.key;
		}

		var quantity = '';
		if ($scope.SelectedQuantity) {
			quantity = $scope.SelectedQuantity.key;
		}			

		$http.get("/search?name=" + name + "&quantity=" + quantity ).success(function (data, status, headers, config) {
			$scope.Result = data.hits.hits.map(function (result) {
				return result._source;
			});
		});
	}

	$scope.SelectFruitType = function (menuItem) {		
		$scope.FruitTypes.forEach(function (item) {
			item.Selected = false;
		});
		menuItem.Selected = true;
		$scope.SelectedFruit = menuItem;
		loadResult();

	};

	$scope.SelectQuantity = function (menuItem) {
		$scope.Quantities.forEach(function (item) {
			item.Selected = false;
		});
		menuItem.Selected = true;
		$scope.SelectedQuantity = menuItem;
		loadResult();
	};

	

}]);