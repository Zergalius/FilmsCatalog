using FilmsCatalog.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FilmsCatalog.Data.Interface
{
    /// <summary>
    /// Работа с постерами
    /// </summary>
    public interface IPoster
    {
        /// <summary>
        /// Добавить
        /// </summary>
        /// <param name="formFile">Файл постера</param>
        /// <returns>Id в базе</returns>
        Task<int> AddAsync(IFormFile formFile);

        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id">Код файла</param>
        /// <returns>Результат удаления</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Получить путь к файлу
        /// </summary>
        /// <param name="id">Код файла</param>
        /// <returns>Постер</returns>
        Poster Get(int id);
    }
}
