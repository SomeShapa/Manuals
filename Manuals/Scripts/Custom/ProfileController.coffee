# CoffeeScript

  Application.controller 'ProfileController', [
    '$scope'
    '$http'
    ($scope, $http) ->
      $scope.Manuals = []
      $http(
        method: 'POST',
        url: '/User/GetManualsByUserId',
        data: {userId: $scope.userId}).success (data) ->
        $scope.Manuals = data
        return
      return
  ]