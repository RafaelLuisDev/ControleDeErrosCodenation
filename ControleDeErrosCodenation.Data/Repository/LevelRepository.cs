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
            try
            {
                return _context.Levels.First(x => x.Name.ToLower() == nome.ToLower());
            }
            //Não conseguiu encontrar nenhum e pegar o primeiro
            catch (InvalidOperationException)
            {
                return null;
            }
        }
    }
}
