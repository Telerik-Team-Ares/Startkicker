'use strict';

var path = require('path');

module.exports = function(app, config) {
	app.get('/templates/:templateName', function(req, res) {
		res.sendFile(path.join(config.rootPath, 'public/app/templates', req.params.templateName));
	});

	app.get('*', function(req, res) {
		res.sendFile(path.join(config.rootPath, 'public/app/index.html'));
	});
};