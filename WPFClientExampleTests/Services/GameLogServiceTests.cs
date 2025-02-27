using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPFClientExample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using WPFClientExample.Repositories;
using WPFClientExample.Commons.Enums;
using WPFClientExample.Models.GameLog;
using System.Xml.Serialization;
using WPFClientExample.Models.DataBase;

namespace WPFClientExampleTests.Services
{
    [TestClass()]
    public class GameLogServiceTests
    {
        private Mock<IUserRepository> userRepositoryMock;
        private Mock<ILocalizationService> localizationServiceMock;
        private GameLogService gameLogService;

        [TestInitialize]
        public void Setup()
        {
            userRepositoryMock = new Mock<IUserRepository>();
            localizationServiceMock = new Mock<ILocalizationService>();

            gameLogService = new GameLogService(userRepositoryMock.Object, localizationServiceMock.Object);
        }

        [TestMethod()]
        public void GetAccountInfoAsyncTest_ByName()
        {
            // Arrange
            string accountName = "TestUser";
            var expectedAccount = new AccountInfo { AccountId = 123, AccountName = accountName };
            userRepositoryMock.Setup(repo => repo.GetAccountInfoByName(accountName))
                .Returns(expectedAccount);

            // Act
            var result = gameLogService.GetAccountInfo(USER_SEARCH_TYPE.NAME, "TestUser");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedAccount.AccountId, result.AccountId);
            Assert.AreEqual(expectedAccount.AccountName, result.AccountName);
            userRepositoryMock.Verify(p => p.GetAccountInfo(It.IsAny<long>()), Times.Never);
            userRepositoryMock.Verify(p => p.GetAccountInfoByName(accountName), Times.Once);
        }

        [TestMethod()]
        public void GetAccountInfoAsyncTest_ById()
        {
            // Arrange
            long accountId = 123;
            var expectedAccount = new AccountInfo { AccountId = accountId, AccountName = "TestUser" };
            userRepositoryMock.Setup(repo => repo.GetAccountInfo(123))
                .Returns(expectedAccount);

            // Act
            var result = gameLogService.GetAccountInfo(USER_SEARCH_TYPE.ID, "123");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedAccount.AccountId, result.AccountId);
            Assert.AreEqual(expectedAccount.AccountName, result.AccountName);
            userRepositoryMock.Verify(p => p.GetAccountInfo(accountId), Times.Once);
            userRepositoryMock.Verify(p => p.GetAccountInfoByName(It.IsAny<string>()), Times.Never);
        }

        [DataTestMethod]
        [DataRow(USER_SEARCH_TYPE.ID, null)]
        [DataRow(USER_SEARCH_TYPE.ID, "")]
        [DataRow(USER_SEARCH_TYPE.NAME, null)]
        [DataRow(USER_SEARCH_TYPE.NAME, "")]
        public void  GetAccountInfoAsyncTest_SearchTextNull(USER_SEARCH_TYPE searchType, string? searchData)
        {
            // Arrange

            // Act
            var result = gameLogService.GetAccountInfo(searchType, searchData);

            // Assert
            Assert.IsNull(result);
            userRepositoryMock.Verify(p => p.GetAccountInfo(It.IsAny<int>()), Times.Never);
            userRepositoryMock.Verify(p => p.GetAccountInfoByName(It.IsAny<string>()), Times.Never);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void GetAccountInfoAsyncTest_Exception()
        {
            // Arrange

            // Act
            var result = gameLogService.GetAccountInfo(USER_SEARCH_TYPE.ID, "abcd");

            // Assert
            userRepositoryMock.Verify(p => p.GetAccountInfo(It.IsAny<int>()), Times.Never);
            userRepositoryMock.Verify(p => p.GetAccountInfoByName(It.IsAny<string>()), Times.Never);
        }

        [TestMethod()]
        public void GetCharacterInfoListAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetCharacterInfoDetailInfoAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetCharacterEquipeedInfoAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetChatLogInfoByCharacterIdAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetCharacterQuestInfoByCharacterIdAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetInventoryHistoryLogAsyncTest()
        {
            Assert.Fail();
        }
    }
}