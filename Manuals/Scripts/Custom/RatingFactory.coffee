# CoffeeScript
Application.factory 'RatingFactory', [
    '$http'
    ($http) ->
        @Change = (manual, liked) ->
            $http(
                method: 'POST'
                url: '/Home/ChangeRating'
                data:
                    manual: manual
                    liked: liked).success (data) ->
                return
            return
        return
]