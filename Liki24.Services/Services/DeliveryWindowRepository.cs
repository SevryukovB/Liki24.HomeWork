using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Data.Ef;
using Liki24.Data;
using Liki24.Data.Dto;
using Liki24.Data.Ef;
using Liki24.Data.Enums;
using Liki24.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Liki24.Services.Services
{
    public class DeliveryWindowRepository : EfRepository<DeliveryWindow>, IDeliveryWindowRepository
    {
        public DeliveryWindowRepository(Liki24Context context) : base(context)
        {
        }

        public ICollection<DeliveryWindowViewDto> GetWindows(DateTime currentDate, int horizon)
        {
            var entities = this.AsNoTracking().Where(x => x.WindowAccessibleEndDate > currentDate);
            var data = new List<DeliveryWindowViewDto>();

            foreach (var item in entities)
            {
                var dateToCreate = currentDate.Date;
                for (int i = 0; i < horizon; i++)
                {
                    if (ContainsDayOfWeek(dateToCreate.DayOfWeek, item.WeekDays))
                    {
                        var viewDto = new DeliveryWindowViewDto
                        {
                            Name = item.Name,
                            Description = item.Description,
                            Start = dateToCreate.Date + item.DeliveryStartTime,
                            Finish = dateToCreate.Date + item.DeliveryEndTime,
                            Price = item.Price,
                            Type = item.Type,
                            Available = IsAvailable(currentDate, (dateToCreate.Date + item.DeliveryStartTime), 
                                (dateToCreate.Date + item.DeliveryEndTime), item.AvailableHoursBefore ?? 0, item.Type)
                        };

                        data.Add(viewDto);
                    }
                    
                    dateToCreate = dateToCreate.AddDays(1);
                }
            }

            return data;
        }

        private bool ContainsDayOfWeek(DayOfWeek dayOfWeek, WeekDay weekDay)
        {
            var dayOfWeekStr = dayOfWeek.ToString();
            var weekDaysStr = weekDay.ToString();

            return weekDaysStr.Contains(dayOfWeekStr);
        }

        private bool IsAvailable(DateTime currentDate, DateTime startTime, DateTime endTime, int hoursBefore, string type)
        {
            if (type == "regular")
            {
                return currentDate < startTime.AddHours(-hoursBefore);
            }
            
            //for urgent check if today first, then check range of time
            if (currentDate.Date == startTime.Date)
            {
                return currentDate >= startTime && currentDate < endTime;
            }

            return true;
        }
    }
}
