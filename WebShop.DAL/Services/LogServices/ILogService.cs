using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Models;

namespace WebShop.DAL.Services.LogServices
{
    public interface ILogService
    {
        List<Log> GetAll();
        List<Log> Get(int n);
        void Add(Log log);
        void AddLogs(IEnumerable<Log> logs);
        int GetCount();
        void Delete(int n);
    }
}
