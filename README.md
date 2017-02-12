# EtwCollector
Simple F# client to listen Etw events.

You can easily listen Microsoft Etw events from the selected provider or providers. 

USAGE: EtwCollector.exe [--help] --session <string> --providers [<string>...]

OPTIONS:

    --session <string>    specify etw session name
    --providers [<string>...]
                          specify etw provider(s).
    --help                display this list of options.
    
    
For example if you want to listen IIS 8.5 Etw events:

> EtwCollector.exe --session "IIS events" --providers "Microsoft-Windows-IIS"

If you want to know what providers you have available you can run:

> logman query providers

