module VimCommandParserLib

open FParsec
open VimAbstractSyntaxTree


let buildCommandName (command) = {Command.Repeater = 0; Command.Name = command; Command.Movement = MNone; Command.Argument = "" }
let buildCommandCountName (count, command) = {Command.Repeater = count; Command.Name = command; Command.Movement = MNone; Command.Argument = "" }
let buildCommandCommandMovement (command, movement) = {Command.Repeater = 0; Command.Name = command; Command.Movement = movement; Command.Argument = "" }
let buildCommandCommandMovementArgument ((command, movement), arg) = {Command.Repeater = 0; Command.Name = command; Command.Movement = movement; Command.Argument = arg }


let deleteLineCommand = pint32 .>>. stringReturn "dd" CDeleteLine |>> buildCommandCountName
let deleteCommand = stringReturn "d" CDelete .>>. stringReturn "t" MTill .>>. pstring "p" |>> buildCommandCommandMovementArgument // TODO - temp
let yankCommand = stringReturn "y" CYank .>>. stringReturn "W" MWord |>> buildCommandCommandMovement // TODO - temp
let quitCommand = stringReturn "ZZ" CQuit |>> buildCommandName

let command = 
    deleteLineCommand <|> deleteCommand <|> yankCommand <|> quitCommand

let commands = many command
// 
// Runners
//

let test p str =
    match run p str with
    | Success(result, _, _)   -> printfn "Success: %A" result
    | Failure(errorMsg, _, _) -> printfn "Failure: %s" errorMsg

let parseTestString _ =
    //test deleteLineCommand "2dd"
    //test deleteCommand "dtp"
    //test yankCommand "yW"
    test commands "2ddyWZZ"

        //2ddyWjjPldtpj2ddZZ
        //2dd yW jj P l dtp j 2dd ZZ

        // 1234567890


