open System.IO

let hasOverlap ((f1, t1),(f2, t2)) = 
    if f1 = f2 || t1 = t2 then true
    elif f1 < f2 && t1 >= f2 then true
    elif f2 < f1 && t2 >= f1 then true
    else false

__SOURCE_DIRECTORY__ + "/input"
|> File.ReadLines
|> Seq.map (fun line ->
    line.Split(",")
    |> Array.map (fun range -> range.Split("-") |> Array.map int |> (fun r -> r[0], r[1]))
    |> (fun x -> x[0], x[1]))
|> Seq.where hasOverlap
|> Seq.length
