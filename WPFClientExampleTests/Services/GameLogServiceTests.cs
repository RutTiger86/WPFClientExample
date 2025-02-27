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
        public void GetAccountInfoTest_ByName()
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
        public void GetAccountInfoTest_ById()
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
        public void  GetAccountInfoTest_SearchTextNull(USER_SEARCH_TYPE searchType, string? searchData)
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
        public void GetAccountInfoTest_Exception()
        {
            // Arrange

            // Act
            var result = gameLogService.GetAccountInfo(USER_SEARCH_TYPE.ID, "abcd");

            // Assert
            userRepositoryMock.Verify(p => p.GetAccountInfo(It.IsAny<int>()), Times.Never);
            userRepositoryMock.Verify(p => p.GetAccountInfoByName(It.IsAny<string>()), Times.Never);
        }

        [TestMethod()]
        public void GetCharacterInfoListTest()
        {
            // Arrange
            long accountId = 123;
            userRepositoryMock.Setup(repo => repo.GetCharacterInfoList(accountId)).Returns([]);

            // Act
            var result = gameLogService.GetCharacterInfoList(accountId);

            // Assert
            Assert.IsNotNull(result);
            userRepositoryMock.Verify(p => p.GetCharacterInfoList(accountId), Times.Once);
        }

        [TestMethod()]
        public void GetCharacterInfoDetailInfoTest()
        {
            // Arrange
            long characterId = 123;
            userRepositoryMock.Setup(repo => repo.GetCharacterDetailInfo(characterId)).Returns(new CharacterDetailInfo() { CharacterName  = "", ServerName = ""});

            // Act
            var result = gameLogService.GetCharacterInfoDetailInfo(characterId);

            // Assert
            Assert.IsNotNull(result);
            userRepositoryMock.Verify(p => p.GetCharacterDetailInfo(characterId), Times.Once);
        }

        [TestMethod()]
        public void GetCharacterEquipeedInfoTest()
        {
            // Arrange
            long characterId = 123;
            userRepositoryMock.Setup(repo => repo.GetCharacterEquipeedInfo(characterId)).Returns([]);

            // Act
            var result = gameLogService.GetCharacterEquipeedInfo(characterId);

            // Assert
            Assert.IsNotNull(result);
            userRepositoryMock.Verify(p => p.GetCharacterEquipeedInfo(characterId), Times.Once);
        }

        [TestMethod()]
        public void GetChatLogInfoByCharacterIdTest()
        {
            // Arrange
            long characterId = 123;
            userRepositoryMock.Setup(repo => repo.GetChatLogInfosByCharacterId(characterId)).Returns([]);

            // Act
            var result = gameLogService.GetChatLogInfoByCharacterId(characterId);

            // Assert
            Assert.IsNotNull(result);
            userRepositoryMock.Verify(p => p.GetChatLogInfosByCharacterId(characterId), Times.Once);
        }

        [TestMethod()]
        public void GetCharacterQuestInfoByCharacterIdTest()
        {
            // Arrange
            long characterId = 123;
            userRepositoryMock.Setup(repo => repo.GetCharacterQuestInfoByCharacterId(characterId)).Returns([]);

            // Act
            var result = gameLogService.GetCharacterQuestInfoByCharacterId(characterId);

            // Assert
            Assert.IsNotNull(result);
            userRepositoryMock.Verify(p => p.GetCharacterQuestInfoByCharacterId(characterId), Times.Once);
        }

        [TestMethod()]
        public void GetInventoryHistoryLogTest()
        {
            // Arrange
            long characterId = 123;
            DateTime startDate = DateTime.Now.AddDays(-1);
            DateTime endDate = DateTime.Now;

            userRepositoryMock.Setup(repo => repo.GetInventoryHistoryLog(characterId, startDate.ToUniversalTime(), endDate.ToUniversalTime())).Returns([]);

            // Act
            var result = gameLogService.GetInventoryHistoryLog(characterId, startDate, endDate);

            // Assert
            Assert.IsNotNull(result);
            userRepositoryMock.Verify(p => p.GetInventoryHistoryLog(characterId, startDate.ToUniversalTime(), endDate.ToUniversalTime()), Times.Once);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void GetInventoryHistoryLogTest_Exception()
        {
            // Arrange
            long characterId = 123;
            DateTime startDate = DateTime.Now.AddDays(+1);
            DateTime endDate = DateTime.Now;

            userRepositoryMock.Setup(repo => repo.GetInventoryHistoryLog(characterId, startDate.ToUniversalTime(), endDate.ToUniversalTime())).Returns([]);

            // Act
            var result = gameLogService.GetInventoryHistoryLog(characterId, startDate, endDate);

            // Assert
            userRepositoryMock.Verify(p => p.GetInventoryHistoryLog(characterId, startDate.ToUniversalTime(), endDate.ToUniversalTime()), Times.Never);
        }
    }
}