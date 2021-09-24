using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TareaFinal.Models
{
    public class ClienteViewModel
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string NombrePais { get; set; }
    }
}
