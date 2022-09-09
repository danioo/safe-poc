namespace Shared

type Location = {
    Longitude: double;
    Latitude: double;
}

type Site = {
    Name: string;
    Id: string;
    Location: Location;
}

type Model = { Sites: Site list; }

type Page =
    | Sites
    | Service of id:string
    | NotFound

module Site =
    let create (name: string, id: string, longitude: double, latitude: double) =
        {
            Id = id
            Name = name
            Location = {
                Longitude = longitude
                Latitude = latitude
            }
        }

module Route =
    let builder typeName methodName =
        sprintf "/api/%s/%s" typeName methodName

    let parseUrl = function
        | [ ] -> Page.Sites
        | [ "service"; serviceId ] -> Page.Service serviceId
        | _ -> Page.NotFound

type ISitesApi = {
    getSites: unit -> Async<Site list>
}