using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMapp.Models
{
    [Table("Options")]
    public class Option
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int OptionID { get; set; } //ID used to connect option with appropriate weights
        public int SessionID { get; set; }
        public string Name { get; set; }

        //public double InfoReliability { get; set; }
        
    }
}
