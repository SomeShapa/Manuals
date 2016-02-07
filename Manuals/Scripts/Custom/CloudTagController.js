﻿Application.controller('controller', function($scope) {
    $scope.words = [
      {text: "Lorem", weight: 13},
      {text: "Ipsum", weight: 10.5},
      {text: "Dolor", weight: 9.4},
      {text: "Sit", weight: 8},
      {text: "Amet", weight: 6.2},
      {text: "Consectetur", weight: 5},
      {text: "Adipiscing", weight: 5},
      {text: "Elit", weight: 5},
      {text: "Nam et", weight: 5},
      {text: "Leo", weight: 4},
      {text: "Sapien", weight: 4},
      {text: "Pellentesque", weight: 3},
      {text: "habitant", weight: 3},
      {text: "morbi", weight: 3},
      {text: "tristisque", weight: 3},
      {text: "senectus", weight: 3},
      {text: "et netus", weight: 3},
      {text: "et malesuada", weight: 3},
      {text: "fames", weight: 2},
      {text: "ac turpis", weight: 2},
      {text: "egestas", weight: 2},
      {text: "Aenean", weight: 2},
      {text: "vestibulum", weight: 2},
      {text: "elit", weight: 2},
      {text: "sit amet", weight: 2},
      {text: "metus", weight: 2},
      {text: "adipiscing", weight: 2},
      {text: "ut ultrices", weight: 2}
    ];

    $scope.colors = ["#800026", "#bd0026", "#e31a1c", "#fc4e2a", "#fd8d3c", "#feb24c", "#fed976"];

    $scope.update = function() {
        $scope.words.splice(-5);
    };
});