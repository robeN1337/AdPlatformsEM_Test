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
        public async Task<IActionResult> Upload(IFormFile file) // 1. загружаем файл
        {

            if (file == null || file.Length == 0 || file.ContentType != "text/plain") // ! загружаемый файл должен быть формата .txt !
            {
                return BadRequest(new { file_error = "Файл пуст / имеет формат, отличный от .txt файла." });
            }

            List<AdPlatform> adPlatforms = new List<AdPlatform>();

            using (StreamReader reader = new StreamReader(file.OpenReadStream())) // 2. читаем файл и разделяем его на две части
            {
                
                string? stream;
                while ((stream = await reader.ReadLineAsync()) != null)
                {
                    var parts = stream.Split(':');
                    if (parts.Length < 2)
                    {
                        return BadRequest(new { incorrect_data_error = "Требуются данные в формате:<название_площадки1>:<локация1>,<локация2>,..." });
                    }
                    
                    AdPlatform platform = new AdPlatform(); // 3. добавляем платформы и локации в список
                    platform.PlatformName = parts[0];
                    platform.Locations = parts[1];

                    adPlatforms.Add(platform);
                }
            }
            var result = _repo.UpdateData(adPlatforms); // 4. обновляем данные для метода поиска
            if (result == false)
            {
                return BadRequest(new { update_data_error = "Не получилось обновить данные для поиска." });
            }

            return Ok(adPlatforms); // 5. возвращаем полученные из .txt платформы и локации
        }


        [HttpGet("searchPlatforms")]
        public IActionResult FindByLoc (string loc) // 6. вводим нужную локацию (/ru/svrd например)
        {
            var result = _repo.FindByLocation(loc);
            if (result == null)
            {
                return BadRequest(new { platforms_empty_error = "Для начала загрузите площадки." });
            }
            return Ok(result);
        }

    }

}
