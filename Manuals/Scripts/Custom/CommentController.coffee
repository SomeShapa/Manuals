Application.controller 'CommentController', [
  '$scope'
  '$http'
  ($scope, $http) ->
    $scope.Comments = []
    $scope.NewComment = {}
    $scope.Page= 0;
    $(window).scroll ->
      if $(window).scrollTop() + $(window).height() > $(document).height() - 1
        $scope.Page = $scope.Page + 1;
        $scope.GetComments()
    $scope.GetComments = ->
      $http(
        method: 'POST'
        url: '/Templates/GetCommentbyPage'
        data:
            manualId:  $scope.NewComment.ManualId
            page:  $scope.Page).success (data) ->
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
    $scope.DeleteComment = (comment) ->
      $http(
        method: 'POST'
        url: '/Templates/DeleteComment'
        data:
          comment: comment).success (data) ->     
        $scope.Comments.splice ($scope.Comments.indexOf comment), 1
        return              
        return
    timerUpdate = setInterval((->
      $scope.GetComments()
      return
    ), 50000)
    timerLoad = setTimeout((->
      $scope.GetComments()
      return
    ), 100)
    return
]
