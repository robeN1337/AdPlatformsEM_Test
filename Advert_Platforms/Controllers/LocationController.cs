using Adver_Platforms.Application.Interfaces;
using Adver_Platforms.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Advert_Platforms.API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class LocationController : ControllerBase
    {

        private ILocationRepository _repo;

        public LocationController (ILocationRepository repo)
        {
            _repo = repo;
        }


        [HttpPost("uploadPlatforms")]
        public async Task<IActionResult> Upload(IFormFile file) // 1. ��������� ����
        {

            if (file == null || file.Length == 0 || file.ContentType != "text/plain") // ! ����������� ���� ������ ���� ������� .txt !
            {
                return BadRequest(new { file_error = "���� ���� / ����� ������, �������� �� .txt �����." });
            }

            List<AdPlatform> adPlatforms = new List<AdPlatform>();

            using (StreamReader reader = new StreamReader(file.OpenReadStream())) // 2. ������ ���� � ��������� ��� �� ��� �����
            {
                
                string? stream;
                while ((stream = await reader.ReadLineAsync()) != null)
                {
                    var parts = stream.Split(':');
                    if (parts.Length < 2)
                    {
                        return BadRequest(new { incorrect_data_error = "��������� ������ � �������:<��������_��������1>:<�������1>,<�������2>,..." });
                    }
                    
                    AdPlatform platform = new AdPlatform(); // 3. ��������� ��������� � ������� � ������
                    platform.PlatformName = parts[0];
                    platform.Locations = parts[1];

                    adPlatforms.Add(platform);
                }
            }
            var result = _repo.UpdateData(adPlatforms); // 4. ��������� ������ ��� ������ ������
            if (result == false)
            {
                return BadRequest(new { update_data_error = "�� ���������� �������� ������ ��� ������." });
            }

            return Ok(adPlatforms); // 5. ���������� ���������� �� .txt ��������� � �������
        }


        [HttpGet("searchPlatforms")]
        public IActionResult FindByLoc (string loc) // 6. ������ ������ ������� (/ru/svrd ��������)
        {
            var result = _repo.FindByLocation(loc);
            if (result == null)
            {
                return BadRequest(new { platforms_empty_error = "��� ������ ��������� ��������." });
            }
            return Ok(result);
        }

    }

}
