module Types

open Fable.Core

type [<Fable.Core.Pojo>] TodoItem = {
    id : int
    completed : bool
    description : string
}

[<Fable.Core.StringEnum>]
type Visibility =
    | All
    | Active
    | Completed

type [<Fable.Core.Pojo>] Msg =
    | InitList of TodoItem list
    | AddTodo of TodoItem
    | DeleteTodo of int
    | ToggleTodo of int * bool
    | SetVisibility of Visibility
    | ShowError of string
    | HideError
    interface Fable.Import.ReactRedux.IDispatchable

type [<Fable.Core.Pojo>] ApplicationState = {
    todos : TodoItem list
    visibilityFilter : Visibility
    error : string option
}