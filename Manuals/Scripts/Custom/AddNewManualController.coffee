# CoffeeScript
Application.controller 'AddNewCreativeController', [
    '$scope'
    '$http'
    ($scope, $http) ->
        $scope.Categories = []
        $scope.NewCreative = { Tags: [] };
        $scope.Tags = [];
        $http(
            method: 'GET',
            url: '/Home/GetCategories').success (data) ->
            $scope.Categories = data
            return
        $http(
            method: 'GET',
            url: '/Home/GetTags',
            data: { Id: $scope.NewCreative.Id }).success (data) ->
            $scope.Tags = data
            return
        $scope.AddTag = (tag) ->
            $http(
                method: 'POST',
                url: '/Home/AddNewTag',
                data: { tagName: $scope.NewTag }).success (data) ->
                $scope.NewCreative.Tags.push data.tag if data.success
                $scope.NewTag = ''
                return
        $scope.Create = (creative) ->
            $http(
                method: 'POST',
                url: '/Home/CreateNewCreative',
                data: creative)
        return
]