using Common.Data.Models;
using Liki24.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Liki24.Data.Dto
{
    public class DeliveryWindowModifyDto : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public WeekDay WeekDays { get; set; } = WeekDay.AllDays;

        public decimal Price { get; set; }

        public DateTime WindowAccessibleStartDate { get; set; }

        public DateTime WindowAccessibleEndDate { get; set; }

        public string Type { get; set; }

        //total minutes from start day
        public int DeliveryStartTime { get; set; }

        //total minutes from start day
        public int DeliveryEndTime { get; set; }

        //null = urgent
        public int? AvailableHoursBefore { get; set; }
    }
}
