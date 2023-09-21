using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Security.Principal;
using System.Text;

using DevsApp.Core.Contracts.Services;

using Newtonsoft.Json;

namespace DevsApp.Core.Services;

/// <summary>
/// 文件服务
/// </summary>
/// <remarks>
/// 如果没有特别说明，默认都是序列化成json文件
/// </remarks>
public class FileService : IFileService
{
    public T Read<T>(string folderPath, string fileName)
    {
        var path = Path.Combine(folderPath, fileName);
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(json);
        }

        return default;
    }

    public void Save<T>(string folderPath, string fileName, T content)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        var fileContent = JsonConvert.SerializeObject(content);
        File.WriteAllText(Path.Combine(folderPath, fileName), fileContent, Encoding.UTF8);
    }

    public void Delete(string folderPath, string fileName)
    {
        if (fileName != null && File.Exists(Path.Combine(folderPath, fileName)))
        {
            File.Delete(Path.Combine(folderPath, fileName));
        }
    }

    public void OpenFolder(in string folderFullName)
    {
        if (string.IsNullOrEmpty(folderFullName) || !Directory.Exists(folderFullName))
        {
            return;
        }

        var psi = new ProcessStartInfo
        {
            FileName = "explorer.exe",
            Arguments = folderFullName
        };

        Process.Start(psi);
    }

    public void OpenFolderAndSelectItem(in string selectedItemPath)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "explorer.exe",
            Arguments = $"/e,/select,\"{selectedItemPath}\""
        };

        Process.Start(psi);
    }
}
