﻿// Generated by IcedCoffeeScript 108.0.9
(function() {
  Application.factory('RatingFactory', [
    '$http', function($http) {
      this.Change = function(manual, liked) {
        $http({
          method: 'POST',
          url: '/Home/ChangeRating',
          data: {
            manual: manual,
            liked: liked
          }
        }).success(function(data) {});
      };
    }
  ]);

}).call(this);
