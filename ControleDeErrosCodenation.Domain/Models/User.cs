using ControleDeErrosCodenation.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeErrosCodenation.Domain.Models
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
