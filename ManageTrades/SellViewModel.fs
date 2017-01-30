namespace ManageTrades.ViewModels

open Core.IntegrationLogic
open Core.Entities

type SellViewModel(owner:Owner) =

    inherit ViewModelBase()

    member this.Symbol with get() = owner.Symbol