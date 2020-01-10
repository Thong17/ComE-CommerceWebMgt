using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_CommerceAssignment.Models
{
    public class BrandModels : CategoryModels
    {
        public int BrandId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Brand { get; set; }
        public string BrandDetails { get; set; }
    }
}