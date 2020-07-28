using ControleDeErrosCodenation.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeErrosCodenation.Domain.Models
{
    public class Environment : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Log> Logs { get; set; }
    }

}
