using System;

namespace VisualRemux.App.Logging;

[Flags]
public enum LogLevel
{
    Debug,
    Info,
    Warn,
    Error
}