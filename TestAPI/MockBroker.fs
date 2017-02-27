module TestAPIImpl

open Services
open Core.Entities
    
let getInfo = function
    | "MSFT" -> Some { Symbol="MSFT" ; Price=100m ; DayLow=95m  ; DayHigh=105m }
    | "TSLA" -> Some { Symbol="TSLA" ; Price=200m ; DayLow=195m ; DayHigh=205m }
    | "ROK"  -> Some { Symbol="ROK"  ; Price=300m ; DayLow=295m ; DayHigh=305m }
    | _      -> None
    
let tryPurchase context balance =
        
    match getInfo context.Symbol with
    | None -> UnknownSymbol context
    | Some stockInfo ->
        
        if  context.Quantity <= 0 then 
            InvalidQuantity context
        
        elif balance <= stockInfo.Price * (decimal)context.Quantity then 
            InsufficientFunds { PurchaseAttempt=context
                                Balance=balance
                                StockPrice=stockInfo.Price }
        
        else PurchaseRequested context
        
let trySell context = SellRequested context
    
let investmentsOf accountId =
    let ownedMSFT =  { AccountId=accountId ; Symbol="MSFT" ;  Quantity=100 }
    let ownedROK  =  { AccountId=accountId ; Symbol="ROK"  ;  Quantity=200 }
    let ownedTSLA =  { AccountId=accountId ; Symbol="TSLA" ;  Quantity=200 }
        
    seq [
            { Shares=ownedMSFT; PricePerShare=(getInfo "MSFT").Value.Price; Total=(getInfo "MSFT").Value.Price * (decimal)ownedMSFT.Quantity; Balance=20000m; PendingTransaction=false }
            { Shares=ownedROK ; PricePerShare=(getInfo "ROK" ).Value.Price; Total=(getInfo "ROK" ).Value.Price * (decimal)ownedROK.Quantity ; Balance=20000m; PendingTransaction=false }
            { Shares=ownedTSLA; PricePerShare=(getInfo "TSLA").Value.Price; Total=(getInfo "TSLA").Value.Price * (decimal)ownedTSLA.Quantity; Balance=20000m; PendingTransaction=false }
        ]