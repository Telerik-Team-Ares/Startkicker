(function() {
	'use strict';

	angular
		.module('Startkicker.services')
		.factory('notifier', notifier);

	function notifier(toastr) {
		toastr.options.showMethod = 'slideDown';
		toastr.options.hideMethod = 'slideUp';
		toastr.options.closeMethod = 'slideUp';
		toastr.options.progressBar = true;

		return {
			success: function(message) {
				toastr.success(message);
			},
			error: function(message) {
				toastr.error(message);
			}
		};
	}
}());