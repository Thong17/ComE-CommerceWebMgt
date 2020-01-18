using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_CommerceAssignment.Models
{
    public class EditProductModels
    {
        public Guid Id { get; set; }

        public string Model { get; set; }

        public string Brand { get; set; }

        public string Category { get; set; }

        public double Price { get; set; }

        public string Color { get; set; }

        public string Storage { get; set; }

        public string Processor { get; set; }

        public string Memory { get; set; }

        public string Display { get; set; }

        public string Details { get; set; }

        public string EditedBy { get; set; }

        public DateTime EditedDate { get; set; }

        public int ProductId { get; set; }
    }
}