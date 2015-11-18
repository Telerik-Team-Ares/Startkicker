(function() {
	'use strict';

	angular
		.module('Startkicker.controllers')
		.controller('AddProjectController', addProjectController);

	addProjectController.$inject = ['$scope', '$location', 'projects', 'categories', 'notifier', 'showServerErrors'];

	function addProjectController($scope, $location, projects, categories, notifier, showServerErrors) {
		var vm = this;

		vm.categories = categories.getCachedCategories();
		vm.project = {};

		vm.addProject = function(project) {
			projects
				.add(project)
				.then(function() {
					$location.path('/');
					notifier.success('New project added successfully!');
				}, function(errorResponse) {
					showServerErrors.all(errorResponse);
				});
		};
	}
}());