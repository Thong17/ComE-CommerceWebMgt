using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_CommerceAssignment.Models
{
    public class CategoryModels
    {
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public string CategoryDetails { get; set; }
    }
}