namespace ManageTrades.ViewModels

open Core.IntegrationLogic

type SellViewModel(symbol) =

    inherit ViewModelBase()

    member this.Symbol with get() = symbol