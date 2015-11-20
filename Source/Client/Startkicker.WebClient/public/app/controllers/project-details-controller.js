(function() {
	'use strict';

	angular
		.module('Startkicker.controllers')
		.controller('ProjectDetailsController', detailsProjectController);

	detailsProjectController.$inject = ['$routeParams', '$location', 'projects', 'notifier', 'showServerErrors'];

	function detailsProjectController($routeParams, $location, projects, notifier, showServerErrors) {
		var vm = this;

		vm.donation = {
			moneyAmount:0,
			id:''
		};

		projects
			.getById($routeParams.id)
			.then(function(response) {
				vm.project = response;
			}, function(error) {
				showServerErrors.all(error);
			})

		vm.donate = function(donation){
			donation.id = vm.project.id;
			console.log(donation);
			if(donation.moneyAmount===0 || !!!donation.id){
				notifier.error('Please choose to which project you want to donate and the right amount of money!');
			}else{
				projects
					.donate(donation)
					.then(function(data){
						notifier.success('Your donation was sucess!');
						vm.project.collectedMoney+=donation.moneyAmount;
					},function(error){
						showServerErrors.all(error);
					})
			}
		};
	}
}());