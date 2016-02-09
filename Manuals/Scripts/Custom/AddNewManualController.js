﻿// Generated by IcedCoffeeScript 108.0.9
(function() {
  Application.controller('AddNewManualController', [
    '$scope', '$http', function($scope, $http) {
      $scope.Categories = [];
      $scope.Templates = [];
      $scope.NewManual = {
        Tags: []
      };
      $scope.Tags = [];
      $http({
        method: 'GET',
        url: '/Home/GetCategories'
      }).success(function(data) {
        $scope.Categories = data;
        $scope.NewManual.CategoryId = $scope.Categories[0].Id;
      });
      $http({
        method: 'GET',
        url: '/Home/GetTemplates'
      }).success(function(data) {
        $scope.Templates = data;
        $scope.NewManual.TemplateId = $scope.Templates[0].Id;
      });
      $http({
        method: 'GET',
        url: '/Home/GetTags',
        data: {
          Id: $scope.NewManual.Id
        }
      }).success(function(data) {
        $scope.Tags = data;
      });
      $scope.AddTag = function(tag) {
        return $http({
          method: 'POST',
          url: '/Home/AddNewTag',
          data: {
            tagName: $scope.NewTag
          }
        }).success(function(data) {
          if (data.success) {
            $scope.NewManual.Tags.push(data.tag);
          }
          $scope.NewTag = '';
        });
      };
      $scope.Create = function(Manual) {
        return $http({
          method: 'POST',
          url: '/Home/CreateNewManual',
          data: {
            model: Manual,
            ReturnUrl: $scope.ReturnUrl
          }
        }).success(function(data) {
          if (data.result === 'Redirect') {
            window.location.href = data.url;
          }
        });
      };
    }
  ]);

}).call(this);
