open Microsoft.Diagnostics.Tracing;
open Microsoft.Diagnostics.Tracing.Session;
open System
open System.Threading
open Argu

type CliArgs =
    |  [<Mandatory>] Session of string
    |  [<Mandatory>] Providers of string List
with
    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | Session _ -> "specify etw session name"
            | Providers _ -> "specify etw provider(s)."
         
let printEvent (event:TraceEvent) = 
    if event.EventName <> "ManifestData" then
        printfn "***** %s *****" event.EventName 
        event.PayloadNames |> Seq.iter (fun x -> printfn "%s:%A" x (event.PayloadByName x)) 

let run (sessionName: string) (providers: list<string>) = 
    use session = new TraceEventSession(sessionName)
    providers 
        |> Seq.iter(fun p -> printfn "***** listening events from provider: %A *****" p; session.EnableProvider(p)|> ignore) 

    let stream = session.Source.Dynamic.Observe(null)
    stream
        |> Observable.subscribe(fun e -> printEvent e) |> ignore
    session.Source.Process() |> ignore 

[<EntryPoint>]
let main argv = 
    let errorHandler = ProcessExiter(colorizer = function ErrorCode.HelpText -> None | _ -> Some ConsoleColor.Red)
    let parser = ArgumentParser.Create<CliArgs>(programName = "EtwCollector.exe", errorHandler = errorHandler)
    let args = parser.ParseCommandLine argv
    let sessionName = args.GetResult(<@ Session @>)
    let providers = args.GetResult(<@ Providers @>)
    run sessionName providers
    0 // return an integer exit code


    
