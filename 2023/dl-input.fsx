#r "nuget: FSharp.Data"

open System.IO
open FSharp.Data

Directory.SetCurrentDirectory __SOURCE_DIRECTORY__

// let day = 2
let day = fsi.CommandLineArgs[1] |> int

let url = $"https://adventofcode.com/2023/day/{day}"

let sessionCookie =
    (File.ReadAllText "session-cookie").Split("=", 2) 
    |> (fun f -> f[0], f[1])


let dayDir = $"day%02i{day}" |> DirectoryInfo
if not dayDir.Exists then dayDir.Create()

let input = Http.RequestString(url + "/input", cookies = [ sessionCookie ])

// write input
File.WriteAllText(
    Path.Combine(dayDir.FullName,"input"),
    input
)

// write instructions

let instructions = 
    Http.RequestString(url, cookies = [ sessionCookie ])

let content = 
    instructions
    |> HtmlDocument.Parse
    |> HtmlDocument.body
    |> (fun nodes -> 
        HtmlNode.cssSelect nodes "article" )
    |> List.map (fun f -> f.ToString())
    |> String.concat "\n"

let markdown = 
    Http.RequestString(
        "https://tools.atatus.com/tools/html-to-markdown",
        body = FormValues [ "html", content]
    )

File.WriteAllText(
    Path.Combine(dayDir.FullName,$"instructions-%02i{day}.md"),
    markdown
    )