# fable-reactredux-todomvc-sample

This is a small ugly sample todomvc application showing an example of how to combine
- [fable-import-react](https://www.npmjs.com/package/fable-import-react)
- [fable-import-redux](https://github.com/wastaz/fable-import-redux)
- [fable-import-reactredux-opinionated](https://github.com/wastaz/fable-import-reactredux-opinionated)
- [fable-import-reduxthunk](https://github.com/wastaz/fable-import-reduxthunk)

## Gettings started

Clone the repository and then run `npm install` in both the `Fable.ReactRedux.TodoMvc` folder and 
the `Fable.TodoMvc.Node` folder.

Start the simple todomvc backend by running `fable && node public/bundle.js` in the `Fable.TodoMvc.Node` folder.
This will start a simple express backend on `http://localhost:3000`.

Start the simple todomvc frontend app by going into the `Fable.ReactRedux.TodoMvc` folder and running
`fable && webpack-dev-server`. This will start up webpack-dev-server on `http://localhost:8080`.

Navigate to `http://localhost:8080` and try not to cry at my the lack of any type of visual design. ;)

## License

MIT, feel free to fork and/or send pull requests :)
