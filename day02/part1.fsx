open System
open System.IO

let modE x y = (x + y) % y

__SOURCE_DIRECTORY__ + "/input"
|> File.ReadLines
|> Seq.where (String.IsNullOrWhiteSpace >> not)
|> Seq.map (fun line ->
    let theirs = (int line[0]) - (int 'A') 
    let ours = (int line[2]) - (int 'X') 
    match modE (ours - theirs) 3 with
    | 1 -> 6
    | 0 -> 3
    | _ -> 0
    |> (+) (ours + 1))
|> Seq.sum
