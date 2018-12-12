using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SistemaAcademico.Models
{
    public class Estudiante
    {
        [Key]
        public int NoControl { get; set; }
        public String Nombre { get; set; }
        public String Apellidos { get; set; }
        public int carreraID { get; set; }
        public Carrera Carrera { get; set; }

    }
}
