(function() {
	'use strict';

	angular
		.module('Startkicker.controllers')
		.controller('RegisterController', homeController);

	homeController.$inject = [];

	function homeController() {
		var vm = this;

		vm.user = {};
		vm.register = function(user) {
			console.log(user);
		};
	}
}());