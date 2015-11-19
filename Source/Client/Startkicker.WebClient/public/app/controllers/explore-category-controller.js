(function() {
	'use strict';

	angular
		.module('Startkicker.controllers')
		.controller('ExploreCategoryController', ExploreCategoryController);

	ExploreCategoryController.$inject = ['$routeParams', 'projects'];

	function ExploreCategoryController($routeParams, projects) {
		var vm = this;

		vm.category = $routeParams.category;

		projects
			.getByCategory(vm.category)
			.then(function(response) {
				vm.projects = response;
			});
	}
}());