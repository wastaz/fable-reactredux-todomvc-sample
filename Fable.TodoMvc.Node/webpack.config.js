var webpack = require("webpack");
var path = require("path");
var fs = require("fs");

var nodeModules = {};
fs.readdirSync('node_modules')
  .filter(function (x) {
    return ['.bin'].indexOf(x) === -1;
  })
  .forEach(function (mod) {
    nodeModules[mod] = 'commonjs ' + mod;
  });

var cfg = {
  devtool: "source-map",
  entry: ["./temp/Fable.TodoMvc.Node/index"],
  target: "node",
  output: {
    path: path.join(__dirname, "public"),
    filename: "bundle.js"
  },
  module: {
    rules: [
      {
	enforce: "pre",
        test: /\.js$/,
        exclude: /node_modules/,
        loader: "source-map-loader"
      }
    ]
  },
  externals: nodeModules
};

module.exports = cfg;
