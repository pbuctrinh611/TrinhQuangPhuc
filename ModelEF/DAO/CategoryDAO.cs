using ModelEF.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.DAO
{
    public class CategoryDAO
    {
        TrinhQuangPhucContext context;

        public CategoryDAO()
        {
            context = new TrinhQuangPhucContext();
        }
        public int Insert(Category entity)
        {
            context.Categories.Add(entity);
            context.SaveChanges();
            return entity.CateID;
        }
        public Category Find(string cate)
        {
            return context.Categories.Find(cate);
        }
        public List<Category> ListAll()
        {
            return context.Categories.ToList();
        }
        public IEnumerable<Category> ListWhereAll(string keysearch, int page, int pageSize)
        {
            IQueryable<Category> model = context.Categories;
            if (!string.IsNullOrEmpty(keysearch))
            {
                model = model.Where(x => x.Name.Contains(keysearch));

            }
            return model.OrderByDescending(x => x.CateID).ToPagedList(page, pageSize);
        }
    }
}
