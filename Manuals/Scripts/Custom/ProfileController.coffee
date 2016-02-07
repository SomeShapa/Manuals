# CoffeeScript
(->
  Application.controller 'ProfileController', [
    '$scope'
    '$http'
    ($scope, $http) ->
      $scope.Manuals = []
      $http(
        method: 'GET',
        url: '/User/GetManuals').success (data) ->
        $scope.Manuals = data
        return
      return
  ]
  return
).call this