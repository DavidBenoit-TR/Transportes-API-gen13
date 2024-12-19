using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Camiones_DTO
    {
        [Key]
        public int ID_Camion { get; set; }

        [Required]
        [StringLength(10)]

        public string Matricula { get; set; }

        [Required]
        [StringLength(25)]

        public string Tipo_Camion { get; set; }

        [Required]
        [StringLength(25)]

        public string Marca { get; set; }

        [Required]
        [StringLength(25)]

        public string Modelo { get; set; }

        public int Capacidad { get; set; }

        public double Kilometraje { get; set; }

        [Required]
        [StringLength(250)]

        public string UrlFoto { get; set; }

        public bool Disponibilidad { get; set; }
    }
}
