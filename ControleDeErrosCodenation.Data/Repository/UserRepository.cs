using ControleDeErrosCodenation.Domain.Models;
using ControleDeErrosCodenation.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControleDeErrosCodenation.Data.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(Context context) : base(context)
        {
        }

        public User SelecionarUser(string username, string password)
        {
            try
            {
                return _context.Users.First(x => x.Username.ToLower() == username.ToLower() && x.Password.ToLower() == password.ToLower());
            }
            //Não conseguiu encontrar nenhum e pegar o primeiro
            catch (InvalidOperationException)
            {
                return null;
            }
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
