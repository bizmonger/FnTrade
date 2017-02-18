module Integration.Factories

open Services
open TestAPIImpl

let dispatcher = Dispatcher()
let accountId =  "Bizmonger"

(*Functions*)
let getDispatcher() = dispatcher
let getAccountId() =  accountId