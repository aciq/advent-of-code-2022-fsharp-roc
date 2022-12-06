open System.IO

let sr = new StreamReader(__SOURCE_DIRECTORY__ + "/input")
let signal = seq { while not(sr.EndOfStream) do char (sr.Read()) }

signal
|> Seq.windowed 4
|> Seq.findIndex (fun chars -> chars |> Seq.distinct |> Seq.length = 4 )
|> (+) 4
