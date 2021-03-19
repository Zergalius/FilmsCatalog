using System.Collections.Generic;

namespace FilmsCatalog.Data.Interface
{
    /// <summary>
    /// Режиссёры
    /// </summary>
    public interface IProducer
    {
        /// <summary>
        /// Получить все записи содержащие текст
        /// </summary>
        /// <param name="term">Текст для поиска</param>
        /// <param name="limit">Сколько максимально вернуть записей</param>
        /// <returns></returns>
        List<string> GetAll(string term, int limit);

        /// <summary>
        /// Найти или создать запись
        /// </summary>
        /// <param name="name">Название записи</param>
        /// <returns></returns>
        int LoadOrCreate(string name);
    }
}
