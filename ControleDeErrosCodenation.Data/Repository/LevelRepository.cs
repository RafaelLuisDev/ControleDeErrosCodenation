using ControleDeErrosCodenation.Domain.Models;
using ControleDeErrosCodenation.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControleDeErrosCodenation.Data.Repository
{
    public class LevelRepository : RepositoryBase<Level>, ILevelRepository, IEnvLevelRepository<Level>
    {
        public LevelRepository(Context context) : base(context)
        {
        }

        public Level SelecionarPorNome(string nome)
        {
            return _context.Levels.FirstOrDefault(x => x.Name.ToLower() == nome.ToLower());
        }
    }
}
