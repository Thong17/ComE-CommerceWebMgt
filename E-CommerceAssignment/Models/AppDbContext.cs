using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace E_CommerceAssignment.Models
{
    public class AppDbContext
    {
        public void addBrand(BrandModels brand)
        {
            string cs = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;

            using(SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("addBrand", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBrand = new SqlParameter();
                paramBrand.ParameterName = "@Brand";
                paramBrand.Value = brand.Brand;
                cmd.Parameters.Add(paramBrand);

                SqlParameter paramDetails = new SqlParameter();
                paramDetails.ParameterName = "@Details";
                paramDetails.Value = brand.BrandDetails;
                cmd.Parameters.Add(paramDetails);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void addCategory(CategoryModels category)
        {
            string cs = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;

            using(SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("addCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramCategory = new SqlParameter();
                paramCategory.ParameterName = "@Category";
                paramCategory.Value = category.Category;
                cmd.Parameters.Add(paramCategory);

                SqlParameter paramDetails = new SqlParameter();
                paramDetails.ParameterName = "@Details";
                paramDetails.Value = category.CategoryDetails;
                cmd.Parameters.Add(paramDetails);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void addModel(ModelModels model)
        {
            string cs = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;

            using(SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("addModel", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramName = new SqlParameter();
                paramName.ParameterName = "@Name";
                paramName.Value = model.Name;
                cmd.Parameters.Add(paramName);

                SqlParameter paramBrand = new SqlParameter();
                paramBrand.ParameterName = "@BrandId";
                paramBrand.Value = model.BrandId;
                cmd.Parameters.Add(paramBrand);

                SqlParameter paramCategory = new SqlParameter();
                paramCategory.ParameterName = "@CategoryId";
                paramCategory.Value = model.CategoryId;
                cmd.Parameters.Add(paramCategory);

                SqlParameter paramCreatedBy = new SqlParameter();
                paramCreatedBy.ParameterName = "@CreatedBy";
                paramCreatedBy.Value = model.CreatedBy;
                cmd.Parameters.Add(paramCreatedBy);

                SqlParameter paramCreatedDate = new SqlParameter();
                paramCreatedDate.ParameterName = "@CreatedDate";
                paramCreatedDate.Value = model.CreatedDate;
                cmd.Parameters.Add(paramCreatedDate);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void addProduct(ProductModels product)
        {
            string cs = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("addProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramPrice = new SqlParameter();
                paramPrice.ParameterName = "@Price";
                paramPrice.Value = product.Price;
                cmd.Parameters.Add(paramPrice);

                SqlParameter paramColor = new SqlParameter();
                paramColor.ParameterName = "@Color";
                paramColor.Value = product.Color;
                cmd.Parameters.Add(paramColor);

                SqlParameter paramStorage = new SqlParameter();
                paramStorage.ParameterName = "@Storage";
                paramStorage.Value = product.Storage;
                cmd.Parameters.Add(paramStorage);

                SqlParameter paramProcessor = new SqlParameter();
                paramProcessor.ParameterName = "@Processor";
                paramProcessor.Value = product.Processor;
                cmd.Parameters.Add(paramProcessor);

                SqlParameter paramMemory = new SqlParameter();
                paramMemory.ParameterName = "@Memory";
                paramMemory.Value = product.Memory;
                cmd.Parameters.Add(paramMemory);

                SqlParameter paramDisplay = new SqlParameter();
                paramDisplay.ParameterName = "@Display";
                paramDisplay.Value = product.Display;
                cmd.Parameters.Add(paramDisplay);

                SqlParameter paramDetails = new SqlParameter();
                paramDetails.ParameterName = "@Details";
                paramDetails.Value = product.Details;
                cmd.Parameters.Add(paramDetails);

                SqlParameter paramBrandId = new SqlParameter();
                paramBrandId.ParameterName = "@BrandId";
                paramBrandId.Value = product.BrandId;
                cmd.Parameters.Add(paramBrandId);

                SqlParameter paramModelId = new SqlParameter();
                paramModelId.ParameterName = "@ModelId";
                paramModelId.Value = product.ModelId;
                cmd.Parameters.Add(paramModelId);

                SqlParameter paramCategoryId = new SqlParameter();
                paramCategoryId.ParameterName = "@CategoryId";
                paramCategoryId.Value = product.CategoryId;
                cmd.Parameters.Add(paramCategoryId);

                SqlParameter paramCreatedBy = new SqlParameter();
                paramCreatedBy.ParameterName = "@CreatedBy";
                paramCreatedBy.Value = product.CreatedBy;
                cmd.Parameters.Add(paramCreatedBy);

                SqlParameter paramCreatedDate = new SqlParameter();
                paramCreatedDate.ParameterName = "@CreatedDate";
                paramCreatedDate.Value = product.CreatedDate;
                cmd.Parameters.Add(paramCreatedDate);

                con.Open();
                int Id = Convert.ToInt32(cmd.ExecuteScalar());
                product.Id = Id;
            }
        }

        public void addProductPhoto(List<ProductPhoto> productPhotos)
        {
            string cs = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;

            foreach (var photo in productPhotos)
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("addProductPhoto", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramPath = new SqlParameter();
                    paramPath.ParameterName = "@Path";
                    paramPath.Value = photo.Path;
                    cmd.Parameters.Add(paramPath);

                    SqlParameter paramSrc = new SqlParameter();
                    paramSrc.ParameterName = "@Src";
                    paramSrc.Value = photo.Src;
                    cmd.Parameters.Add(paramSrc);

                    SqlParameter paramTitle = new SqlParameter();
                    paramTitle.ParameterName = "@Title";
                    paramTitle.Value = photo.Title;
                    cmd.Parameters.Add(paramTitle);

                    SqlParameter paramProductId = new SqlParameter();
                    paramProductId.ParameterName = "@ProductId";
                    paramProductId.Value = photo.ProductId;
                    cmd.Parameters.Add(paramProductId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<BrandModels> getBrands
        {
            get
            {
                string cs = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                List<BrandModels> brands = new List<BrandModels>();

                using(SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("getBrands", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        BrandModels brand = new BrandModels();
                        brand.BrandId = Convert.ToInt32(reader["Id"]);
                        brand.Brand = reader["Brand"].ToString();
                        brand.BrandDetails = reader["Details"].ToString();

                        brands.Add(brand);
                    }
                }
                return brands;
            }
        }

        public IEnumerable<CategoryModels> getCategories
        {
            get
            {
                string cs = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                List<CategoryModels> categories = new List<CategoryModels>();

                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("getCategories", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CategoryModels category = new CategoryModels();
                        category.CategoryId = Convert.ToInt32(reader["Id"]);
                        category.Category = reader["Category"].ToString();
                        category.CategoryDetails = reader["Details"].ToString();

                        categories.Add(category);
                    }
                }
                return categories;
            }
        }

        public IEnumerable<ModelModels> getModels
        {
            get
            {
                string cs = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                List<ModelModels> models = new List<ModelModels>();

                using(SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("getModels", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ModelModels model = new ModelModels();
                        model.Id = Convert.ToInt32(reader["Id"]);
                        model.Name = reader["Name"].ToString();
                        model.BrandId = Convert.ToInt32(reader["BrandID"]);
                        model.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                        model.CreatedBy = reader["CreatedBy"].ToString();
                        model.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);

                        models.Add(model);
                    }
                }
                return models;
            }
        }

        public IEnumerable<ModelModels> getModelBrands(int id)
        {
            string cs = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
            List<ModelModels> models = new List<ModelModels>();

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("getModelBrands", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBrandId = new SqlParameter();
                paramBrandId.ParameterName = "@BrandId";
                paramBrandId.Value = id;
                cmd.Parameters.Add(paramBrandId);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ModelModels model = new ModelModels();
                    model.Id = Convert.ToInt32(reader["Id"]);
                    model.Name = reader["Name"].ToString();
                    model.BrandId = Convert.ToInt32(reader["BrandID"]);
                    model.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                    model.CreatedBy = reader["CreatedBy"].ToString();
                    model.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);

                    models.Add(model);
                }
            }
            return models;
        }

        public IEnumerable<ProductModels> getProducts
        {
            get
            {
                string cs = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;

                List<ProductModels> products = new List<ProductModels>();

                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("getProducts", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductModels product = new ProductModels();
                        product.Id = Convert.ToInt32(reader["Id"]);
                        product.Price = Convert.ToDouble(reader["Price"]);
                        product.Color = reader["Color"].ToString();
                        product.Storage = reader["Storage"].ToString();
                        product.Processor = reader["Processor"].ToString();
                        product.Memory = reader["Memory"].ToString();
                        product.Display = reader["Display"].ToString();
                        product.Details = reader["Details"].ToString();
                        product.CreatedBy = reader["CreatedBy"].ToString();
                        product.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                        product.ModelId = Convert.ToInt32(reader["ModelId"]);
                        product.BrandId = Convert.ToInt32(reader["BrandId"]);
                        product.CategoryId = Convert.ToInt32(reader["CategoryId"]);

                        products.Add(product);
                    }
                }
                return products;
            }
        }

        public IEnumerable<ProductPhoto> getProductPhotos(int id)
        {
            string cs = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
            List<ProductPhoto> photos = new List<ProductPhoto>();

            using(SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("getProductPhotos", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramProductId = new SqlParameter();
                paramProductId.ParameterName = "@ProductId";
                paramProductId.Value = id;
                cmd.Parameters.Add(paramProductId);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ProductPhoto photo = new ProductPhoto();
                    photo.Id = Convert.ToInt32(reader["Id"]);
                    photo.Path = reader["Path"].ToString();
                    photo.Src = reader["Src"].ToString();
                    photo.Title = reader["Title"].ToString();

                    photos.Add(photo);
                }
            }
            return photos;
        }

        public IEnumerable<ProductModels> getProductModels(int id)
        {
            string cs = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
            List<ProductModels> products = new List<ProductModels>();

            using(SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("getProductModels", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramModelId = new SqlParameter();
                paramModelId.ParameterName = "@ModelId";
                paramModelId.Value = id;
                cmd.Parameters.Add(paramModelId);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ProductModels product = new ProductModels();
                    product.Id = Convert.ToInt32(reader["Id"]);
                    product.Price = Convert.ToDouble(reader["Price"]);
                    product.Color = reader["Color"].ToString();
                    product.Storage = reader["Storage"].ToString();
                    product.Processor = reader["Processor"].ToString();
                    product.Memory = reader["Memory"].ToString();
                    product.Display = reader["Display"].ToString();
                    product.Details = reader["Details"].ToString();
                    product.ModelId = Convert.ToInt32(reader["ModelId"]);
                    product.BrandId = Convert.ToInt32(reader["BrandId"]);
                    product.CategoryId = Convert.ToInt32(reader["CategoryId"]);

                    products.Add(product);
                }
            }
            return products;
        }

        public IEnumerable<ProductModels> getProductBrands(int id)
        {
            string cs = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
            List<ProductModels> products = new List<ProductModels>();

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("getProductBrands", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramModelId = new SqlParameter();
                paramModelId.ParameterName = "@BrandId";
                paramModelId.Value = id;
                cmd.Parameters.Add(paramModelId);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ProductModels product = new ProductModels();
                    product.Id = Convert.ToInt32(reader["Id"]);
                    product.Price = Convert.ToDouble(reader["Price"]);
                    product.Color = reader["Color"].ToString();
                    product.Storage = reader["Storage"].ToString();
                    product.Processor = reader["Processor"].ToString();
                    product.Memory = reader["Memory"].ToString();
                    product.Display = reader["Display"].ToString();
                    product.Details = reader["Details"].ToString();
                    product.ModelId = Convert.ToInt32(reader["ModelId"]);
                    product.BrandId = Convert.ToInt32(reader["BrandId"]);
                    product.CategoryId = Convert.ToInt32(reader["CategoryId"]);

                    products.Add(product);
                }
            }
            return products;
        }

        public void updateBrand(BrandModels brand)
        {
            string cs = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;

            using(SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("updateBrand", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = brand.BrandId;
                cmd.Parameters.Add(paramId);

                SqlParameter paramBrand = new SqlParameter();
                paramBrand.ParameterName = "@Brand";
                paramBrand.Value = brand.Brand;
                cmd.Parameters.Add(paramBrand);

                SqlParameter paramDetails = new SqlParameter();
                paramDetails.ParameterName = "@Details";
                paramDetails.Value = brand.BrandDetails;
                cmd.Parameters.Add(paramDetails);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void updateCategory(CategoryModels category)
        {
            string cs = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("updateCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = category.CategoryId;
                cmd.Parameters.Add(paramId);

                SqlParameter paramCategory = new SqlParameter();
                paramCategory.ParameterName = "@Category";
                paramCategory.Value = category.Category;
                cmd.Parameters.Add(paramCategory);

                SqlParameter paramDetails = new SqlParameter();
                paramDetails.ParameterName = "@Details";
                paramDetails.Value = category.CategoryDetails;
                cmd.Parameters.Add(paramDetails);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}