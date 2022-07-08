using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class PublisherRepository:BaseRepository
    {
        public ListResponse<Publisher> GetPublishers(int pageSize,int pageIndex,string keyword)
        {
            keyword = keyword?.ToLower()?.Trim();
            var query = _context.Publishers.Where(c => keyword == null || c.Name.ToLower().Contains(keyword)).AsQueryable();
            int totalReocrds = query.Count();
            List<Publisher> publishers = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new ListResponse<Publisher>()
            {
                Results = publishers,
                TotalRecords = totalReocrds,
            };
        }

        public Publisher GetPublisher(int id)
        {
            return _context.Publishers.Where(c=>c.Id == id).FirstOrDefault();
        }

        public Publisher AddPublisher(Publisher model)
        {
            var query=_context.Publishers.Add(model);
            _context.SaveChanges();
            return query.Entity;
        }

        public Publisher UpdatePublisher(Publisher model)
        {
            var query=_context.Publishers.Update(model);
            _context.SaveChanges();
            return query.Entity;
        }

        public bool DeletePublisher(int id)
        {
            if(id<=0)
                return false;
            var query=_context.Publishers.Where(c=>c.Id == id).FirstOrDefault();
            _context.Publishers.Remove(query);
            _context.SaveChanges();
            return true;
        }
    }
}
