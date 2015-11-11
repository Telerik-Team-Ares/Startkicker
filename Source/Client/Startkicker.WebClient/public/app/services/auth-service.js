(function() {
	'use strict';

	angular
		.module('Startkicker.services')
		.factory('auth', auth);

	auth.$inject = ['$http', '$q'];

	function auth($http, $q) {
		var url = 'http://localhost:50777/api/account/register';

		function registerUser(user) {
			var deferred = $q.defer();

			$http
				.post(url, user, { headers: { 'Content-Type': 'application/json' } })
				.then(function(response) {
					deferred.resolve(response.data);
				}, function(err) {
					deferred.reject(err);
				});

			return deferred.promise;

		}

		return {
			registerUser: registerUser
		};
	}
}());