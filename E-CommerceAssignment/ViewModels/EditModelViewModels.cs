using E_CommerceAssignment.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_CommerceAssignment.ViewModels
{
    public class EditModelViewModels : BrandModels
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression("[^ ]+ [^ ]+")]
        public string Name { get; set; }
        [Required]
        public List<BrandModels> Brands { get; set; }

        [Required]
        public List<CategoryModels> Categories { get; set; }
    }
}