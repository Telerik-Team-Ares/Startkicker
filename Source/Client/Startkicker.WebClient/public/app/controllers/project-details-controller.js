(function() {
	'use strict';

	angular
		.module('Startkicker.controllers')
		.controller('ProjectDetailsController', detailsProjectController);

	detailsProjectController.$inject = ['$location', 'projects', 'notifier','showServerErrors'];

	function detailsProjectController($location, projects, notifier, showServerErrors) {
		var vm = this;

		vm.project = {name:''};

		vm.getDetails = function() {
			projects
				.getProjectDetails(vm.project.id)
				.then(function(response) {
					console.log(response);
					vm.project = response;
					//$location.path('/');
					notifier.success('Project details successfully get!');
				},function(error){
					console.log(error);
					showServerErrors.all(error);
				});
		};
	}
}());