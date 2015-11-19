(function() {
	'use strict';

	angular
		.module('Startkicker.controllers')
		.controller('UserProfileController', userProfileController);

	userProfileController.$inject = ['$routeParams', 'users', 'showServerErrors'];

	function userProfileController($routeParams, users, showServerErrors) {
		var vm = this;

		users
			.get($routeParams.userName)
			.then(function(response) {
				vm.user = response;
			}, function(error) {
				showServerErrors.all(error);
			});
	}
}());