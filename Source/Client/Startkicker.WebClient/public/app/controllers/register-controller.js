(function() {
	'use strict';

	angular
		.module('Startkicker.controllers')
		.controller('RegisterController', homeController);

	homeController.$inject = ['auth'];

	function homeController(auth) {
		var vm = this;

		vm.user = {
			email: 'pesho@pesho.com',
			password: '1234567',
			confirmPassword: '1234567',
			firstName: 'Pesho',
			lastName: 'Ivanov'
		};

		vm.register = function(user) {
			auth.registerUser(user);
		};
	}
}());