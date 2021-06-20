using ModelEF.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.DAO
{
    public class UserAccountDAO
    {
        TrinhQuangPhucContext context;

        public UserAccountDAO()
        {
            context = new TrinhQuangPhucContext();
        }
        public int Insert(UserAccount entity)
        {
            context.UserAccounts.Add(entity);
            context.SaveChanges();
            return entity.UserID;
        }   
        public UserAccount Find(string userName)
        {
            return context.UserAccounts.Find(userName);
        }
        public List<UserAccount> ListAll()
        {
            return context.UserAccounts.ToList();
        }
        public IEnumerable<UserAccount> ListWhereAll(string keysearch, int page, int pageSize)
        {
            IQueryable<UserAccount> model = context.UserAccounts;
            if (!string.IsNullOrEmpty(keysearch))
            {
                model = model.Where(x => x.UserName.Contains(keysearch));
            }
            return model.OrderByDescending(x => x.UserID).ToList();
        }
        public UserAccount GetById(string userName)
        {
            return context.UserAccounts.SingleOrDefault(x => x.UserName == userName);
        }
        public int login(string user, string pass)
        {
            var rs = context.UserAccounts.SingleOrDefault(x => x.UserName == user);
            if (rs == null)
            {
                return 0;
            }
            else
            {
                if (rs.Status == 0)
                {
                    return -1;
                }
                else
                {
                    if (rs.Password == pass)
                        return 1;
                    else
                        return -2;
                }
            }
        }
        public object login(string username, object p)
        {
            throw new NotImplementedException();
        }
    }
}
