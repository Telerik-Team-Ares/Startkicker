(function() {
	'use strict';

	angular
		.module('Startkicker.services')
		.factory('users', users);

	users.$inject = ['$http', '$q', 'ApiBaseUrl'];

	function users($http, $q, ApiBaseUrl) {

		function get(userName) {
			var url = ApiBaseUrl + '/users/profile?userName=' + userName,
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
			get: get
		};
	}
}());