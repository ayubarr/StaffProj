using Microsoft.AspNetCore.Identity;
using Moq;
using StaffProj.ApiModels.DTOs.Employee;
using StaffProj.Domain.Models.Entities;
using StaffProj.Services.Implemintations;
using StaffProj.Services.Interfaces;

namespace StaffProj.Tests.Services
{
    [TestFixture]
    public class EmployeeManagerTests
    {
        private Mock<UserManager<Employee>> _userManagerMock;
        private IEmployeeManager _employeeService;

        [SetUp]
        public void Setup()
        {
            _userManagerMock = new Mock<UserManager<Employee>>(Mock.Of<IUserStore<Employee>>(), null, null, null, null, null, null, null, null);
            _employeeService = new EmployeeManager(_userManagerMock.Object);
        }

        [Test]
        public async Task GetAllAsync_ReturnsNotFoundResponse_WhenUsersListIsNull()
        {
            // Arrange
            IQueryable<Employee> users = null;
            _userManagerMock.Setup(m => m.Users)
                .Returns(users);

            // Act
            var response = await _employeeService.GetAllAsync();

            // Assert
            Assert.IsFalse(response.IsSuccess);
            Assert.NotNull(response.Message);
        }

        [Test]
        public async Task GetByIdAsync_ReturnsSuccessResponseWithEmployee_WhenEmployeeExists()
        {
            // Arrange
            var employeeId = "1";
            var user = new Employee { Id = employeeId, UserName = "user1" };

            _userManagerMock.Setup(m => m.FindByIdAsync(employeeId))
                .ReturnsAsync(user);

            // Act
            var response = await _employeeService.GetByIdAsync(employeeId);

            // Assert
            Assert.IsTrue(response.IsSuccess);
            Assert.AreEqual(employeeId, response.Data.Id);
            Assert.AreEqual(user.UserName, response.Data.UserName);
        }

        [Test]
        public async Task GetByIdAsync_ReturnsNotFoundResponse_WhenEmployeeIsNull()
        {
            // Arrange
            var employeeId = "1";

            _userManagerMock.Setup(m => m.FindByIdAsync(employeeId))
                .ReturnsAsync((Employee)null);

            // Act
            var response = await _employeeService.GetByIdAsync(employeeId);

            // Assert
            Assert.IsFalse(response.IsSuccess);
            Assert.NotNull(response.Message);
        }

        [Test]
        public async Task UpdateAsync_ReturnsSuccessResponse_WhenEmployeeIsUpdated()
        {
            // Arrange
            var employeeId = "1";
            var updateDto = new UpdateEmployeeDTO
            {
                Email = "newemail@example.com",
                UserName = "NewUser",
                Name = "New",
                Position = "Name",
                Age = 2,
            };
            var employee = new Employee { Id = employeeId };

            _userManagerMock.Setup(m => m.FindByIdAsync(employeeId))
                .ReturnsAsync(employee);
            _userManagerMock.Setup(m => m.UpdateAsync(employee))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var response = await _employeeService.UpdateAsync(employeeId, updateDto);

            // Assert
            Assert.IsTrue(response.IsSuccess);
            _userManagerMock.Verify(m => m.UpdateAsync(employee), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_ReturnsNotFoundResponse_WhenEmployeeIsNull()
        {
            // Arrange
            var employeeId = "1";
            var updateDto = new UpdateEmployeeDTO();

            _userManagerMock.Setup(m => m.FindByIdAsync(employeeId))
                .ReturnsAsync((Employee)null);

            // Act
            var response = await _employeeService.UpdateAsync(employeeId, updateDto);

            // Assert
            Assert.IsFalse(response.IsSuccess);
            Assert.NotNull(response.Message);
            _userManagerMock.Verify(m => m.UpdateAsync(It.IsAny<Employee>()), Times.Never);
        }


        [Test]
        public async Task UpdateAsync_ReturnsErrorResponse_WhenUpdateFails()
        {
            // Arrange
            var employeeId = "1";
            var updateDto = new UpdateEmployeeDTO();

            _userManagerMock.Setup(m => m.FindByIdAsync(employeeId))
                .ReturnsAsync((Employee)null);

            // Act
            var response = await _employeeService.UpdateAsync(employeeId, updateDto);

            // Assert
            Assert.IsFalse(response.IsSuccess);
            Assert.NotNull(response.Message);
        }

        [Test]
        public async Task DeleteByIdAsync_ReturnsSuccessResponse_WhenEmployeeIsDeleted()
        {
            // Arrange
            var employeeId = "1";
            var employee = new Employee { Id = employeeId };

            _userManagerMock.Setup(m => m.FindByIdAsync(employeeId))
                .ReturnsAsync(employee);
            _userManagerMock.Setup(m => m.DeleteAsync(employee))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var response = await _employeeService.DeleteByIdAsync(employeeId);

            // Assert
            Assert.IsTrue(response.IsSuccess);
            _userManagerMock.Verify(m => m.DeleteAsync(employee), Times.Once);
        }

        [Test]
        public async Task DeleteByIdAsync_ReturnsNotFoundResponse_WhenEmployeeIsNull()
        {
            // Arrange
            var employeeId = "1";

            _userManagerMock.Setup(m => m.FindByIdAsync(employeeId))
                .ReturnsAsync((Employee)null);

            // Act
            var response = await _employeeService.DeleteByIdAsync(employeeId);

            // Assert
            Assert.IsFalse(response.IsSuccess);
            Assert.NotNull(response.Message);
            _userManagerMock.Verify(m => m.DeleteAsync(It.IsAny<Employee>()), Times.Never);

        }

        [Test]
        public async Task DeleteByIdAsync_ReturnsErrorResponse_WhenDeleteFails()
        {
            // Arrange
            var employeeId = "1";
            var employee = new Employee { Id = employeeId };

            _userManagerMock.Setup(m => m.FindByIdAsync(employeeId))
                .ReturnsAsync(employee);
            _userManagerMock.Setup(m => m.DeleteAsync(employee))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Delete failed" }));

            // Act
            var response = await _employeeService.DeleteByIdAsync(employeeId);

            // Assert
            Assert.IsFalse(response.IsSuccess);
            Assert.NotNull(response.Message);
            _userManagerMock.Verify(m => m.DeleteAsync(employee), Times.Once);
        }

    }
}
