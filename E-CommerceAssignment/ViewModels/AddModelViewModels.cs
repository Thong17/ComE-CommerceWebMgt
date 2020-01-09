using E_CommerceAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_CommerceAssignment.ViewModels
{
    public class AddModelViewModels : BrandModels 
    {
        public string Name { get; set; }
        public List<BrandModels> Brands { get; set; }
        public List<CategoryModels> Categories { get; set; }
    }
}