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

		function getProjectDetails(projectId){
			var url = ApiBaseUrl + '/projects/getById',
				deferred = $q.defer();
			var data = {
				params:{
				id:projectId
				}
			};
			$http
				.get(url, data)
				.then(function(response) {
					deferred.resolve(response.data);
				}, function(err) {
					deferred.reject(err);
				});

			return deferred.promise;
		}

		return {
			add: add,
			getProjectDetails: getProjectDetails
		};
	}
}());