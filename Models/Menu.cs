using System.Collections.Generic;

namespace ASP_Core_Hemtenta.Models
{
    public class Menu
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public List<MenuItem> MenuItems { get; set; }
    }
}
