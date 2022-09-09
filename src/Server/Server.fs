module Server

open Fable.Remoting.Server
open Fable.Remoting.Giraffe
open Saturn

open Shared

module Storage =
    let sites = ResizeArray()

    let addSite (site: Site) =
        sites.Add site
        Ok()

    do
        addSite (Site.create("Honeywell - Adelaide", "SIT-01558669", 138.561056, -34.9428368)) |> ignore
        addSite (Site.create("Honeywell - Al Hidd", "SIT-01560410", 50.652032, 26.2340617)) |> ignore
        addSite (Site.create("Honeywell - Al Hidd", "SIT-01558697", 50.652032, 26.2340617)) |> ignore
        addSite (Site.create("Honeywell - Albuquerque", "SIT-01560391", -106.586813, 35.191443)) |> ignore
        addSite (Site.create("Honeywell - Almaty - 3", "SIT-01568704", 76.90961, 43.22314)) |> ignore
        addSite (Site.create("Honeywell - Amman", "SIT-01568721", 35.8606583, 31.9752646)) |> ignore
        addSite (Site.create("Honeywell - Amsterdam", "SIT-01568672", 4.8124255, 52.309918)) |> ignore
        addSite (Site.create("Honeywell - Apodaca", "SIT-01566439", -100.13313, 25.78071)) |> ignore
        addSite (Site.create("Honeywell - Apodaca - 3", "SIT-01568657", -100.1858743, 25.776468)) |> ignore
        addSite (Site.create("Honeywell - Assago", "SIT-01558748", 9.1483902, 45.4033832)) |> ignore
        addSite (Site.create("Honeywell - Atlanta", "SIT-01568428", -84.3406066, 33.8307308)) |> ignore

let sitesApi =
    {
        getSites = fun () -> async { return Storage.sites |> List.ofSeq }
    }

let sitesWebApp =
    Remoting.createApi ()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.fromValue sitesApi
    |> Remoting.buildHttpHandler

let app =
    application {
        url "http://*:8085"
        use_router sitesWebApp
        memory_cache
        use_static "public"
        use_gzip
    }

[<EntryPoint>]
let main _ =
    run app
    0