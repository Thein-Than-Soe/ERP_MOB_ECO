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
    public partial class FrmSeasonal : ContentView
    {
        private List<RES_SEASONAL_STOCK> mRES_SEASONAL_STOCK_LST = new List<RES_SEASONAL_STOCK>();
        private FrmSeasonalCard mFrmSeasonalCard = new FrmSeasonalCard();
        private int pageIndex = 0;
        private int pageCount = 1;// IntercomService.mItemPerPage;
        private int remainPageItemCount = 0;

        public FrmSeasonal()
        {
            InitializeComponent();
        }

        public FrmSeasonal(List<RES_SEASONAL_STOCK> argRES_SEASONAL_STOCK_LST, bool argTitleName)
        {
            try
            {
                InitializeComponent();
                LayoutTitle.IsVisible = argTitleName;
                mRES_SEASONAL_STOCK_LST = argRES_SEASONAL_STOCK_LST;
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
                if (mRES_SEASONAL_STOCK_LST != null && mRES_SEASONAL_STOCK_LST.Count > 0)
                {
                    GrdSeasonalList.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    for (int i = 0; i < mRES_SEASONAL_STOCK_LST.Count; i++)
                    {
                        mFrmSeasonalCard = new FrmSeasonalCard(mRES_SEASONAL_STOCK_LST[i]);
                        GrdSeasonalList.Children.Add(mFrmSeasonalCard);
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


            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
    }
}