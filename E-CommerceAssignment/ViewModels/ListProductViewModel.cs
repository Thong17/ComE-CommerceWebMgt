using E_CommerceAssignment.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_CommerceAssignment.ViewModels
{
    public class ListProductViewModel
    {
        public List<BrandModels> Brands { get; set; }
        public IPagedList<GetProductViewModel> Products { get; set; }
        public List<int> EachProductsOfBrands { get; set; }
    }
}