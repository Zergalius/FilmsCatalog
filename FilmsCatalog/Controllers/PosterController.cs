using FilmsCatalog.Common;
using FilmsCatalog.Data.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace FilmsCatalog.Controllers
{
    public class PosterController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IPoster posters;

        public PosterController(IWebHostEnvironment webHostEnvironment, IPoster posters)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.posters = posters;
        }

        public FileResult Get(int id)
        {
            var uploadedFile = posters.Get(id);

            if (uploadedFile == null)
            {
                return FileNotFound();
            }

            string path = Path.Combine(webHostEnvironment.WebRootPath, Function.GetRelativePath(uploadedFile), uploadedFile.InternalName.ToString());

            if (!System.IO.File.Exists(path))
            {
                return FileNotFound();
            }

            return PhysicalFile(path, uploadedFile.Type, uploadedFile.InternalName.ToString());
        }

        public FileResult FileNotFound()
        {
            string path = Path.Combine(webHostEnvironment.WebRootPath, "images", "filenotfound.png");
            return PhysicalFile(path, "image/png");
        }
    }
}