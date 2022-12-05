module CustomRouter

open Shared
open Feliz
open Feliz.Bulma
open Feliz.Router
open Feliz.PigeonMaps
open Feliz.Popover

let navBrand =
    Bulma.navbarBrand.div [
        Bulma.navbarItem.a [
            prop.children [
                Html.img [
                    prop.src "https://www.expereo.one/ca433dcd9c59c6ffd94be45fbc58fc0c.svg"
                    prop.alt "Logo"
                ]
            ]
        ]
    ]

let pigeonMap (model: Model) = PigeonMaps.map [
    map.center(50.879, 4.6997)
    map.minZoom 3
    map.zoom 3
    map.maxZoom 10
    map.markers [
        for site in model.Sites do
            PigeonMaps.marker [
                marker.anchor(site.Location.Latitude, site.Location.Longitude)
                marker.render (fun marker -> [
                    Popover.popover [
                        popover.body [
                            Html.div [
                                prop.text site.Name
                                prop.style [
                                    style.backgroundColor.black
                                    style.padding 10
                                    style.borderRadius 5
                                    style.opacity 0.7
                                    style.color.lightGray
                                ]
                            ]
                        ]
                        popover.isOpen marker.hovered
                        popover.disableTip
                        popover.children[
                            Html.i [
                                prop.className [ "fa"; "fa-map-marker"; "fa-2x" ]
                            ]
                        ]
                    ]
                ])
            ]
    ]
]

[<ReactComponent>]
let CustomRouter ( model: Model ) =
    let (pageUrl, updateUrl) = React.useState(Route.parseUrl(Router.currentUrl()))

    React.router [
        router.onUrlChanged (Route.parseUrl >> updateUrl)
        router.children [
            Bulma.hero [
                hero.isFullHeight
                prop.style [
                    style.overflow.hidden
                ]
                prop.children [
                    Bulma.heroHead [
                        prop.style [
                            style.borderBottomWidth 1
                            style.borderBottomColor "#dbdbdb"
                            style.borderBottomStyle borderStyle.solid
                        ]
                        prop.children [
                            Bulma.navbar [
                                Bulma.container [
                                    navBrand
                                    match pageUrl with
                                    | Sites -> Html.h1 "Sites"
                                    | Service serviceId -> Html.h1 (sprintf "Sites %s" serviceId)
                                    | NotFound -> Html.h1 "404"
                                ]
                            ]
                        ]
                    ]
                    Bulma.heroBody [
                        prop.style [
                            style.height (length.calc "100vh - 3.25rem - 1px")
                            style.margin 0
                            style.padding 0
                        ]
                        prop.children [
                            match pageUrl with
                            | Sites -> pigeonMap model
                            | Service serviceId -> Html.h1 (sprintf "Service ID %s" serviceId)
                            | NotFound -> Html.h1 "Not Found!"
                        ]
                    ]
                ]
            ]
        ]
    ]