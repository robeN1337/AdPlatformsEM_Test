using Adver_Platforms.Application.Interfaces;
using Adver_Platforms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adver_Platforms.Infrastructure.Repositories
{
    public class LocationRepository : ILocationRepository
    {

        private Dictionary<string, List<string>> _platformList = new();
       

        public bool UpdateData (List<AdPlatform> platforms)
        {
            var newDict = new Dictionary<string, List<string>>();
            foreach (var platform in platforms)
            {
                var locs = platform.Locations.Split(",");

                foreach (var loc in locs)
                {
                    newDict[loc] = [platform.PlatformName];
                }
            }

            _platformList = newDict;
            return true;
        }

        public bool CheckData()
        {
            if (_platformList.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }


        public List<string> FindByLocation(string location)
        {
            var isdata = CheckData(); // проверяем были ли уже загружены данные по платформам
            if (isdata == false) return null;

            if (string.IsNullOrWhiteSpace(location))
                return new List<string>();

            var result = new HashSet<string>();

            var parts = location.Split("/", StringSplitOptions.RemoveEmptyEntries);

            var curr_location_string = "";
            foreach (var part in parts)
            {
                curr_location_string += "/" + part;

                if (_platformList.TryGetValue(curr_location_string, out var platform))
                {
                    result.UnionWith(platform);
                }
            }

            return result.ToList();
        }
    }
}
