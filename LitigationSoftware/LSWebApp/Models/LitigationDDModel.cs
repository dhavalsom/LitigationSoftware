using System.Collections.Generic;
using System.Web.Mvc;

namespace LSWebApp.Models
{
    public class LitigationDDModel
    {
        public List<SelectListItem> Source { get; set; }
        public string BindingProperty { get; set; }
        public string Description { get; set; }
        public string AddActionName { get; set; }
        public string RefreshActionName { get; set; }

        public LitigationDDModel(List<SelectListItem> source, string bindingProperty
            , string addActionName, string refreshActionName)
        {
            this.Source = source;
            this.BindingProperty = bindingProperty;
            this.AddActionName = addActionName;
            this.RefreshActionName = refreshActionName;
        }

    }
}