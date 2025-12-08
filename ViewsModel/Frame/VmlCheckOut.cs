using CS.ERP.PL.POS.DAT;
using CS.ERP.PL.POS.REQ;
using CS.ERP.PL.POS.RES;
using CS.ERP.PL.SYS.REQ;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.POS;
using CS.ERP_MOB.Services.SYS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Controls;

namespace CS.ERP_MOB.ViewsModel.Frame
{
    public class VmlCheckOut : BaseViewModel
    {
        RES_SHOPPING ShoppingData;

        private bool mIsCheckoutPage;
        public bool IsCheckoutPage
        {
            get
            {
                return mIsCheckoutPage;
            }
            set
            {
                mIsCheckoutPage = value;
                NotifyPropertyChanged("IsCheckoutPage");
            }
        }

        private bool mIsDeliveryPage;
        public bool IsDeliveryPage
        {
            get
            {
                return mIsDeliveryPage;
            }
            set
            {
                mIsDeliveryPage = value;
                NotifyPropertyChanged("IsDeliveryPage");
            }
        }

        private bool mIsShippingPage;
        public bool IsShippingPage
        {
            get
            {
                return mIsShippingPage;
            }
            set
            {
                mIsShippingPage = value;
                NotifyPropertyChanged("IsShippingPage");
            }
        }

        private List<RES_SHOPPING> mShopping;
        public List<RES_SHOPPING> Shopping
        {
            get
            {
                return mShopping;
            }
            set
            {
                mShopping = value;
                NotifyPropertyChanged("Shopping");
            }
        }

        private List<RES_SHOPPING_DETAIL> mShoppingList;
        public List<RES_SHOPPING_DETAIL> ShoppingList
        {
            get
            {
                return mShoppingList;
            }
            set
            {
                mShoppingList = value;
                NotifyPropertyChanged("ShoppingList");
            }
        }

        private List<RES_COUNTRY_DTL> mCountryList;
        public List<RES_COUNTRY_DTL> CountryList
        {
            get
            {
                return mCountryList;
            }
            set
            {
                mCountryList = value;
                NotifyPropertyChanged("CountryList");
            }
        }

        private List<RES_PICKUP_POINT> mPickupPointList;
        public List<RES_PICKUP_POINT> PickupPointList
        {
            get
            {
                return mPickupPointList;
            }
            set
            {
                mPickupPointList = value;
                NotifyPropertyChanged("PickupPointList");
            }
        }

        private List<RES_SHIPPING_TIME> mShippingTimeList;
        public List<RES_SHIPPING_TIME> ShippingTimeList
        {
            get
            {
                return mShippingTimeList;
            }
            set
            {
                mShippingTimeList = value;
                NotifyPropertyChanged("ShippingTimeList");
            }
        }

        public async void GetShoppingData()
        {
            //JSN_REQ_SHOPPING request = new JSN_REQ_SHOPPING();
            //REQ_AUTHORIZATION authData = Common.mCommon.REQ_AUTHORIZATION;// Common.mCommon.REQ_AUTHORIZATION;
            //ShoppingData = new RES_SHOPPING();

            //authData.UserID = "guest";
            //authData.UserPassword = "123";
            //authData.TransactionName = "1";
            //request.REQ_AUTHORIZATION = authData;
            //request.RES_SHOPPING = ShoppingData;
            //string requestJson = JsonConvert.SerializeObject(request);
            ////IntercomService.ShowLoader();
            //var response = await POSConfigService.wsCall(requestJson, POSConfigService.wsgetShopping);
            //if (response != null)
            //{
            //    var responseData = JsonConvert.DeserializeObject<JSN_SHOPPING>(response);
            //    if (responseData.Message.Code == "7")
            //    {
            //        var data = responseData.RES_SHOPPING;
            //        ShoppingList = responseData.RES_SHOPPING_DETAIL;
            //        if (data.Count > 0)
            //        {
            //            ShoppingData = data[0];
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
            //   // await IntercomService.CloseLoader();
            //}
        }

        public async void loadReferenceData()
        {
            //REQ_AUTHORIZATION authData = Common.mCommon.REQ_AUTHORIZATION;

            //authData.UserID = "guest";
            //authData.UserPassword = "123";
            //authData.TransactionName = "1";
            //string requestJson = JsonConvert.SerializeObject(authData);
            ////IntercomService.ShowLoader();
            //var response = await POSConfigService.wsCall(requestJson, POSConfigService.wsloadCustomer);
            //if (response != null)
            //{
            //    var responseData = JsonConvert.DeserializeObject<JSN_LOAD_CUSTOMER>(response);
            //    if (responseData.Message.Code == "7")
            //    {
            //        CountryList = responseData.RES_COUNTRY_DTL;
            //        PickupPointList = responseData.RES_PICKUP_POINT;
            //        ShippingTimeList = responseData.RES_SHIPPING_TIME;

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
            //    //wait IntercomService.CloseLoader();
            //}
        }
    }
}
