using System;
using System.Collections.Generic;

#nullable disable

namespace TareaFinal.Models
{
    public partial class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int NombrePais { get; set; }

        public virtual Pais NombrePaisNavigation { get; set; }
    }
}
