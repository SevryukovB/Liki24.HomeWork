using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Liki24.Data;
using Liki24.Data.Dto;
using Liki24.Data.Enums;
using Liki24.Services.Abstractions;

namespace Liki24.Test
{
    public class FakeDeliveryWindowRepository : IDeliveryWindowRepository
    {
        private readonly List<DeliveryWindow> _deliveryWindows;
        private bool _disposed;

        public FakeDeliveryWindowRepository()
        {
            _deliveryWindows = new List<DeliveryWindow>();
            _deliveryWindows.Add(new DeliveryWindow
            {
                Id = 1,
                Name = "Срочная доставка",
                Description = "Доставка за 1-2 часа",
                WindowAccessibleStartDate = new DateTime(2020, 1, 1),
                WindowAccessibleEndDate = new DateTime(2021, 1, 1),
                DeliveryStartTime = new TimeSpan(9, 0, 0),
                DeliveryEndTime = new TimeSpan(14, 0, 0),
                WeekDays = WeekDay.Wednesday | WeekDay.Friday | WeekDay.Saturday,
                Price = 100,
                Type = "urgent"
            });

            _deliveryWindows.Add(new DeliveryWindow
            {
                Id = 2,
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

            _deliveryWindows.Add(new DeliveryWindow
            {
                Id = 3,
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
        }

        public Type ElementType { get; }
        public Expression Expression { get; }
        public IQueryProvider Provider { get; }

        public DeliveryWindow Create(DeliveryWindow entity)
        {
            entity.Id = 1 + _deliveryWindows.Max(x => x.Id);
            _deliveryWindows.Add(entity);
            return entity;
        }

        public DeliveryWindow Update(DeliveryWindow entity)
        {
            _deliveryWindows[_deliveryWindows.FindIndex(ind => ind.Id == entity.Id)] = entity;
            return entity;
        }

        public DeliveryWindow GetById(int id)
        {
            return _deliveryWindows
                .FirstOrDefault(a => a.Id == id);
        }

        public void Delete(int entityId)
        {
            var existing = _deliveryWindows.First(a => a.Id == entityId);
            _deliveryWindows.Remove(existing);
        }

        public ICollection<DeliveryWindowViewDto> GetWindows(DateTime startTime, int horizon)
        {
            return new List<DeliveryWindowViewDto>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<DeliveryWindow> GetEnumerator()
        {
            return _deliveryWindows.GetEnumerator();
        }

        #region IDisposable implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            _disposed = true;
            if (disposing) _deliveryWindows?.Clear();
        }

        #endregion
    }
}
