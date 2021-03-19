using FilmsCatalog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FilmsCatalog.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        /// <summary>
        /// Режиссёры
        /// </summary>
        public DbSet<Producer> Producers { get; set; }

        /// <summary>
        /// Фильмы
        /// </summary>
        public DbSet<Film> Films { get; set; }

        /// <summary>
        /// Постеры
        /// </summary>
        public DbSet<Poster> Posters { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}