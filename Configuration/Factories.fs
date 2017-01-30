module Integration.Factories

open Services
open TestAPI

let broker =     MockBroker()
let dispatcher = Dispatcher()
let accountId =  "Bizmonger"

(*Functions*)
let getDispatcher() = dispatcher
let getBroker() =     broker
let getAccountId() =  accountId