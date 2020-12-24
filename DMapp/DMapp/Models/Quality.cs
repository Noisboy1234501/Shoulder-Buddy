using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMapp.Models
{   
    [Table("Qualities")]
    public class Quality
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int QualityID { get; set; } // Currently this ID might be useful only to differentiate qualities which have the same name
        public int SessionID { get; set; }
        public string Name { get; set; }
        public double Importance { get; set; }
    }
}
