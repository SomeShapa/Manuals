﻿(->
  Application.controller 'CoommentController', [
    '$scope'
    '$http'
    ($scope, $http) ->
      $scope.ChangeCommentRating = (manual, liked) ->
        $http(
          method: 'POST'
          url: '/Templates/ChangeCommentRating'
          data:
            comment: comment
            liked: liked).success (data) ->
          comment.Rating = data.newRating
          return
        return
      return
  ]
  return
).call this

# ---
# generated by js2coffee 2.1.0