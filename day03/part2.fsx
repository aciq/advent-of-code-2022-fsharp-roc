open System
open System.IO

let getPrio (c: char) =
    match int c with
    | x when x >= 97 -> x - 96
    | x when x >= 65 -> (x - 64) + 26

let alpha = [ [ 65..90 ]; [ 97..122 ] ] |> List.concat |> List.map char

__SOURCE_DIRECTORY__ + "/input"
|> File.ReadLines
|> Seq.chunkBySize 3
|> Seq.map (fun group -> 
    alpha |> Seq.find (fun c -> 
        group |> Seq.forall (Seq.contains c)))
|> Seq.map getPrio
|> Seq.sum
