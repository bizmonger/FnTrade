namespace Home.ViewModels

open System.Collections.ObjectModel
open System.Collections.Generic
open System.Windows.Input
open Core.IntegrationLogic
open Core.Entities
open Integration.Factories
open Services
open TestAPIImpl
open System.Linq


type HomeViewModel() as this =

    inherit ViewModelBase()

    let dispatcher = getDispatcher()
    let accountId =  getAccountId()

    do dispatcher.ExecuteBuyRequested.Add <| fun o -> 
        let shares = o :?> Shares
        this.Investments |> Seq.toList
                         |> List.filter (fun s -> s.Shares.Symbol = shares.Symbol)
                         |> function 
                            | h::t -> let collection = (this.Investments :> Collection<SharesInfo>)
                                      collection.Remove(h) |> ignore
                                      this.Investments.Insert(0,
                                        { Shares             = shares
                                          PricePerShare      = getInfo shares.Symbol
                                                               |> function | Some info -> info.Price
                                                                           | None      -> 0.00m
                                          Total              = 0.00m
                                          Balance            = 0.00m
                                          PendingTransaction = true })
                            | _    -> () // IDK...
        

    let searchCommand =
        DelegateCommand( (fun _ -> this.StockInfo <- getInfo this.Symbol) ,
                          fun _ -> true) :> ICommand
    let sellCommand =
        DelegateCommand( (fun o -> dispatcher.Sell (o :?> SharesInfo) ) ,
                          fun _ -> true) :> ICommand
    let buyCommand = 
        DelegateCommand( (fun o -> dispatcher.Buy  (o :?> SharesInfo) ) ,
                          fun _ -> true) :> ICommand

    let mutable symbol = ""
    let mutable stockInfo = None
    let mutable investments = ObservableCollection<SharesInfo>()

    member this.Load() =
        let result = investmentsOf accountId
        this.Investments <- ObservableCollection<SharesInfo>(result)

    member this.Symbol
        with get() =      symbol 
        and  set(value) = symbol <- value
                          base.NotifyPropertyChanged(<@ this.Symbol @>)
    member this.StockInfo
        with get() =      stockInfo
        and  set(value) = stockInfo <- value
                          base.NotifyPropertyChanged(<@ this.StockInfo @>)
    member this.Investments
        with get() =      investments
        and  set(value) = investments <- value
                          base.NotifyPropertyChanged(<@ this.Investments @>)

    member this.Search = searchCommand
    member this.Sell =   sellCommand
    member this.Buy =    buyCommand