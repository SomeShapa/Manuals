Application.controller 'HomeController', [
    '$scope'
    '$http'
    ($scope, $http) ->
        $scope.Creatives = []
        $http(
            method: 'GET',
            url: '/Home/GetCreatives').success (data) ->
            $scope.Creatives = data
            return
        return
]
