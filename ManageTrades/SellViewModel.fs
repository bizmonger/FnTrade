namespace ManageTrades.ViewModels

open System
open System.Windows.Input
open Core.IntegrationLogic
open Core.Entities
open Integration.Factories
open Services

type SellViewModel(info:SharesInfo) as this =

    inherit ViewModelBase()

    let dispatcher = getDispatcher()
    let accountId =  getAccountId()
    let broker =     getBroker() :> IBroker

    let mutable sellQty = ""

    let confirm =
        DelegateCommand( (fun _ -> 

            if   Int32.Parse this.SellQty > 0 && Int32.Parse this.SellQty <= this.Shares
            then dispatcher.ConfirmSell { AccountId = accountId
                                          Symbol    = this.Symbol
                                          Quantity  = Int32.Parse this.SellQty } ) ,
                          fun _ -> true ) :> ICommand

    member this.Symbol     with get() = info.Shares.Symbol
    member this.Shares     with get() = info.Shares.Qty
    member this.StockPrice with get() = info.PricePerShare
    member this.Total      with get() = ((decimal)info.Shares.Qty * info.PricePerShare)

    member this.SellQty    with get() =      sellQty
                           and  set(value) = sellQty <- value

    member this.Confirm = confirm