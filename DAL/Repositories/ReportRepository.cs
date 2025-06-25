using DAL.Data;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly NewsContext _context;
        public ReportRepository(NewsContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, int>> GetTagUsageByDateAsync(DateTime? date, int? month, int? year)
        {
            var query = _context.NewsTags.Include(nt => nt.Tag).Include(nt => nt.NewsArticle).AsQueryable();
            if (date.HasValue)
            {
                query = query.Where(nt => nt.NewsArticle.CreatedDate.HasValue && nt.NewsArticle.CreatedDate.Value.Date == date.Value.Date);
            }
            else if (month.HasValue && year.HasValue)
            {
                query = query.Where(nt => nt.NewsArticle.CreatedDate.HasValue && nt.NewsArticle.CreatedDate.Value.Month == month.Value && nt.NewsArticle.CreatedDate.Value.Year == year.Value);
            }
            var result = await query.GroupBy(nt => nt.Tag.TagName)
                .Select(g => new { Tag = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Tag, x => x.Count);
            return result;
        }

        public async Task<Dictionary<int, int>> GetArticleCountByMonthAsync(int year)
        {
            var result = await _context.NewsArticles
                .Where(na => na.CreatedDate.HasValue && na.CreatedDate.Value.Year == year)
                .GroupBy(na => na.CreatedDate.Value.Month)
                .Select(g => new { Month = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Month, x => x.Count);
            return result;
        }
    }
}
