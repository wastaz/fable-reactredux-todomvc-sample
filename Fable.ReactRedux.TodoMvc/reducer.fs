module Reducer

open Types

let private toggleTodo todoId completedState todos =
    todos 
    |> List.map (fun t ->
        match t with
        | todo when todo.id = todoId -> { todo with completed = completedState }
        | _ -> t
    )

let reduceTodos (state : ApplicationState) (action : Msg) =
    match action with
    | InitList(todos) ->
        { state with todos = todos }
    | AddTodo(todo) -> 
        { state with todos = todo :: state.todos }
    | DeleteTodo(todoId) ->
        { state with todos = state.todos |> List.filter (fun t -> t.id <> todoId) }
    | ToggleTodo(todoId, completedState) -> 
        { state with todos = toggleTodo todoId completedState state.todos }
    | SetVisibility(visibility) ->
        { state with visibilityFilter = visibility }
    | ShowError(error) ->
        { state with error = Some error }
    | HideError ->
        { state with error = None }
        