module Index

open Fable.Import
open Fable.Helpers.Redux
open Fable.Helpers.ReactRedux

open Types

module R = Fable.Helpers.React

Node.require.Invoke("core-js") |> ignore

let createApp initialState =
    R.div [] [
        AddTodoComponent.createAddTodoComponent ()
        TodoListComponent.createTodoList ()
        R.fn FooterComponent.createFooter (obj()) []
    ]

let initialStoreState = { 
    todos = []
    visibilityFilter = Types.Visibility.All 
    error = None
}

let middleware = Redux.applyMiddleware ReduxThunk.middleware
let store = createStore Reducer.reduceTodos initialStoreState (Some middleware)

let provider = createProvider store (R.fn createApp (obj()) [])

ReactDom.render(provider, Browser.window.document.getElementById "content") |> ignore

Msg.ShowError "Hello world!" |> store.dispatch
