open System
open System.IO

__SOURCE_DIRECTORY__ + "/input"
|> File.ReadLines
|> Seq.map (fun f ->
    let p1 = f[.. f.Length / 2 - 1]
    let p2 = f[f.Length / 2 ..]
    p2 |> Seq.find (fun d -> p1 |> Seq.contains d))
|> Seq.map (fun f ->
    match f |> int with
    | x when x >= 97 -> x - 96
    | x when x >= 65 -> (x - 64) + 26)
|> Seq.sum
