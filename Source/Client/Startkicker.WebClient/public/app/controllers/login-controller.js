(function() {
	'use strict';

	angular
		.module('Startkicker.controllers')
		.controller('LoginController', loginController);

	loginController.$inject = ['$scope', '$location', 'auth', 'notifier', 'showServerErrors'];

	function loginController($scope, $location, auth, notifier, showServerErrors) {
		var vm = this;

		vm.user = {};

		vm.login = function(user) {
			auth
				.loginUser(user)
				.then(function() {
					$scope.$emit('userLoggedIn', user.username);
					$location.path('/');
					notifier.success('Login successfully!');
				}, function(error) {
					showServerErrors.all(error);
				});
		};

		vm.cancelLogin = function() {
			$location.path('/');
		};
	}
}());