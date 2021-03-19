namespace FilmsCatalog.ViewModels
{
    public class FilmViewModel
    {
        /// <summary>
        /// Код записи
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Год выпуска
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Продюссер
        /// </summary>
        public string Producer { get; set; }

        /// <summary>
        /// Код записи постера фильма
        /// </summary>
        public int? PosterId { get; set; }
    }
}
