using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DAL.Repository;
//using DomainModel;
//using Interfaces.Repository;
using Moq;

using NUnit.Framework;
namespace UnitTests.Mocks
{
    public static class MockUowRepository
    {
        /*public static Mock<IDbRepos> GetMock()
        {
            var mock = new Mock<IDbRepos>();
            var orderRepoMock = MockOrderRepository.GetMock();
            var phoneRepoMock = MockPhoneRepository.GetMock();
            var manufRepoMock = MockManufRepository.GetMock();
            var reportRepoMock = MockReportRepository.GetMock();

            mock.Setup(m => m.Orders).Returns(() => orderRepoMock.Object);
            mock.Setup(m => m.Phones).Returns(() => phoneRepoMock.Object);
            mock.Setup(m => m.Manufacturers).Returns(() => manufRepoMock.Object);
            mock.Setup(m => m.Reports).Returns(() => reportRepoMock.Object);
            // не тестируем запись в бд
            mock.Setup(m => m.Save()).Returns(() => { return 1; });
            return mock;
        }*/
    }
}
