module LinkComponent

open Fable.Import

open Types

module R = Fable.Helpers.React

type [<Fable.Core.Pojo>] LinkProps = {
    active : bool
    children : React.ReactElement list
    onClick : unit -> unit
    filter : Visibility
}

open R.Props

let createLink props =
    let onClick = 
        fun (e : React.MouseEvent) ->
            e.preventDefault()
            props.onClick()
    if props.active then
        R.span [] props.children
    else 
        R.a [ Href "#"; OnClick onClick ]  props.children

open Fable.Core.JsInterop

let mapStateToProps state ownprops =
    { ownprops with
        active = JS.Object.is(state.visibilityFilter, ownprops.filter)
    }

let mapDispatchToProps (dispatch : ReactRedux.Dispatcher) ownprops =
    { ownprops with
        onClick = fun _ -> SetVisibility ownprops.filter |> dispatch
    }

let setDefaultProps visibility children ownprops =
    { ownprops with
        filter = visibility
        children = children
    }

open Fable.Helpers.ReactRedux

let createFilterLink visibility children =
    let props = {
        active = false
        children = children
        filter = visibility
        onClick = fun () -> ()
    }
    createConnector ()
    |> withStateMapper mapStateToProps
    |> withDispatchMapper mapDispatchToProps
    |> withProps (setDefaultProps visibility children)
    |> buildFunction createLink
