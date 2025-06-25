using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IReportService
    {
        Task<Dictionary<string, int>> GetTagUsageByDateAsync(DateTime? date, int? month, int? year);
        Task<Dictionary<int, int>> GetArticleCountByMonthAsync(int year);
    }
} 