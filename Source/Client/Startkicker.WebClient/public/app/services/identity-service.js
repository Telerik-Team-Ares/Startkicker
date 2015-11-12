(function() {
	'use strict';

	angular
		.module('Startkicker.services')
		.factory('identity', identity);

	function identity() {

		function saveLoggedUser(user) {
			localStorage.setItem('username', user.username);
			localStorage.setItem('token', user.token);
		}

		function getUsername() {
			return localStorage.getItem('username');
		}

		function getAccessToken() {
			return localStorage.getItem('token');
		}

		function removeLoggedUser() {
			localStorage.setItem('username', '');
			localStorage.setItem('token', '');
		}

		return {
			saveLoggedUser: saveLoggedUser,
			getUsername: getUsername,
			getAccessToken: getAccessToken,
			removeLoggedUser: removeLoggedUser
		};
	}
}());