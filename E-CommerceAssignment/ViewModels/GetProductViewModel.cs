using E_CommerceAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_CommerceAssignment.ViewModels
{
    public class GetProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
        public string Storage { get; set; }
        public string Processor { get; set; }
        public string Memory { get; set; }
        public string Display { get; set; }
        public string Details { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<ProductPhoto> Photos { get; set; }
    }
}