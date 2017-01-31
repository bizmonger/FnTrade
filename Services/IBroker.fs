namespace Services

open Core.Entities

type IBroker =

    abstract member GetInfo       : string -> StockInfo option
    abstract member TryPurchase   : Shares -> decimal -> PurchaseResult
    abstract member TrySell       : Shares -> SellResult
    abstract member InvestmentsOf : string -> SharesInfo seq