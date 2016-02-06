# CoffeeScript
Application.controller 'AddNewManualController', [
    '$scope'
    '$http'
    ($scope, $http) ->
        $scope.Categories = []
        $scope.NewManual = { Tags: [] };
        $scope.Tags = [];
        $http(
            method: 'GET',
            url: '/Home/GetCategories').success (data) ->
            $scope.Categories = data
            return
        $http(
            method: 'GET',
            url: '/Home/GetTags',
            data: { Id: $scope.NewManual.Id }).success (data) ->
            $scope.Tags = data
            return
        $scope.AddTag = (tag) ->
            $http(
                method: 'POST',
                url: '/Home/AddNewTag',
                data: { tagName: $scope.NewTag }).success (data) ->
                $scope.NewManual.Tags.push data.tag if data.success
                $scope.NewTag = ''
                return
        $scope.Create = (Manual) ->
            $http(
                method: 'POST',
                url: '/Home/CreateNewManual',
                data: Manual)
        return
]