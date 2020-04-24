using System;
using System.Collections.Generic;
using System.Text;

namespace Liki24.Data.Dto
{
    public class DeliveryWindowViewDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime Finish { get; set; }

        public decimal Price { get; set; }

        public string Type { get; set; }

        public bool Available { get; set; }
    }
}
