module Integration.Factories

open Services
open TestAPI

(*Functions*)
let getDispatcher() = Dispatcher()
let getBroker() = MockBroker()
let getAccountId() = "Bizmonger"