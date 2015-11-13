(function() {
	'use strict';

	angular
		.module('Startkicker.controllers')
		.controller('AddProjectController', addProjectController);

	addProjectController.$inject = ['$location', 'projects', 'notifier'];

	function addProjectController($location, projects, notifier) {
		var vm = this;

		vm.project = {};

		vm.addProject = function(project) {
			projects
				.add(project)
				.then(function() {
					$location.path('/');
					notifier.success('New project added success!');
				});
		};
	}
}());