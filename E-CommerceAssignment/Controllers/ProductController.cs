using E_CommerceAssignment.Models;
using E_CommerceAssignment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace E_CommerceAssignment.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        public ActionResult Index(string search, int? page)
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

            /*Search product and sort by date*/
            listProduct.Products = getProducts.Where(p => p.Name.StartsWith(search ?? "", StringComparison.OrdinalIgnoreCase)).OrderByDescending(p => p.CreatedDate).ToList().ToPagedList(page ?? 1, 20);


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
        public ActionResult Search(string search, int? page)
        {
            AppDbContext dbContext = new AppDbContext();

            List<ProductModels> products = dbContext.getProducts.ToList();

            List<GetProductViewModel> searchProducts = new List<GetProductViewModel>();


            foreach (var item in products)
            {
                ModelModels model = dbContext.getModels.SingleOrDefault(m => m.Id == item.ModelId);
                BrandModels brand = dbContext.getBrands.SingleOrDefault(b => b.BrandId == model.BrandId);
                CategoryModels category = dbContext.getCategories.SingleOrDefault(c => c.CategoryId == model.CategoryId);
                List<ProductPhoto> photos = dbContext.getProductPhotos(item.Id).ToList();

                GetProductViewModel searchProduct = new GetProductViewModel 
                {
                    Id = item.Id,
                    Name = model.Name,
                    Brand = brand.Brand,
                    Category = category.Category,
                    Color = item.Color,
                    Price = item.Price,
                    Processor = item.Processor,
                    Memory = item.Memory,
                    Storage = item.Storage,
                    Display = item.Display,
                    Details = item.Details,
                    Photos = photos
                };

                searchProducts.Add(searchProduct);

            }
            var result = searchProducts.Where(p => p.Name.StartsWith(search ?? "", StringComparison.OrdinalIgnoreCase)).ToList().ToPagedList(page ?? 1, 50);

            return View(result);
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
                    Name = models.Name.Trim(),
                    BrandId = models.BrandId,
                    CategoryId = models.CategoryId,
                    CreatedBy = User.Identity.Name,
                    CreatedDate = DateTime.Now
                };

                dbContext.addModel(model);

                return RedirectToAction("Index", model.BrandId);
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
            ViewBag.ModelId = id;
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
            
            ViewBag.Result = "No product created";
            return View();
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

                ModelModels model = dbContext.getModels.SingleOrDefault(m => m.Id == product.ModelId);

                productModels.Photos = new List<ProductPhoto>();
                foreach(var item in Photo)
                {
                    var fileName = Path.GetFileName(item.FileName);
                    var path = "/Public/Products/" + model.Name;
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

                return RedirectToAction("Products", "Product", new { id = product.ModelId });
                
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

        [HttpGet]
        public ActionResult EditModel(int id)
        {
            AppDbContext dbContext = new AppDbContext();
            List<BrandModels> brands = dbContext.getBrands.ToList();
            List<CategoryModels> categories = dbContext.getCategories.ToList();
            ModelModels model = dbContext.getModels.SingleOrDefault(m => m.Id == id);

            EditModelViewModels viewModels = new EditModelViewModels();
            viewModels.Id = id;
            viewModels.Name = model.Name;
            viewModels.BrandId = model.BrandId;
            viewModels.Brands = brands;
            viewModels.CategoryId = model.CategoryId;
            viewModels.Categories = categories;
            return View(viewModels);
        }

        [HttpPost]
        public ActionResult EditModel(EditModelViewModels viewModels)
        {
            AppDbContext dbContext = new AppDbContext();

            ModelModels model = new ModelModels
            {
                Id = viewModels.Id,
                Name = viewModels.Name,
                CategoryId = viewModels.CategoryId,
                BrandId = viewModels.BrandId,
                CreatedBy = User.Identity.Name,
                CreatedDate = DateTime.Now,
            };

            dbContext.updateModel(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
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

        [HttpPost]
        public ActionResult EditProduct(List<HttpPostedFileBase> Photo, EditProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                AppDbContext dbContext = new AppDbContext();

                ModelModels model = dbContext.getModels.SingleOrDefault(m => m.Id == product.ModelId);

                ProductModels oldProduct = dbContext.getProducts.SingleOrDefault(p => p.Id == product.Id);

                BrandModels brand = dbContext.getBrands.SingleOrDefault(b => b.BrandId == model.BrandId);

                CategoryModels category = dbContext.getCategories.SingleOrDefault(c => c.CategoryId == model.CategoryId);

                EditProductModels editProduct = new EditProductModels
                {
                    Id = Guid.NewGuid(),
                    Model = model.Name,
                    Brand = brand.Brand,
                    Category = category.Category,
                    Price = oldProduct.Price,
                    Color = oldProduct.Color,
                    Storage = oldProduct.Storage,
                    Processor = oldProduct.Processor,
                    Memory = oldProduct.Memory,
                    Display = oldProduct.Display,
                    Details = oldProduct.Details,
                    EditedBy = User.Identity.Name,
                    EditedDate = DateTime.Now,
                    ProductId = product.Id
                };

                dbContext.addEditedProduct(editProduct);


                ProductModels productModels = new ProductModels();
                productModels.Id = product.Id;
                productModels.Price = product.Price;
                productModels.Color = product.Color;
                productModels.Storage = product.Storage;
                productModels.Processor = product.Processor;
                productModels.Memory = product.Memory;
                productModels.Display = product.Display;
                productModels.Details = product.Details;
                productModels.ModelId = product.ModelId;

                productModels.Photos = new List<ProductPhoto>();
                
                

                List<ProductPhoto> photos = new List<ProductPhoto>();

                foreach (var photo in Photo)
                {
                    if(photo != null)
                    {
                        
                        List<ProductPhoto> Photos = dbContext.getProductPhotos(product.Id).ToList();
                        
                        if (Photos.Count > 0)
                        {
                            foreach (var item in Photos)
                            {
                                System.IO.File.Delete(item.Src);
                                dbContext.deleteProductPhoto(item.Id);
                            }
                        }
                        

                        var fileName = Path.GetFileName(photo.FileName);
                        var path = "/Public/Products/" + model.Name;
                        var photoUrl = Server.MapPath(path);
                        var photoTitle = Path.GetFileNameWithoutExtension(photo.FileName);
                        var uniqName = Guid.NewGuid().ToString() + "_" + fileName;

                        if (!Directory.Exists(photoUrl))
                        {
                            Directory.CreateDirectory(photoUrl);
                        }

                        var photoPath = Path.Combine(photoUrl, uniqName);
                        ProductPhoto newPhoto = new ProductPhoto
                        {
                            Path = path,
                            Src = photoPath,
                            Title = uniqName,
                            ProductId = product.Id
                        };
                        photos.Add(newPhoto);

                        photo.SaveAs(photoPath);

                        
                    }
                    
                }
                dbContext.addProductPhoto(photos);
                dbContext.updateProduct(productModels);
                

                return RedirectToAction("Index");

            }

            return View(product);
        }

        /*working on Edit product photo*/
        [HttpGet]
        public ActionResult EditPhoto(int id)
        {
            AppDbContext dbContext = new AppDbContext();
            List<ProductPhoto> photos = new List<ProductPhoto>();
            photos = dbContext.getProductPhotos(id).ToList();
            ProductModels product = dbContext.getProducts.SingleOrDefault(p => p.Id == id);
            ModelModels model = dbContext.getModels.SingleOrDefault(m => m.Id == product.ModelId);
            
            EditPhotoViewModel editPhoto = new EditPhotoViewModel
            {
                Model = model.Name,
                ProductId = id,
                Photos = photos
            };

            return View(editPhoto);
        }

        [HttpPost]
        public ActionResult EditPhoto(EditPhotoViewModel model)
        {

            List<ProductPhoto> Photos = new List<ProductPhoto>();
            if (ModelState.IsValid)
            {
                AppDbContext dbContext = new AppDbContext();

                List<ProductPhoto> exPhotos = model.Photos;

                if(exPhotos != null)
                {
                    foreach(var exPhoto in exPhotos)
                    {
                        System.IO.File.Delete(exPhoto.Src);
                        dbContext.deleteProductPhoto(exPhoto.Id);
                    }
                }
                foreach(var photo in model.Photo)
                {
                    var fileName = Path.GetFileName(photo.FileName);
                    var path = "/Public/Products/" + model.Model;
                    var photoUrl = Server.MapPath(path);
                    var photoTitle = Path.GetFileNameWithoutExtension(photo.FileName);
                    var uniqName = Guid.NewGuid().ToString() + "_" + fileName;

                    if (!Directory.Exists(photoUrl))
                    {
                        Directory.CreateDirectory(photoUrl);
                    }

                    var photoPath = Path.Combine(photoUrl, uniqName);
                    ProductPhoto newPhoto = new ProductPhoto
                    {
                        Path = path,
                        Src = photoPath,
                        Title = uniqName,
                    };
                    Photos.Add(newPhoto);

                    photo.SaveAs(photoPath);
                }

                dbContext.addProductPhoto(Photos);

                return RedirectToAction("Index");
            }

            return View(model);
        }
        /*Pendding*/
        
        /*Adding multiple product*/
        public PartialViewResult AddProducts(int id)
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
            return PartialView("_AddProducts", addProductView);
        }

        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {
            AppDbContext dbContext = new AppDbContext();
            ProductModels product = dbContext.getProducts.SingleOrDefault(p => p.Id == id);
            ModelModels model = dbContext.getModels.SingleOrDefault(m => m.Id == product.ModelId);
            BrandModels brand = dbContext.getBrands.SingleOrDefault(b => b.BrandId == model.BrandId);
            CategoryModels category = dbContext.getCategories.SingleOrDefault(c => c.CategoryId == model.CategoryId);

            EditProductModels toBeDeleted = new EditProductModels
            {
                Id = Guid.NewGuid(),
                Model = model.Name,
                Brand = brand.Brand,
                Category = category.Category,
                Price = product.Price,
                Color = product.Color,
                Storage = product.Storage,
                Processor = product.Processor,
                Memory = product.Memory,
                Display = product.Display,
                Details = product.Details,
                EditedBy = User.Identity.Name,
                EditedDate = DateTime.Now,
                ProductId = product.Id
            };

            dbContext.addDeletedProduct(toBeDeleted);

            List<ProductPhoto> photos = dbContext.getProductPhotos(id).ToList();

            if(photos.Count > 0)
            {
                foreach(var photo in photos)
                {
                    System.IO.File.Delete(photo.Src);
                    dbContext.deleteProductPhoto(photo.Id);
                }
            }

            dbContext.deleteProduct(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteModel(int id)
        {
            AppDbContext dbContext = new AppDbContext();

            List<ProductModels> products = dbContext.getProductModels(id).ToList();
            if(products.Count > 0)
            {
                foreach(var product in products)
                {
                    List<ProductPhoto> photos = dbContext.getProductPhotos(product.Id).ToList();
                    if(photos.Count > 0)
                    {
                        foreach(var photo in photos)
                        {
                            System.IO.File.Delete(photo.Src);
                            dbContext.deleteProductPhoto(photo.Id);
                        }
                    }

                    dbContext.deleteProduct(product.Id);
                }
            }
            dbContext.deleteModel(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteBrand(int id)
        {
            AppDbContext dbContext = new AppDbContext();

            List<ModelModels> models = dbContext.getModelBrands(id).ToList();

            if(models.Count > 0)
            {
                foreach(var model in models)
                {
                    List<ProductModels> products = dbContext.getProductModels(model.Id).ToList();

                    if(products.Count > 0)
                    {
                        foreach(var product in products)
                        {
                            List<ProductPhoto> photos = dbContext.getProductPhotos(product.Id).ToList();

                            if(photos.Count > 0)
                            {
                                foreach(var photo in photos)
                                {
                                    System.IO.File.Delete(photo.Src);
                                    dbContext.deleteProductPhoto(photo.Id);
                                }
                            }
                            dbContext.deleteProduct(product.Id);
                        }
                    }
                    dbContext.deleteModel(model.Id);
                }
            }
            dbContext.deleteBrand(id);
            return RedirectToAction("Brands");
        }

    }
}