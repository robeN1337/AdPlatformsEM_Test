using Adver_Platforms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adver_Platforms.Application.Interfaces
{
    public interface ILocationRepository 
    {
        public bool UpdateData(List<AdPlatform> platforms);
        public List<string> FindByLocation(string location);
        public bool CheckData();
    }
}
