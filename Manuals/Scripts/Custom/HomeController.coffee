Application.controller 'HomeController', [
    '$scope'
    '$http'
    ($scope, $http) ->
        $scope.Manuals = []
        $http(
            method: 'GET',
            url: '/Home/GetManuals').success (data) ->
            $scope.Manuals = data
            return
        $scope.ChangeRating = (manual,liked) ->
            $http(
                method: 'POST'
                url: '/Home/ChangeRating'
                data:
                    manual: manual
                    liked: liked).success (data) ->
                    manual.Rating = data.newRating
                    return
             return
        return
]
