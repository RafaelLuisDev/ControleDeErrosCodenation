using ControleDeErrosCodenation.Domain.Models;
using ControleDeErrosCodenation.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControleDeErrosCodenation.Data.Repository
{
    public class LogRepository : RepositoryBase<Log>, ILogRepository
    {
        public LogRepository(Context context) : base(context)
        {
            
        }

        public List<Log> ActiveLogs()
        {
            return _context.Logs.Where(x => x.Archived == false).ToList();
        }

        public List<Log> ArchivedLogs()
        {
            return _context.Logs.Where(x => x.Archived == true).ToList();
        }

        public Log GetLog(int id)
        {
            return _context.Logs.Find(id);
        }
    }
}
