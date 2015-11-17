(function() {
	'use strict';

	angular.module('Startkicker.controllers', []);
	angular.module('Startkicker.services', []);

	angular
		.module('Startkicker', ['ngRoute', 'Startkicker.controllers', 'Startkicker.services'])
		.config(routesConfig)
		.run(['categories', cashCategories])
		.value('toastr', toastr)
		.constant('BaseUrl', 'http://localhost:50777')
		.constant('ApiBaseUrl', 'http://localhost:50777/api');

	routesConfig.$inject = ['$routeProvider'];

	function routesConfig($routeProvider) {
		$routeProvider
			.when('/', {
				templateUrl: 'templates/home.html',
				controller: 'HomeController',
				controllerAs: 'vm',
			})
			.when('/register', {
				templateUrl: 'templates/register.html',
				controller: 'RegisterController',
				controllerAs: 'vm',
			})
			.when('/login', {
				templateUrl: 'templates/login.html',
				controller: 'LoginController',
				controllerAs: 'vm',
			})
			.when('/projects/add', {
				templateUrl: 'templates/projects-add.html',
				controller: 'AddProjectController',
				controllerAs: 'vm',
			})
			.when('/projects/details', {
				templateUrl: 'templates/project-details.html',
				controller: 'ProjectDetailsController',
				controllerAs: 'vm',
			})
			.otherwise({ redirectTo: '/' });
	}

	function cashCategories(categories) {
		categories
			.getAll()
			.then(function(response) {
				localStorage.setItem('categories', response.data);
			});
	}
}());