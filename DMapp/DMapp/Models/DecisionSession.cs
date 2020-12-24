using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMapp.Models
{
    [Table("DecisionSessions")]
    public class DecisionSession
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int SessionID { get; set; } // ID used to connect Decision Session with appropriate options and qualities
        public int SessionCategoryID { get; set; } // used to filter the list in ItemsPage
        public string Title { get; set; }
        public string Goal { get; set; }
        public string DateOfDecisionMaking { get; set; }
        public string DateOfSuccessAssesement { get; set; }
        public double SuccessRate { get; set; }
        

        public double TimeForChoices { get; set; }

        //systems on/off
    
        public string StopWatch { get; set; }
        

    }
}
