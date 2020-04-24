using System;
using System.Collections.Generic;
using AutoMapper;
using Common.Data.Data;
using Liki24.Data;
using Liki24.Data.Dto;
using Liki24.Data.Enums;
using Liki24.Services.Abstractions;
using Liki24.Web.Controllers;
using Liki24.Web.Mappings;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Liki24.Test
{
    public class DeliveryWindowControllerTest
    {
        private readonly DeliveryWindowController _controller;
        private readonly IDeliveryWindowRepository _deliveryWindowRepository;
        private readonly Mock<IUnitOfWork> _mockUnitWork;

        public DeliveryWindowControllerTest()
        {
            _mockUnitWork = new Mock<IUnitOfWork>();
            
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DeliveryWindowProfile());
            });
            var mapper = mockMapper.CreateMapper();
            _deliveryWindowRepository = new FakeDeliveryWindowRepository();
            _controller = new DeliveryWindowController(mapper, _mockUnitWork.Object, _deliveryWindowRepository);
        }

        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            var testItem = GetDeliveryWindowModify();

            // Act
            var createdResponse = _controller.Create(testItem);

            // Assert
            Assert.IsType<OkObjectResult>(createdResponse);
        }


        [Fact]
        public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = GetDeliveryWindowModify();

            // Act
            var createdResponse = _controller.Create(testItem) as OkObjectResult;
            var item = createdResponse.Value as DeliveryWindow;

            // Assert
            Assert.IsType<DeliveryWindow>(item);
            Assert.Equal("Срочная доставка", item.Name);
        }

        [Fact]
        public void Update_ValidObjectPassed_ReturnsUpdatedResponse()
        {
            // Arrange
            var testItem = GetDeliveryWindowModify();
            testItem.Id = 1;

            // Act
            var createdResponse = _controller.Update(testItem);

            // Assert
            Assert.IsType<OkObjectResult>(createdResponse);
        }


        [Fact]
        public void Update_ValidObjectPassed_ReturnedResponseHasUpdatedItem()
        {
            // Arrange
            var testItem =  GetDeliveryWindowModify();
            testItem.Id = 1;
            testItem.Name = "new name";

            // Act
            var createdResponse = _controller.Update(testItem) as OkObjectResult;
            var item = createdResponse.Value as DeliveryWindow;

            // Assert
            Assert.IsType<DeliveryWindow>(item);
            Assert.Equal("new name", item.Name);
        }

        [Fact]
        public void Get_ValidObjectPassed_ReturnsGetResponse()
        {
            // Act
            var createdResponse = _controller.GetWindows(DateTime.Now, 3);

            // Assert
            Assert.IsType<OkObjectResult>(createdResponse);
        }

        [Fact]
        public void Get_ValidObjectPassed_ReturnedResponseHasGetItems()
        {
            // Act
            var createdResponse = _controller.GetWindows(DateTime.Now, 3) as OkObjectResult;
            var items = createdResponse.Value as IEnumerable<DeliveryWindowViewDto>;

            // Assert
            Assert.IsType<List<DeliveryWindowViewDto>>(items);
        }

        private DeliveryWindowModifyDto GetDeliveryWindowModify()
        {
            return new DeliveryWindowModifyDto
            {
                Name = "Срочная доставка",
                Description = "Доставка за 1-2 часа",
                WindowAccessibleStartDate = new DateTime(2020, 1, 1),
                WindowAccessibleEndDate = new DateTime(2021, 1, 1),
                Price = 100,
                Type = "regular",
                WeekDays = WeekDay.AllDays,
                DeliveryStartTime = 1140,
                DeliveryEndTime = 1840,
                AvailableHoursBefore = 3
            };
        }
    }
}
