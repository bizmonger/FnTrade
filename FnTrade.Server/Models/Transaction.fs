namespace FnTrade.Server.Models

open Newtonsoft.Json

[<CLIMutable>]
type Shares = { 
    AccountId : string
    Symbol    : string
    Qty       : int 
}