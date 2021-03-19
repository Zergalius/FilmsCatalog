using System;
using System.Collections.Generic;
using System.Linq;

namespace FilmsCatalog.ViewModels
{
    /// <summary>
    /// Информация о пагинации
    /// </summary>
    public class PageViewModel
    {
        /// <summary>
        /// Номер текущей страницы
        /// </summary>
        public int PageNumber { get; private set; }

        /// <summary>
        /// Общее количество страниц
        /// </summary>
        public int TotalPages { get; private set; }

        /// <summary>
        /// Список номеров страниц для отрисовки пагинации
        /// </summary>
        public List<int> ListPages { get; private set; }

        public PageViewModel(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            var pages = new[] { 1, 2 }.Concat(
                        Enumerable.Range(pageNumber - 2, 5)).Concat(
                        new[] { TotalPages - 1, TotalPages });

            ListPages = pages.Where(n => n >= 1 && n <= TotalPages).Distinct().ToList();
        }

        /// <summary>
        /// Есть предыдущие страницы
        /// </summary>
        public bool HasPreviousPage
        {
            get
            {
                return PageNumber > 1;
            }
        }

        /// <summary>
        /// Есть следующие страницы
        /// </summary>
        public bool HasNextPage
        {
            get
            {
                return PageNumber < TotalPages;
            }
        }
    }
}
