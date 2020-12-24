using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMapp.Models
{   
    [Table("Weights")]
    public class Weight
    {   
        [PrimaryKey,AutoIncrement,Unique]
        public int WeightID { get; set; } // // Currently this ID might be useful only to differentiate weights which have the same amount
        public int SessionID { get; set; }
        public int OptionID { get; set; }
        public double Amount { get; set; }
    }
}
