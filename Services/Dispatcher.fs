namespace Services

open System.Diagnostics
open System

[<DebuggerNonUserCode>]
type Dispatcher() =

    let sellRequested = new Event<EventHandler<_>,_>()
    let buyRequested =  new Event<EventHandler<_>,_>()
    
    [<CLIEvent>]
    member this.SellRequested =  sellRequested.Publish
    member this.Sell accountId symbol = sellRequested.Trigger(this , symbol)

    [<CLIEvent>]
    member this.BuyRequested =  buyRequested.Publish
    member this.Buy accountId symbol = buyRequested.Trigger(this , symbol)