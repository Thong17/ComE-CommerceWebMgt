using E_CommerceAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_CommerceAssignment.ViewModels
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public List<ProductModels> Products { get; set; }

    }
}