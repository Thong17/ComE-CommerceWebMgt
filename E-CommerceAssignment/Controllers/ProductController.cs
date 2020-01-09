using E_CommerceAssignment.Models;
using E_CommerceAssignment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace E_CommerceAssignment.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            AppDbContext dbContext = new AppDbContext();
            ListProductViewModel listProduct = new ListProductViewModel();
            listProduct.Brands = dbContext.getBrands.ToList();
            listProduct.Products = dbContext.getProducts.ToList();
            
            return View(listProduct);
        }

        [HttpGet]
        public ActionResult AddBrand()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBrand(BrandModels model)
        {
            if (ModelState.IsValid)
            {
                AppDbContext dbContext = new AppDbContext();
                BrandModels brand = new BrandModels
                {
                    Brand = model.Brand,
                    BrandDetails = model.BrandDetails + "_create by " + User.Identity.Name + " on " + DateTime.Now
                };

                dbContext.addBrand(brand);
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryModels model)
        {
            if (ModelState.IsValid)
            {
                AppDbContext dbContext = new AppDbContext();
                CategoryModels category = new CategoryModels
                {
                    Category = model.Category,
                    CategoryDetails = model.CategoryDetails + "_create by " + User.Identity.Name + " on " + DateTime.Now
                };
                dbContext.addCategory(category);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Brands()
        {
            AppDbContext dbContext = new AppDbContext();
            List<BrandModels> brands = dbContext.getBrands.ToList();

            return View(brands);
        }

        public ActionResult Categories()
        {
            AppDbContext dbContext = new AppDbContext();
            List<CategoryModels> categories = dbContext.getCategories.ToList();

            return View(categories);
        }

        [HttpGet]
        public ActionResult AddModel()
        {
            AppDbContext dbContext = new AppDbContext();
            List<BrandModels> brands = dbContext.getBrands.ToList();
            List<CategoryModels> categories = dbContext.getCategories.ToList();

            AddModelViewModels viewModels = new AddModelViewModels();

            viewModels.Brands = brands;
            viewModels.Categories = categories;
            
            return View(viewModels);
        }

        [HttpPost]
        public ActionResult AddModel(ModelModels models)
        {
            if (ModelState.IsValid)
            {
                AppDbContext dbContext = new AppDbContext();
                ModelModels model = new ModelModels
                {
                    Name = models.Name,
                    BrandId = models.BrandId,
                    CategoryId = models.CategoryId,
                    CreatedBy = User.Identity.Name,
                    CreatedDate = DateTime.Now
                };

                dbContext.addModel(model);

                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Models()
        {
            AppDbContext dbContext = new AppDbContext();
            List<ModelModels> models = dbContext.getModels.ToList();

            return View(models);
        }

        public ActionResult GetModel(int id)
        {
            AppDbContext dbContext = new AppDbContext();
            List<ModelModels> models = dbContext.getModelBrands(id).ToList();

            return View(models);
        }

        [HttpGet]
        public ActionResult AddProduct(int id)
        {
            AppDbContext dbContext = new AppDbContext();
            ModelModels model = dbContext.getModels.SingleOrDefault(m => m.Id == id);

            BrandModels brand = dbContext.getBrands.SingleOrDefault(b => b.BrandId == model.BrandId);
            CategoryModels category = dbContext.getCategories.SingleOrDefault(c => c.CategoryId == model.CategoryId);

            AddProductViewModel addProductView = new AddProductViewModel
            {
                Brand = brand.Brand,
                BrandId = brand.BrandId,
                Model = model.Name,
                ModelId = model.Id,
                Category = category.Category
            };

            return View(addProductView);
        }

        public ActionResult Products(int id)
        {
            AppDbContext dbContext = new AppDbContext();
            List<ProductModels> products = dbContext.getProductModels(id).ToList();
            
            if (products.Count > 0)
            {
                ProductViewModel productView = new ProductViewModel();
                ModelModels model = dbContext.getModels.SingleOrDefault(m => m.Id == id);
                BrandModels brand = dbContext.getBrands.SingleOrDefault(b => b.BrandId == model.BrandId);
                CategoryModels category = dbContext.getCategories.SingleOrDefault(c => c.CategoryId == model.CategoryId);
                foreach (var item in products.ToList())
                {
                    ProductModels product = new ProductModels
                    {
                        Id = item.Id,
                        Price = item.Price,
                        Color = item.Color,
                        Storage = item.Storage,
                        Processor = item.Processor,
                        Memory = item.Memory,
                        Display = item.Display,
                        Details = item.Details,
                        BrandId = item.BrandId,
                        ModelId = item.ModelId,
                        CreatedBy = item.CreatedBy,
                        CreatedDate = item.CreatedDate
                    };

                    List<ProductPhoto> photos = dbContext.getProductPhotos(item.Id).ToList();

                    product.Photos = photos;


                    productView.Name = model.Name;
                    productView.Brand = brand.Brand;
                    productView.Category = category.Category;
                    productView.Products = products;

                }
                return View(productView);
            }

            return View("Error");
        }

        [HttpPost]
        public ActionResult AddProduct(List<HttpPostedFileBase> Photo, AddProductViewModel product)
        {

            if (ModelState.IsValid)
            {
                AppDbContext dbContext = new AppDbContext();
                ProductModels productModels = new ProductModels();
                productModels.Price = product.Price;
                productModels.Color = product.Color;
                productModels.Storage = product.Storage;
                productModels.Processor = product.Processor;
                productModels.Memory = product.Memory;
                productModels.Display = product.Display;
                productModels.Details = product.Details;
                productModels.BrandId = product.BrandId;
                productModels.ModelId = product.ModelId;
                productModels.CreatedBy = User.Identity.Name;
                productModels.CreatedDate = DateTime.Now;

                dbContext.addProduct(productModels);


                productModels.Photos = new List<ProductPhoto>();
                foreach(var item in Photo)
                {
                    var fileName = Path.GetFileName(item.FileName);
                    var path = "/Public/Products/" + product.Model;
                    var photoUrl = Server.MapPath(path);
                    var photoTitle = Path.GetFileNameWithoutExtension(item.FileName);
                    var uniqName = Guid.NewGuid().ToString() + "_" + fileName;
                    if (!Directory.Exists(photoUrl))
                    {
                        Directory.CreateDirectory(photoUrl);
                    }

                    var photoPath = Path.Combine(photoUrl, uniqName);
                    ProductPhoto photo = new ProductPhoto
                    {
                        Path = path,
                        Src = photoPath,
                        Title = uniqName,

                        /*Working on get last index for created product*/
                        ProductId = 11
                    };
                    productModels.Photos.Add(photo);

                    item.SaveAs(photoPath);
                }

                dbContext.addProductPhoto(productModels.Photos);

                return RedirectToAction("Index");
                
            }

            return View();
        }
    }
}