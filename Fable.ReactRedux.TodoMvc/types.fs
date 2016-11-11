module Types

type EmptyProps() = class end
type EmptyCtx() = class end

type TodoItem = {
    id : int
    completed : bool
    description : string
}

[<Fable.Core.StringEnum>]
type Visibility =
    | All
    | Active
    | Completed

type Msg =
    | InitList of TodoItem list
    | AddTodo of TodoItem
    | DeleteTodo of int
    | ToggleTodo of int * bool
    | SetVisibility of Visibility
    | ShowError of string
    | HideError
    interface Fable.Import.ReactRedux.IDispatchable

type ApplicationState = {
    todos : TodoItem list
    visibilityFilter : Visibility
    error : string option
}