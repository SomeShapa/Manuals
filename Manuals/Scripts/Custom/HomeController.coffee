  Application.controller 'HomeController', [
    '$scope'
    '$http'
    ($scope, $http) ->
      $scope.Manuals = []
      $scope.CategoryFilter= "";
      $scope.TagFilter ="";
      $scope.Page= 0;
      $(window).scroll ->
        if $(window).scrollTop() + $(window).height() > $(document).height() - 1
            $http(
                method: 'POST'
                url: '/Home/GetManualPage'
                data:
                  category:  $scope.CategoryFilter
                  tag:  $scope.TagFilter
                  page:  $scope.Page).success (data) ->
                   $scope.Manuals= $scope.Manuals.concat data
                   $scope.Page+=1;
                return
        
      $scope.colors = [
        '#800026'
        '#bd0026'
        '#e31a1c'
        '#fc4e2a'
        '#fd8d3c'
        '#feb24c'
        '#fed976'
      ]

      $http(
        method: 'POST'
        url: '/Home/GetManualPage'
        data:
          category:  $scope.CategoryFilter
          tag:  $scope.TagFilter
          page:  $scope.Page).success (data) ->
            $scope.Manuals= $scope.Manuals.concat data
            $scope.Page+=1;
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

      $scope.Tags = []
      $http(
        method: 'GET'
        url: '/Home/GetTags').success (data) ->
        $scope.words = []
        angular.forEach data, (item) ->
          flag=true;
          angular.forEach $scope.words, (word) ->
            if word.text==item.Name 
                word.weight+=1
                flag = false;
          if flag
             $scope.words.push
               text: item.Name
               weight: 1
               link: 'http://google.com'
             return
        return

      $scope.update = ->
        $scope.words.splice -5
        return
      return
  ]
  return