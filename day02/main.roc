app "main"
    packages { pf: "https://github.com/roc-lang/basic-cli/releases/download/0.1.1/zAoiC9xtQPHywYk350_b7ust04BmWLW00sjb9ZPtSQk.tar.br" }
    imports [ pf.Stdout, pf.File, pf.Path, pf.Task.{ Task } ]
    provides [main] to pf

part1 = \lines ->
    lines
    |> List.keepIf (\line -> line != "")
    |> List.map \line ->
        bytes = Str.toUtf8 line |> List.map Num.toI32
        # 0,rock 1,paper 2,scissor
        theirs = List.first bytes |> Result.withDefault 96546546 |> Num.sub 65
        ours = List.last bytes |> Result.withDefault 545354234 |> Num.sub 88
        score1 = 
            when (ours - theirs) is 
                1 -> 6
                -2 -> 6
                0 -> 3
                _ -> 0
        score1 |> Num.add (ours + 1)
    |> List.sum
    |> Num.toStr

innerTask : Task {} [Err]
innerTask = 
    inputString <- "input" |> Path.fromStr |> File.readUtf8 |> Task.await
    inputLines = inputString |> Str.split "\n" 
    p1 = part1 inputLines
    Stdout.write "\(p1)"

main =
    Task.onFail innerTask \_ -> crash "aaaaa"