(function() {
	'use strict';

	angular
		.module('Startkicker.controllers')
		.controller('CategoriesController', categoriesController);

	categoriesController.$inject = ['categories'];

	function categoriesController(categories) {
		var vm = this;

		vm.categories = categories.getCachedCategories();

		};
}());