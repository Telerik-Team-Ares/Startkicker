(function() {
	'use strict';

	angular
		.module('Startkicker.controllers')
		.controller('ExploreController', exploreController);

	exploreController.$inject = ['categories'];

	function exploreController(categories) {
		var vm = this;

		vm.categories = categories.getCachedCategories();

	}
}());