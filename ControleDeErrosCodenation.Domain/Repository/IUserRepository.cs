using ControleDeErrosCodenation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeErrosCodenation.Domain.Repository
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User SelecionarUser(string username, string password);
    }
}
