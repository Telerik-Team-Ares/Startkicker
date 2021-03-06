(function() {
	'use strict';

	angular.module('Startkicker.controllers', []);
	angular.module('Startkicker.services', []);
	angular.module('Startkicker.directives', []);

	angular
		.module('Startkicker', ['ngRoute', 'Startkicker.controllers', 'Startkicker.services', 'Startkicker.directives'])
		.config(routesConfig)
		.run(['identity', 'pubnub', '$http', checkForLoggedUser])
		.run(['categories', cashCategories])
		.value('toastr', toastr)
		.value('pubnub', PUBNUB)
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
			.when('/projects/details/:id', {
				templateUrl: 'templates/project-details.html',
				controller: 'ProjectDetailsController',
				controllerAs: 'vm',
			})
			.when('/users/profile/:userName', {
				templateUrl: 'templates/user-profile.html',
				controller: 'UserProfileController',
				controllerAs: 'vm',
			})
			.when('/projects/explore', {
				templateUrl: 'templates/explore.html',
				controller: 'ExploreController',
				controllerAs: 'vm',
			})
			.when('/projects/explore/:category', {
				templateUrl: 'templates/explore-category.html',
				controller: 'ExploreCategoryController',
				controllerAs: 'vm',
			})
			.when('/categories', {
				templateUrl: 'templates/categories.html',
				controller: 'CategoriesController',
				controllerAs: 'vm',
			})
			.otherwise({ redirectTo: '/' });
	}

	function checkForLoggedUser(identity, pubnub, $http) {
		var token = identity.getAccessToken();

		if (!!token) {
			$http.defaults.headers.common.Authorization = 'Bearer ' + token;
		}

		pubnub.subscribe({
			channel: identity.getUsername(),
			callback: function (message) {
				toastr.success(message);
			}
		});
	}

	function cashCategories(categories) {
		categories
			.getAll()
			.then(function(response) {
				localStorage.setItem('categories', JSON.stringify(response));
			});
	}
}());