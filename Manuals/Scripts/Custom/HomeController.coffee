Application.controller 'HomeController', [
    '$scope'
    '$http'
    ($scope, $http) ->
        $scope.Manuals = []
        $http(
            method: 'GET',
            url: '/Home/GetManuals').success (data) ->
            $scope.Manuals = data
            return
        return
]
