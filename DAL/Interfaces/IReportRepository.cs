using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IReportRepository
    {
        Task<Dictionary<string, int>> GetTagUsageByDateAsync(DateTime? date, int? month, int? year);
        Task<Dictionary<int, int>> GetArticleCountByMonthAsync(int year);
    }
}
