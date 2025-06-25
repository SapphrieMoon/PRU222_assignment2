using BLL.Interfaces;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public Task<Dictionary<string, int>> GetTagUsageByDateAsync(DateTime? date, int? month, int? year)
        {
            return _reportRepository.GetTagUsageByDateAsync(date, month, year);
        }

        public Task<Dictionary<int, int>> GetArticleCountByMonthAsync(int year)
        {
            return _reportRepository.GetArticleCountByMonthAsync(year);
        }
    }
} 