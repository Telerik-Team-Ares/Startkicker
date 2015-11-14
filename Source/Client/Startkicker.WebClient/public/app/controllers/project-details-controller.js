(function() {
	'use strict';

	angular
		.module('Startkicker.controllers')
		.controller('ProjectDetailsController', detailsProjectController);

	detailsProjectController.$inject = ['$location', 'projects', 'notifier'];

	function detailsProjectController($location, projects, notifier) {
		var vm = this;

		vm.project = {name:''};

		vm.getDetails = function() {
			projects
				.getProjectDetails(vm.project.id)
				.then(function(response) {
					console.log(response);
					vm.project.name = response.name;
					//$location.path('/');
					notifier.success('Project details successfully get!');
				});
		};
	}
}());