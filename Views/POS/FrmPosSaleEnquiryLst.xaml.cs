using CS.ERP.PL.POS.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Messaging;
using CS.ERP.PL.SYS.REQ;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using CS.ERP_MOB.Services.CHT;


namespace CS.ERP_MOB.Views.POS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmPosSaleEnquiryLst : ContentView
    {
        #region
        private Cht_Service_WebSocket _webSocketClient;

        //public MainPage()
        //{
        //    InitializeComponent();
        //    _webSocketClient = new Cht_Service_WebSocket();
        //    _webSocketClient.OnMessageReceived += HandleIncomingMessage;
        //}

        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();
        //    await _webSocketClient.ConnectAsync();
        //}
        //protected override async void OnDisappearing()
        //{
        //    await _webSocketClient.DisconnectAsync();
        //    base.OnDisappearing();
        //}

        private void HandleIncomingMessage(string msg)
        {
            // UI update or action based on message
            MainThread.BeginInvokeOnMainThread(() =>
            {
                WeakReferenceMessenger.Default.Send(msg);
                //DisplayAlert("Message", msg, "OK");


                JObject json = JObject.Parse(msg);
                var Req = json.Value<string>("Req");
                var Res = json.Value<string>("Res");
                var Srv = json.Value<string>("Srv");
                if (!string.IsNullOrEmpty(Req))
                {
                    Console.WriteLine($"Received Request: {Req}");
                    // Handle requests like "offer", "answer", "login", etc.
                    switch (Req)//Type, ConnectedUser, DB, Response
                    {
                        case "Register":
                            //
                            break;
                        case "getDiscussion":
                            //
                            break;
                        case "getDiscussionData":
                            //
                            break;


                        case "Offer":
                            //
                            break;
                        case "Answer":
                            //
                            break;
                        case "ice-candidate":
                            //
                            break;

                        default:
                            break;
                    }

                }
                mVmlSalesInvoice.searchData(msg);
            });
        }

        private async void OnSendClicked(object sender, EventArgs e)
        {
            var obj = new
            {
                Req = "register",
                UserAsk = "23",
                REQ_AUTHORIZATION = "Hello from MAUI client"
            };

            await _webSocketClient.SendMessageAsync(obj);
        }
        private async void send()
        {
            var obj = new
            {
                Req = "Sample",
                UserAsk = "1",
                REQ_AUTHORIZATION = new REQ_AUTHORIZATION()
            };

            await _webSocketClient.SendMessageAsync(obj);
        }
        private async void Disconnect()
        {
            _webSocketClient.DisconnectAsync();
        }
        private async void connect()
        {
            _webSocketClient = new Cht_Service_WebSocket();
            _webSocketClient.OnMessageReceived += HandleIncomingMessage;
            await _webSocketClient.ConnectAsync();

        }
        #endregion



        #region "Declaring"
        VmlSalesInvoice mVmlSalesInvoice;
        #endregion
        #region "Constructor"
        public FrmPosSaleEnquiryLst()
        {
            try
            {
                InitializeComponent();
                connect();


                BindingContext = mVmlSalesInvoice = new VmlSalesInvoice();
                mVmlSalesInvoice.mJSN_REQ_SALE_INVOICE_JUN.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlSalesInvoice.mJSN_REQ_SALE_INVOICE_JUN.RES_SALE_INVOICE = new RES_SALE_INVOICE();
                mVmlSalesInvoice.mJSN_REQ_SALE_INVOICE_JUN.RES_SALE_INVOICE_DETAIL.Add(new RES_SALE_INVOICE_DETAIL());
                mVmlSalesInvoice.mJSN_REQ_SALE_INVOICE_JUN.RES_SALE_BROWSE.Add(new RES_SALE_BROWSE());
                mVmlSalesInvoice.getInvoice();
                //switchLanguage();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region "Private Mehtod"
        public void switchLanguage()
        {
            try
            {
                switch (Common.mCommon.UserSetting.LanguageCode_0_50)
                {
                    case "EN"://English
                        {
                            //entSearch.Placeholder = "Search";
                            //TabActive.Title = TabActive.Title + " (" + mVmlSalesInvoice.InvoiceActiveList.Count + ")";
                            //TabPartial.Title = TabPartial.Title + " (" + mVmlSalesInvoice.InvoicePartialList.Count + ")";
                            //TabClosed.Title = TabClosed.Title + " (" + mVmlSalesInvoice.InvoiceClosedList.Count + ")";
                        }
                        break;
                    case "MM"://Myanmar
                        {
                            //entSearch.Placeholder = "ရှာဖွေရန်";
                            //TabActive.Title = TabActive.Title + " (" + mVmlSalesInvoice.InvoiceActiveList.Count + ")";
                            //TabPartial.Title = TabPartial.Title + " (" + mVmlSalesInvoice.InvoicePartialList.Count + ")";
                            //TabClosed.Title = TabClosed.Title + " (" + mVmlSalesInvoice.InvoiceClosedList.Count + ")";

                            //CCode.Title = "ကုန်ရောင်းပြေစာအမှတ်";
                            //CDate.Title = "ကုန်ရောင်း နေ့စွဲ";
                            //CCustomer.Title = "၀ယ်ယူသူ";
                            //CTotal.Title = "စုစုပေါင်း သင့်ငွေ";

                            //AcCode.Title = "ကုန်ရောင်းပြေစာအမှတ်";
                            //AcDate.Title = "ကုန်ရောင်း နေ့စွဲ";
                            //AcCustomer.Title = "၀ယ်ယူသူ";
                            //AcTotal.Title = "စုစုပေါင်း သင့်ငွေ";

                            //PCode.Title = "ကုန်ရောင်းပြေစာအမှတ်";
                            //PDate.Title = "ကုန်ရောင်း နေ့စွဲ";
                            //PCustomer.Title = "၀ယ်ယူသူ";
                            //PTotal.Title = "စုစုပေါင်း သင့်ငွေ";
                        }
                        break;
                    default:
                        {
                            //entSearch.Placeholder = "Search";
                            //TabActive.Title = TabActive.Title + " (" + mVmlSalesInvoice.InvoiceActiveList.Count + ")";
                            //TabPartial.Title = TabPartial.Title + " (" + mVmlSalesInvoice.InvoicePartialList.Count + ")";
                            //TabClosed.Title = TabClosed.Title + " (" + mVmlSalesInvoice.InvoiceClosedList.Count + ")";
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Event"
        private async void TgrNew_Tapped(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new FrmPosSaleInvoiceSet());

                //if (!Common.bindMenu("access-set"))
                //{
                //    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                //    WeakReferenceMessenger.Default.Send("No Access Right");
                //    //MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                //}
                //Common.routeMenu(Common.mCommon.SelectedMenu);


                //if (Common.bindMenu("access-set"))
                //{
                //    Common.routeMenu("access-set", "Access Entry");
                //}
                //else
                //{
                //    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "no access right");
                //}
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void entSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                //if (entSearch.Text != null && entSearch.Text != "")
                //{
                //    mVmlSalesInvoice.searchData(e.NewTextValue);
                //}
                //else
                //{
                //    mVmlSalesInvoice.searchData("");
                //}
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        private void lstView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var l_RES_SALE_INVOICE = (RES_SALE_INVOICE)e.SelectedItem;
                if (l_RES_SALE_INVOICE != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        WeakReferenceMessenger.Default.Send("No Access Right");
                        //MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_SALE_INVOICE.Ask) ;
                    //}
                    //else
                    //{
                    //    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "no access right");
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void cardView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var l_RES_SALE_INVOICE = (RES_SALE_INVOICE)e.SelectedItem;
                if (l_RES_SALE_INVOICE != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        WeakReferenceMessenger.Default.Send("No Access Right");
                        //MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_SALE_INVOICE.Ask) ;
                    //}
                    //else
                    //{
                    //    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "no access right");
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        private void grdView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var l_RES_SALE_INVOICE = (RES_SALE_INVOICE)e.SelectedItem;
                if (l_RES_SALE_INVOICE != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        WeakReferenceMessenger.Default.Send("No Access Right");
                        //MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_SALE_INVOICE.Ask);
                    //}
                    //else
                    //{
                    //    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "no access right");
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        private void TgrCardView_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (!Common.bindMenu("access-set"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                    WeakReferenceMessenger.Default.Send("No Access Right");
                    //MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);

                //if (Common.bindMenu("access-set"))
                //{
                //    Common.routeMenu("access-set", "Access Entry");
                //}
                //else
                //{
                //    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "no access right");
                //}
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void TgrRefresh_Tapped(object sender, EventArgs e)
        {
            try
            {
               // entSearch.Text = "";
                mVmlSalesInvoice.getInvoice();
                //test web socket send
                send();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        private void SelectItemCommand(object sender, EventArgs e)
        {
            try
            {
                //entSearch.Text = "";
                mVmlSalesInvoice.getInvoice();
                //test web socket send
                send();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        private void TgrDisplayColumn_Tapped(object sender, EventArgs e)
        {
            try
            {
                //mVmlSalesInvoice.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        private void OnFabClicked(object sender, EventArgs e)
        {
            // Handle FAB click event
            Debug.WriteLine("FAB Clicked", "You clicked the floating action button!", "OK");
        }

        #endregion

        private void OnEntryCompleted(object sender, EventArgs e)
        {
            try
            {
                string text = ((Entry)sender).Text;

                if (text != null && text != "")
                {
                    mVmlSalesInvoice.searchData(text);
                }
                else
                {
                    mVmlSalesInvoice.searchData("");
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}