using FilmsCatalog.Common;
using FilmsCatalog.Data.Interface;
using FilmsCatalog.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsCatalog.Data.Services
{
    public class PosterService : IPoster
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public PosterService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<int> AddAsync(IFormFile formFile)
        {
            if (formFile == null || !Function.IsImage(formFile.ContentType))
            {
                return 0;
            }

            Poster poster = new()
            {
                DateCreate = DateTime.Now,
                InternalName = Guid.NewGuid(),
                Type = formFile.ContentType
            };

            try
            {
                string relativePath = Path.Combine(
                    "FileStorage", "Uploads",
                    poster.DateCreate.Year.ToString(),
                    poster.DateCreate.Month.ToString(),
                    poster.DateCreate.Day.ToString()
                    );
                string absolutpath = Path.Combine(webHostEnvironment.WebRootPath, relativePath);
                Directory.CreateDirectory(absolutpath);
                using FileStream output = File.Create(Path.Combine(absolutpath, poster.InternalName.ToString()));
                formFile.CopyTo(output);
            }
            catch (Exception)
            {
                return 0;
            }

            context.Posters.Add(poster);
            await context.SaveChangesAsync();
            return poster.Id;
        }

        public Poster Get(int id)
        {
            return context.Posters.FirstOrDefault(c => c.Id == id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Poster item = context.Posters.FirstOrDefault(c => c.Id == id);
            string path = Path.Combine(webHostEnvironment.WebRootPath, Function.GetRelativePath(item), item.InternalName.ToString());
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                context.Posters.Remove(item);
                await context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }


    }
}