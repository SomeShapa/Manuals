# CoffeeScript

Application.controller 'ProfileController', [
    '$scope'
    '$http'
    ($scope, $http) ->
        $scope.Manuals = []
        $scope.User = {}
        $scope.TopDiscManuals = {}
        $scope.TopRatManuals={}
        $scope.GetManuals = ->
            $http(
                method: 'POST',
                url: '/User/GetManualsByUserId',
                data: Id: $scope.Id ).success (data) ->
                $scope.Manuals = data
                return
        $scope.GeMostDisc = ->
            $http(
                method: 'POST',
                url: '/Home/GetMostDiscussedManual',
                data: UserId: $scope.Id ).success (data) ->
                $scope.TopDiscManuals = data
                return
        $scope.GetTop = ->
            $http(
                method: 'POST',
                url: '/Home/GetTopManual',
                data: UserId: $scope.Id ).success (data) ->
                $scope.TopRatManuals = data
                return
        $scope.ChangeRating = (manual, liked) ->
            $http(
              method: 'POST'
              url: '/Home/ChangeRating'
              data:
                manual: manual
                liked: liked).success (data) ->
              manual.Rating = data.newRating
              return
            return

        $scope.DeleteManual = (manual) ->  
            $http(
              method: 'POST'
              url: '/Home/DeleteManual'
              data:
                manual: manual).success (data) ->     
                     $scope.Manuals.splice ($scope.Manuals.indexOf manual), 1
              return              
            return

        return
]