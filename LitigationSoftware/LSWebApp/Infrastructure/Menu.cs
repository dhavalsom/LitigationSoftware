using System.Collections.Generic;

namespace LSWebApp.Infrastructure
{
    public class Menu
    {
        public List<MenuItem> Menus { get; set; }
    }

    public enum Pages
    {
        None,
        MainPage,
        SecondPage
    }
}