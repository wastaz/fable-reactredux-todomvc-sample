module TodoComponent

open Fable.Helpers.React

module R = Fable.Helpers.React
open R.Props

type TodoProps = {
    toggle : unit -> unit
    delete : unit -> unit
    completed : bool
    text : string
}

let createTodo props = 
    R.li 
        [ Style [ (if props.completed then "line-through" else "none") |> box |> TextDecoration ] ] 
        [ 
            R.text [ OnClick <| fun _ -> props.toggle () ] [ unbox props.text ] 
            R.button [ OnClick <| fun _ -> props.delete () ] [ unbox "X" ]
        ]
