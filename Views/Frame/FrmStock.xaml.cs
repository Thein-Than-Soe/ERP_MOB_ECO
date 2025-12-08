using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.Data;
using CS.ERP_MOB.Models.Frame;
using CS.ERP_MOB.Services.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;


namespace CS.ERP_MOB.Views.Frame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmStock : ContentView
    {
        private List<RES_STOCK_HOME> mRES_STOCK_HOME_LST = new List<RES_STOCK_HOME>();
        private FrmStockCard mFrmStockCard = new FrmStockCard();
        private int pageIndex = 0;
        private int pageCount = 1;// IntercomService.mItemPerPage;
        private int remainPageItemCount = 0;

        public FrmStock()
        {
            InitializeComponent();
        }

        public FrmStock(List<RES_STOCK_HOME> argRES_STOCK_HOME_LST, bool argTitleName)
        {
            try
            {
                InitializeComponent();
                LayoutTitle.IsVisible = argTitleName;
                mRES_STOCK_HOME_LST = argRES_STOCK_HOME_LST;
                InitGridRows();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        //init grid row and column of a given items list
        private void InitGridRows()
        {
            try
            {
                if (mRES_STOCK_HOME_LST != null && mRES_STOCK_HOME_LST.Count > 0)
                {
                    GrdStockList.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    for (int i = 0; i < mRES_STOCK_HOME_LST.Count; i++)
                    {
                        mFrmStockCard = new FrmStockCard(mRES_STOCK_HOME_LST[i]);
                        GrdStockList.Children.Add(mFrmStockCard);
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        private void TgrBack_Tapped(object sender, EventArgs e)
        {
            try
            {
                ContentView l_ContentView = new HomePage();
                ModelRoute l_ModelRoute = new ModelRoute(l_ContentView, "Home", "");
                MessagingCenter.Send<Application, ModelRoute>(Application.Current, "ViewChange", l_ModelRoute);

                //ContentView contentView = new HomePage();
                //RoutingModel routemodel = new RoutingModel(contentView, "Home");
                //MessagingCenter.Send<Application, RoutingModel>(Application.Current, "ViewChange", routemodel);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
    }
}