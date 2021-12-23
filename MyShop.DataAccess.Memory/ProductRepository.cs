using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.Memory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;

        List<Product> products;

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if(products == null)
            { products = new List<Product>(); }
        }

        public void Commit()
        {
            cache["products"] = products;
        }

        public void Insert(Product p)
        {
            products.Add(p);
        }

        public void Update(Product prd)
        {
            Product protoupdate = products.Find(p => p.ID == prd.ID);

            if (protoupdate != null)
                protoupdate = prd;
            else
                throw new Exception("Product not found");
        }

        public Product find(string ID)
        {
            Product product = products.Find(p => p.ID == ID);

            if (product != null)
                return product;
            else
                throw new Exception("Product not found");
        } 

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void delete(string ID)
        {
            Product product = products.Find(p => p.ID == ID);

            if (product != null)
                products.Remove(product);
            else
                throw new Exception("Product not found");
        }
    }
}
