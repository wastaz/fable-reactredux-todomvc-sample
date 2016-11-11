module Backend

open Fable.Core
open Fable.Import
open Fable.Helpers.Fetch

open Types

type private TodoDto = {
    description : string
    completed : bool
}

[<Measure>]
type ms

[<Literal>]
let private baseurl = "http://localhost:3000/"

let private defaultErrorTimeout = Some 2500<ms>

let private jsonHeaders = 
    [ HttpRequestHeaders.Accept "application/json"
      HttpRequestHeaders.ContentType "application/json"
    ]

let toUrl id =
    match id with
    | Some(i) -> sprintf "%stodo/%i" baseurl i
    | None -> sprintf "%stodo" baseurl

let private dispatchShowError (dispatch : ReactRedux.Dispatcher) (time : int<ms> option) error =
    Msg.ShowError error |> dispatch
    time |> Option.iter (fun t -> 
        Browser.window.setTimeout(box (fun () -> Msg.HideError |> dispatch), t) 
        |> ignore
    )

let addTodo desc (dispatch : ReactRedux.Dispatcher) =
    async {
        let! response =
            postRecord(
                toUrl None, 
                { description = desc; completed = false }, 
                [ RequestProperties.Headers jsonHeaders ])
        if response.Ok then
            let! obj = response.json<TodoItem> () |> Microsoft.FSharp.Control.Async.AwaitPromise
            Msg.AddTodo obj |> dispatch 
        else
            dispatchShowError dispatch defaultErrorTimeout "Could not add todo item!" 
    } 
    |> Async.StartAsPromise

let deleteTodo id (dispatch : ReactRedux.Dispatcher) =
    async {
        let! response = 
            fetchAsync(
                toUrl <| Some id, 
                [ RequestProperties.Method HttpMethod.DELETE ]) 
        if response.Ok then
            Msg.DeleteTodo id |> dispatch
        else
            dispatchShowError dispatch defaultErrorTimeout "Could not delete todo item!"
    } 
    |> Async.StartAsPromise

let updateTodo todo (dispatch : ReactRedux.Dispatcher) =
    async {
        let! response = 
            fetchAsync(
                toUrl <| Some todo.id,
                [ RequestProperties.Method HttpMethod.PUT 
                  RequestProperties.Body (unbox (Fable.Core.JsInterop.toJson todo))
                  RequestProperties.Headers jsonHeaders ])
        if response.Ok then
            Msg.ToggleTodo(todo.id, todo.completed) |> dispatch
        else
            dispatchShowError dispatch defaultErrorTimeout "Could not toggle todo state!"
    } 
    |> Async.StartAsPromise

let getAllTodos (dispatch : ReactRedux.Dispatcher) =
    async {
        
        let! response =
            fetchAsync(
                toUrl None,
                [ RequestProperties.Headers jsonHeaders ])
        if response.Ok then
            let! todos = response.json<TodoItem array> () |> Microsoft.FSharp.Control.Async.AwaitPromise
            todos |> Array.toList |> Msg.InitList |> dispatch
        else
            dispatchShowError dispatch None "Could not fetch toods from server!"
    } |> Async.StartAsPromise