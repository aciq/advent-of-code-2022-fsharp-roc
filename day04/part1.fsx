open System.IO

let isContainedBy (f1, t1) (f2, t2) = f1 <= f2 && t1 >= t2

__SOURCE_DIRECTORY__ + "/input"
|> File.ReadLines
|> Seq.map (fun line ->
    line.Split(",")
    |> Array.map (fun range -> range.Split("-") |> Array.map int |> (fun r -> r[0], r[1]))
    |> (fun x -> x[0], x[1]))
|> Seq.where (fun (left, right) -> isContainedBy left right || isContainedBy right left)
|> Seq.length
