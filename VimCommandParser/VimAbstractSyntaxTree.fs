module VimAbstractSyntaxTree


type MovementName = MNone
                    | MWord
                    | MTill
                    | MEnd

type CommandName = CDeleteLine
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

