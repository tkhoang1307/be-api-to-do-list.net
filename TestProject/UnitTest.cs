using AutoFixture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList;
using ToDoList.Controllers;
using ToDoList.Entities;
using ToDoList.Model.RequestModels.Status;
using ToDoList.Model.ResponseModels;
using ToDoList.Model.ResponseModels.Status;
using ToDoList.Repository.IRepositories;
using ToDoList.Repository.Repositories;
using ToDoList.Service.IServices;
using ToDoList.Service.Services;
using ToDoList.UnitOfWork;
using Xunit;

namespace TestProject
{
    public class UnitTest
    {
        [Fact]
        public async void CreateNewStatus_ShouldReturnOkApiResponse_UnitTestService()
        {
            //Arrange
            var fixture = new Fixture();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockStatusRepository = new Mock<IStatusRepository>();

            var statusService = new StatusService(mockUnitOfWork.Object);

            var statusCreationModel = fixture.Create<StatusCreationModel>();

            var createdStatus = new Status
            {
                Id = Guid.NewGuid(),
                Name = statusCreationModel.Name,
            };

            var expectedApiResponse = new ApiResponse().SetOk(new StatusResponseModel
            {
                Id = createdStatus.Id,
                Name = createdStatus.Name
            });

            mockUnitOfWork.Setup(u => u.StatusRepository).Returns(mockStatusRepository.Object);
            mockStatusRepository.Setup(repo => repo.CreateNewStatus(It.IsAny<StatusCreationModel>()))
                               .ReturnsAsync(createdStatus);    

            //Act
            var result = await statusService.CreateNewStatus(statusCreationModel);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(expectedApiResponse.StatusCode, result.StatusCode);

            var actualResponseData = Assert.IsType<StatusResponseModel>(result.Data);
            var expectedResponseData = Assert.IsType<StatusResponseModel>(expectedApiResponse.Data);

            Assert.Equal(expectedResponseData.Id, actualResponseData.Id);
            Assert.Equal(expectedResponseData.Name, actualResponseData.Name);

            //Assert.Equal(expectedApiResponse.Data, actualResponseData);
        }

        //[Fact]
        [Theory]
        [InlineData("f7cf98e1-8f10-490e-b85a-ad1954ff6fbc")]
        [InlineData("f7cf98e1-8f10-490e-b85a-ad1954ff5fbc")]
        public async void UpdateStatus_ShouldReturnApiResponse_WithUpdatedStatusData(Guid statusId)
        {
            // Arrange
            var fixture = new Fixture();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockStatusRepository = new Mock<IStatusRepository>();

            var statusService = new StatusService(mockUnitOfWork.Object);

            //var statusId = Guid.NewGuid();
            var statusUpdationRequest = fixture.Create<StatusUpdationRequestModel>();

            var updatedStatus = new Status
            {
                Id = statusId,
                Name = statusUpdationRequest.Name,
            };

            mockUnitOfWork.Setup(u => u.StatusRepository).Returns(mockStatusRepository.Object);
            mockStatusRepository.Setup(repo => repo.IsExistedStatus(statusId)).ReturnsAsync(true);
            mockStatusRepository.Setup(repo => repo.UpdateStatus(statusId, statusUpdationRequest)).ReturnsAsync(updatedStatus);

            // Act
            var result = await statusService.UpdateStatus(statusId, statusUpdationRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);

            if (result.IsSuccess)
            {
                var expectedApiResponse = new ApiResponse().SetOk(new StatusResponseModel
                {
                    Id = updatedStatus.Id,
                    Name = updatedStatus.Name,
                });

                Assert.Equal(expectedApiResponse.StatusCode, result.StatusCode);

                var actualResponseDataSuccessfully = Assert.IsType<StatusResponseModel>(result.Data);
                var expectedResponseDataSuccessfully = Assert.IsType<StatusResponseModel>(expectedApiResponse.Data);

                Assert.Equal(expectedResponseDataSuccessfully.Id, actualResponseDataSuccessfully.Id);
                Assert.Equal(expectedResponseDataSuccessfully.Name, actualResponseDataSuccessfully.Name);
            }
            else
            {
                var expectedApiResponseFail = new ApiResponse().SetBadRequest(null, "This status existed");
                
                Assert.Equal(expectedApiResponseFail.StatusCode, result.StatusCode);
                Assert.Equal(expectedApiResponseFail.Data, result.Data);
                Assert.Equal(expectedApiResponseFail.ErrorMessage, result.ErrorMessage);

            }
        }
    }
}