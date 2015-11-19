(function() {
	'use strict';

	angular
		.module('Startkicker.controllers')
		.controller('CategoriesController', CategoriesController);

	CategoriesController.$inject = ['categories'];

	function CategoriesController(categories) {
		var vm = this;

		vm.categories = categories.getCachedCategories();
		vm.newCategoryName = '';

		vm.addCategory = function(categoryName) {
			categories
				.add({ name: categoryName })
				.then(function(response) {
					vm.categories.push(response);
					vm.newCategoryName = '';
				});
		};
	}
}());