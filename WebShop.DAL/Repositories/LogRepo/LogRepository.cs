using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Models;

namespace WebShop.DAL.Repositories.LogRepo
{
    public class LogRepository : ILogRepository
    {
        private readonly WebShopContext _context;
        public LogRepository(WebShopContext context)
        {
            _context = context;
        }
        public void Add(Log log)
        {
            if (log == null)
            {
                throw new ArgumentNullException(nameof(log), "Log cannot be null");
            }
            _context.Logs.Add(log);
            _context.SaveChanges();
        }

        public void AddLogs(IEnumerable<Log> logs)
        {
            if (logs == null || !logs.Any())
            {
                throw new ArgumentNullException(nameof(logs), "Logs cannot be null or empty");
            }
            _context.Logs.AddRange(logs);
            _context.SaveChanges();
        }

        public void Delete(int n)
        {
            var logsToDelete = _context.Logs.Take(n).ToList();
            if (logsToDelete.Count == 0)
            {
                throw new InvalidOperationException("No logs found to delete.");
            }
            _context.Logs.RemoveRange(logsToDelete);
            _context.SaveChanges();
        }

        public List<Log> Get(int n)
        {
            if (n <= 0)
            {
                throw new ArgumentOutOfRangeException("Number of logs to retrieve must be greater than zero.");
            }
            return _context.Logs
                .Take(n)
                .OrderByDescending(l => l.Id)
                .ToList();
        }

        public List<Log> GetAll()
        {
            return _context.Logs.ToList();
        }
    }
}
