using ModelEF.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.DAO
{
    public class ProductDAO
    {
        TrinhQuangPhucContext context;

        public ProductDAO()
        {
            context = new TrinhQuangPhucContext();
        }
        public int Insert(Product entity)
        {
            context.Products.Add(entity);
            context.SaveChanges();
            return entity.ProductID;
        }
        public Product Find(string product)
        {
            return context.Products.Find(product);
        }
        public List<Product> ListAll()
        {
            return context.Products.ToList();
        }
        public IEnumerable<Product> ListWhereAll(string keysearch)
        {
            IQueryable<Product> model = context.Products;
            if (!string.IsNullOrEmpty(keysearch))
            {
                model = model.Where(x => x.ProductName.Contains(keysearch));
            }
            return model.OrderBy(x => x.Quantity).OrderByDescending(x => x.UnitCost).ToList();
        }
    }
}
