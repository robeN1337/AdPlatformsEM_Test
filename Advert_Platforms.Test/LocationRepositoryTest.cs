using Adver_Platforms.Domain.Entities;
using Adver_Platforms.Infrastructure.Repositories;
using Xunit;

namespace Advert_Platforms.Test
{
    public class LocationRepositoryTest
    {

        public List<AdPlatform> _adPlatform = new List<AdPlatform>
        {
            new AdPlatform { PlatformName = "яндекс.ƒирект", Locations = "/ru" },
            new AdPlatform { PlatformName = "–евдинский рабочий", Locations = "/ru/svrd/revda,/ru/svrd/pervik" },
            new AdPlatform { PlatformName = "√азета уральских москвичей", Locations = "/ru/msk,/ru/permobl,/ru/chelobl" },
            new AdPlatform { PlatformName = " рута€ реклама", Locations = "/ru/svrd" }
        };


        [Fact]
        public void UpdateDataAndSearchTest()
        {
            var _repo = new LocationRepository();
            var result = _repo.UpdateData(_adPlatform);
            Assert.True(result); // проверка успешности обновлени€ платформ

            var findplatform_ru = _repo.FindByLocation("/ru");
            Assert.Contains("яндекс.ƒирект", findplatform_ru);
            Assert.DoesNotContain("√азета уральских москвичей", findplatform_ru);

            var findplatform_ru_svrd = _repo.FindByLocation("/ru/svrd");
            Assert.Contains("яндекс.ƒирект", findplatform_ru_svrd);
            Assert.Contains(" рута€ реклама", findplatform_ru_svrd);

            var findplatform_ru_svrd_revda = _repo.FindByLocation("/ru/svrd/revda");
            Assert.Contains("яндекс.ƒирект", findplatform_ru_svrd_revda);
            Assert.Contains(" рута€ реклама", findplatform_ru_svrd_revda);
            Assert.Contains("–евдинский рабочий", findplatform_ru_svrd_revda);


            var findplatform_ru_obl = _repo.FindByLocation("/ru/permobl");
            Assert.Contains("яндекс.ƒирект", findplatform_ru_obl);
            Assert.Contains("√азета уральских москвичей", findplatform_ru_obl);
            
        }
    }
}