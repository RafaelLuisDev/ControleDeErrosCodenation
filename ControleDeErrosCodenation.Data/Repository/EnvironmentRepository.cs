using ControleDeErrosCodenation.Domain.Models;
using ControleDeErrosCodenation.Domain.Repository;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Environment = ControleDeErrosCodenation.Domain.Models.Environment;

namespace ControleDeErrosCodenation.Data.Repository
{
    public class EnvironmentRepository : RepositoryBase<Environment>, IEnvironmentRepository, IEnvLevelRepository<Environment>
    {
        public EnvironmentRepository(Context context) : base(context)
        {

        }

        public Environment SelecionarPorNome(string nome)
        {
            return _context.Environments.FirstOrDefault(x => x.Name.ToLower() == nome.ToLower());
        }
    }
}
