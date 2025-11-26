using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDeStock.Models
{
    public class Equipo
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public Dictionary<int, int> Componentes { get; set; } // Código de componente y cantidad requerida
    }
}
