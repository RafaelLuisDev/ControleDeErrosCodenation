using ControleDeErrosCodenation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeErrosCodenation.Domain.Repository
{
    public interface ILogRepository : IRepositoryBase<Log>
    {
        List<Log> ActiveLogs();
        
        List<Log> ArchivedLogs();

        Log GetLog(int id);
    }
}
