module VimCommandParserLib

open FParsec
open VimAbstractSyntaxTree

let lineDownMovementParser = stringReturn "j" MLineDown
let lineUpMovementParser = stringReturn "k" MLineUp
let charLeftMovementParser = stringReturn "h" MCharacterLeft
let charRightMovementParser = stringReturn "l" MCharacterRight
let wordMovementParser = stringReturn "W" MWord
let tillMovementParser = stringReturn "t" MTill 

let movementsParser =
    lineDownMovementParser
    <|> lineUpMovementParser
    <|> charLeftMovementParser
    <|> charRightMovementParser
    <|> wordMovementParser
    <|> tillMovementParser

let buildCommandName (command) = {Command.Repeater = 0; Command.Name = command; Command.Movement = MNone; Command.Argument = "" }
let buildCommandMovement (movement) = {Command.Repeater = 0; Command.Name = CMove; Command.Movement = movement; Command.Argument = "" }
let buildCommandCountName (count, command) = {Command.Repeater = count; Command.Name = command; Command.Movement = MNone; Command.Argument = "" }
let buildCommandCommandMovement (command, movement) = {Command.Repeater = 0; Command.Name = command; Command.Movement = movement; Command.Argument = "" }
let buildCommandCommandMovementArgument ((command, movement), arg) = {Command.Repeater = 0; Command.Name = command; Command.Movement = movement; Command.Argument = arg }

let moveCommandParser = movementsParser |>> buildCommandMovement
let deleteLineCommandParser = pint32 .>>. stringReturn "dd" CDeleteLine |>> buildCommandCountName
//let deleteCommandParser = stringReturn "d" CDelete .>>. movementsParser .>>. pchar  |>> buildCommandCommandMovementArgument // TODO - temp
let yankCommandParser = stringReturn "y" CYank .>>. movementsParser |>> buildCommandCommandMovement 
let quitCommandParser = stringReturn "ZZ" CQuit |>> buildCommandName

let commandParser = 
    moveCommandParser
    <|> deleteLineCommandParser 
    //<|> deleteCommandParser 
    <|> yankCommandParser 
    <|> quitCommandParser

let commandsParser = many commandParser
// 
// Runners
//

let test p str =
    match run p str with
    | Success(result, _, _)   
        -> 
            printfn "Success: " 
            VimCommandPrinter.PrintCommands result
    | Failure(errorMsg, _, _) -> printfn "Failure: %s" errorMsg

let parseTestString _ =
    //test deleteLineCommand "2dd"
    //test deleteCommand "dtp"
    //test yankCommand "yW"
    test commandsParser "2ddyWjjlj2ddZZ"

        //2ddyWjjPldtpj2ddZZ
        //2dd yW jj P l dtp j 2dd ZZ

        // 1234567890


