namespace ManageTrades.ViewModels

open Core.IntegrationLogic
open Core.Entities
open Services
open Integration.Factories

type RequestSellConfirmedViewModel(request:RequestInfo) =

    inherit ViewModelBase()

    let broker = getBroker() :> IBroker
    let mutable total = 0m

    member this.Symbol   with get() = request.Symbol
    member this.Quantity with get() = request.Quantity
    member this.Total    with get() =      total
                         and  set(value) = total <- value
                                           base.NotifyPropertyChanged(<@ this.Total @>)
    member this.Load() =
        match broker.GetInfo request.Symbol with
        | Some info -> this.Total <- info.Price * (decimal) request.Quantity
        | None      -> failwith (sprintf "Failed to retrieve stock information for %s" this.Symbol)