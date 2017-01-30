namespace Core.IntegrationLogic

open System
open System.Windows.Input
open System.Diagnostics

[<DebuggerNonUserCode>]
type DelegateCommand(action : obj -> unit, canExecute : obj -> bool) = 

    let event = new DelegateEvent<EventHandler>()
    interface ICommand with
        
        [<CLIEvent>]
        member this.CanExecuteChanged = event.Publish

        member this.CanExecute arg = canExecute (arg)
        member this.Execute arg = action (arg)