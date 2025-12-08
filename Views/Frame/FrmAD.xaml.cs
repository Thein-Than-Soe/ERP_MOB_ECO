using CS.ERP.PL.SYS.DAT;
using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;


namespace CS.ERP_MOB.Views.Frame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmAD : ContentView
    {
        private List<RES_AD> mRES_AD_LST = new List<RES_AD>();
        private FrmADCard mFrmADCard = new FrmADCard();
        public FrmAD()
        {
            InitializeComponent();
        }

        public FrmAD(List<RES_AD> argRES_AD_LST)
        {
            try
            {
                InitializeComponent();
                mRES_AD_LST = argRES_AD_LST;
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
                if (mRES_AD_LST != null && mRES_AD_LST.Count> 0)
                {
                    foreach(RES_AD l_RES_AD in mRES_AD_LST)
                    {
                        GrdADList.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                        mFrmADCard = new FrmADCard(l_RES_AD);
                        GrdADList.Children.Add(mFrmADCard);
                    }

                    //int rowNumber = 0;
                    //while (rowNumber < mRES_AD_LST.Count)
                    //{
                    //    GrdADList.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

                    //    RES_AD item = mRES_AD_LST[rowNumber];

                    //    ADCartView cartView = new ADCartView(item);
                    //    GrdADList.Children.Add(cartView, 0, rowNumber);

                    //    //increase row number
                    //    rowNumber++;
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
    }
}