using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment2.Pages.Report
{
    public class IndexModel : PageModel
    {
        private readonly IReportService _reportService;
        public IndexModel(IReportService reportService)
        {
            _reportService = reportService;
        }

        [BindProperty(SupportsGet = true)]
        public int? SelectedMonth { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SelectedYear { get; set; }

        public Dictionary<string, int> TagUsage { get; set; } = new();
        public Dictionary<int, int> ArticleCountByMonth { get; set; } = new();

        public async Task OnGetAsync()
        {
            int year = SelectedYear ?? DateTime.Now.Year;
            int? month = SelectedMonth;

            TagUsage = await _reportService.GetTagUsageByDateAsync(null, month, year);
            ArticleCountByMonth = await _reportService.GetArticleCountByMonthAsync(year);
        }
    }
} 