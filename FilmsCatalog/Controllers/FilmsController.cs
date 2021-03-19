using FilmsCatalog.Data.Interface;
using FilmsCatalog.Filters;
using FilmsCatalog.Models;
using FilmsCatalog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsCatalog.Controllers
{
    public class FilmsController : Controller
    {
        /// <summary>
        /// Фильмы
        /// </summary>
        private readonly IFilm films;

        /// <summary>
        /// Режиссёры
        /// </summary>
        private readonly IProducer producers;

        /// <summary>
        /// Постеры фильмов
        /// </summary>
        private readonly IPoster posters;

        private readonly SignInManager<User> _signInManager;

        public FilmsController(IFilm films, IProducer producers, IPoster posters, SignInManager<User> signInManager)
        {
            this.films = films;
            this.producers = producers;
            this.posters = posters;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index(int page = 1, int count = 12)
        {
            int countRecord = await films.GetCount();

            FilmsIndexViewModel model = new()
            {
                Page = new(countRecord, page, count)
            };

            var items = await films.GetFilmsAsync(page, count);
            if (items.Count > 0)
            {
                model.Films = items
                    .Select(c => new FilmViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        PosterId = c.PosterId,
                        Producer = c.Producer != null ? c.Producer.Name : "",
                        Year = c.Year
                    })
                    .ToList();
            }

            return PartialView(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Film film = await films.GetAsync((int)id);
            if (film == null)
            {
                return NotFound();
            }

            FilmDetailsViewModel model = new()
            {
                Id = film.Id,
                UserId = film.UserId,
                Name = film.Name,
                Description = film.Description,
                Year = film.Year,
                Producer = film.Producer?.Name,
                PosterId = film.Poster?.Id
            };

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(FilmCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Create));
            }
            int? producerId = null;
            if (!string.IsNullOrEmpty(model.Producer))
            {
                producerId = producers.LoadOrCreate(model.Producer);
            }
            int? posterId = null;
            if (model.Poster != null)
            {
                posterId = await posters.AddAsync(model.Poster);
            }

            Film film = new()
            {
                Year = model.Year,
                Name = model.Name,
                Description = model.Description,
                PosterId = posterId,
                ProducerId = producerId,
                UserId = _signInManager.UserManager.GetUserId(User)
            };

            if (await films.AddAsync(film))
            {
                return RedirectToAction(nameof(Edit), new { id = film.Id });
            }
            ModelState.AddModelError(string.Empty, "Ошибка при сохранении");
            return RedirectToAction(nameof(Create));
        }

        [ServiceFilter(typeof(AuthorFilter))]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await films.GetAsync((int)id);
            if (film == null)
            {
                return NotFound();
            }
            FilmEditViewModel model = new()
            {
                Id = film.Id,
                Name = film.Name,
                Description = film.Description,
                PosterId = film.PosterId,
                Producer = film.Producer?.Name,
                Year = film.Year
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(AuthorFilter))]
        public async Task<IActionResult> Edit(FilmEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            var item = await films.GetAsync(model.Id);
            if (item == null)
            {
                return NotFound();
            }

            if (model.Producer != item.Producer?.Name)
            {
                item.ProducerId = producers.LoadOrCreate(model.Producer);
            }

            if (model.Poster != null)
            {
                int newPosterId = await posters.AddAsync(model.Poster);
                if (newPosterId > 0 && item.PosterId != null)
                {
                    await posters.DeleteAsync((int)item.PosterId);
                }
                item.PosterId = newPosterId;
            }

            item.Name = model.Name;
            item.Description = model.Description;
            item.Producer = null;
            item.Year = model.Year;
            await films.SaveAsync(item);

            return RedirectToAction(nameof(Edit), new { id = item.Id });
        }

        [ServiceFilter(typeof(AuthorFilter))]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await films.GetAsync((int)id);
            if (film == null)
            {
                return NotFound();
            }

            FilmDetailsViewModel model = new()
            {
                Id = film.Id,
                UserId = film.UserId,
                Name = film.Name,
                Description = film.Description,
                Year = film.Year,
                Producer = film.Producer.Name,
                PosterId = film.Poster?.Id
            };

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(AuthorFilter))]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await films.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            if (item.PosterId != null)
            {
                await posters.DeleteAsync((int)item.PosterId);
            }
            await films.DeleteAsync(item.Id);

            return RedirectToAction(nameof(Index), "Home");
        }
    }
}