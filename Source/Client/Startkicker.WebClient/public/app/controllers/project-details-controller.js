(function() {
	'use strict';

	angular
		.module('Startkicker.controllers')
		.controller('ProjectDetailsController', detailsProjectController);

	detailsProjectController.$inject = ['$routeParams', '$location', 'projects', 'notifier','showServerErrors'];

	function detailsProjectController($routeParams, $location, projects, notifier, showServerErrors) {
		var vm = this;

		console.log($routeParams.id);


		projects
			.getById($routeParams.id)
			.then(function (response) {
				console.log(response);
				vm.project = response;
			},function(error){
				showServerErrors.all(error);
			})



		// vm.project = {name:''};

		// vm.getDetails = function() {
		// 	projects
		// 		.getProjectDetails(vm.project.id)
		// 		.then(function(response) {
		// 			console.log(response);
		// 			vm.project = response;
		// 			//$location.path('/');
		// 			notifier.success('Project details successfully get!');
		// 		},function(error){
		// 			console.log(error);
		// 			showServerErrors.all(error);
		// 		});
		// };
	}
}());