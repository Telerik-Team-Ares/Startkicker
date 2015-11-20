(function() {
	'use strict';

	angular
		.module('Startkicker.controllers')
		.controller('ProjectDetailsController', detailsProjectController);

	detailsProjectController.$inject = ['$routeParams', '$location', 'projects', 'notifier', 'showServerErrors'];

	function detailsProjectController($routeParams, $location, projects, notifier, showServerErrors) {
		var vm = this;

		projects
			.getById($routeParams.id)
			.then(function(response) {
				vm.project = response;
			}, function(error) {
				showServerErrors.all(error);
			});
	}
}());