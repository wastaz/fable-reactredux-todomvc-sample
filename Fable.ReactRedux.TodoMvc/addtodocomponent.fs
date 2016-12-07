module AddTodoComponent

open Fable.Import
open Types

module R = Fable.Helpers.React
open R.Props

type [<Fable.Core.Pojo>] AddTodoProps = {
    onAddTodo : string -> unit
}

let private defaultProps = {
    onAddTodo = fun _ -> ()
}

type private AddTodo (props, ctx) =
    inherit React.Component<AddTodoProps, obj>(props)

    let mutable (inputElement : Fable.Import.Browser.Element option) = None

    member x.onSubmit (e : React.FormEvent) =
        match inputElement with 
        | Some(iel) ->
            let el = unbox<Browser.HTMLInputElement> iel
            match el.value with
            | null -> ()
            | s ->
                let value = s.Trim()
                if value.Length > 0 then
                    props.onAddTodo value
                el.value <- ""
        | _ -> ()
        e.preventDefault()

    member x.render () =
        R.div [] [
            R.form [ OnSubmit x.onSubmit ] [
                R.input [ Ref (fun el -> inputElement <- unbox<Fable.Import.Browser.Element> el |> Some) ] []
                R.button [ Type "submit" ] [ unbox "Add Todo" ]
            ]
        ]


open Fable.Core.JsInterop

open Fable.Helpers.ReactRedux
open Fable.Helpers.ReduxThunk

let private mapDispatchToProps (dispatch : ReactRedux.Dispatcher) =
    [
        "onAddTodo" ==> fun desc -> dispatch <| asThunk (Backend.addTodo desc)
    ]

let createAddTodoComponent () =
    createConnector ()
    |> withDispatchMapper mapDispatchToProps
    |> buildComponent<AddTodo, _, _, _> defaultProps []
    