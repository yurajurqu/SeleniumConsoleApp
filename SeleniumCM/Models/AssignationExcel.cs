using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeleniumCM.Models
{
    class AssignationExcel
    {

        public int Id { get; set; }
        public String Summary { get; set; }
        public decimal TotalTime { get; set; }
        public bool Installed { get; set; }
        public bool SourcesCompiled
        { get; set; }
        public bool DDS
        { get; set; }
        public int Cycle
        { get; set; }
        public int NumberPbls
        { get; set; }
        public int NumberDefects
        { get; set; }
        public int ClosingState { get; set; }
        public bool Special { get; set; }
    }
}
