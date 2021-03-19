namespace FilmsCatalog.Models
{
    /// <summary>
    /// Фильм
    /// </summary>
    public class Film
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
        /// Код записи о продюссере
        /// </summary>
        public int? ProducerId { get; set; }

        /// <summary>
        /// Продюссер
        /// </summary>
        public Producer Producer { get; set; }

        /// <summary>
        /// Код записи пользователя
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Код записи постера 
        /// </summary>
        public int? PosterId { get; set; }

        /// <summary>
        /// Постер 
        /// </summary>
        public Poster Poster { get; set; }
    }
}