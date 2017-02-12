# EtwCollector
Simple F# client to listen Etw events.

You can easily listen Microsoft Etw events from the selected provider or providers. At this time client only prints event payload names and values.

USAGE: EtwCollector.exe [--help] --session <string> --providers [<string>...]

OPTIONS:

    --session <string>    specify etw session name
    --providers [<string>...]
                          specify etw provider(s).
    --help                display this list of options.
    
    
For example if you want to listen IIS 8.5 events:

> EtwCollector.exe --session "IIS events" --providers "Microsoft-Windows-IIS"

To listen multiple providers separate with whitespace:

> EtwCollector.exe --session "IIS events" --providers "Microsoft-Windows-IIS" "Microsoft-Windows-IIS-W3SVC"

If you want to know what providers you have available in your system:

> logman query providers

