(function() {
	'use strict';

	angular
		.module('Startkicker.controllers')
		.controller('LayoutController', layoutController);

	layoutController.$inject = ['$scope', 'auth', 'identity'];

	function layoutController($scope, auth, identity) {
		var vm = this;

		vm.loggedUser = identity.getUsername();
		vm.hasLoggedUser = !!vm.loggedUser;

		$scope.$on('userLoggedIn', function(ev, username) {
			vm.loggedUser = username;
			vm.hasLoggedUser = true;
		});

		vm.logout = function() {
			auth.logoutUser();
			vm.hasLoggedUser = false;
		};
	}
}());