using ControleDeErrosCodenation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeErrosCodenation.Domain.Repository
{
    public interface ILevelRepository : IRepositoryBase<Level>, IEnvLevelRepository<Level>
    {
    }
}
