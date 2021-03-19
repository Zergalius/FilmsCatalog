using FilmsCatalog.Data.Interface;
using FilmsCatalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FilmsCatalog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProducer producers;

        public HomeController(ILogger<HomeController> logger, IProducer producers)
        {
            _logger = logger;
            this.producers = producers;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public object Autocomplete(string term, string source)
        {
            term = term?.Trim();
            List<string> items = new();
            if (source == "Year")
            {
                int min = 1895;
                int max = DateTime.Now.Year;

                items = Enumerable
                    .Repeat(min, (max - min) / 1 + 1)
                    .Select((tr, ti) => (tr + ti).ToString())
                    .OrderByDescending(c => c)
                    .Take(10)
                    .ToList();

                if (!string.IsNullOrEmpty(term))
                {
                    items = items.Where(c => c.Contains(term)).ToList();
                }
            }
            if (source == "Producer")
            {
                items = producers.GetAll(term, 10).ToList();
            }

            return items;
        }
    }
}