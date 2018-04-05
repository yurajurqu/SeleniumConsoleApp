using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeleniumCM.Models
{
    public class DefectDTO
    {
        public string IdAuthorization { get; set; }
        public string Codigo { get; set; }
        public string CodigoProyectoFalla
 { get; set; }
        public string Severidad { get; set; }
        public string Descripcion { get; set; }
        public string Comentarios { get; set; }
        public string Prioridad { get; set; }
        public String TipoDefecto { get; set; }
        public string NombreAplicacion { get; set; }


        public string Gerencia { get; set; }
        public string Proveedor { get; set; }
        public string PlannedFor { get; set; }
        public string Coordinador { get; set; }
        public string Ceq { get; set; }

        public String ToString()
        {
            return IdAuthorization + " "
                + Codigo + " "
                 + CodigoProyectoFalla + " "
                  + Severidad + " "
                   + Descripcion + " "

                    + Comentarios + " "
                    + Prioridad + " "
                    + TipoDefecto + " "
                    + NombreAplicacion + " "
                    + Gerencia + " "
                    + Proveedor + " "

                          + PlannedFor + " "
                    + Ceq + " ";

        }
    }
}
