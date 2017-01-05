module TodoListComponent

open Fable.Core.JsInterop
module RR = Fable.Helpers.ReactRedux
open Fable.Import

open Types

module R = Fable.Helpers.React

type [<Fable.Core.Pojo>] TodoListProps = {
    todos : TodoItem list
    error : string option
    initTodoList : unit -> unit
    onToggleTodo : TodoItem -> unit
    onDeleteTodo : int -> unit
}

open TodoComponent

type private TodoList (props, ctx) =
    inherit React.Component<TodoListProps, obj> (props)
    
    member x.componentDidMount () =
        props.initTodoList ()

    member x.render () =
        let todoList = 
            props.todos
            |> List.map (fun t -> 
                { delete = fun _ -> props.onDeleteTodo t.id
                  toggle = fun _ -> props.onToggleTodo t
                  completed = t.completed
                  text = t.description
                })
            |> List.map (fun t -> R.fn TodoComponent.createTodo t [])
            |> R.ul []
            |> Some
        let errorElement =
            props.error
            |> Option.map (fun e -> R.div [ R.Props.Style [ R.Props.CSSProp.Color "red" ] ] [ R.str e ])
        R.div [] <| List.choose id [ todoList; errorElement ]


let private getVisibleTodos (todos : TodoItem list) visibility =
    todos
    |> List.filter (fun t ->
        match visibility with
        | All -> true
        | Active -> not t.completed
        | Completed -> t.completed
    )

let private mapStateToProps (state : ApplicationState) (ownprops : TodoListProps) =
    { ownprops with
        error = state.error
        todos = getVisibleTodos state.todos state.visibilityFilter
    }
    
open Fable.Helpers.ReactRedux
open Fable.Helpers.ReduxThunk

let private mapDispatchToProps (dispatch : ReactRedux.Dispatcher) ownprops =
    { ownprops with
        initTodoList = fun () -> Backend.getAllTodos |> asThunk |> dispatch
        onToggleTodo = fun todo -> Backend.updateTodo { todo with completed = not todo.completed } |> asThunk |> dispatch
        onDeleteTodo = fun id -> Backend.deleteTodo id |> asThunk |> dispatch
    }

let private setDefaultProps (ownprops : TodoListProps) =
    { ownprops with
        todos = []
        error = None
    }

let createTodoList =
    createConnector ()
    |> withStateMapper mapStateToProps
    |> withDispatchMapper mapDispatchToProps
    |> withProps setDefaultProps
    |> buildComponent<TodoList, _, _, _>
