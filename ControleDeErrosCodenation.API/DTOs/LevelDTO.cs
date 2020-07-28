using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeErrosCodenation.API.DTOs
{
    public class LevelDTO
    {
        [Required]
        public string Name { get; set; }
    }

    public class UpdateLevelDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string NewName { get; set; }
    }
}
