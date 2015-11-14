(function() {
	'use strict';

	angular
		.module('Startkicker.controllers')
		.controller('AddProjectController', addProjectController);

	addProjectController.$inject = ['$location', 'projects', 'notifier','showServerErrors'];

	function addProjectController($location, projects, notifier, showServerErrors) {
		var vm = this;

		vm.project = {};

		vm.addProject = function(project) {
			projects
				.add(project)
				.then(function() {
					$location.path('/');
					notifier.success('New project added success!');
				},
				function (errorResponse) {
					showServerErrors.all(errorResponse);
				}

			);


		};
	}
}());