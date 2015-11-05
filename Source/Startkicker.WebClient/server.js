'use strict';

var express = require('express');
var path = require('path');

var env = process.env.NODE_ENV || 'development';
var config = require('./config/config')[env];

var app = express();

app.use(express.static(path.join(config.rootPath, 'public')));

require('./config/routes')(app, config);

app.listen(config.port, function() {
	console.log('Server running at http://localhost:' + config.port);
	console.log('Environment: ' + env);
});