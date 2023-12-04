#r "nuget: FSharp.Data"
open System.IO
open FSharp.Data

Directory.SetCurrentDirectory __SOURCE_DIRECTORY__

// let day = 2
let day = fsi.CommandLineArgs[1] |> int

let url = $"https://adventofcode.com/2023/day/{day}"

let sessionCookie =
    (File.ReadAllText "session.txt").Split("=", 2) 
    |> (fun f -> f[0], f[1])


let dayDir = $"day%02i{day}" |> DirectoryInfo
if not dayDir.Exists then dayDir.Create()

let input = Http.RequestString(url + "/input", cookies = [ sessionCookie ])

// write input
File.WriteAllText(Path.Combine(dayDir.FullName,"input"), input)
