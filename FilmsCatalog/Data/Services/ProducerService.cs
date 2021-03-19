using FilmsCatalog.Common;
using FilmsCatalog.Data.Interface;
using FilmsCatalog.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FilmsCatalog.Data.Services
{
    public class ProducerService : IProducer
    {
        private readonly ApplicationDbContext context;

        public ProducerService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<string> GetAll(string term, int limit)
        {
            term = term?.RemoveWhiteSpacesTrim();
            IQueryable<Producer> items = context.Producers.AsNoTracking();

            if (!string.IsNullOrEmpty(term))
            {
                items = items.Where(c => c.Name.ToLower().Contains(term.ToLower()));
            }

            return items.Where(c => !string.IsNullOrEmpty(c.Name)).Select(c => c.Name).OrderBy(c => c).Take(limit).ToList();
        }

        public int LoadOrCreate(string name)
        {
            name = name?.RemoveWhiteSpacesTrim();
            var item = context.Producers.FirstOrDefault(c => c.Name == name);
            if (item == null)
            {
                item = new();
                item.Name = name;
                context.Producers.Add(item);
                context.SaveChanges();
            }
            return item.Id;
        }
    }
}
