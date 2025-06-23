using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Models;

namespace WebShop.DAL.Repositories.LogRepo
{
    public interface ILogRepository
    {
        List<Log> GetAll();
        List<Log> Get(int n);
        void Add(Log log);
        void AddLogs(IEnumerable<Log> logs);
        void Delete(int n);
    }
}
