using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TareaFinal.Models
{
    public partial class Pais
    {
        public Pais()
        {
            Clientes = new HashSet<Cliente>();
        }

        [Key]
        public int IdPais { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
