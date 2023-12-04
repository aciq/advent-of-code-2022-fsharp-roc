#r "nuget: Fs.Scripting"
open Fs.Scripting

open System.IO
open System
open System.Diagnostics
open System.IO
open System.Text

Directory.SetCurrentDirectory __SOURCE_DIRECTORY__

let projDir = "day02"

let watcher = new FileSystemWatcher(projDir,"*.fst",EnableRaisingEvents=true, IncludeSubdirectories=true)
let onChanged (_:WaitForChangedResult) =
    stdout.WriteLine "file changed "
    Process.mirror( $"{projDir}/compile.sh", "", projDir , allowNonZeroExit=true).Result |> ignore
    // Process.mirror( $"{projDir}/main" ).Result
  

let rec loop (nextproctime:DateTimeOffset) : unit =
    let changed = watcher.WaitForChanged(WatcherChangeTypes.Changed)
    match DateTimeOffset.Now > nextproctime with 
    | false -> loop (nextproctime) 
    | true -> 
        onChanged changed
        loop (nextproctime.AddSeconds(2))   
        
loop DateTimeOffset.Now        
        
