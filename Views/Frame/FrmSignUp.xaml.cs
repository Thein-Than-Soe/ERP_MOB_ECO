
//using CS.ERP.PL.POS.DAT;
using CommunityToolkit.Mvvm.Messaging;
using CS.ERP.PL.SYS.DAT;
using CS.ERP.PL.SYS.REQ;
using CS.ERP.PL.SYS.RES;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.SYS;
using CS.ERP_MOB.ViewsModel.Frame;
using Newtonsoft.Json;
using RGPopup.Maui.Extensions;
using static CS.ERP_MOB.General.Utility;
using static SQLite.SQLite3;
using ApplicationMessage = CS.ERP_MOB.General.ApplicationMessage;

namespace CS.ERP_MOB.Views.Frame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmSignUp : ContentView
    {
        #region "Declaration"
        VmlSignUp mVmlSignUp;
        JSN_REQ_CUSTOMER_REG ml_JSN_REQ_CUSTOMER_REG = new JSN_REQ_CUSTOMER_REG();
        JSN_CUSTOMER_REG mJSN_CUSTOMER_REG = new JSN_CUSTOMER_REG();
        JSN_LOAD_SUBSCRIBER mJSN_LOAD_SUBSCRIBER = new JSN_LOAD_SUBSCRIBER();
        JSN_REQ_SUBSCRIBER mJSN_REQ_SUBSCRIBER = new JSN_REQ_SUBSCRIBER();
        JSN_SUBSCRIBER mJSN_SUBSCRIBER = new JSN_SUBSCRIBER();
        SignUpState mSignUpState = SignUpState.SignUp;

        int seconds = 30;
        #endregion
        #region "Constructor"
        public FrmSignUp()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlSignUp = new VmlSignUp();
                mVmlSignUp.IsSignupPage = true;
                emailOTPBox();
                phoneOTPBox();

                // Application.Current.MainPage.Navigation.PushPopupAsync(new SubscriptionPaymentPopup(new RES_SUB_PAYMENT(), new RES_SUB_PLAN()));

                //listen payment dialog callback
                MessagingCenter.Subscribe<Application, bool>(Application.Current, "PaymentDialog", (sender, IsPaymentSuccess) =>
                {
                    if (IsPaymentSuccess)
                    {
                        activateSubscriber();
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "Fail subscription payment");
                        //IntercomService.RouteMenu("signin", "Sign in");
                    }
                });
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion
        #region "Property"
        #endregion
        #region "Private Method"
        private void emailOTPBox()
        {
            otp1.TextChanged += (s, e) => { if (otp1.Text.Length == 1) otp2.Focus(); };
            otp2.TextChanged += (s, e) =>
            {
                if (otp2.Text?.Length == 1)
                    otp3.Focus();
                else if (string.IsNullOrEmpty(otp2.Text))
                    otp1.Focus();
            };
            otp3.TextChanged += (s, e) =>
            {
                if (otp3.Text?.Length == 1)
                    otp4.Focus();
                else if (string.IsNullOrEmpty(otp3.Text))
                    otp2.Focus();
            };
            otp4.TextChanged += (s, e) =>
            {
                if (otp4.Text?.Length == 1)
                    otp5.Focus();
                else if (string.IsNullOrEmpty(otp4.Text))
                    otp3.Focus();
            };
            otp5.TextChanged += (s, e) =>
            {
                if (otp5.Text?.Length == 1)
                    otp6.Focus();
                else if (string.IsNullOrEmpty(otp5.Text))
                    otp4.Focus();
            };
            otp6.TextChanged += (s, e) =>
            {
                if (otp6.Text?.Length == 1)
                    otp6.Focus();
                else if (string.IsNullOrEmpty(otp6.Text))
                    otp5.Focus();
            };
        }
        private void phoneOTPBox()
        {
            phoneOTP1.TextChanged += (s, e) => { if (phoneOTP1.Text.Length == 1) phoneOTP2.Focus(); };
            phoneOTP2.TextChanged += (s, e) =>
            {
                if (phoneOTP2.Text?.Length == 1)
                    phoneOTP3.Focus();
                else if (string.IsNullOrEmpty(phoneOTP2.Text))
                    phoneOTP1.Focus();
            };
            phoneOTP3.TextChanged += (s, e) =>
            {
                if (phoneOTP3.Text?.Length == 1)
                    phoneOTP4.Focus();
                else if (string.IsNullOrEmpty(phoneOTP3.Text))
                    phoneOTP2.Focus();
            };
            phoneOTP4.TextChanged += (s, e) =>
            {
                if (phoneOTP4.Text?.Length == 1)
                    phoneOTP5.Focus();
                else if (string.IsNullOrEmpty(phoneOTP4.Text))
                    phoneOTP3.Focus();
            };
            phoneOTP5.TextChanged += (s, e) =>
            {
                if (phoneOTP5.Text?.Length == 1)
                    phoneOTP6.Focus();
                else if (string.IsNullOrEmpty(phoneOTP5.Text))
                    phoneOTP4.Focus();
            };
            phoneOTP6.TextChanged += (s, e) =>
            {
                if (phoneOTP6.Text?.Length == 1)
                    phoneOTP6.Focus();
                else if (string.IsNullOrEmpty(phoneOTP6.Text))
                    phoneOTP5.Focus();
            };
        }
        #region "Bind Object"
        private bool bindRegistration()
        {
            bool flag = true;
            try
            {
                ml_JSN_REQ_CUSTOMER_REG.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                //Name
                if (!string.IsNullOrWhiteSpace(entName.Text))
                {
                    ml_JSN_REQ_CUSTOMER_REG.RES_CUSTOMER.CustomerName_0_255 = entName.Text.Trim();
                }
                else
                {
                    flag = false;
                    entName.Focus();
                }
                //Email
                if (!string.IsNullOrWhiteSpace(entEmail.Text))
                {
                    ml_JSN_REQ_CUSTOMER_REG.RES_CUSTOMER.CustomerEmail = entEmail.Text.Trim();
                }
                else
                {
                    flag = false;
                    entEmail.Focus();
                }
                //Country
                if (pikCountry.SelectedItem != null)
                {
                    var l_RES_COUNTRY = pikCountry.SelectedItem as RES_COUNTRY;
                    ml_JSN_REQ_CUSTOMER_REG.RES_CUSTOMER.CustomerCountryAsk = l_RES_COUNTRY.Ask;
                }
                else
                {
                    flag = false;
                    pikCountry.Focus();
                }
                //Phone
                if (!string.IsNullOrWhiteSpace(entPhone.Text))
                {
                    ml_JSN_REQ_CUSTOMER_REG.RES_CUSTOMER.CustomerMobilePhone = entPhone.Text.Trim();
                }
                else
                {
                    flag = false;
                    entPhone.Focus();
                }
                //Password

                if (!string.IsNullOrWhiteSpace(entPassword.Text))
                {
                    if (!string.IsNullOrWhiteSpace(entConfirmPassword.Text))
                    {
                        if (entPassword.Text == entConfirmPassword.Text)
                        {
                            ml_JSN_REQ_CUSTOMER_REG.RES_CUSTOMER.CustomerPwd = entPassword.Text.Trim();
                        }
                        else
                        {
                            flag = false;
                            entConfirmPassword.Focus();
                            WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("MsgRegisterPWDNotSame"));
                        }
                    }
                    else
                    {
                        flag = false;
                        entConfirmPassword.Focus();
                    }
                }
                else
                {
                    flag = false;
                    entPassword.Focus();
                }
                return flag;

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private bool bindAuthorization()
        {
            bool flag = true;
            try
            {
                Common.mCommon.REQ_AUTHORIZATION.UserID = this.mJSN_CUSTOMER_REG.RES_CUSTOMER.CustomerEmail;
                Common.mCommon.REQ_AUTHORIZATION.UserPassword = this.mJSN_CUSTOMER_REG.RES_CUSTOMER.CustomerPwd;
                return flag;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private bool bindActivationCode()
        {
            bool flag = true;
            try
            {
                ml_JSN_REQ_CUSTOMER_REG.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                //Name
                string code = otp1.Text + otp2.Text + otp3.Text + otp4.Text + otp5.Text + otp6.Text;
                if (mJSN_CUSTOMER_REG.RES_CUSTOMER.Ask != "0" && !string.IsNullOrWhiteSpace(code))
                {
                    ml_JSN_REQ_CUSTOMER_REG.RES_CUSTOMER = mJSN_CUSTOMER_REG.RES_CUSTOMER;
                    ml_JSN_REQ_CUSTOMER_REG.RES_CUSTOMER.CustomerDescription_0_500 = lblEmailPrefix.Text + code;
                }
                else
                {
                    flag = false;
                    //entActivationCode.Focus();
                    otp1.Focus();

                }
                return flag;

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private bool bindPhoneActivationCode()
        {
            bool flag = true;
            try
            {
                ml_JSN_REQ_CUSTOMER_REG.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                //Name
                string code = phoneOTP1.Text + phoneOTP2.Text + phoneOTP3.Text + phoneOTP4.Text + phoneOTP5.Text + phoneOTP6.Text;
                if (mJSN_CUSTOMER_REG.RES_CUSTOMER.Ask != "0" && !string.IsNullOrWhiteSpace(code))
                {
                    ml_JSN_REQ_CUSTOMER_REG.RES_CUSTOMER = mJSN_CUSTOMER_REG.RES_CUSTOMER;
                    ml_JSN_REQ_CUSTOMER_REG.RES_CUSTOMER.CustomerDescription_0_500 = lblPhonePrefix.Text + code;
                }
                else
                {
                    flag = false;
                    //entActivationCode.Focus();
                    phoneOTP1.Focus();

                }
                return flag;

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private bool bindSubscriber()
        {
            bool flag = true;
            try
            {
                mJSN_REQ_SUBSCRIBER.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                var l_RES_SUB_PLAN = (RES_SUB_PLAN)lstSubscriptionPlan.SelectedItem;
                if (l_RES_SUB_PLAN != null)
                {
                    mJSN_REQ_SUBSCRIBER.RES_CUSTOMER = mJSN_CUSTOMER_REG.RES_CUSTOMER;
                    mJSN_REQ_SUBSCRIBER.RES_SUB_PLAN.Add(l_RES_SUB_PLAN);
                    mJSN_REQ_SUBSCRIBER.RES_SUB_PLAN_DETAIL = l_RES_SUB_PLAN.RES_SUB_PLAN_DETAIL;
                    mJSN_REQ_SUBSCRIBER.RES_SUBSCRIBER.SubscriberEmail = mJSN_CUSTOMER_REG.RES_CUSTOMER.CustomerEmail;
                    mJSN_REQ_SUBSCRIBER.RES_SUBSCRIBER.SubscriberName_0_255 = mJSN_CUSTOMER_REG.RES_CUSTOMER.CustomerName_0_255;
                    mJSN_REQ_SUBSCRIBER.RES_SUBSCRIBER.SubscriberCountryAsk = mJSN_CUSTOMER_REG.RES_CUSTOMER.CustomerCountryAsk;
                    mJSN_REQ_SUBSCRIBER.RES_SUBSCRIBER.SubscriberMobilePhone = mJSN_CUSTOMER_REG.RES_CUSTOMER.CustomerMobilePhone;
                    mJSN_REQ_SUBSCRIBER.RES_SUBSCRIBER.ParentAsk = mJSN_REQ_SUBSCRIBER.RES_CUSTOMER.Ask;
                    mJSN_REQ_SUBSCRIBER.RES_SUBSCRIBER.SubscriberTypeAsk = l_RES_SUB_PLAN.SubscriberTypeAsk;
                    mJSN_REQ_SUBSCRIBER.RES_SUBSCRIBER.AccessAsk = "1";
                    mJSN_REQ_SUBSCRIBER.RES_SUBSCRIBER.CreditLimit = "0";
                    mJSN_REQ_SUBSCRIBER.RES_SUBSCRIBER.SubscriberFee = l_RES_SUB_PLAN.Price;
                }
                else
                {
                    flag = false;
                }
                return flag;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion
        #region "Api Method"
        private async void saveRegistration()
        {
            string l_Request = "";
            var l_Response = "";
            try
            {
                l_Request = JsonConvert.SerializeObject(ml_JSN_REQ_CUSTOMER_REG);
                General.Utility.openLoader();
                l_Response = await Sys_Service.ApiCall(l_Request, Sys_Name.wsSaveRegistration);
                if (l_Response != null)
                {
                    this.mJSN_CUSTOMER_REG = JsonConvert.DeserializeObject<JSN_CUSTOMER_REG>(l_Response);
                    if (mJSN_CUSTOMER_REG.Message.Code == "7")
                    {
                        if (Common.mCommon.UserSetting.VerifiedByEmail == "1")
                        {
                            showVerifyEmail();
                        }
                        else if (Common.mCommon.UserSetting.VerifiedByPhoneNo == "1")
                        {
                            showVerifyPhone();
                        }
                        WeakReferenceMessenger.Default.Send(this.mJSN_CUSTOMER_REG?.Message?.Message);
                    }
                    else
                    {
                        WeakReferenceMessenger.Default.Send(this.mJSN_CUSTOMER_REG?.Message?.Message);
                    }
                }
                else
                {
                    WeakReferenceMessenger.Default.Send("Server Err");
                }
                Utility.closeLoader();
            }
            catch (Exception ex)
            {
                Utility.closeLoader();
                throw ex.InnerException;
            }

        }
        private async void getActivationCode()
        {
            string l_Request = "";
            var l_Response = "";
            try
            {
                l_Request = JsonConvert.SerializeObject(ml_JSN_REQ_CUSTOMER_REG);
                General.Utility.openLoader();
                l_Response = await Sys_Service.ApiCall(l_Request, Sys_Name.wsgetActivationCode);
                if (l_Response != null)
                {

                    if (JsonConvert.DeserializeObject<JSN_CUSTOMER_REG>(l_Response).Message.Code == "7")
                    {
                        this.mJSN_CUSTOMER_REG = JsonConvert.DeserializeObject<JSN_CUSTOMER_REG>(l_Response);
                        if (Common.mCommon.UserSetting.VerifiedByEmail == "1")
                        {
                            showVerifyEmail();
                        }
                        else if (Common.mCommon.UserSetting.VerifiedByPhoneNo == "1")
                        {
                            showVerifyPhone();
                        }
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", this.mJSN_CUSTOMER_REG.Message.Message);
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", this.mJSN_LOAD_SUBSCRIBER.Message.Message);
                    }
                }
                else
                {
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "Server Err");
                }
                Utility.closeLoader();
            }
            catch (Exception ex)
            {
                Utility.closeLoader();
                throw ex.InnerException;
            }

        }
        private async void activateRegistration()
        {
            string l_Request = "";
            var l_Response = "";
            try
            {
                l_Request = JsonConvert.SerializeObject(ml_JSN_REQ_CUSTOMER_REG);
                General.Utility.openLoader();
                l_Response = await Sys_Service.ApiCall(l_Request, Sys_Name.wsActivateRegistration);
                if (l_Response != null)
                {
                    if (JsonConvert.DeserializeObject<JSN_CUSTOMER_REG>(l_Response).Message.Code == "7")
                    {
                        this.mJSN_CUSTOMER_REG = JsonConvert.DeserializeObject<JSN_CUSTOMER_REG>(l_Response);
                        if (bindAuthorization())
                        {
                            showSubscription();
                        }
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", this.mJSN_CUSTOMER_REG.Message.Message);
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", this.mJSN_LOAD_SUBSCRIBER.Message.Message);
                    }
                }
                else
                {
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "Server Err");
                }
                Utility.closeLoader();
            }
            catch (Exception ex)
            {
                Utility.closeLoader();
                throw ex.InnerException;
            }

        }
        private async void loadSubscriber()
        {
            string l_Request = "";
            var l_Response = "";
            try
            {
                l_Request = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                General.Utility.openLoader();
                l_Response = await Sys_Service.ApiCall(l_Request, Sys_Name.wsLoadSubscriber);
                if (l_Response != null)
                {

                    if (JsonConvert.DeserializeObject<JSN_LOAD_SUBSCRIBER>(l_Response).Message.Code == "7")
                    {
                        this.mJSN_LOAD_SUBSCRIBER = JsonConvert.DeserializeObject<JSN_LOAD_SUBSCRIBER>(l_Response);
                        showSubscriberType();
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "We are ready to support your business grow!");
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", this.mJSN_LOAD_SUBSCRIBER.Message.Message);
                    }
                }
                else
                {
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "Server Err");
                }
                Utility.closeLoader();
            }
            catch (Exception ex)
            {
                Utility.closeLoader();
                throw ex.InnerException;
            }
        }
        private async void saveSubscriber()
        {
            string l_Request = "";
            var l_Response = "";
            try
            {
                l_Request = JsonConvert.SerializeObject(mJSN_REQ_SUBSCRIBER);
                Utility.openLoader();
                l_Response = await Sys_Service.ApiCall(l_Request, Sys_Name.wsSaveSubscriber);
                if (l_Response != null)
                {
                    this.mJSN_SUBSCRIBER = JsonConvert.DeserializeObject<JSN_SUBSCRIBER>(l_Response);
                    if (mJSN_SUBSCRIBER?.Message.Code == "7")
                    {
                        if (Convert.ToDecimal(this.mJSN_REQ_SUBSCRIBER.RES_SUBSCRIBER.SubscriberFee) == 0)
                        {
                            if (this.mJSN_SUBSCRIBER.RES_SUBSCRIBER.Count > 0)
                                mJSN_REQ_SUBSCRIBER.RES_SUBSCRIBER = this.mJSN_SUBSCRIBER.RES_SUBSCRIBER[0];
                            Utility.closeLoader();
                            this.activateSubscriber();
                        }
                        else
                        {
                            Utility.closeLoader();
                            showSubscriptionPayment();
                        }
                        WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("MsgSubscribeSuccess"));
                    }
                    else
                    {
                        WeakReferenceMessenger.Default.Send(this.mJSN_SUBSCRIBER.Message.Message);
                    }
                }
                else
                {
                    WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("ErrWebService"));
                }
                Utility.closeLoader();
            }
            catch (Exception ex)
            {
                Utility.closeLoader();
                throw ex.InnerException;
            }
        }
        private async void activateSubscriber()
        {
            string l_Request = "";
            var l_Response = "";
            try
            {
                l_Request = JsonConvert.SerializeObject(mJSN_REQ_SUBSCRIBER);
                Utility.openLoader();
                l_Response = await Sys_Service.ApiCall(l_Request, Sys_Name.wsActivateSubscriber);
                if (l_Response != null)
                {
                    this.mJSN_SUBSCRIBER = JsonConvert.DeserializeObject<JSN_SUBSCRIBER>(l_Response);
                    if (mJSN_SUBSCRIBER?.Message.Code == "7")
                    {
                        Utility.closeLoader();
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "Sign In", MenuUrl = "signin", logoImg = "" };
                        Common.routeMenu(Common.mCommon.SelectedMenu);
                        //this.mJSN_SUBSCRIBER = JsonConvert.DeserializeObject<JSN_SUBSCRIBER>(l_Response);
                        //showActivation();
                        //MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "We are ready to support your business grow!");
                    }
                    else
                    {
                        WeakReferenceMessenger.Default.Send(this.mJSN_SUBSCRIBER.Message.Message);
                    }
                }
                else
                {
                    WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("ErrWebService"));
                }
                Utility.closeLoader();
            }
            catch (Exception ex)
            {
                Utility.closeLoader();
                throw ex.InnerException;
            }
        }
        #endregion
        #region "Show View"
        private void showVerifyEmail()
        {
            try
            {
                switchSignUpState(SignUpState.EmailActivate);
                lblEmailPrefix.Text = this.mJSN_CUSTOMER_REG.RES_CUSTOMER.CustomerDescription_0_500;
                lblEmailOTPInstruction.Text = Common.mCommon.GetLanguageValueByKey("frame.Register.lbl.ActivationCode") + Common.mCommon.maskEmail(this.mJSN_CUSTOMER_REG.RES_CUSTOMER.CustomerEmail);
                otp1.Focus();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private async Task showVerifyPhone()
        {
            try
            {
                string result = "";
                RES_MESSAGE? response = await Common.mCommon.getSMSOTP();
                if (response != null && response.Code == "7")
                {
                    var parts = response.Message.Split(',');

                    if (parts.Length > 1)
                    {
                        result = parts[1];
                    }
                }
                switchSignUpState(SignUpState.PhoneActivate);
                lblPhonePrefix.Text = result;
                lblPhoneOTPInstruction.Text = Common.mCommon.GetLanguageValueByKey("frame.Register.lbl.PhoneActivationCode") + Common.mCommon.maskPhone(this.mJSN_CUSTOMER_REG.RES_CUSTOMER.CustomerMobilePhone);
                phoneOTP1.Focus();

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void showSubscription()
        {
            try
            {
                switchSignUpState(SignUpState.Subscribe);
                //entActivationCode.Text = this.mJSN_CUSTOMER_REG.RES_CUSTOMER.CustomerDescription_0_500;
                //entActivationCode.Focus();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void showSubscriberType()
        {
            try
            {
                switchSignUpState(SignUpState.SubscriptionType);
                lstSubscriberType.ItemsSource = mJSN_LOAD_SUBSCRIBER.RES_SUB_TYPE;
                lstSubscriberType.SelectedItem = mJSN_LOAD_SUBSCRIBER.RES_SUB_TYPE[0];
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void showSubscriberPlan(RES_SUB_TYPE argRES_SUB_TYPE)
        {
            try
            {
                switchSignUpState(SignUpState.SubscriptionPlan);
                lstSubscriptionPlan.ItemsSource = argRES_SUB_TYPE.RES_SUB_PLAN;
                lstSubscriptionPlan.SelectedItem = argRES_SUB_TYPE.RES_SUB_PLAN[0];
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private async void showSubscriptionPayment()
        {
            try
            {
                if (this.mJSN_SUBSCRIBER.RES_SUBSCRIBER[0].Ask != "0" && this.mJSN_SUBSCRIBER.RES_SUB_PAYMENT.Ask != "0")
                {
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new FrmSubscriptionPayment(mJSN_SUBSCRIBER.RES_SUB_PAYMENT, mJSN_SUBSCRIBER.RES_SUB_PLAN[0]));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void switchSignUpState(SignUpState argSignUpState)
        {
            try
            {
                mVmlSignUp.IsSignupPage = false;
                mVmlSignUp.IsActivatePage = false;
                mVmlSignUp.IsPhoneActivatePage = false;
                mVmlSignUp.IsOtherServicePage = false;
                mVmlSignUp.IsChooseTypePage = false;
                mVmlSignUp.IsChoosePlanPage = false;
                mSignUpState = argSignUpState;
                switch (mSignUpState)
                {
                    case SignUpState.SignUp:
                        mVmlSignUp.IsSignupPage = true;
                        break;
                    case SignUpState.EmailActivate:
                        {
                            mVmlSignUp.IsActivatePage = true;
                            StartEmailResendTimer();
                            break;
                        }
                    case SignUpState.PhoneActivate:
                        mVmlSignUp.IsPhoneActivatePage = true;
                        StartPhoneResendTimer();
                        break;
                    case SignUpState.Subscribe:
                        mVmlSignUp.IsOtherServicePage = true;
                        break;
                    case SignUpState.SubscriptionType:
                        mVmlSignUp.IsChooseTypePage = true;
                        break;
                    case SignUpState.SubscriptionPlan:
                        mVmlSignUp.IsChoosePlanPage = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion #endregion
        #endregion
        #region "Public Method"
        #endregion
        #region "Event"
        private void btnSignUp_onClicked(object sender, EventArgs e)
        {
            try
            {
                if (bindRegistration())
                {
                    saveRegistration();
                }
                else
                {
                    WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("MsgBlanked"));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void btnCancel_onClicked(object sender, EventArgs e)
        {
            try
            {
                if (!Common.bindMenu("signin"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "Sign In", MenuUrl = "signin", logoImg = "" };
                    WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("MsgAccess"));
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        void StartEmailResendTimer()
        {
            seconds = 30;
            btnEmailResend.IsEnabled = false;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                seconds--;

                btnEmailResend.Text = $"Resend ({seconds})";

                if (seconds <= 0)
                {
                    btnEmailResend.Text = "Resend";
                    btnEmailResend.IsEnabled = true;
                    return false;
                }

                return true;
            });
        }
        void StartPhoneResendTimer()
        {
            seconds = 30;
            btnPhoneResend.IsEnabled = false;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                seconds--;

                btnPhoneResend.Text = $"Resend ({seconds})";

                if (seconds <= 0)
                {
                    btnPhoneResend.Text = "Resend";
                    btnPhoneResend.IsEnabled = true;
                    return false;
                }

                return true;
            });
        }
        private void btnResend_onClicked(object sender, EventArgs e)
        {
            try
            {
                if (bindActivationCode())
                {
                    getActivationCode();
                }
                else
                {
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MandatoryField);
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void btnPhoneResend_onClicked(object sender, EventArgs e)
        {
            try
            {
                if (bindActivationCode())
                {
                    getActivationCode();
                }
                else
                {
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MandatoryField);
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void btnEmailVerify_onClicked(object sender, EventArgs e)
        {
            try
            {
                if (bindActivationCode())
                {
                    if (Common.mCommon.UserSetting.VerifiedByPhoneNo == "1")
                    {
                        showVerifyPhone();
                    }
                    else
                    {
                        activateRegistration();
                    }
                }
                else
                {
                    WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("MsgBlanked"));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void btnPhoneVerify_onClicked(object sender, EventArgs e)
        {
            try
            {
                if (bindPhoneActivationCode())
                {
                    activateRegistration();
                }
                else
                {
                    WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("MsgBlanked"));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void btnUnSubscribeService_onClicked(object sender, EventArgs e)
        {
            try
            {
                Common.mCommon.signIn(Common.mCommon.REQ_AUTHORIZATION);
                //if (Common.bindMenu("signin"))
                //{
                //    Common.routeMenu("signin", "Sign in");
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
        private void btnSubscribeService_onClicked(object sender, EventArgs e)
        {
            try
            {
                if (bindActivationCode())
                {
                    loadSubscriber();
                }
                else
                {
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "(*) Fields can't be blaked");
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void btnCancelSubscribeType_onClicked(object sender, EventArgs e)
        {
            try
            {
                if (!Common.bindMenu("signin"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "Sign In", MenuUrl = "signin", logoImg = "" };
                    WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("MsgAccess"));
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void btnNextSubscribeType_onClicked(object sender, EventArgs e)
        {
            try
            {
                var l_RES_SUB_TYPE = (RES_SUB_TYPE)lstSubscriberType.SelectedItem;
                if (l_RES_SUB_TYPE != null)
                {
                    showSubscriberPlan(l_RES_SUB_TYPE);
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void lstSubscriberType_onItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var l_RES_SUB_TYPE = (RES_SUB_TYPE)lstSubscriberType.SelectedItem;
                if (l_RES_SUB_TYPE != null)
                {
                    showSubscriberPlan(l_RES_SUB_TYPE);
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void TgrSubscriptionType_Tapped(object sender, EventArgs e)
        {
            try
            {
                var l_RES_SUB_TYPE = (RES_SUB_TYPE)lstSubscriberType.SelectedItem;
                if (l_RES_SUB_TYPE != null)
                {
                    showSubscriberPlan(l_RES_SUB_TYPE);
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void btnBackSubscriptionPlan_onClicked(object sender, EventArgs e)
        {
            try
            {
                showSubscriberType();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void btnNextSubscriptionPlan_onClicked(object sender, EventArgs e)
        {
            try
            {
                if (bindSubscriber())
                {
                    saveSubscriber();
                }
                else
                {
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "(*) Fields can't be blaked");
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void lstSubscriptionPlan_onItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                //if (bindSubscriber())
                //{
                //    saveSubscriber();
                //}
                //else
                //{
                //    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "(*) Fields can't be blaked");
                //}
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void Input_Focused(object sender, FocusEventArgs e)
        {
            try
            {
                //stlSignUp.Margin = new Thickness(0, 0, 0, 250);
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
                //stlSignUp.Margin = new Thickness(0, 0, 0, 0);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion
    }
}