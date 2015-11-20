(function() {
	'use strict';

	angular
		.module('Startkicker.directives')
		.directive('projectListItem', projectListItem);

	function projectListItem() {
		return {
			restrict: 'A',
			scope: {
				project: '='
			},
			templateUrl: 'templates/project-list-item.html'
		};
	}
}());