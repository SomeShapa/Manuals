# CoffeeScript
Application.controller 'LangController', [
    '$scope'
    '$http'
    ($scope, $http) ->
        $scope.ChangeCulture = (lang) ->
            $http(
                method: 'POST',
                url: '/Language/ChangeCulture',
                data: { lang: lang }).success (data) ->
                location.reload() if data.result == 'Refresh'
                return
        return
]