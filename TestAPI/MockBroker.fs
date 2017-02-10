module TestAPIImpl

open Services
open Core.Entities
    
let getInfo = function
    | "MSFT" -> Some { Symbol="MSFT" ; Price=100m ; DayLow=95m  ; DayHigh=105m }
    | "TSLA" -> Some { Symbol="TSLA" ; Price=200m ; DayLow=195m ; DayHigh=205m }
    | "ROK"  -> Some { Symbol="ROK"  ; Price=300m ; DayLow=295m ; DayHigh=305m }
    | _      -> None
    
let tryPurchase context balance =
    let request = { AccountId=context.AccountId 
                    RequestInfo.Symbol=context.Symbol
                    RequestInfo.Quantity=context.Quantity }
        
    match getInfo context.Symbol with
    | None -> UnknownSymbol request
    | Some stockInfo ->
        
        if  context.Quantity <= 0 then 
            InvalidQuantity request
        
        elif balance <= stockInfo.Price * (decimal)context.Quantity then 
            InsufficientFunds { PurchaseAttempt=request
                                Balance=balance
                                StockPrice=stockInfo.Price }
        
        else PurchaseRequested request
        
let trySell context =
    SellRequested { AccountId= context.AccountId
                    Symbol=    context.Symbol
                    Quantity=  context.Quantity }
    
let investmentsOf accountId =
    let ownedMSFT =  { AccountId=accountId ; Symbol="MSFT" ;  Qty=100 }
    let ownedROK  =  { AccountId=accountId ; Symbol="ROK"  ;  Qty=200 }
    let ownedTSLA  = { AccountId=accountId ; Symbol="TSLA" ;  Qty=200 }
        
    seq [
            { Shares=ownedMSFT ; PricePerShare=(getInfo "MSFT").Value.Price ; Total=(getInfo "MSFT").Value.Price * (decimal)ownedMSFT.Qty; Balance=20000m }
            { Shares=ownedROK  ; PricePerShare=(getInfo "ROK" ).Value.Price ; Total=(getInfo "ROK" ).Value.Price * (decimal)ownedROK.Qty ; Balance=20000m }
            { Shares=ownedTSLA ; PricePerShare=(getInfo "TSLA").Value.Price ; Total=(getInfo "TSLA").Value.Price * (decimal)ownedTSLA.Qty; Balance=20000m }
        ]