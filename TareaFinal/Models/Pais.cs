using System;
using System.Collections.Generic;

#nullable disable

namespace TareaFinal.Models
{
    public partial class Pais
    {
        public Pais()
        {
            Clientes = new HashSet<Cliente>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
