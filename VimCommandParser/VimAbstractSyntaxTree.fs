module VimAbstractSyntaxTree


type MovementName = MNone
                    | MLineDown
                    | MLineUp
                    | MCharacterLeft
                    | MCharacterRight
                    | MWord
                    | MTill
                    | MEnd

type CommandName =  CMove
                    |CDeleteLine
                    | CYank
                    | CDelete
                    | CQuit
                    //| SDone
                    //| SAddedPlanned
                    //| SAddedInProgress
                    //| SAddedDone
                    //| SRemoved


type Command = 
    { Repeater : int32
      Name : CommandName
      Movement : MovementName
      Argument : string
    }

