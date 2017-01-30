module Search

open Services
open Core.Entities
open TestAPI

(*Functions*)
let getQuote (service:IBroker) symbol = 
    service.GetInfo symbol