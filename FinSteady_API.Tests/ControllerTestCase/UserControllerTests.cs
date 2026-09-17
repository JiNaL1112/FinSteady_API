using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FinSteady_API.Models;
using FinSteady_API.Repositories.Interface;
using FinSteady_API.Controllers;
using FinSteady_API.Models.Request;
using FinSteady_API.Models.Request.Users;
using FinSteady_API.Controllers;
using FinSteady_API.Infrastructure;

namespace FinSteady_API.Tests.ControllerTestCase
{
    [TestFixture]
    public class UserControllerTests
    {
        private Mock<IUserRepository> _mockRepo;
        private UserController _controller;
        private APIResponse _response;

        [SetUp]
        public void SetUp()
        {
            _mockRepo = new Mock<IUserRepository>();
            _controller = new UserController(_mockRepo.Object);
            _response = new APIResponse();
        }

        [Test]
        public async Task GetUser_ReturnsOk_WhenUserExists()
        {
            // Arrange
            var userId = 1;
            var user = new User { UserId = userId };
            _mockRepo.Setup(repo => repo.GetUserById(userId)).ReturnsAsync(user);

            // Act
            var result = await _controller.GetUser(userId);

            // Assert
            var okResult = result.Result as OkObjectResult;
            var response = okResult?.Value as APIResponse;
            Assert.AreEqual(HttpStatusCode.OK, response?.StatusCode);
            Assert.AreEqual(user, response?.Result);
        }

        [Test]
        public async Task GetUser_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = 1;
            _mockRepo.Setup(repo => repo.GetUserById(userId)).ReturnsAsync((User)null);

            // Act
            var result = await _controller.GetUser(userId);

            // Assert
            var notFoundResult = result.Result as NotFoundObjectResult;
            var response = notFoundResult?.Value as APIResponse;
            Assert.AreEqual(HttpStatusCode.NotFound, response?.StatusCode);
        }

        [Test]
        public async Task GetUsers_ReturnsOk_WithUsers()
        {
            // Arrange
            var users = new List<User> { new User(), new User() };
            _mockRepo.Setup(repo => repo.GetUsers()).ReturnsAsync(users);

            // Act
            var result = await _controller.GetUsers();

            // Assert
            var okResult = result.Result as OkObjectResult;
            var response = okResult?.Value as APIResponse;
            Assert.AreEqual(HttpStatusCode.OK, response?.StatusCode);
            Assert.AreEqual(users, response?.Result);
        }

        [Test]
        public async Task DeleteUser_ReturnsNoContent_WhenUserDeleted()
        {
            // Arrange
            var userId = 1;
            var user = new User { UserId = userId };
            _mockRepo.Setup(repo => repo.GetUserById(userId)).ReturnsAsync(user);
            _mockRepo.Setup(repo => repo.DeleteUser(user)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteUser(userId);

            // Assert
            var okResult = result.Result as OkObjectResult;
            var response = okResult?.Value as APIResponse;
            Assert.AreEqual(HttpStatusCode.NoContent, response?.StatusCode);
        }

        [Test]
        public async Task UpdateUser_ReturnsNoContent_WhenUserUpdated()
        {
            // Arrange
            var userId = 1;
            var updateDTO = new UserRequestModel { Email = "test@example.com" };
            var user = new User { UserId = userId, Email = "test@example.com" };
            _mockRepo.Setup(repo => repo.GetUserById(userId)).ReturnsAsync(user);
           _mockRepo.Setup(repo => repo.UpdateUser(user, It.IsAny<User>())).ReturnsAsync(user);

            // Act
            var result = await _controller.UpdateUser(userId, updateDTO);

            // Assert
            var okResult = result.Result as OkObjectResult;
            var response = okResult?.Value as APIResponse;
            Assert.AreEqual(HttpStatusCode.NoContent, response?.StatusCode);
        }

        [Test]
        public async Task Login_ReturnsOk_WhenCredentialsAreValid()
        {
            // Arrange
            var loginRequest = new LoginRequestModel { Email = "test@example.com", Password = "password" };
            var user = new User { Email = "test@example.com", PasswordHash = "password" };
            _mockRepo.Setup(repo => repo.GetUsers()).ReturnsAsync(new List<User> { user });

            // Act
            var result = await _controller.login(loginRequest);

            // Assert
            var okResult = result.Result as OkObjectResult;
            var response = okResult?.Value as APIResponse;
            Assert.AreEqual(HttpStatusCode.NoContent, response?.StatusCode);
            Assert.AreEqual(user, response?.Result);
        }

        [Test]
        public async Task ForgotPassword_ReturnsOk_WhenEmailExists()
        {
            // Arrange
            var forgotPasswordRequest = new ForgotPasswordRequestModel { Email = "test@example.com" };
            var user = new User { Email = "test@example.com" };
            _mockRepo.Setup(repo => repo.GetUsers()).ReturnsAsync(new List<User> { user });

            // Act
            var result = await _controller.forgotPassword(forgotPasswordRequest);

            // Assert
            var okResult = result.Result as OkObjectResult;
            var response = okResult?.Value as APIResponse;
            Assert.AreEqual(HttpStatusCode.NoContent, response?.StatusCode);
            Assert.AreEqual("Email sent successfully", response?.Result);
        }

        [Test]
        public async Task ResetPassword_ReturnsOk_WhenPasswordsMatch()
        {
            // Arrange
            var userId = 1;
            var resetPasswordModel = new ResetPasswordModel { Password = "newPassword", ConfirmPassword = "newPassword" };
            var user = new User { UserId = userId };
            _mockRepo.Setup(repo => repo.GetUserById(userId)).ReturnsAsync(user);
            _mockRepo.Setup(repo => repo.UpdateUser(user, user)).ReturnsAsync(user);

            // Act
            var result = await _controller.resetPassword(userId, resetPasswordModel);

            // Assert
            var okResult = result.Result as OkObjectResult;
            var response = okResult?.Value as APIResponse;
            Assert.AreEqual(HttpStatusCode.NoContent, response?.StatusCode);
            Assert.AreEqual("Password update successfully", response?.Result);
        }

        [Test]
        public async Task UserRegistration_ReturnsCreated_WhenUserIsCreated()
        {
            // Arrange
            var createDTO = new UserRequestModel { Email = "test@example.com" };
            var user = new User { UserId = 1, Email = "test@example.com" };
            _mockRepo.Setup(repo => repo.AddUser(It.IsAny<User>())).ReturnsAsync(user);

            // Act
            var result = await _controller.userRegistration(createDTO);

            // Assert
            var createdResult = result.Result as CreatedAtRouteResult;
            var response = createdResult?.Value as APIResponse;
            Assert.AreEqual(HttpStatusCode.Created, response?.StatusCode);
            Assert.AreEqual(user, response?.Result);
        }
    }
}
