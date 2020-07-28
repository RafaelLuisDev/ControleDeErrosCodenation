using ControleDeErrosCodenation.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControleDeErrosCodenation.Data.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, IEntity
    {
        protected readonly Context _context;
        public RepositoryBase(Context context)
        {
            _context = context;
        }

        public void Incluir(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Alterar(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public T SelecionarPorId(int id)
        {
            return _context.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public void Excluir(int id)
        {
            var entity = SelecionarPorId(id);
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public List<T> SelecionarTodos()
        {
            return _context.Set<T>().ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
