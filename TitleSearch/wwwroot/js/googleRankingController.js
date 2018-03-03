app.controller('GoogleRankingController', ['$scope', 'googleRankingService', function ($scope, googleRankingService) {
    $scope.rankings = [];
    $scope.validUrlPattern = /(https?:\/\/)?([^\.]+\.){1,}.+/;
    $scope.loading = false;

    $scope.searchRanking = function() {
        $scope.loading = true;
        googleRankingService.getRankings($scope.urlToSearch, $scope.searchTerm)
            .then(function(response) {
                $scope.rankings = response.data;
                $scope.loading = false;
            });
        
    };

    
}]);