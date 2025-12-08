using System;
using Microsoft.Maui.Controls;

namespace CS.ERP_MOB.Models.Frame
{
    public class ModelRoute
    {
        public string Title { get; set; }
        public ContentView Page { get; set; }
        public ContentPage ContentPage { get; set; }
        public string Icon { get; set; }
        public ModelRoute(ContentView argView, string argTitle, string argIcon)
        {
            this.Page = argView;
            this.Title =argTitle;
            this.Icon = argIcon;
        }
        public ModelRoute(ContentPage argView, string argTitle, string argIcon)
        {
            this.ContentPage = argView;
            this.Title = argTitle;
            this.Icon = argIcon;
        }
    }
}
