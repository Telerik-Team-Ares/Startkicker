(function() {
	'use strict';

	angular
		.module('Startkicker.controllers')
		.controller('HomeController', homeController);

	homeController.$inject = ['projects'];

	function homeController(projects) {
		var vm = this;

		projects
			.getAll()
			.then(function(response) {
				vm.projects = response;
				console.log(vm.projects);
			});
	}
}());