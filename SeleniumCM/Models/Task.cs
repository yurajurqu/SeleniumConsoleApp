using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeleniumCM.Models
{
    class Task
    {
        public string TaskName { get; set; }
        public decimal ReportTime { get; set; }
        public decimal Weight { get; set; }
    

        public Task()
        {
            TaskName = "Default TaskName";
            ReportTime = 99999;
            Weight = 0;
        }

        
    }
}
