#r "nuget: resharp.native, 0.0.8"
let c = Resharp.Native.RegexCompiler()

let full = (System.IO.File.ReadAllText("day01.txt"))

let small =
    """3   4
4   3
2   5
1   3
3   9
3   3"""

let lefts = c.Compile(@"^[0-9]+")
let rights = c.Compile(@"[0-9]+$")

let conv: Resharp.Native.Match seq -> int array =
    Seq.map _.Value >> Seq.map int >> Seq.sort >> Seq.toArray

let main1 (input: string) =
    let l = c.Matches(lefts, input) |> conv
    let r = c.Matches(rights, input) |> conv
    Seq.zip l r |> Seq.map (fun (a, b) -> abs (a - b)) |> Seq.sum

main1 small
main1 full

let main2 (input: string) =
    let l = c.Matches(lefts, input) |> conv
    let r = c.Matches(rights, input) |> conv

    let sim l r =
        l
        |> Seq.map (fun a -> (r |> Seq.where (fun b -> b = a) |> Seq.length) * a)
        |> Seq.sum

    sim l r

main2 small
main2 full
