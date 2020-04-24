using Common.Data.Data;
using Liki24.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Liki24.Data.Dto;

namespace Liki24.Services.Abstractions
{
    public interface IDeliveryWindowRepository : IRepository<DeliveryWindow>
    {
        ICollection<DeliveryWindowViewDto> GetWindows(DateTime startTime, int horizon);
    }
}
