(function() {
	'use strict';

	angular
		.module('Startkicker.controllers')
		.controller('RegisterController', registerController);

	registerController.$inject = ['$location', 'auth', 'notifier'];

	function registerController($location, auth, notifier) {
		var vm = this;

		vm.user = {};

		vm.register = function(user) {
			auth
				.registerUser(user)
				.then(function() {
					$location.path('/');
					notifier.success('Registration successfully!');
				});
		};

		vm.cancelRegister = function() {
			$location.path('/');
		};
	}
}());