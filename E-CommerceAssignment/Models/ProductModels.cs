using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_CommerceAssignment.Models
{
    public class ProductModels
    {
        public int Id { get; set; }
        [Required]
        public double Price { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public string Storage { get; set; }

        [Required]
        public string Processor { get; set; }

        [Required]
        public string Memory { get; set; }

        [Required]
        public string Display { get; set; }

        public string Details { get; set; }

        public int ModelId { get; set; }

        public int BrandId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<ProductPhoto> Photos { get; set; }
    }
}