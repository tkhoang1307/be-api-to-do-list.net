using AutoFixture;
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
    }
}