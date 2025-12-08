using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS.ERP_MOB.ViewsModel.Frame;
using Syncfusion.Maui.Core.Carousel;

namespace CS.ERP_MOB.ViewsModel.Frame
{
    public class VmlSlider : BaseViewModel
    {
        public List<CarouselItem> Items { get; set; }
        public VmlSlider()
        {
            Items = new List<CarouselItem>
            {
                new CarouselItem { Title = "Company Logo 1", Image = "company_logo.png" },
                new CarouselItem { Title = "Product Logo 2", Image = "product_logo.png" },
                new CarouselItem { Title = "Bot Dotent", Image = "dotnet_bot.png" }
            };
        }
    }
}
public class CarouselItem
{
    public string Title { get; set; }
    public string Image { get; set; }
}

