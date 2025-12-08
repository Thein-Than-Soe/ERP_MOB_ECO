using CS.ERP.PL.SYS.DAT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;


namespace CS.ERP_MOB.Views.Frame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmADCard : ContentView
    {
        public FrmADCard()
        {
            InitializeComponent();
        }

        public FrmADCard(RES_AD argRES_AD)
        {
            try
            {
                InitializeComponent();
                ImgAD.Source = argRES_AD.ADPhotoURL;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            
        }

        #region ADIconSource
        public static readonly BindableProperty ADIconSourceProperty =  BindableProperty.Create(propertyName: nameof(ADIconSource),
            returnType: typeof(string),
            declaringType: typeof(FrmADCard),
            defaultValue: "icon",
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: ImgADPropertyChange);

        public string ADIconSource
        {
            get; set;
        }

        private static void ImgADPropertyChange (BindableObject argBindable, object oldValue, object argNewValue)
        {
            try
            {
                FrmADCard ThisControl = (FrmADCard)argBindable;
                string NewValue = (string)argNewValue;
                ThisControl.ImgAD.Source = NewValue;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        #endregion 
    }
}