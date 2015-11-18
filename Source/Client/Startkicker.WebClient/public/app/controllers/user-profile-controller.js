(function() {
	'use strict';

	angular
		.module('Startkicker.controllers')
		.controller('UserProfileController', userProfileController);

	userProfileController.$inject = ['$routeParams', 'users','showServerErrors'];

	function userProfileController($routeParams,users, showServerErrors) {
		var vm = this;

		console.log($routeParams.id);

		users
			.get($routeParams.userName)
			.then(function (response) {
				console.log(response);
				vm.user = response;
			}, function(error){
				showServerErrors.all(error);
			})
	}
}());