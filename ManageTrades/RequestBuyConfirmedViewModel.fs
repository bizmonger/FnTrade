namespace ManageTrades.ViewModels

open Core.IntegrationLogic
open Core.Entities
open Services
open Integration.Factories
open TestAPIImpl
open System.Net.Http
open System
open System.Net.Http.Headers

type RequestBuyConfirmedViewModel(request:RequestInfo) =

    inherit ViewModelBase()

    let mutable total = 0m

    member this.Symbol   with get() = request.Symbol
    member this.Quantity with get() = request.Quantity
    member this.Total    with get() =      total
                         and  set(value) = total <- value
                                           base.NotifyPropertyChanged(<@ this.Total @>)
    member this.Load() =
//        let client = new HttpClient()
//        client.BaseAddress <- Uri("http://localhost:48213/api/transaction");
//        client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue("application/json"));
//
//        let response = client.GetAsync("api/yourcustomobjects").Result;
//        if response.IsSuccessStatusCode
//        then let result = response.Content.ReadAsStringAsync().RunSynchronously
//             ()
//        else failwith "Something bad happened"

        getInfo request.Symbol |> function
        | Some info -> this.Total <- info.Price * (decimal) request.Quantity
        | None      -> failwith   <| sprintf "Failed to retrieve stock information for %s" this.Symbol