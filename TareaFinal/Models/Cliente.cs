using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TareaFinal.Models
{
    public partial class Cliente
        {
        [Key]
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int IdPais { get; set; }

        public virtual Pais NombrePaisNavigation { get; set; }
    }
}
