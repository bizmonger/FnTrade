namespace FnTrade.Server.Controllers
open System
open System.Collections.Generic
open System.Linq
open System.Net.Http
open System.Web.Http
open FnTrade.Server.Models

type TransactionController() =
    inherit ApiController()

    let values = [| { AccountId = "Bizmonger"; Symbol = "TSLA"; Qty = 5  }
                    { AccountId = "Bizmonger"; Symbol = "MSFT"; Qty = 10 }
                    { AccountId = "Scarface" ; Symbol = "ROK" ; Qty = 2  } |]

    member x.Get() =           values
    member x.Get (id:string) = 
        values |> Array.filter (fun x -> x.AccountId.ToLower() = id.ToLower())