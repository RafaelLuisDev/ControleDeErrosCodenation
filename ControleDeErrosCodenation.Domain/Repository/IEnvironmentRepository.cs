using ControleDeErrosCodenation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Environment = ControleDeErrosCodenation.Domain.Models.Environment;

namespace ControleDeErrosCodenation.Domain.Repository
{
    public interface IEnvironmentRepository : IRepositoryBase<Environment>, IEnvLevelRepository<Environment>
    { 
    }
}
