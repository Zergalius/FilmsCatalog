using System;

namespace FilmsCatalog.Models
{
    /// <summary>
    /// Постер
    /// </summary>
    public class Poster
    {
        /// <summary>
        /// Код записи
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime DateCreate { get; set; }

        /// <summary>
        /// Bнутреннее имя
        /// </summary>
        public Guid InternalName { get; set; }

        /// <summary>
        /// Тип файла
        /// </summary>
        public string Type { get; set; }
    }
}