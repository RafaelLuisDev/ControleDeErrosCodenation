using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeErrosCodenation.API.DTOs
{
    public class LogDTO
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        public string Origin { get; set; }
        [Required]
        public string Environment { get; set; }
        [Required]
        public string Level { get; set; }
        public bool Archived { get; set; }
        public DateTime Date { get; set; }
    }
}
