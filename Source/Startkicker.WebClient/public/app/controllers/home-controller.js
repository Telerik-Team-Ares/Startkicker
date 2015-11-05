(function() {
	'use strict';

	angular
		.module('Startkicker.controllers')
		.controller('HomeController', homeController);

	homeController.$inject = [];

	function homeController() {
		var vm = this;

		vm.name = 'Home';
	}
}());