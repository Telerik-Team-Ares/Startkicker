(function() {
	'use strict';

	angular
		.module('Startkicker.services')
		.factory('projects', projects);

	projects.$inject = ['$http', '$q', 'ApiBaseUrl', 'identity'];

	function projects($http, $q, ApiBaseUrl, identity) {

		function add(project){
			var url = ApiBaseUrl + '/projects/add',
				deferred = $q.defer();

			$http
				.post(url, project)
				.then(function(response) {
					deferred.resolve(response.data);
				}, function(err) {
					deferred.reject(err);
				});

			return deferred.promise;
		}

		return {
			add: add
		};
	}
}());