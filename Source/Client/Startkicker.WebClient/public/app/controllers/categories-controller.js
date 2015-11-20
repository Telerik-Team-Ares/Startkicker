(function() {
	'use strict';

	angular
		.module('Startkicker.controllers')
		.controller('CategoriesController', CategoriesController);

	CategoriesController.$inject = ['categories', 'notifier'];

	function CategoriesController(categories, notifier) {
		var vm = this;

		vm.categories = categories.getCachedCategories();
		vm.newCategoryName = '';

		vm.addCategory = function(categoryName) {
			categories
				.add({ name: categoryName })
				.then(function(response) {
					vm.categories.push(response);
					vm.newCategoryName = '';
					notifier.success('New category added successfully!');
				});
		};

		vm.removeCategory = function(categoryId, index) {
			categories
				.remove(categoryId, index)
				.then(function(response) {
					vm.categories.splice(index, 1);
					notifier.success(response);
				});
		};
	}
}());