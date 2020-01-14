using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_CommerceAssignment.ViewModels
{
    public class EditModelViewModels
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public int BrandId { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
    }
}