﻿// Generated by IcedCoffeeScript 108.0.9
(function() {
  Application.controller('LangController', [
    '$scope', '$http', function($scope, $http) {
      $scope.ChangeCulture = function(lang) {
        return $http({
          method: 'POST',
          url: '/Language/ChangeCulture',
          data: {
            lang: lang
          }
        }).success(function(data) {
          if (data.result === 'Refresh') {
            location.reload();
          }
        });
      };
    }
  ]);

}).call(this);