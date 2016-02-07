Application.controller 'ThemeController', [
  '$scope'
  '$http'
  ($scope, $http) ->
    $http(
      method: 'GET'
      url: '/Language/GetTheme').success (data) ->
      $scope.Theme = data.result
      return
    return
]# CoffeeScript
