(function() {
	'use strict';

	angular
		.module('Startkicker.controllers')
		.controller('RegisterController', registerController);

	registerController.$inject = ['$location', 'auth', 'notifier', 'showServerErrors'];

	function registerController($location, auth, notifier, showServerErrors) {
		var vm = this;

		vm.user = {};

		vm.register = function(user) {
			auth
				.registerUser(user)
				.then(function() {
					$location.path('/');
					notifier.success('Registration successfully!');
				}, function(error) {
					showServerErrors.all(error);
				});
		};

		vm.cancelRegister = function() {
			$location.path('/');
		};
	}
}());