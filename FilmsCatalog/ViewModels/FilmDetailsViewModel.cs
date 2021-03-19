using System.ComponentModel.DataAnnotations;

namespace FilmsCatalog.ViewModels
{
    /// <summary>
    /// Модель создания записи о чильме
    /// </summary>
    public class FilmDetailsViewModel
    {
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [Display(Name = "Название")]
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
        public int Year { get; set; }

        /// <summary>
        /// Режиссер
        /// </summary>
        [Display(Name = "Режиссер")]
        public string Producer { get; set; }

        /// <summary>
        /// Постер
        /// </summary>
        [Display(Name = "Постер")]
        public int? PosterId { get; set; }

        /// <summary>
        /// Код записи пользователя
        /// </summary>
        public string UserId { get; set; }
    }
}
