using System.Collections.Generic;
using System.Linq;

namespace LSWebApp.Infrastructure
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string DisplayText { get; set; }
        public bool HasChildren
        {
            get
            {
                return SubMenu != null && SubMenu.Count() > 0;
            }
        }
        public List<MenuItem> SubMenu { get; set; }
    }
}