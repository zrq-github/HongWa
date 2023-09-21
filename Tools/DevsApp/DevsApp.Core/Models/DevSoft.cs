using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevsApp.Core.Contracts.Models;

namespace DevsApp.Core.Models;
public class DevSoft : IDevSoft
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Version { get; set; }
    public string LastVersion { get; set; }
    public string Description { get; set; }
    public string ScrumTeam { get; set; }
}
