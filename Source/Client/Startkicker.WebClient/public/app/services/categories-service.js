(function() {
	'use strict';

	angular
		.module('Startkicker.services')
		.factory('categories', categories);

	categories.$inject = ['$http', '$q', 'ApiBaseUrl'];

	function categories($http, $q, ApiBaseUrl) {
		var url = ApiBaseUrl + '/categories';

		function add(category) {
			var deferred = $q.defer();

			$http
				.post(url, category)
				.then(function(response) {
					var newCategory = {
							name: category.name,
							id: response.data
						},
						cachedCategories = getCachedCategories();

					cachedCategories.push(newCategory);
					localStorage.setItem('categories', JSON.stringify(cachedCategories));

					deferred.resolve(newCategory);
				}, function(err) {
					deferred.reject(err);
				});

			return deferred.promise;
		}

		function getAll() {
			var deferred = $q.defer();

			$http
				.get(url)
				.then(function(response) {
					deferred.resolve(response.data);
				}, function(err) {
					deferred.reject(err);
				});

			return deferred.promise;
		}

		function remove(id, index) {
			var deferred = $q.defer();

			$http
				.delete(url + '/' + id)
				.then(function(response) {
					var cachedCategories = getCachedCategories();

					cachedCategories.splice(index, 1);
					localStorage.setItem('categories', JSON.stringify(cachedCategories));

					deferred.resolve(response.data);
				}, function(err) {
					deferred.reject(err);
				});

			return deferred.promise;
		}

		function getCachedCategories() {
			return JSON.parse(localStorage.getItem('categories'));
		}

		return {
			add: add,
			getAll: getAll,
			getCachedCategories: getCachedCategories,
			remove: remove
		};
	}
}());