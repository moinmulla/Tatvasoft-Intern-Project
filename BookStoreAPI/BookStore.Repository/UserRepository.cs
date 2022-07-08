using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class UserRepository : BaseRepository
    {
        public List<User> GetUsers()
        {
            return _context.Users.ToList();  
        }

        public User Login(LoginModel model)
        {
            return _context.Users.FirstOrDefault(c => c.Email.Equals(model.Email.ToLower()) && c.Password.Equals(model.Password));
        }

        public ListResponse<Role> GetRoles()
        {
            var query = _context.Roles;
            int totalReocrds = query.Count();
            List<Role> roles = query.ToList();
            return new ListResponse<Role>()
            {
                Results = roles,
                TotalRecords = totalReocrds,
            };
        }

        public User Register(RegisterModel model)
        {
            User user = new User()
            {
                Email = model.Email,
                Password = model.Password,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Roleid = model.Roleid,
            };
            var entry= _context.Users.Add(user);
            _context.SaveChanges();
            return entry.Entity;
        }

        public User GetUser(int id)
        {
            if (id > 0)
            {
                return _context.Users.Where(w => w.Id == id).FirstOrDefault();
            }
            return null;
        }

        public bool UpdateUser(User model)
        {
            if(model.Id> 0)
            {
                _context.Users.Update(model);
                _context.SaveChanges();

                return true;
            }
            return false;
        }

        public bool DeleteUser(User model)
        {
            if (model.Id > 0)
            {
                _context.Users.Remove(model);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }

}
