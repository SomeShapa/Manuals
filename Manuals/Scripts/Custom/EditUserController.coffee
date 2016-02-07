# CoffeeScript
Application.controller 'EditUserController', [
    '$scope'
    '$http'
    ($scope,$http) ->
        $scope.SaveTheme = (Theme) ->
            $http(
                method: 'POST'
                url: '/Language/SaveTheme'
                data: User).success (data) ->
            return

        $scope.SaveUser = ->
            #$scope.User.BirthDate = $scope.User.BirthDate.toISOString() if $scope.User.BirthDate
            $http(
                method: 'POST'
                url: '/User/UpdateUser'
                data:{ user: $scope.User, returnUrl: $scope.returnUrl }).success (data) ->
                window.location.href = data.url if data.result == 'Redirect'
        $scope.GetUser = ()->
            $http(
                method: 'POST',
                url: '/User/GetUserById',
                data:{ Id: $scope.Id }).success (data) ->
                $scope.User = data
                return
        return

]

Application.directive 'input', ($parse) ->
  {
    restrict: 'E'
    require: '?ngModel'
    link: (scope, element, attrs) ->
      if attrs.ngModel
        val = attrs.value or element.text()
        $parse(attrs.ngModel).assign scope, val
        scope.GetUser()
      return

  }