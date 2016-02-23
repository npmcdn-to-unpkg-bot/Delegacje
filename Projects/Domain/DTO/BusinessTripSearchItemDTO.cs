using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Domain.DTO
{
    public class BusinessTripSearchItemDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Reason { get; set; }
        public string Purpose { get; set; }
        public string Date { get; set; }
        public string Note { get; set; }
        public decimal Total { get; set; }
    }
}
