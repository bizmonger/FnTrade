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
        Quantity       : int 
    }

    type Account = { AccountId:string; Shares:Shares seq }
    
    type SharesInfo = {
        Shares             : Shares
        PricePerShare      : decimal
        Total              : decimal
        Balance            : decimal
        PendingTransaction : bool
    }
    
    type InsufficientFunds = {
        PurchaseAttempt : Shares
        Balance         : decimal
        StockPrice      : decimal
    }    
    
    type PurchaseResult =
        | PurchaseRequested of Shares
        | UnknownSymbol     of Shares
        | InvalidQuantity   of Shares
        | InsufficientFunds of InsufficientFunds
    
    type SellResult =
        | SellRequested        of Shares
        | InsufficientQuantity of Shares