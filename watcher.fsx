open System.IO
open System
open System.Diagnostics

let watcher = new FileSystemWatcher(".","*.roc",EnableRaisingEvents=true, IncludeSubdirectories=true)

let p() = 
    new Process(StartInfo=ProcessStartInfo(
            fileName="roc",
            arguments="check day02/main.roc",
            WorkingDirectory= __SOURCE_DIRECTORY__ ,
            RedirectStandardInput=true
        )
    )
       
let rec loop (nextproctime:DateTimeOffset) : unit =
    let _ = watcher.WaitForChanged(WatcherChangeTypes.Changed)
    match DateTimeOffset.Now > nextproctime with 
    | false -> loop (nextproctime) 
    | true -> 
        use p = p()
        p.Start() |> ignore
        p.WaitForExit()
        loop (nextproctime.AddSeconds(2))   

loop DateTimeOffset.Now
