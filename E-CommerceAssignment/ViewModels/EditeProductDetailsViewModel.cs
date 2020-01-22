using E_CommerceAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_CommerceAssignment.ViewModels
{
    public class EditeProductDetailsViewModel
    {
        public List<EditProductModels> OldProduct { get; set; }

        public List<GetProductViewModel> NewProduct { get; set; }
    }
}