using Adver_Platforms.Domain.Entities;
using Adver_Platforms.Infrastructure.Repositories;
using Xunit;

namespace Advert_Platforms.Test
{
    public class LocationRepositoryTest
    {

        public List<AdPlatform> _adPlatform = new List<AdPlatform>
        {
            new AdPlatform { PlatformName = "������.������", Locations = "/ru" },
            new AdPlatform { PlatformName = "���������� �������", Locations = "/ru/svrd/revda,/ru/svrd/pervik" },
            new AdPlatform { PlatformName = "������ ��������� ���������", Locations = "/ru/msk,/ru/permobl,/ru/chelobl" },
            new AdPlatform { PlatformName = "������ �������", Locations = "/ru/svrd" }
        };


        [Fact]
        public void UpdateDataAndSearchTest()
        {
            var _repo = new LocationRepository();
            var result = _repo.UpdateData(_adPlatform);
            Assert.True(result); // �������� ���������� ���������� ��������

            var findplatform_ru = _repo.FindByLocation("/ru");
            Assert.Contains("������.������", findplatform_ru);
            Assert.DoesNotContain("������ ��������� ���������", findplatform_ru);

            var findplatform_ru_svrd = _repo.FindByLocation("/ru/svrd");
            Assert.Contains("������.������", findplatform_ru_svrd);
            Assert.Contains("������ �������", findplatform_ru_svrd);

            var findplatform_ru_svrd_revda = _repo.FindByLocation("/ru/svrd/revda");
            Assert.Contains("������.������", findplatform_ru_svrd_revda);
            Assert.Contains("������ �������", findplatform_ru_svrd_revda);
            Assert.Contains("���������� �������", findplatform_ru_svrd_revda);


            var findplatform_ru_obl = _repo.FindByLocation("/ru/permobl");
            Assert.Contains("������.������", findplatform_ru_obl);
            Assert.Contains("������ ��������� ���������", findplatform_ru_obl);
            
        }
    }
}