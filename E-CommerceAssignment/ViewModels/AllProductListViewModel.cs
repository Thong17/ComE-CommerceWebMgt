using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_CommerceAssignment.ViewModels
{
    public class AllProductListViewModel
    {
        public int Result { get; set; }

        public IPagedList Pages { get; set; }

        public List<GetProductViewModel> Products { get; set; }
    }
}