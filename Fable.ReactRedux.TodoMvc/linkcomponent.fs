module LinkComponent

open Fable.Import

open Types

module R = Fable.Helpers.React

type LinkProps = {
    active : bool
    children : React.ReactElement<obj> list
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
let mapStateToProps state ownProps =
    let isActive = JS.Object.is(state.visibilityFilter, ownProps.filter)
    [
        "active" ==> unbox isActive
    ]

let mapDispatchToProps (dispatch : ReactRedux.Dispatcher) ownProps =
    [
        "onClick" ==> fun _ -> SetVisibility ownProps.filter |> dispatch
    ]

open Fable.Helpers.ReactRedux

let createFilterLink visibility children =
    let initial = {
        filter = visibility
        active = false
        onClick = fun () -> ()
        children = children
    }
    createConnector ()
    |> withStateMapperWithProps mapStateToProps
    |> withDispatchMapperWithProps mapDispatchToProps
    |> buildFunction createLink initial []
