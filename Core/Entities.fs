namespace Core

[<AutoOpen>]
module Entities =

    type StockInfo = { 
        Symbol  : string
        Price   : decimal
        DayLow  : decimal
        DayHigh : decimal
    }
    
    type Shares = { 
        AccountId : string
        Symbol    : string
        Qty       : int 
    }

    type Account = { AccountId:string; Shares:Shares seq }
    
    type SharesInfo = {
        Shares        : Shares
        PricePerShare : decimal
        Total         : decimal
        Balance       : decimal
    }
    
    type RequestInfo = { 
        AccountId : string
        Symbol    : string
        Quantity  : int 
    }
    
    type InsufficientFunds = {
        PurchaseAttempt : RequestInfo
        Balance         : decimal
        StockPrice      : decimal
    }    
    
    type PurchaseResult =
        | PurchaseRequested of RequestInfo
        | UnknownSymbol     of RequestInfo
        | InvalidQuantity   of RequestInfo
        | InsufficientFunds of InsufficientFunds
    
    type SellResult =
        | SellRequested        of RequestInfo
        | InsufficientQuantity of RequestInfo