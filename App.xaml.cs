using CS.ERP.PL.SYS.REQ;
using CS.ERP.PL.SYS.RES;
using CS.ERP_MOB.DB;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.SYS;
using CS.ERP_MOB.Views.Frame;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using FreshMvvm.Maui;
using CS.ERP.BOL;
using CS.ERP.PL.SYS.DAT;
using CommunityToolkit.Mvvm.Messaging;
using CS.ERP_MOB.Models.Frame;
using CS.ERP_MOB.Extensions;

namespace CS.ERP_MOB
{
    public partial class App : Application
    {

        //public static object DB { get; internal set; }
        static Database mDatabase = new Database();
        public static Database Database
        {
            get
            {
                if (mDatabase == null)
                {

                    mDatabase = new Database();
                }
                return mDatabase;
            }
        }
        private ApplicationMessage mDApplicationMessage = new ApplicationMessage();
        public ApplicationMessage ApplicationMessage
        {
            get
            {
                if (mDApplicationMessage == null)
                {

                    mDApplicationMessage = new ApplicationMessage();
                }
                return mDApplicationMessage;
            }
        }

       
        public App()
        {
            try
            {

                InitializeComponent();

                //MainPage = new AppShell();
                //var seconds = TimeSpan.FromSeconds(3);
                //Xamarin.Forms.Device.StartTimer(seconds,
                //    () =>
                //    {
                //    //IntercomService.Current.CheckConnection();
                //    Common.mCommon.checkInternetCon();
                //        return true;
                //    });
                //get from remember user
                //Common.mCommon.signInAuto();

                var page = FreshPageModelResolver.ResolvePageModel<MainPageModel>();
                var basicNavContainer = new FreshNavigationContainer(page);
                MainPage = basicNavContainer;

                General.Common.mCommon.signInAuto();

            }
            catch (Exception ex)
            {
                //ex.InnerException.Message;
                throw ex.InnerException;
            }
        }

        protected override void OnStart()
        {
            try
            {
                //Common.mCommon.signIn(Common.mCommon.REQ_AUTHORIZATION);
            }
            catch (Exception ex)
            {
                //throw ex.InnerException;
            }
            
        }

        protected override void OnSleep()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                //throw ex.InnerException; 
            }
        }

        protected override void OnResume()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                //throw ex.InnerException;
            }
        }
    }
}
