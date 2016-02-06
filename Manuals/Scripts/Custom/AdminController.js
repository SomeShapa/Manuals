
Application.controller("AdminController", ['$scope', '$http', function ($scope, $http) {
    $scope.Users = [];
    $http({
        method: 'GET',
        url: '/Admin/GetUsers'
    }).success(function (data) {
        $scope.Users = data;
    })

    $scope.DeleteUser = function (Id) {
        $http({
            method: 'POST',
            url: '/Admin/DeleteUser',
            data: { Id: Id }
        }).success(function (data) {
            if (data.success) { 
                angular.forEach($scope.Users, function (value, key) { 
                    if (value.Id == data.Id) { 
                        $scope.Users.splice(key, 1); 
                    } 
                });
            }
        })
    }

}]);
