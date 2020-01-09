using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_CommerceAssignment.Models
{
    public class ModelModels 
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int BrandId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}