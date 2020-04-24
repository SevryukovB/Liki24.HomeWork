﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Liki24.Data.Enums
{
    [Flags]
    public enum WeekDay
    {
        Sunday = 1,
        Monday = 2,
        Tuesday = 4,
        Wednesday = 8,
        Thursday = 16,
        Friday = 32,
        Saturday = 64,
        AllDays = WeekDay.Sunday | WeekDay.Monday | WeekDay.Tuesday | WeekDay.Wednesday | WeekDay.Thursday | WeekDay.Friday | WeekDay.Saturday
    }
}
