module Index

open Elmish
open Fable.Remoting.Client
open Shared
open Feliz.Router
open CustomRouter

type Msg =
    | GotSites of Site list

let sitesApi =
    Remoting.createApi ()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.buildProxy<ISitesApi>

let init () : Model * Cmd<Msg> =
    let model = { Sites = []; }

    let cmd = Cmd.OfAsync.perform sitesApi.getSites () GotSites

    model, cmd

let update msg model : Model * Cmd<Msg> =
    match msg with
    | GotSites sites -> { model with Sites = sites }, Cmd.none

let view model dispatch =
    CustomRouter model