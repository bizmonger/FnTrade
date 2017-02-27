namespace Core

[<AutoOpen>]
module EventStore =

    type Events =
        | BuyRequested  of SharesInfo
        | SellRequested of SharesInfo

    let store aggregate event events = 
        events@[aggregate,event]