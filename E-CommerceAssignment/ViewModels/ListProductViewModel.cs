using E_CommerceAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_CommerceAssignment.ViewModels
{
    public class ListProductViewModel
    {
        public List<BrandModels> Brands { get; set; }
        public List<GetProductViewModel> Products { get; set; }
        public List<int> EachProductsOfBrands { get; set; }
    }
}