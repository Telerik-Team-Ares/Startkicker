(function() {
	'use strict';

	angular
		.module('Startkicker.directives')
		.directive('fileModel', fileModel);

	fileModel.$inject = ['$parse', '$q'];

	function fileModel($parse, $q) {
		return {
			restrict: 'A',
			link: function(scope, element, attr) {
				var model = $parse(attr.fileModel);

				element.bind('change', function() {
					readFile(element[0].files[0])
						.then(function (values) {
							model.assign(scope.vm, values);
						});
				});

				// $q.all(Array.prototype.slice.call(element.files, 0).map(readFile))
				// 	.then(function(values) {
				// 		console.log(values);
				// 	});

				function readFile(file) {
					var deferred = $q.defer();

					var reader = new FileReader();
					reader.onload = function(e) {
						var convertedFile = e.target.result;
						var result = {
							originalFileName: file.name.substr(0, file.name.lastIndexOf('.')),
							fileExtension: file.name.substr(file.name.lastIndexOf('.') + 1).toLowerCase(),
							base64Content: convertedFile.substr(convertedFile.indexOf(',') + 1)
						};

						deferred.resolve(result);
					};
					reader.onerror = function(e) {
						deferred.reject(e);
					};
					reader.readAsDataURL(file);

					return deferred.promise;
				}
			}
		};
	}
}());