using ControleDeErrosCodenation.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeErrosCodenation.Domain.Models
{
    public class Log : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string Origin { get; set; }
        public bool Archived { get; set; }
        public DateTime Date { get; set; }
        public int IdEnvironment { get; set; }
        public virtual Environment Environment { get; set; }
        public int IdLevel { get; set; }
        public virtual Level Level { get; set; }
    }
}
