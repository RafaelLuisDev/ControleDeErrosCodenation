using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeErrosCodenation.Domain.Repository
{
    public interface IEnvLevelRepository<T> where T : class
    {
        T SelecionarPorNome(string nome);
    }
}
