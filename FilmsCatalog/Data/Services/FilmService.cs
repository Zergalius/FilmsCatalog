using FilmsCatalog.Data.Interface;
using FilmsCatalog.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsCatalog.Data.Services
{
    public class FilmService : IFilm
    {
        private readonly ApplicationDbContext context;

        public FilmService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> GetCount()
        {
            return await context.Films.AsNoTracking()
                .CountAsync();
        }

        public async Task<List<Film>> GetFilmsAsync(int start, int count)
        {
            return await context.Films.AsNoTracking()
                .Include(c => c.Poster)
                .Include(c => c.Producer)
                .OrderBy(c => c.Id)
                .Skip((start - 1) * count)
                .Take(count)
                .ToListAsync();
        }

        public async Task<Film> GetAsync(int id)
        {
            return await context.Films.AsNoTracking()
                .Include(c => c.Poster)
                .Include(c => c.Producer)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Film item = await GetAsync(id);
            context.Films.Remove(item);
            try
            {
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddAsync(Film film)
        {
            context.Add(film);
            try
            {
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> SaveAsync(Film film)
        {
            context.Films.Update(film);
            return await context.SaveChangesAsync();
        }
    }
}