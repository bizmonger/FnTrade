namespace ManageTrades.ViewModels

open System.Windows.Input
open Core.IntegrationLogic
open Core.Entities
open Integration.Factories
open Services

type SellViewModel(payload:SharesInfo) as this =

    inherit ViewModelBase()

    let dispatcher = getDispatcher()
    let accountId =  getAccountId()
    let broker =     getBroker() :> IBroker

    let mutable sellQty = 0

    let confirm =
        DelegateCommand( (fun _ -> 

            if   this.SellQty > 0 && this.SellQty <= this.Shares
            then dispatcher.ConfirmSell { AccountId = accountId
                                          Symbol    = this.Symbol
                                          Quantity  = this.SellQty } ) ,
                          fun _ -> true ) :> ICommand

    member this.Symbol     with get() = payload.Shares.Symbol
    member this.Shares     with get() = payload.Shares.Qty
    member this.StockPrice with get() = payload.PricePerShare
    member this.Total      with get() = (decimal)payload.Shares.Qty * payload.PricePerShare

    member this.SellQty    with get() =      sellQty
                           and  set(value) = sellQty <- value