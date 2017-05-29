// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open VimCommandParserLib

[<EntryPoint>]
let main argv = 
    //printfn "%A" argv

    //2ddyWjjPldtpj2ddZZ

    parseTestString()

    System.Console.ReadKey() |> ignore
    0 // return an integer exit code
