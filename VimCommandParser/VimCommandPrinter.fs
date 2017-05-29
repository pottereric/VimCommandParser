module VimCommandPrinter
open VimAbstractSyntaxTree

let PrintMovement m =
    match m with
    | MLineDown -> "down"
    | MLineUp -> "up"
    | MCharacterLeft -> "left"
    | MCharacterRight -> "right"
    | MTill -> "until"
    | MWord -> "entire_word"
    | _ -> "unknown_movment"

let PrintCommand c =
    match c with
    | { Name = CQuit} -> printfn "Quit Vim"
    | { Name = CMove} -> printfn "Move %s " (PrintMovement c.Movement) 
    | { Name = CYank} -> printfn "Yank %s %s" (PrintMovement c.Movement) c.Argument
    | { Name = CDeleteLine} -> printfn "Delete Line %d times" c.Repeater
    | _ -> printfn "%d %s %s %s" c.Repeater "unknown" "unknown"  c.Argument

let PrintCommands (commands : Command list) =
    List.iter (fun c -> PrintCommand c)  commands
