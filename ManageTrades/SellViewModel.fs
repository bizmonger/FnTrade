namespace ManageTrades.ViewModels

open Core.IntegrationLogic

type SellViewModel() =

    inherit ViewModelBase()

    let mutable symbol = ""

    member this.Symbol
        with get() =      symbol 
        and  set(value) = symbol <- value
                          base.NotifyPropertyChanged(<@ this.Symbol @>)