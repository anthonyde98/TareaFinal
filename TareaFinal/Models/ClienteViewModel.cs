using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TareaFinal.Models
{
    public class ClienteViewModel
    {
        [Key]
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string IdPais { get; set; }
    }
}
