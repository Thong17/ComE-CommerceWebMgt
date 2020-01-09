using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_CommerceAssignment.Models
{
    public class ProductPhoto
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public string Src { get; set; }

        public string Title { get; set; }

        public int ProductId { get; set; }
    }
}