(function() {
	'use strict';

	angular.module('Startkicker.controllers', []);

	angular
		.module('Startkicker', ['ngRoute', 'Startkicker.controllers'])
		.config(routesConfig);

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
			});
	}
}());