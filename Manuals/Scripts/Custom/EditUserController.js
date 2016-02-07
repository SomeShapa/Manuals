﻿// Generated by IcedCoffeeScript 108.0.9
(function() {
  Application.controller('EditUserController', [
    '$scope', '$http', function($scope, $http) {
      $scope.SaveTheme = function(Theme) {
        $http({
          method: 'POST',
          url: '/Language/SaveTheme',
          data: User
        }).success(function(data) {});
      };
      $scope.SaveUser = function() {
        return $http({
          method: 'POST',
          url: '/User/UpdateUser',
          data: {
            user: $scope.User,
            returnUrl: $scope.returnUrl
          }
        }).success(function(data) {
          if (data.result === 'Redirect') {
            return window.location.href = data.url;
          }
        });
      };
      $scope.GetUser = function() {
        return $http({
          method: 'POST',
          url: '/User/GetUserById',
          data: {
            Id: $scope.Id
          }
        }).success(function(data) {
          $scope.User = data;
        });
      };
    }
  ]);

  Application.directive('input', function($parse) {
    return {
      restrict: 'E',
      require: '?ngModel',
      link: function(scope, element, attrs) {
        var val;
        if (attrs.ngModel) {
          val = attrs.value || element.text();
          $parse(attrs.ngModel).assign(scope, val);
          scope.GetUser();
        }
      }
    };
  });

}).call(this);