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
        public ActionResult Index(string search)
        {
            AppDbContext dbContext = new AppDbContext();
            ListProductViewModel listProduct = new ListProductViewModel();
            listProduct.Brands = dbContext.getBrands.ToList();

            List<ProductModels> products = dbContext.getProducts.ToList();
            List<GetProductViewModel> getProducts = new List<GetProductViewModel>();

            /*Get products details*/
            foreach(var item in products)
            {
                ModelModels model = dbContext.getModels.SingleOrDefault(m => m.Id == item.ModelId);
                BrandModels brand = dbContext.getBrands.SingleOrDefault(b => b.BrandId == model.BrandId);
                CategoryModels category = dbContext.getCategories.SingleOrDefault(c => c.CategoryId == model.CategoryId);

                GetProductViewModel getProduct = new GetProductViewModel
                {
                    Id = item.Id,
                    Name = model.Name,
                    Brand = brand.Brand,
                    Category = category.Category,
                    Price = item.Price,
                    Color = item.Color,
                    Storage = item.Storage,
                    Processor = item.Processor,
                    Memory = item.Memory,
                    Display = item.Display,
                    Details = item.Details,
                    CreatedBy = item.CreatedBy,
                    CreatedDate = item.CreatedDate
                };
                getProduct.Photos = dbContext.getProductPhotos(item.Id).ToList();

                getProducts.Add(getProduct);
            }

            /*Search product*/
            listProduct.Products = getProducts.Where(p => p.Name.StartsWith(search ?? "", StringComparison.OrdinalIgnoreCase)).ToList();
            
            /*Get the number of each product in a brand*/
            listProduct.EachProductsOfBrands = new List<int>();
            foreach(var brand in listProduct.Brands)
            {
                List<ProductModels> productsOfBrands = dbContext.getProductBrands(brand.BrandId).ToList();

                int numberOfBrand = productsOfBrands.Count;
                listProduct.EachProductsOfBrands.Add(numberOfBrand);
            }
            
            /*Create Index view with product details*/
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
                Category = category.Category,
                CategoryId = category.CategoryId
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
                productModels.CategoryId = product.CategoryId;
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

                        /*Get Id from the addProduct cmd.ExecuteScalar()*/
                        ProductId = productModels.Id
                    };
                    productModels.Photos.Add(photo);

                    item.SaveAs(photoPath);
                }

                dbContext.addProductPhoto(productModels.Photos);

                return RedirectToAction("Index");
                
            }

            return View();
        }

        [HttpGet]
        public ActionResult EditBrand(int id)
        {
            AppDbContext dbContext = new AppDbContext();
            BrandModels brand = dbContext.getBrands.SingleOrDefault(b => b.BrandId == id);

            return View(brand);
        }

        [HttpPost]
        public ActionResult EditBrand(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                int Id = Convert.ToInt32(collection["BrandId"]);
                string Brand = collection["Brand"];
                string Details = collection["BrandDetails"];

                AppDbContext dbContext = new AppDbContext();
                BrandModels brand = new BrandModels
                {
                    BrandId = Id,
                    Brand = Brand,
                    BrandDetails = Details
                };
                dbContext.updateBrand(brand);

                return RedirectToAction("Brands");
            }
            return View(collection);
        }

        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            AppDbContext dbContext = new AppDbContext();
            CategoryModels category = dbContext.getCategories.SingleOrDefault(c => c.CategoryId == id);

            return View(category);
        }

        [HttpPost]
        public ActionResult EditCategory(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                int Id = Convert.ToInt32(collection["CategoryId"]);
                string Category = collection["Category"];
                string Details = collection["CategoryDetails"];

                AppDbContext dbContext = new AppDbContext();
                CategoryModels category = new CategoryModels
                {
                    CategoryId = Id,
                    Category = Category,
                    CategoryDetails = Details
                };
                dbContext.updateCategory(category);

                return RedirectToAction("Categories");
            }

            return View(collection);
        }

        public ActionResult EditProduct(int id)
        {
            AppDbContext dbContext = new AppDbContext();

            ProductModels product = dbContext.getProducts.SingleOrDefault(p => p.Id == id);
            ModelModels model = dbContext.getModels.SingleOrDefault(m => m.Id == product.ModelId);
            List<BrandModels> brands = dbContext.getBrands.ToList();
            List<CategoryModels> categories = dbContext.getCategories.ToList();
            List<ProductPhoto> photos = dbContext.getProductPhotos(product.Id).ToList();
            List<ModelModels> models = dbContext.getModelBrands(model.BrandId).ToList();
            EditProductViewModel viewModel = new EditProductViewModel
            {
                Id = product.Id,
                Models = models,
                ModelId = product.ModelId,
                Brands = brands,
                BrandId = product.BrandId,
                Categories = categories,
                CategoryId = product.CategoryId,
                Price = product.Price,
                Color = product.Color,
                Storage = product.Storage,
                Processor = product.Processor,
                Memory = product.Memory,
                Display = product.Display,
                Details = product.Details,
                CreatedBy = product.CreatedBy,
                CreatedDate = product.CreatedDate,
                Photos = photos
            };


            return View(viewModel);
        }
    }
}