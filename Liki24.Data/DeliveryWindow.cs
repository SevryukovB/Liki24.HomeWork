using Common.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Liki24.Data.Enums;

namespace Liki24.Data
{
    public class DeliveryWindow : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public WeekDay WeekDays { get; set; } = WeekDay.AllDays;

        public decimal Price { get; set; }

        public DateTime WindowAccessibleStartDate { get; set; }

        public DateTime WindowAccessibleEndDate { get; set; }

        public string Type { get; set; }

        public TimeSpan DeliveryStartTime { get; set; }

        public TimeSpan DeliveryEndTime { get; set; }

        //null = urgent
        public int? AvailableHoursBefore { get; set; } 
    }
}
