(function() {
	'use strict';

	angular
		.module('Startkicker.services')
		.factory('auth', auth);

	auth.$inject = ['$http', '$q', '$window', 'BaseUrl', 'ApiBaseUrl', 'identity'];

	function auth($http, $q, $window, BaseUrl, ApiBaseUrl, identity) {
		function registerUser(user) {
			var url = ApiBaseUrl + '/account/register',
				deferred = $q.defer();

			$http
				.post(url, user)
				.then(function(response) {
					deferred.resolve(response.data);
				}, function(err) {
					deferred.reject(err);
				});

			return deferred.promise;
		}

		function loginUser(user) {
			var url = BaseUrl + '/Token',
				data = 'grant_type=password&username=' + user.username + '&password=' + user.password,
				deferred = $q.defer();

			$http
				.post(url, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
				.then(function(response) {
					var loggedUser = {
						username: response.data.userName,
						token: response.data.access_token
					};

					identity.saveLoggedUser(loggedUser);
					deferred.resolve(loggedUser);
					$http.defaults.headers.common.Authorization = 'Bearer ' + response.data.access_token;
				}, function(err) {
					deferred.reject(err);
				});

			return deferred.promise;
		}

		function logoutUser() {
			identity.removeLoggedUser();
			$http.defaults.headers.common.Authorization = null;
			$window.location.reload();
		}

		return {
			registerUser: registerUser,
			loginUser: loginUser,
			logoutUser: logoutUser
		};
	}
}());