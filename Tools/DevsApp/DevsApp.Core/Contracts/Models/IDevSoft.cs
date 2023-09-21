using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevsApp.Core.Contracts.Models;
public interface IDevSoft
{
    /// <summary>
    /// ID
    /// </summary>
    /// <remarks>
    /// 就目前来说没什么用
    /// </remarks>
    public string Id { get; set; }

    /// <summary>
    /// name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 当前本地的版本
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    /// 服务器上最新的版本
    /// </summary>
    public string LastVersion { get; set; }

    /// <summary>
    /// 产品描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 产品属于哪一个团队的
    /// </summary>
    public string ScrumTeam { get; set; }
}
