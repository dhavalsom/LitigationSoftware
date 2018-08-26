using LSWebApp.Infrastructure;
using System.Collections.Generic;
using System.Web;
using System.Xml;

namespace LSWebApp.Models
{
    public class ViewModelBase
    {
        public Menu MenuObject { get; set; }
        public ViewModelBase(Pages page)
        {
            XmlDocument xMenus = new XmlDocument();
            xMenus.Load(HttpContext.Current.Server.MapPath("~/Menu.xml"));
            foreach (XmlNode item in xMenus.ChildNodes[0].ChildNodes)
            {
                if (item.Attributes["Value"] != null
                    && int.Parse(item.Attributes["Value"].Value.ToString()) == (int)page)
                {
                    MenuObject = new Menu{ Menus = new List<MenuItem>() };
                    foreach (XmlNode menu in item.ChildNodes)
                    {
                        var menuItem = new MenuItem
                        {
                            ActionName = menu.Attributes["ActionName"] != null ? menu.Attributes["ActionName"].Value.ToString() : string.Empty,
                            ControllerName = menu.Attributes["ControllerName"] != null ? menu.Attributes["ControllerName"].Value.ToString() : string.Empty,
                            DisplayText = menu.Attributes["DisplayText"] != null ? menu.Attributes["DisplayText"].Value.ToString() : string.Empty,
                            Id = menu.Attributes["Id"] != null ? int.Parse(menu.Attributes["Id"].Value.ToString()) : -1,
                            SubMenu = new List<MenuItem>()
                        };
                        foreach (XmlNode subMenu in menu.ChildNodes)
                        {
                            var subMenuItem = new MenuItem
                            {
                                ActionName = subMenu.Attributes["ActionName"] != null ? subMenu.Attributes["ActionName"].Value.ToString() : string.Empty,
                                ControllerName = subMenu.Attributes["ControllerName"] != null ? subMenu.Attributes["ControllerName"].Value.ToString() : string.Empty,
                                DisplayText = subMenu.Attributes["DisplayText"] != null ? subMenu.Attributes["DisplayText"].Value.ToString() : string.Empty,
                                Id = subMenu.Attributes["Id"] != null ? int.Parse(subMenu.Attributes["Id"].Value.ToString()) : -1,
                            };
                            menuItem.SubMenu.Add(subMenuItem);
                        }
                        MenuObject.Menus.Add(menuItem);
                    }
                }
            }
        }
    }
}