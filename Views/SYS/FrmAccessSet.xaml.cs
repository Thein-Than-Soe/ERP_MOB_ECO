using CS.ERP.PL.SYS.DAT;
using CS.ERP.PL.SYS.REQ;
using CS.ERP.PL.SYS.RES;
using CS.ERP_MOB.Data;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.SYS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;


namespace CS.ERP_MOB.Views.SYS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmAccessSet : ContentView
    {
        #region"Declaring"
        private RES_ACCESS AccessData = new RES_ACCESS();
        #endregion
        #region"Constructor"
        public FrmAccessSet()
        {
            try
            {
                InitializeComponent();
                ClearFormData();
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(1, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public FrmAccessSet(string Ask)
        {
            try
            {
                InitializeComponent();
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(1, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);
             //   GetAccessDataByAsk(Ask);
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
                //AccessData.AccessCode = CodeInput.Text;
                //AccessData.AccessName = NameInput.Text;
                //AccessData.AccessDescription = DescriptionInput.Text;
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
                //CodeInput.BadgeText =AccessData.AccessCode;
                //NameInput.BadgeText =AccessData.AccessName;
                //DescriptionInput.BadgeText =AccessData.AccessDescription;
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
                AccessData = new RES_ACCESS();
                //CodeInput.BadgeText ="";
                //NameInput.BadgeText ="";
                //DescriptionInput.BadgeText ="";
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
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
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
                    BindDataToObject();
                    this.btnSave_onClickAsync("SAVE");
                }
                else if (control.link.Equals("btnDelete_onClick"))
                {
                    this.btnSave_onClickAsync("DEL");
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
               //     AccessData.StatusAsk = "6";
               // }

               // request.RES_ACCESS.Add(AccessData);
               // string requestJson = JsonConvert.SerializeObject(request);
               //// IntercomService.ShowLoader();
               // var response = await SYSConfigService.wsCall(requestJson, SYSConfigService.wssaveAccess);
               // if (response != null)
               // {
               //     var responseData = JsonConvert.DeserializeObject<JSN_ACCESS>(response);
               //     if (responseData.Message.Code == "7")
               //     {
               //         var data = responseData.RES_ACCESS_LST;
               //         if (data != null && data.Count > 0)
               //         {
               //             AccessData = data[0];
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


        
    }
}