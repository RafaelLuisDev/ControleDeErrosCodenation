using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeErrosCodenation.Domain.Repository
{
    public interface IRepositoryBase<T> : IDisposable where T : class, IEntity
    {
        void Incluir(T entity);
        void Alterar(T entity);
        T SelecionarPorId(int id);
        void Excluir(int id);
        List<T> SelecionarTodos();
    }
}
