using E_CommerceAssignment.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_CommerceAssignment.ViewModels
{
    public class EditProductViewModel : ProductModels
    {
        public List<BrandModels> Brands { get; set; }
        public List<CategoryModels> Categories { get; set; }
        public List<ModelModels> Models { get; set; }
        [DataType(DataType.Upload)]
        public List<HttpPostedFileBase> Photo { get; set; }
    }
}