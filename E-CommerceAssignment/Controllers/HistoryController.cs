using E_CommerceAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_CommerceAssignment.Controllers
{
    [Authorize]
    public class HistoryController : Controller
    {
        AppDbContext dbContext = new AppDbContext();
        // GET: History
        public ActionResult Index()
        {
            List<EditProductModels> products = dbContext.getEditedProducts.OrderByDescending(p => p.EditedDate).ToList();
            return View(products);
        }
    }
}