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
            $scope.NewManual.CategoryId = $scope.Categories[0].Id
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
                data: { model: Manual, ReturnUrl: $scope.ReturnUrl }).success (data) ->
                window.location.href = data.url if data.result == 'Redirect'
                return
        return
]