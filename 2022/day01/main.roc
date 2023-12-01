app "main"
    packages { pf: "https://github.com/roc-lang/basic-cli/releases/download/0.1.1/zAoiC9xtQPHywYk350_b7ust04BmWLW00sjb9ZPtSQk.tar.br" }
    imports [
        pf.Stdout,
        # pf.Stdin,
        pf.File,
        pf.Path,
        pf.Task.{ Task, await }
    ]
    provides [main] to pf


State : { curr: I32, elves: List I32 }

foldLine : State, Str -> State
foldLine = \acc, line -> 
    when line is 
        "" -> { acc & curr : 0, elves : List.append acc.elves acc.curr}
        _ ->
            cals = line |> Str.toI32 |> Result.withDefault 0
            { acc & curr : acc.curr + cals}

innerTask : Task {} [Err]
innerTask = 
    inputString <- "input" |> Path.fromStr |> File.readUtf8 |> Task.await
  
    result =
        inputString 
        |> Str.split "\n" 
        |> List.walk { curr: 0, elves: []} foldLine
        
    part1 = result.elves |> List.max |> Result.withDefault 0 |> Num.toStr
    part2 = 
        result.elves 
        |> List.sortDesc 
        |> List.takeFirst 3 
        |> List.sum 
        |> Num.toStr
    
    Stdout.write "part1: \(part1), part2: \(part2)"

main =
    Task.onFail innerTask \_ -> crash "aaaaa"