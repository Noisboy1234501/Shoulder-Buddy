using System;
using System.Collections.Generic;
using System.Text;

namespace DMapp.Models
{

    public enum MenuItemType
    {
        Browse
        
    }
    public class HomeMenuItem
    {
            public MenuItemType Id { get; set; }

            public string Title { get; set; }
    }
}
