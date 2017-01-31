namespace TestAPI

open Services
open Core.Entities

type MockBroker() =

    interface IBroker with

        member this.GetInfo symbol =
            match symbol with
            | "MSFT" -> Some { Symbol=symbol ; Price=100m ; DayLow=95m  ; DayHigh=105m }
            | "TSLA" -> Some { Symbol=symbol ; Price=200m ; DayLow=195m ; DayHigh=205m }
            | "ROK"  -> Some { Symbol=symbol ; Price=300m ; DayLow=295m ; DayHigh=305m }
            | _      -> None

        member this.TryPurchase context balance =
            let request = { AccountId=context.AccountId 
                            RequestInfo.Symbol=context.Symbol
                            RequestInfo.Quantity=context.Qty }

            match (this :> IBroker).GetInfo context.Symbol with
            | None -> UnknownSymbol request
            | Some stockInfo ->

                if  context.Qty <= 0 then 
                    InvalidQuantity request

                elif balance <= stockInfo.Price * (decimal)context.Qty then 
                    InsufficientFunds { PurchaseAttempt=request
                                        Balance=balance
                                        StockPrice=stockInfo.Price }

                else PurchaseRequested request
            
        member this.TrySell context =
            SellRequested { AccountId=       context.AccountId
                            Symbol=   context.Symbol
                            Quantity= context.Qty }

        member this.InvestmentsOf accountId =

            let ownedMSFT =  { AccountId=accountId ; Symbol="MSFT" ;  Qty=100 }
            let ownedROK  =  { AccountId=accountId ; Symbol="ROK"  ;  Qty=200 }
            let ownedTSLA  = { AccountId=accountId ; Symbol="TSLA" ;  Qty=200 }

            seq [
                 { Shares=ownedMSFT ; PricePerShare=((this :> IBroker).GetInfo "MSFT").Value.Price ; Total=((this :> IBroker).GetInfo "MSFT").Value.Price * (decimal)ownedMSFT.Qty }
                 { Shares=ownedROK  ; PricePerShare=((this :> IBroker).GetInfo "ROK" ).Value.Price ; Total=((this :> IBroker).GetInfo "ROK" ).Value.Price * (decimal)ownedROK.Qty  }
                 { Shares=ownedTSLA ; PricePerShare=((this :> IBroker).GetInfo "TSLA").Value.Price ; Total=((this :> IBroker).GetInfo "TSLA").Value.Price * (decimal)ownedTSLA.Qty }
                ]