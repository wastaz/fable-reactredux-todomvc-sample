var path = require("path");
var webpack = require("webpack");

var cfg = {
  devtool: "source-map",
  entry: [
    "webpack-dev-server/client?http://localhost:8080",
    "webpack/hot/only-dev-server",
    "./temp/index"
  ],
  output: {
    path: path.join(__dirname, "public"),
    filename: "bundle.js"
  },
  plugins: [
    new webpack.HotModuleReplacementPlugin()
  ],
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
  performance: { hints: false },
  devServer: {
    hot: true,
    contentBase: "public/",
    publicPath: "/",
    historyApiFallback: true
  }
};

module.exports = cfg;
