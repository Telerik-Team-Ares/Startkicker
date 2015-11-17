(function() {
	'use strict';

	angular
		.module('Startkicker.services')
		.factory('categories', categories);

	categories.$inject = ['$http', '$q', 'ApiBaseUrl'];

	function categories($http, $q, ApiBaseUrl) {

		function add(category) {
			var url = ApiBaseUrl + '/categories',
				deferred = $q.defer();

			$http
				.post(url, category)
				.then(function(response) {
					deferred.resolve(response.data);
				}, function(err) {
					deferred.reject(err);
				});

			return deferred.promise;
		}

		function getAll() {
			var url = ApiBaseUrl + '/categories',
				deferred = $q.defer();

			$http
				.get(url)
				.then(function(response) {
					deferred.resolve(response.data);
				}, function(err) {
					deferred.reject(err);
				});

			return deferred.promise;
		}

		return {
			add: add,
			getAll: getAll
		};
	}
}());