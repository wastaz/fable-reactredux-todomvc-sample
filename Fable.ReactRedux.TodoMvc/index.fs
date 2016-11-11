module Index

open Fable.Import
open Fable.Helpers.Redux
open Fable.Helpers.ReactRedux

open Types

module R = Fable.Helpers.React

Node.require.Invoke("core-js") |> ignore

let createApp _ =
    R.div [] [
        AddTodoComponent.createAddTodoComponent ()
        TodoListComponent.createTodoList TodoListComponent.defaultProps
        R.fn FooterComponent.createFooter None []
    ]

let initialStoreState = { 
    todos = []
    visibilityFilter = Types.Visibility.All 
    error = None
}

let middleware = Redux.applyMiddleware(ReduxThunk.middleware, unionMiddleware)
let store = createStore Reducer.reduceTodos initialStoreState middleware

let provider = createProvider store (R.fn createApp None [])

ReactDom.render(provider, Browser.window.document.getElementById "content") |> ignore

Msg.ShowError "Hello world!" |> store.dispatch
