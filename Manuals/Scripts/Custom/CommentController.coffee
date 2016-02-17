Application.controller 'CommentController', [
  '$scope'
  '$http'
  ($scope, $http) ->
    $scope.Comments = []
    $scope.NewComment = {}
    $scope.GetComments = ->
      $http(
        method: 'POST'
        url: '/Templates/GetComments'
        data: 
           manualId: $scope.NewComment.ManualId).success (data) ->
        $scope.Comments = data 
        return
      return
    $scope.Create = (comment) ->
      $http(
        method: 'POST'
        url: '/Templates/CreateNewComment'
        data: model: comment).success (data) ->
        $scope.GetComments()
        return
      return
    $scope.ChangeCommentRating = (comment, liked) ->
      $http(
        method: 'POST'
        url: '/Templates/ChangeCommentRating'
        data:
          comment: comment
          liked: liked).success (data) ->
        comment.Rating = data.newRating
        return
      return
    timerUpdate = setInterval((->
      $scope.GetComments()
      return
    ), 5000)
    timerLoad = setTimeout((->
      $scope.GetComments()
      return
    ), 100)
    return
]
