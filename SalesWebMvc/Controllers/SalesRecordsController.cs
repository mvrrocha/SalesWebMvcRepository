 using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            ViewData["minDate"] = (minDate.HasValue) ? minDate.Value.ToString("yyyy-MM-dd") : new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd");
            ViewData["maxDate"] = (maxDate.HasValue) ? maxDate.Value.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd");
            
            return View(await _salesRecordService.FindByDateAsync(minDate, maxDate));
        }

        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            ViewData["minDate"] = (minDate.HasValue) ? minDate.Value.ToString("yyyy-MM-dd") : new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd");
            ViewData["maxDate"] = (maxDate.HasValue) ? maxDate.Value.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd");

            return View(await _salesRecordService.FindByDateGroupingAsync(minDate, maxDate));
        }
    }
}
