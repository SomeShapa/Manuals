﻿// Generated by IcedCoffeeScript 108.0.9
(function() {
  Application.controller('HomeController', [
    '$scope', '$http', function($scope, $http) {
      $scope.Manuals = [];
      $scope.CategoryFilter = "";
      $scope.TagFilter = "";
      $scope.TagAllow = true;
      $scope.Page = 0;
      $(window).scroll(function() {
        if ($(window).scrollTop() + $(window).height() > $(document).height() - 1) {
          $http({
            method: 'POST',
            url: '/Home/GetManualPage',
            data: {
              category: $scope.CategoryFilter,
              tag: $scope.TagFilter,
              page: $scope.Page
            }
          }).success(function(data) {
            $scope.Manuals = $scope.Manuals.concat(data);
            return $scope.Page += 1;
          });
        }
      });
      $http({
        method: 'GET',
        url: '/Home/GetCategories'
      }).success(function(data) {
        $scope.Categories = data;
      });
      $scope.colors = ['#800026', '#bd0026', '#e31a1c', '#fc4e2a', '#fd8d3c', '#feb24c', '#fed976'];
      $http({
        method: 'POST',
        url: '/Home/GetManualPage',
        data: {
          category: $scope.CategoryFilter,
          tag: $scope.TagFilter,
          page: $scope.Page
        }
      }).success(function(data) {
        $scope.Manuals = $scope.Manuals.concat(data);
        $scope.Page += 1;
      });
      $scope.ChangeRating = function(manual, liked) {
        $http({
          method: 'POST',
          url: '/Home/ChangeRating',
          data: {
            manual: manual,
            liked: liked
          }
        }).success(function(data) {
          manual.Rating = data.newRating;
        });
      };
      $scope.DeleteManual = function(manual) {
        $http({
          method: 'POST',
          url: '/Home/DeleteManual',
          data: {
            manual: manual
          }
        }).success(function(data) {
          return $scope.Manuals.splice($scope.Manuals.indexOf(manual), 1);
        });
        return;
      };
      $scope.compare = function(tags, tag) {
        var f;
        if (tag == '') {
          return true;
        } else {
          f = false;
          tags.forEach(function(item, i, arr) {
            if (item.Name == tag) {
              f = true;
            }
          });
          if (f) {
            return true;
          }
        }
        return false;
      };
      $scope.Tags = [];
      $http({
        method: 'GET',
        url: '/Home/GetTags'
      }).success(function(data) {
        $scope.words = [];
        angular.forEach(data, function(item) {
          var flag;
          flag = true;
          angular.forEach($scope.words, function(word) {
            if (word.text === item.Name) {
              word.weight += 1;
              return flag = false;
            }
          });
          if (flag) {
            $scope.words.push({
              text: item.Name,
              weight: 1,
              handlers: {
                click: function() {
                  $scope.TagFilter = item.Name;
                }
              }
            });
          }
        });
      });
      $scope.update = function() {
        $scope.words.splice(-5);
      };
    }
  ]);

  return;

}).call(this);
