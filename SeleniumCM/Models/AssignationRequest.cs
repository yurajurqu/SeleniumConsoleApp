using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeleniumCM.Models
{
    public class AssignationRequest
    {
        //Exitoso 0
        //Exitoso con Errores 1
        //Stopper (No Instalado) 2
        public enum AttentionClosingState { Exitoso, ExitosoConErrores, Stopper };

        public bool Installation { get; set; }
        public bool SourcesCompiled { get; set; }
        public bool  DDS { get; set; }
        public int Cycle { get; set; }
        public int NumberPBL { get; set; }
        public decimal TimeSpent { get; set; }
        public int NumberDefects { get; set; }
        public int Id { get; set; }
        public AttentionClosingState ClosingState { get; set; }
        public bool Special { get; set; }
        

        public AssignationRequest()
        {
            Installation = true;
            SourcesCompiled = true;
            DDS = false;
            Cycle = 1;
            NumberPBL = 0;
            TimeSpent = 4;
            NumberDefects = 0;
            ClosingState = AttentionClosingState.Exitoso;
            Special = false;

        }

       
    }
}
