using FilmsCatalog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmsCatalog.Data.Interface
{
    /// <summary>
    /// Работа с фильмами
    /// </summary>
    public interface IFilm
    {
        /// <summary>
        /// Получить количество записей
        /// </summary>
        /// <returns>Количество записей</returns>
        Task<int> GetCount();

        /// <summary>
        /// Получить список записей для страницы
        /// </summary>
        /// <param name="page">Номер страницы</param>
        /// <param name="count">Количество записей на странице</param>
        /// <returns>Список записей</returns>
        Task<List<Film>> GetFilmsAsync(int page, int count);

        /// <summary>
        /// Добавить запись в базу
        /// </summary>
        /// <param name="film">Добавляемая запись</param>
        /// <returns></returns>
        Task<bool> AddAsync(Film film);

        /// <summary>
        /// Получить запись из базы
        /// </summary>
        /// <param name="id">Код записи</param>
        /// <returns>Запись</returns>
        Task<Film> GetAsync(int id);

        /// <summary>
        /// Удалить запись из базы
        /// </summary>
        /// <param name="id">Код записи</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Сохранить запись в базу
        /// </summary>
        /// <param name="film">Сохраняемая запись</param>
        /// <returns></returns>
        Task<int> SaveAsync(Film film);
    }
}
