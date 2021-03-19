using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FilmsCatalog.ViewModels
{
    /// <summary>
    /// Модель создания записи о чильме
    /// </summary>
    public class FilmCreateViewModel
    {
        /// <summary>
        /// Наименование
        /// </summary>
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Введите название фильма")]
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        [Display(Name = "Описание")]
        public string Description { get; set; }

        /// <summary>
        /// Год выпуска
        /// </summary>
        [Display(Name = "Год выпуска")]
        [Required(ErrorMessage = "Укажите год выпуска фильма")]
        public int Year { get; set; }

        /// <summary>
        /// Режиссер
        /// </summary>
        [Display(Name = "Режиссер")]
        [Required(ErrorMessage = "Укажите режиссера")]
        public string Producer { get; set; }

        /// <summary>
        /// Постер
        /// </summary>
        [Display(Name = "Постер")]
        public IFormFile Poster { get; set; }
    }
}
