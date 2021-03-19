using System.Collections.Generic;

namespace FilmsCatalog.ViewModels
{
    public class FilmsIndexViewModel
    {
        /// <summary>
        /// Список фильмов для отображения
        /// </summary>
        public List<FilmViewModel> Films { get; set; } = new List<FilmViewModel>();

        /// <summary>
        /// Информация о пагинации
        /// </summary>
        public PageViewModel Page { get; set; }

    }
}
