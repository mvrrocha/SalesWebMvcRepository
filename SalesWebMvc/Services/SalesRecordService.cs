using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;

            if (minDate.HasValue)
                result = result.Where(a => a.Date >= minDate.Value);

            if (maxDate.HasValue)
                result = result.Where(a => a.Date <= maxDate.Value);

            return await result.Include(a => a.Seller).Include(a => a.Seller.Department).OrderByDescending(a => a.Date).ToListAsync();
        }

        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;

            if (minDate.HasValue)
                result = result.Where(a => a.Date >= minDate.Value);

            if (maxDate.HasValue)
                result = result.Where(a => a.Date <= maxDate.Value);

            return await result.Include(a => a.Seller).Include(a => a.Seller.Department).OrderByDescending(a => a.Date).GroupBy(a => a.Seller.Department).ToListAsync();
        }
    }
}
