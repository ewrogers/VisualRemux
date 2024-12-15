using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace VisualRemux.App.Services;

public class FileService(TopLevel topLevel) : IFileService
{
    public async Task<IReadOnlyList<IStorageFile>?> OpenFilesAsync(string title,
        params IEnumerable<FilePickerFileType> fileTypes)
    {
        var fileTypesList = new List<FilePickerFileType>();
        fileTypesList.AddRange(fileTypes);
        fileTypesList.Add(FilePickerFileTypes.All);

        var options = new FilePickerOpenOptions
        {
            Title = title,
            FileTypeFilter = fileTypesList,
            AllowMultiple = true,
        };

        var files = await topLevel.StorageProvider.OpenFilePickerAsync(options);

        return files.Count > 0 ? files : null;
    }

    public async Task<IStorageFolder?> OpenFolderAsync(string title)
    {
        var options = new FolderPickerOpenOptions
        {
            Title = title,
        };

        var folders = await topLevel.StorageProvider.OpenFolderPickerAsync(options);
        return folders.Count > 0 ? folders[0] : null;
    }
}