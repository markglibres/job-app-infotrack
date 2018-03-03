app.service('googleRankingService', ['$http', function ($http) {

    var baseUrl = '/api/search/';
    var google = {
        rankings: baseUrl + 'rankings'
    };

    this.getRankings = function (urlToSearch, searchTerm) {

        var data = {
            params: {
                url: encodeURIComponent(urlToSearch),
                searchTerm: encodeURIComponent(searchTerm)
            }
        };

        return $http.get(google.rankings, data);
    };
    
}]);