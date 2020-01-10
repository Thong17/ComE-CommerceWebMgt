using E_CommerceAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_CommerceAssignment.ViewModels
{
    public class AddProductViewModel : ProductModels
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Category { get; set; }
        public List<HttpPostedFileBase> Photo { get; set; }
    }
}