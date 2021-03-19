using FilmsCatalog.Data.Interface;
using FilmsCatalog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FilmsCatalog.Filters
{
    public class AuthorFilter : Attribute, IAsyncActionFilter
    {
        private readonly IFilm films;

        public AuthorFilter(IFilm films)
        {
            this.films = films;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var control = (Controller)context.Controller;
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = control.RedirectToAction("Index", "Home");
            }
            else
            {
                var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                int filmId = 0;

                if (context.ActionArguments.ContainsKey("id"))
                {
                    var id = context.ActionArguments["id"].ToString();
                    if (!int.TryParse(id, out filmId))
                    {
                        context.Result = control.RedirectToAction("Index", "Home");
                        return;
                    }
                }
                if (context.ActionArguments.ContainsKey("model"))
                {
                    try
                    {
                        filmId = ((FilmEditViewModel)context.ActionArguments["model"]).Id;
                    }
                    catch
                    {
                        context.Result = control.RedirectToAction("Index", "Home");
                        return;
                    }
                }
                if (filmId == 0)
                {
                    context.Result = control.RedirectToAction("Index", "Home");
                    return;
                }
                var film = await films.GetAsync(filmId);
                if (film.UserId != userId)
                {
                    context.Result = control.RedirectToAction("Index", "Home");
                    return;
                }
                else
                {
                    await next();
                }
            }
        }
    }
}
