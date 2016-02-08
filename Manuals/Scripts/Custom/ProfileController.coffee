# CoffeeScript

Application.controller 'ProfileController', [
    '$scope'
    '$http'
    ($scope, $http) ->
        $scope.Manuals = []
        $scope.User = {}
        $scope.GetManuals = ->
            $http(
                method: 'POST',
                url: '/User/GetManualsByUserId',
                data: Id: $scope.Id ).success (data) ->
                $scope.Manuals = data
                return

        return
]