(function() {
	'use strict';

	angular
		.module('Startkicker.services')
		.factory('projects', projects);

	projects.$inject = ['$http', '$q', 'ApiBaseUrl'];

	function projects($http, $q, ApiBaseUrl) {

		function add(project) {
			var url = ApiBaseUrl + '/projects',
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

		function getAll() {
			var url = ApiBaseUrl + '/projects?page=1&size=10',
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

		function getById(id) {
			var url = ApiBaseUrl + '/projects/' + id,
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
			getAll: getAll,
			getById: getById
		};
	}
}());