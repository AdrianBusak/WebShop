using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Models;
using WebShop.DAL.Repositories.LogRepo;

namespace WebShop.DAL.Services.LogServices
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }
        public void Add(Log log) => _logRepository.Add(log);

        public void AddLogs(IEnumerable<Log> logs) => _logRepository.AddLogs(logs);

        public void Delete(int n) => _logRepository.Delete(n);

        public List<Log> Get(int n) => _logRepository.Get(n);

        public List<Log> GetAll() => _logRepository.GetAll();

        public int GetCount() => _logRepository.GetAll().Count;
    }
}
