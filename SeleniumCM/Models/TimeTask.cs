using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeleniumCM.Models
{
    public class TimeTask
    {
        public string IdTicket { get; set; }
        public string TaskName { get; set; }
        public string TaskPlannedFor { get; set; }
        public string TaskDueDate { get; set; }
        public string TaskCycle { get; set; }
        public string Description { get; set; }
        public string StreamsInstalled { get; set; }
        public string TimeSpentHr { get; set; }
        public string TaskStartTime { get; set; }
        public string TicketUser { get; set; }

        public String ToString()
        {
            return String.Format("Id {0} TaskName {1} TaskPlannedFor {2} TaskDueDate {3} TaskCycle {4} Description {5} StreamsInstalled {6} TimeSpentHr {7} TaskStartTime {8} ticket user {9}",
               IdTicket,TaskName,TaskPlannedFor,TaskDueDate,TaskCycle,Description,StreamsInstalled,TimeSpentHr,
               TaskStartTime,
               TicketUser);
        }
    }
}
