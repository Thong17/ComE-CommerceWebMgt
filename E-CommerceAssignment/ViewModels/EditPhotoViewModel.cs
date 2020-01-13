using E_CommerceAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_CommerceAssignment.ViewModels
{
    public class EditPhotoViewModel
    {
        public string Model { get; set; }

        public List<HttpPostedFileBase> Photo { get; set; }

        public List<ProductPhoto> Photos { get; set; }

        public int ProductId { get; set; }

    }
}