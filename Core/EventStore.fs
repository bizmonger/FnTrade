namespace Core

[<AutoOpen>]
module EventStore =

    type Events =
        | BuyRequested  of RequestInfo
        | SellRequested of RequestInfo

    let store aggregate event events = 
        events@[aggregate,event]