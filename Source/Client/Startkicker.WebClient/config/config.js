'use strict';

var path = require('path');

var rootPath = path.normalize(__dirname + '/..');

module.exports = {
	development: {
		rootPath: rootPath,
		port: 8080
	},
	production: {
		rootPath: rootPath,
		port: process.env.PORT
	}
};