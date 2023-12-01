open System
open System.IO

let modE x y = (x + y) % y

__SOURCE_DIRECTORY__ + "/input"
|> File.ReadLines
|> Seq.where (String.IsNullOrWhiteSpace >> not)
|> Seq.map (fun line ->
    let theirs = (int line[0]) - (int 'A')
    let ours = (int line[2]) - (int 'X')

    match ours with
    | 0 -> (modE (theirs - 1) 3) + 1
    | 1 -> 3 + theirs + 1
    | _ -> 6 + (modE (theirs + 1) 3) + 1)
|> Seq.sum
