using System.Net.NetworkInformation;
using CommunityToolkit.Mvvm.ComponentModel;

namespace DevsApp.Models;

/// <summary>
/// 下载精度
/// </summary>
public partial class DownloadProgress : ObservableObject
{
    [ObservableProperty]
    private double _progress = 0;

    [ObservableProperty]
    private bool _isPaused = false;

    [ObservableProperty]
    private bool _isError = false;

    /// <summary>
    /// IsIndeterminate的优先级大于Progress
    /// </summary>
    [ObservableProperty]
    private bool _isIndeterminate = false;

    [ObservableProperty]
    private string _message = string.Empty;

    public void Reset()
    {
        Progress = 0;
        IsPaused = false;
        IsError = false;
        IsIndeterminate = false;
        Message = string.Empty;
    }
}

public enum DownState
{
    /// <summary>
    /// 未定义
    /// </summary>
    None = -1,

    /// <summary>
    /// 准备下载
    /// </summary>
    Down = 1 << 0,

    /// <summary>
    /// 正在下载
    /// </summary>
    Downloading = 1 << 1,

    /// <summary>
    /// 下载成功
    /// </summary>
    Succeed = 1 << 2,

    /// <summary>
    /// 下载失败
    /// </summary>
    Fail = 1 << 3,

    /// <summary>
    /// 下载完成
    /// </summary>
    Finished = 1 << 4
}