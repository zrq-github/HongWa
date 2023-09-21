namespace DevsApp.Core.Contracts.Services;

public interface IFileService
{
    T Read<T>(string folderPath, string fileName);

    void Save<T>(string folderPath, string fileName, T content);

    void Delete(string folderPath, string fileName);

    /// <summary>
    /// 打开文件夹
    /// </summary>
    void OpenFolder(in string folderFullName);

    void OpenFolderAndSelectItem(in string selectedItemPath);
}
