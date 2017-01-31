namespace Home.ViewModels

open System.Windows.Input
open Core.IntegrationLogic
open Core.Entities
open Integration.Factories
open Services
open Search
open System.Collections.ObjectModel

type HomeViewModel() as this =

    inherit ViewModelBase()

    let dispatcher = getDispatcher()
    let accountId =  getAccountId()
    let broker =     getBroker() :> IBroker

    let searchCommand = 
        DelegateCommand( (fun _ -> this.StockInfo <- getQuote broker this.Symbol) ,
                          fun _ -> true) :> ICommand
    let sellCommand =
        DelegateCommand( (fun o -> dispatcher.Sell (o :?> SharesInfo) ) ,
                          fun _ -> true) :> ICommand
    let buyCommand = 
        DelegateCommand( (fun o -> dispatcher.Buy  { Owner.AccountId=accountId ; Symbol=o :?> string} ) ,
                          fun _ -> true) :> ICommand

    let mutable symbol = ""
    let mutable stockInfo = None
    let mutable investments = ObservableCollection<SharesInfo>()

    member this.Load() =
        let result = broker.InvestmentsOf accountId
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