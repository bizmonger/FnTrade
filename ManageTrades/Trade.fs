module ManageTradeImpl

open Services
open Core.Entities
open TestAPIImpl

let buyShares purchaseFn context balance = (context , balance) ||> purchaseFn
let sellShares sellFn context =             context |> sellFn

(*Test*)
let context = { AccountId= "Bizmonger"
                Symbol=    "ROK"
                Quantity=        5 }

let result = buyShares tryPurchase context 5000m