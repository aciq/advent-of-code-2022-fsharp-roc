open System.IO

__SOURCE_DIRECTORY__ + "/input"
|> File.ReadLines
|> Seq.fold (fun (curr,elves) line -> 
    match line with 
    | "" -> (0, curr :: elves)
    | n -> (curr + int n, elves)
    ) (0,[])
|> snd
|> Seq.sortDescending
|> Seq.take 3
|> Seq.sum

