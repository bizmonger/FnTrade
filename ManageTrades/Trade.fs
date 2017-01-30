module ManageTradeImpl

open Services
open Core.Entities
open TestAPI

let buyShares (service:IBroker) context balance =
    (context , balance) ||> service.TryPurchase

let sellShares (service:IBroker) context =
    context |> service.TrySell

(*Test*)
let context = { AccountId= "Bizmonger"
                Symbol=    "ROK"
                Qty=        100 }

let result = buyShares (MockBroker()) context 5000m