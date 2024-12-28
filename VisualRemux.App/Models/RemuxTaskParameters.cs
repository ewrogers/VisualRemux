namespace VisualRemux.App.Models;

public class RemuxTaskParameters
{
    public required string InputFile { get; set; }
    public required string OutputFile { get; set; }
    public required string OutputFormat { get; set; }
}