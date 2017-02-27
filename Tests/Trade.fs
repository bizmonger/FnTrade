module Testing

open System
open System.Net.Http
open System.Net.Http.Headers
open NUnit.Framework
open FsUnit
open FSharp.Data
open Xunit
open Newtonsoft.Json

type Share = { 
    AccountId : string
    Symbol    : string
    Quantity       : int 
}

[<Test>]
let ``WebAPI call for everything`` () =
    
    let client = new HttpClient()
    client.BaseAddress <- Uri("http://localhost:48213/");
    client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue("application/json"));

    let response = client.GetAsync("api/transaction").Result;
    if response.IsSuccessStatusCode
    then let result = response.Content.ReadAsStringAsync().Result
         let shares = JsonConvert.DeserializeObject<seq<Share>>(result);
         ()
    else failwith "Something bad happened"

[<Test>]
let ``WebAPI call with argument`` () =
    
    let client = new HttpClient()
    client.BaseAddress <- Uri("http://localhost:48213/");
    client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue("application/json"));

    let response = client.GetAsync("api/transaction/bizmonger").Result; // Appended argument
    if response.IsSuccessStatusCode
    then let result = response.Content.ReadAsStringAsync().Result
         let shares = JsonConvert.DeserializeObject<seq<Share>>(result);
         ()
    else failwith "Something bad happened"

[<Literal>] 
let Dev_AllTransactions = "http://localhost:48213/api/transaction"

[<Literal>] 
let RuntimeUri =          "http://production.mycompany.com/api/transaction"
    
type Repository = JsonProvider<Dev_AllTransactions>

[<Test>]
let ``Type Provider call for read-all`` () =

    Repository.Load "http://localhost:48213/api/transaction"
    |> Array.toList
    |> List.map(fun x -> { AccountId= x.AccountId
                           Symbol=    x.Symbol
                           Quantity=       x.Qty })

    |> should equal [{ AccountId = "Bizmonger"; Symbol = "TSLA"; Quantity = 5  }
                     { AccountId = "Bizmonger"; Symbol = "MSFT"; Quantity = 10 }
                     { AccountId = "Scarface" ; Symbol = "ROK" ; Quantity = 2  }]
[<Test>]
let ``Type Provider call with argument`` () =

    let accountId = "bizmonger"
    Repository.Load (sprintf "%s%s" "http://localhost:48213/api/transaction/" accountId)
    |> Array.toList
    |> List.map(fun x -> { AccountId= x.AccountId
                           Symbol=    x.Symbol
                           Quantity=  x.Qty })

    |> should equal <| [{ AccountId = "Bizmonger"; Symbol = "TSLA"; Quantity = 5  }
                        { AccountId = "Bizmonger"; Symbol = "MSFT"; Quantity = 10 }]

[<Test>]
let ``WebAPI post`` () =

    let entity = { AccountId = "Bizmonger"; Symbol = "TSLA"; Quantity = 5 }
    let json = JsonConvert.SerializeObject(entity)
    let content = new StringContent(json)
    content.Headers.ContentType <- new MediaTypeHeaderValue("application/json")
    
    let client = new HttpClient()
    client.BaseAddress <- Uri("http://localhost:48213/api/");
    let response = client.PostAsync("Transaction", content).Result;
    
    response.IsSuccessStatusCode |> should equal true