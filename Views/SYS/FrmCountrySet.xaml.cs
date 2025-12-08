using CS.ERP.PL.SYS.DAT;
using CS.ERP.PL.SYS.REQ;
using CS.ERP.PL.SYS.RES;
using CS.ERP_MOB.Data;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.SYS;
using CS.ERP_MOB.ViewsModel.SYS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;


namespace CS.ERP_MOB.Views.SYS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmCountrySet : ContentView
    {
        #region"Declaring"
        private RES_COUNTRY mRES_COUNTRY = new RES_COUNTRY();
        VmlCountry mVmlCountry;
        #endregion
        #region"Constructor"
        public FrmCountrySet()
        {
            try
            {
               
                
                    InitializeComponent();
                    BindingContext = mVmlCountry = new VmlCountry();                  
                    List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                    BindableLayout.SetItemsSource(ControlButtons, controls);
                    mVmlCountry.mJSN_REQ_COUNTRY.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                    mVmlCountry.mJSN_REQ_COUNTRY.RES_COUNTRY.Add(new RES_COUNTRY());
                
               
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public FrmCountrySet(string argAsk)
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlCountry = new VmlCountry();
                mRES_COUNTRY.Ask = argAsk;                
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);

                mVmlCountry.mJSN_REQ_COUNTRY.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlCountry.mJSN_REQ_COUNTRY.RES_COUNTRY.Add(mRES_COUNTRY);
               

                mVmlCountry.getCountry();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }


        public FrmCountrySet(RES_COUNTRY argRES_COUNTRY)
        {
            try
            {
                InitializeComponent();               
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);

                mRES_COUNTRY = argRES_COUNTRY;
                BindDataToForm();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
        #region"Private Method"
        private void BindDataToObject()
        {
            try
            {
                if (entCode.Text == "" || entName.Text == "" || entMobileCode.Text == "")
                {

                }
                mRES_COUNTRY.CountryCode_0_50 = entCode.Text.Trim();
                mRES_COUNTRY.CountryName_0_255 = entName.Text.Trim();
                mRES_COUNTRY.CountryDescription_0_500 = entDescription.Text.Trim();
                mRES_COUNTRY.CountryMobileCode_0_50 = entMobileCode.Text.Trim();                
                mRES_COUNTRY.Scheme_0_255 = entScheme.Text.Trim();
                mRES_COUNTRY.SuperScheme_0_255 = entSuperscheme.Text.Trim();          
               
                mVmlCountry.mJSN_REQ_COUNTRY.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlCountry.mJSN_REQ_COUNTRY.RES_COUNTRY.Add(mRES_COUNTRY);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        private void BindDataToForm()
        {
            try
            {
                mRES_COUNTRY.CountryCode_0_50 = entCode.Text.Trim();
                mRES_COUNTRY.CountryName_0_255 = entName.Text.Trim();
                mRES_COUNTRY.CountryDescription_0_500 = entDescription.Text.Trim();
                mRES_COUNTRY.CountryMobileCode_0_50 = entMobileCode.Text.Trim();
                mRES_COUNTRY.Scheme_0_255 = entScheme.Text.Trim();
                mRES_COUNTRY.SuperScheme_0_255 = entSuperscheme.Text.Trim();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        public void ClearFormData()
        {
            try
            {
                mRES_COUNTRY = new RES_COUNTRY();
                entCode.Text ="";
                entName.Text ="";
                entDescription.Text ="";
                entMobileCode.Text ="";
                entScheme.Text ="";
                entSuperscheme.Text ="";
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        #endregion
        #region"Publich Method"
        #endregion
        #region""
        #endregion
        #region"Event"
        private void BackTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {

                if (!Common.bindMenu("access-lst"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmCountryLst", MenuUrl = "sys-country-lst", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);

                ////ContentView contentView = new AccessListPage();
                ////RoutingModel routemodel = new RoutingModel("Access List", contentView);
                ////MessagingCenter.Send<Application, RoutingModel>(Application.Current, "ViewChange", routemodel);
                //Common.routeMenu("access-lst", "Access List");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void ControlTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                RES_CONTROL control = (RES_CONTROL)(((Microsoft.Maui.Controls.Frame)sender).BindingContext);
                if (control.link.Equals("btnSave_onClick"))
                {
                    if(mRES_COUNTRY.Ask != "0")
                    {
                        mRES_COUNTRY.StatusAsk = "2";
                    }
                    else
                    {
                        mRES_COUNTRY.StatusAsk = "1";
                    }
                   
                    BindDataToObject();
                    mVmlCountry.saveCountry();
                }
                else if (control.link.Equals("btnDelete_onClick"))
                {
                    mRES_COUNTRY.StatusAsk = "6";
                    BindDataToObject();
                    mVmlCountry.saveCountry();
                }
                else if (control.link.Equals("btnNew_onClick"))
                {
                    ClearFormData();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        private async void btnSave_onClickAsync(string action)
        {
            try
            {
                // JSN_REQ_ACCESS request = new JSN_REQ_ACCESS();
                // REQ_AUTHORIZATION authData = Common.mCommon.REQ_AUTHORIZATION;

                // //Crypto crypto = new Crypto();
                // //authData.UserPassword = crypto.Decrypt(authData.UserPassword);
                // authData.UserID = "yi";
                // authData.UserPassword = "Artsign@123";
                // authData.TransactionName = "1";
                // request.REQ_AUTHORIZATION = authData;

                // if (action.Equals("DEL"))
                // {
                //     mRES_COUNTRY.StatusAsk = "6";
                // }

                // request.RES_COUNTRY.Add(mRES_COUNTRY);
                // string requestJson = JsonConvert.SerializeObject(request);
                //// IntercomService.ShowLoader();
                // var response = await SYSConfigService.wsCall(requestJson, SYSConfigService.wssaveAccess);
                // if (response != null)
                // {
                //     var responseData = JsonConvert.DeserializeObject<JSN_ACCESS>(response);
                //     if (responseData.Message.Code == "7")
                //     {
                //         var data = responseData.RES_COUNTRY_LST;
                //         if (data != null && data.Count > 0)
                //         {
                //             mRES_COUNTRY = data[0];
                //             BindDataToForm();
                //         }

                //         if (action.Equals("DEL"))
                //         {
                //             ClearFormData();
                //             MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "Delete Success");
                //         }
                //         else if (action.Equals("VOID"))
                //         {
                //             MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "Void Success");
                //         }
                //         else
                //         {
                //             MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "Save Success");
                //         }

                //     }
                //     else
                //     {
                //         MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "Fail");
                //     }

                //     //await IntercomService.CloseLoader();

                // }
                // else
                // {
                //     MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "Server Err");
                //     //await IntercomService.CloseLoader();
                // }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void NewTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                ClearFormData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        private void Input_Focused(object sender, FocusEventArgs e)
        {
            try
            {//AbsoluteLayout.SetLayoutBounds(FooterLayout, new Rectangle(0, 0.5, 1, 40));
             //AbsoluteLayout.SetLayoutFlags(FooterLayout, AbsoluteLayoutFlags.PositionProportional);
             //AbsoluteLayout.SetLayoutFlags(FooterLayout, AbsoluteLayoutFlags.WidthProportional);
             //RootLayout.TranslationY = -250;
                RootLayout.Margin = new Thickness(0, 0, 0, 250);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        private void Input_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                //RootLayout.TranslationY = 0;
                RootLayout.Margin = new Thickness(0, 0, 0, 0);
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
                ClearFormData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        private void TgrNew_Tapped(object sender, EventArgs e)
        {
            try
            {
                ClearFormData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        private void TgrControl_Tapped(object sender, EventArgs e)
        {
            try
            {
                ClearFormData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }


        #endregion

        public async void GetmRES_COUNTRYByAsk(string Ask)
        {

            //JSN_REQ_ACCESS request = new JSN_REQ_ACCESS();
            //REQ_AUTHORIZATION authData = Common.mCommon.REQ_AUTHORIZATION;
            //RES_COUNTRY mRES_COUNTRY = new RES_COUNTRY();

            ////Crypto crypto = new Crypto();
            ////authData.UserPassword = crypto.Decrypt(authData.UserPassword);
            //authData.UserID = "yi";
            //authData.UserPassword = "Artsign@123";
            //authData.TransactionName = "1";
            //request.REQ_AUTHORIZATION = authData;
            //mRES_COUNTRY.Ask = Ask;
            //request.RES_COUNTRY.Add(mRES_COUNTRY);
            //string requestJson = JsonConvert.SerializeObject(request);
            ////IntercomService.ShowLoader();
            //var response = await SYSConfigService.wsCall(requestJson, SYSConfigService.wsgetAccess);
            //if (response != null)
            //{
            //    var responseData = JsonConvert.DeserializeObject<JSN_ACCESS>(response);
            //    if (responseData.Message.Code == "7")
            //    {
            //        var data = responseData.RES_COUNTRY_LST;
            //        if (data != null && data.Count > 0)
            //        {
            //            mRES_COUNTRY = data[0];
            //            BindDataToForm();
            //        }

            //        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "Success");
            //    }
            //    else
            //    {
            //        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "Fail");
            //    }

            //    //await IntercomService.CloseLoader();

            //}
            //else
            //{
            //    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "Server Err");
            //    //await IntercomService.CloseLoader();
            //}
        }
    }
}