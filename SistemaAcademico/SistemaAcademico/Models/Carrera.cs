using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAcademico.Models
{
    public class Carrera
    {
        public int carreraID { get; set; }
        public String nombreCarrera { get; set; }
        public ICollection<Estudiante> Estudiantes { get; set; }
    }
}
