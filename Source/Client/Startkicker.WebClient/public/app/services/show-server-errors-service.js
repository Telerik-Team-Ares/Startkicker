(function() {
	'use strict';

	angular
		.module('Startkicker.services')
		.factory('showServerErrors', showServerErrors);

	showServerErrors.$inject = ['notifier'];


	function showServerErrors(notifier) {

		function showErrorMessagesWithDelay(obj,prop, delayCoeficient){
			setTimeout(function(){
				console.log(obj[prop]);
				notifier.error(obj[prop]);
			},delayCoeficient*2000);
		}

		function all(errorResponse){
			var i =0;
			for (var key in errorResponse.data.modelState) {
				if (errorResponse.data.modelState.hasOwnProperty(key)) {
					var obj = errorResponse.data.modelState[key];
					for (var prop in obj) {
						if(obj.hasOwnProperty(prop)){
							showErrorMessagesWithDelay(obj, prop,i);
							i++;
						}
					}
				}
			}
		}

		return {
			all: all
		};
	}
}());