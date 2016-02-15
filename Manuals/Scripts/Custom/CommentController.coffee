Application.controller 'CommentController', [
    '$scope'
    '$http'
    ($scope, $http) ->
      $scope.Comments = []
      $scope.NewComment = {};
      $http(
        method: 'GET'
        url: '/Templates/GetComments').success (data) ->
        $scope.Comments = data
        return
      $scope.Create = (comment) ->
        $http(
           method: 'POST',
           url: '/Templates/CreateNewComment',
           data: { model: comment}).success (data) ->
           $scope.Comments.add(comment)
           return
        return
      $scope.ChangeRating = (comment, liked) ->
        $http(
          method: 'POST'
          url: '/Template/ChangeCommentRating'
          data:
            comment: comment
            liked: liked).success (data) ->
          comment.Rating = data.newRating
          return
        return
      return
]
return
