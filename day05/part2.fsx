open System.IO
open System.Text.RegularExpressions

let parseCrates (lines: string seq) =
    let crateLines =
        lines
        |> Seq.takeWhile (fun line -> not (line.StartsWith " 1"))
        |> Seq.rev // start from bottom up
        |> Seq.toArray

    let numOfCrates = ((crateLines[0].Length + 3) / 4)
    let crates = Array.create numOfCrates []

    crateLines
    |> Seq.fold
        (fun crates line ->
            line
            |> Seq.chunkBySize 4
            |> Seq.map (fun f -> f[1]) // index 1 is char --> [^]
            |> Seq.indexed
            |> Seq.fold // add chars to respective crate
                (fun crates (crateIndex, char) ->
                    match char with
                    | ' ' -> crates
                    | _ -> crates |> Array.updateAt crateIndex (char :: crates[crateIndex]))
                crates)
        crates

let moveToCrate count from dest (crates: char list array) =
    let taken, remaining = crates[from] |> List.splitAt count
    crates
    |> Array.updateAt from remaining
    |> Array.updateAt dest (taken  @ crates[dest])

let parseInstructions (lines: string seq) = 
    lines
    |> Seq.map (fun line -> Regex.Match(line, "move (\\d+) from (\\d+) to (\\d+)") )
    |> Seq.where (fun f -> f.Success)
    |> Seq.map (fun f -> f.Groups |> Seq.skip 1 |> Seq.map (fun g -> int g.Value) |> Seq.toArray)
    |> Seq.toArray


let lines = __SOURCE_DIRECTORY__ + "/input" |> File.ReadLines

let crates = lines |> parseCrates
let instructions = lines |> parseInstructions |> Seq.toArray

let result = 
    instructions
    |> Seq.fold (fun crates inst -> 
        crates |> moveToCrate inst[0] (inst[1]-1) (inst[2]-1)
    ) crates
    |> Seq.map (List.tryHead)
    |> Seq.choose id
    |> (Seq.toArray >> System.String)
    
result
