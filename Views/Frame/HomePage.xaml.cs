using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.Data;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Models.Frame;
using CS.ERP_MOB.Services.SYS;
using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;


namespace CS.ERP_MOB.Views.Frame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentView
    {
        public HomePage()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception)
            {
               
            }
        }
    }
}