Application.controller 'ThemeController', [
  '$scope'
  '$http'
  ($scope, $http) ->
    $http(
      method: 'GET'
      url: '/Language/GetTheme').success (data) ->
      $scope.Theme = data.result
      return

    $scope.SaveTheme = (Theme) ->
      $http(
        method: 'POST'
        url: '/Language/SaveTheme'
        data: Theme: Theme).success (data) ->
      return
    return
]# CoffeeScript
