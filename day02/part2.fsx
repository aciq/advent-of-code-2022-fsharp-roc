open System
open System.IO

//1 : A for Rock, B for Paper, and C for Scissors
//2 : X for Rock, Y for Paper, and Z for Scissors

// score
// shape you selected (1 for Rock, 2 for Paper, and 3 for Scissors)
// outcome of the round (0 if you lost, 3 if the round was a draw, and 6 if you won)

type Outcome = Lose | Draw | Win
    with
    static member parse (c:char) = 
        match c with 
        | 'X' -> Lose
        | 'Y' -> Draw
        | 'Z' -> Win
        | _ -> failwith "invalid input"

type Play = Rock | Paper | Scissors
    with 
    static member parse (c:char) = 
        match c with 
        | 'A' | 'X' -> Rock
        | 'B' | 'Y' -> Paper
        | 'C' | 'Z' -> Scissors
        | _ -> failwith "invalid input"

    static member findPlay (theirs:Play) (outcome:Outcome) : Play =
        match theirs, outcome with 
        | Rock, Lose | Paper, Win | Scissors, Draw -> Scissors
        | Scissors, Lose | Rock, Win | Paper, Draw -> Paper
        | Paper, Lose | Scissors, Win | Rock, Draw -> Rock

    static member score (theirs:Play) (ours:Play) =
        match theirs, ours with 
        | Rock, Rock -> 3 + 1
        | Rock, Paper -> 6 + 2
        | Rock, Scissors -> 0 + 3
        
        | Paper, Rock -> 0 + 1
        | Paper, Paper -> 3 + 2
        | Paper, Scissors -> 6 + 3

        | Scissors, Rock -> 6 + 1
        | Scissors, Paper -> 0 + 2
        | Scissors, Scissors -> 3 + 3

__SOURCE_DIRECTORY__ + "/input"
|> File.ReadLines
|> Seq.where (String.IsNullOrWhiteSpace >> not)
|> Seq.map (fun line -> 
    let (theirs,outcome) = line[0] |> Play.parse, line[2] |> Outcome.parse
    let correctPlay = (theirs,outcome) ||> Play.findPlay
    (theirs,correctPlay) ||> Play.score
    )
|> Seq.sum