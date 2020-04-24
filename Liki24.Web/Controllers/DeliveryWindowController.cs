using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common.Data.Data;
using Liki24.Data;
using Liki24.Data.Dto;
using Liki24.Data.Enums;
using Liki24.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Liki24.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryWindowController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDeliveryWindowRepository _deliveryWindowRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeliveryWindowController(IMapper mapper, IUnitOfWork unitOfWork, IDeliveryWindowRepository deliveryWindowRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _deliveryWindowRepository = deliveryWindowRepository;
        }

        [HttpPost]
        public IActionResult Create(DeliveryWindowModifyDto request)
        {
            var entity = _mapper.Map<DeliveryWindow>(request);
            var createdEntity = _deliveryWindowRepository.Create(entity);
            _unitOfWork.SaveChanges();
            return Ok(createdEntity);
        }

        [HttpPut]
        public IActionResult Update(DeliveryWindowModifyDto request)
        {
            var entity = _mapper.Map<DeliveryWindow>(request);
            var updatedEntity = _deliveryWindowRepository.Update(entity);
            _unitOfWork.SaveChanges();
            return Ok(updatedEntity);
        }

        [HttpGet("available-windows")]
        public IActionResult GetWindows(DateTime currentDate, int horizon)
        {
            return Ok(_deliveryWindowRepository.GetWindows(currentDate, horizon));
        }

        [HttpGet]
        public void Get()
        {
            //SEEDING for test
            _deliveryWindowRepository.Create(new DeliveryWindow
            {
                Name = "Срочная доставка",
                Description = "Доставка за 1-2 часа",
                WindowAccessibleStartDate = new DateTime(2020, 1, 1),
                WindowAccessibleEndDate = new DateTime(2021, 1, 1),
                DeliveryStartTime = new TimeSpan(9,0,0),
                DeliveryEndTime = new TimeSpan(14, 0, 0),
                WeekDays = WeekDay.Wednesday | WeekDay.Friday| WeekDay.Saturday,
                Price = 100,
                Type = "urgent"
            });

            _deliveryWindowRepository.Create(new DeliveryWindow
            {
                Name = "Срочная доставка",
                Description = "Доставка за 30 минут",
                WindowAccessibleStartDate = new DateTime(2020, 1, 1),
                WindowAccessibleEndDate = new DateTime(2021, 1, 1),
                DeliveryStartTime = new TimeSpan(9, 0, 0),
                DeliveryEndTime = new TimeSpan(21, 0, 0),
                WeekDays = WeekDay.Wednesday | WeekDay.Friday,
                Price = 200,
                Type = "urgent"
            });

            _deliveryWindowRepository.Create(new DeliveryWindow
            {
                Name = "14:00 - 18:00",
                Description = "Доставка 14:00 - 18:00",
                WindowAccessibleStartDate = new DateTime(2020, 1, 1),
                WindowAccessibleEndDate = new DateTime(2021, 1, 1),
                DeliveryStartTime = new TimeSpan(14, 0, 0),
                DeliveryEndTime = new TimeSpan(18, 0, 0),
                WeekDays = WeekDay.Sunday | WeekDay.Saturday | WeekDay.Friday,
                Price = 50,
                Type = "regular",
                AvailableHoursBefore = 3
            });

            _unitOfWork.SaveChanges();
        }
    }
}