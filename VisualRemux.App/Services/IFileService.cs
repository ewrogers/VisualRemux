using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;

namespace VisualRemux.App.Services;

public interface IFileService
{
    Task<IReadOnlyList<IStorageFile>?> OpenVideoFilesAsync(string title)
    {
        var videoFileType = new FilePickerFileType("Video Files")
        {
            Patterns = ["*.mp4", "*.mkv"]
        };

        return OpenFilesAsync(title, videoFileType);
    }

    Task<IReadOnlyList<IStorageFile>?> OpenFilesAsync(string title, params IEnumerable<FilePickerFileType> fileTypes);
}