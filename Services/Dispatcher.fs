namespace Services

open System.Diagnostics
open System

type Dispatcher() =

    let sellRequested = new Event<EventHandler<_>,_>()
    let buyRequested =  new Event<EventHandler<_>,_>()
    let confirmSellRequested = new Event<EventHandler<_>,_>()
    
    [<CLIEvent>]
    member this.SellRequested = sellRequested.Publish
    member this.Sell owner = sellRequested.Trigger(this , owner)

    [<CLIEvent>]
    member this.BuyRequested = buyRequested.Publish
    member this.Buy owner = buyRequested.Trigger(this , owner)

    [<CLIEvent>]
    member this.ConfirmSellRequested = confirmSellRequested.Publish
    member this.ConfirmSell info = buyRequested.Trigger(this , info)