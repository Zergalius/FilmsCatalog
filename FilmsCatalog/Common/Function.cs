using FilmsCatalog.Models;
using System;
using System.IO;

namespace FilmsCatalog.Common
{
    public static class Function
    {
        /// <summary>
        /// Сгенерировать путь до папки с постером
        /// </summary>
        /// <param name="poster"></param>
        /// <returns></returns>
        public static string GetRelativePath(Poster poster)
        {
            return Path.Combine(
                    "FileStorage", "Uploads",
                    poster.DateCreate.Year.ToString(),
                    poster.DateCreate.Month.ToString(),
                    poster.DateCreate.Day.ToString());
        }

        /// <summary>
        /// Картинка?
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsImage(string type)
        {
            return type switch
            {
                "image/jpeg" or "image/bmp" or "image/png" => true,
                _ => false,
            };
        }

        /// <summary>
        /// Удалить лишние пробелы
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RemoveWhiteSpacesTrim(this string s)
        {
            return string.Join(" ", s.Split(new char[] { ' ' },
                   StringSplitOptions.RemoveEmptyEntries)).Trim();
        }
    }
}