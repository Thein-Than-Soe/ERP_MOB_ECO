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
    public partial class FrmPromotion : ContentView
    {
        private List<RES_PROMOTION> mRES_PROMOTION_LST = new List<RES_PROMOTION>();
        private FrmPromotionCard mFrmPromotionCard = new FrmPromotionCard();
        private int pageIndex = 0;
        private int pageCount = 1;// IntercomService.mItemPerPage;
        private int remainPageItemCount = 0;

        public FrmPromotion()
        {
            InitializeComponent();
        }

        public FrmPromotion(List<RES_PROMOTION> argRES_PROMOTION_LST, bool argTitleName)
        {
            try
            {
                InitializeComponent();
                LayoutTitle.IsVisible = argTitleName;
                mRES_PROMOTION_LST = argRES_PROMOTION_LST;
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
                if (mRES_PROMOTION_LST != null && mRES_PROMOTION_LST.Count>0)
                {
                    GrdPromotionList.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    for (int i = 0; i < mRES_PROMOTION_LST.Count; i++)
                    {
                        mFrmPromotionCard = new FrmPromotionCard(mRES_PROMOTION_LST[i]);
                        GrdPromotionList.Children.Add(mFrmPromotionCard);
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
                ModelRoute l_ModelRoute = new ModelRoute(l_ContentView, "Home","");
                MessagingCenter.Send<Application, ModelRoute>(Application.Current, "ViewChange", l_ModelRoute);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            
        }
    }
}