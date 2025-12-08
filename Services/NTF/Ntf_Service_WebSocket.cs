using System.Net.WebSockets;
using System.Reactive.Subjects;
using System.Text;
using System.Text.Json;
using CommunityToolkit.Mvvm.Messaging;
using CS.ERP.PL.SYS.REQ;
using CS.ERP.PL.SYS.RES;
using CS.ERP_MOB.Services.CHT;
using CS.ERP_MOB.General;
using Newtonsoft.Json;
using CS.ERP.PL.WSS.DAT;
using CS.ERP_MOB.DB;
using Stripe;
using CS.ERP.PL.NTF.DAT;
using CS.ERP.PL.SYS.DAT;

namespace CS.ERP_MOB.Services.NTF
{
    public class Ntf_Service_WebSocket
    {
        #region "Declaring"

        DAT_REQ_REGISTER mDAT_REQ_REGISTER = new DAT_REQ_REGISTER();
        DAT_REQ_NOTI mDAT_REQ_NOTI = new DAT_REQ_NOTI();
        private readonly int _reconnectInterval = 5000; // 5 seconds
        private int _reconnectAttempts = 0;
        private string _webSocketUrl = "";
        private bool _isReconnect = false;
        private bool _isFirstRegister = true;
        private ClientWebSocket _socket = new ClientWebSocket();
        private readonly Subject<object> _messageSubject = new();
        private bool _isScreenSharing = false;
        string mRequest = "";
        string mResponse = "";
        public Action<string>? OnMessageReceived; // <- Set from ViewModel
        #endregion

        //private readonly IntercomService _ics;
        //private readonly ChtConfigService _chtCon;

        public Ntf_Service_WebSocket()
        {

        }
        public static ApiConfig mApiConfig = new ApiConfig
        {
            Ask = 1,
            ProductCode = "NTF",
            UploadURL = "https://updqasrv.kumudr.com",
            APIURL = "https://ntfqaapi.kumudr.com/Service.svc",
            APIProtocol = "wss://",
            APIServer = "ntfqasrv.kumudr.com",
            APIPort = "",
            APIServiceName = "/Ntf",
            ApiContentType = "application/json",
            ApiAcceptType = "application/json",
            ApiKey = "",
            PublicKey = "",
            SecreteKey = "",
            User = "",
            Password = "",
            Sequence = 1
        };


        public async Task connectSocket()
        {
            string url = mApiConfig.APIProtocol + mApiConfig.APIServer + mApiConfig.APIPort + mApiConfig.APIServiceName;
            _webSocketUrl = url;
            _socket = new ClientWebSocket();

            try
            {
                await _socket.ConnectAsync(new Uri(url), CancellationToken.None);


                if (_isReconnect)
                {
                    await RegisterUser();
                    _isReconnect = false;
                }
                else if (_isFirstRegister)
                {
                    await RegisterUser();
                    _isFirstRegister = false;
                }

                _ = ReceiveMessages();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WebSocket connection error: {ex.Message}");
                //await Reconnect();
            }
        }

        public async Task ReceiveMessages()
        {
            var buffer = new byte[64 * 1024];
            try
            {
                while (_socket.State == WebSocketState.Open)
                {
                    using var ms = new MemoryStream();
                    WebSocketReceiveResult result;
                    do
                    {
                        result = await _socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                        ms.Write(buffer, 0, result.Count);
                    }
                    while (!result.EndOfMessage);


                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                        break;
                    }

                    var messageString = Encoding.UTF8.GetString(ms.ToArray());

                    // Call the handler (e.g. ViewModel)
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        OnMessageReceived?.Invoke(messageString);
                    });

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WebSocket receive error: {ex.Message}");
            }

        }

        private async Task handleSaveChatData(string message)
        {
            throw new NotImplementedException();
        }

        public async void StartSocket()
        {
            try
            {
                Utility.openLoader();
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Cht_Service.ApiCall(mRequest, Ntf_Name.wsStartSocketServer);
                if (mResponse != null && mResponse != "")
                {


                    Utility.closeLoader();
                }
                else
                {
                    Utility.closeLoader();
                    WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("ErrWebService"));
                }
            }
            catch (Exception ex)
            {
                Utility.closeLoader();
                throw ex.InnerException;
            }
        }
        public async Task saveNoti(RES_NOTI_LST argRES_NOTI_LST)
        {
            mDAT_REQ_NOTI.Req = "SaveNoti";
            mDAT_REQ_NOTI.UserAsk = Common.mCommon.User.UserAsk;
            Common.mCommon.REQ_AUTHORIZATION.ProductAsk = "10";
            mDAT_REQ_NOTI.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
            mDAT_REQ_NOTI.RES_NOTI_LST = argRES_NOTI_LST;
            mDAT_REQ_NOTI.RES_NOTI_LST.DeviceTypeAsk = "3";//3 for mobile
            mDAT_REQ_NOTI.RES_NOTI_LST.NotiTypeAsk = "1";//1 for general
            mDAT_REQ_NOTI.RES_NOTI_LST.UserAsk = Common.mCommon.User.UserAsk;
            await SendMessage(mDAT_REQ_NOTI);
        }
        public async Task RegisterUser()
        {
            mDAT_REQ_REGISTER.Req = "Register";
            mDAT_REQ_REGISTER.UserAsk = Common.mCommon.User.UserAsk;
            Common.mCommon.REQ_AUTHORIZATION.ProductAsk = "10";
            mDAT_REQ_REGISTER.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;

            await SendMessage(mDAT_REQ_REGISTER);
        }

        public async Task SendMessage(object message)
        {
            if (_socket?.State == WebSocketState.Open)
            {
                var messageString = JsonConvert.SerializeObject(message);
                var buffer = Encoding.UTF8.GetBytes(messageString);
                await _socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                Console.WriteLine($"sendMsg: {messageString}");
            }
            else
            {
                Console.WriteLine("WebSocket not open");
            }
        }

        public async Task SendMessages(DAT_REQ_DISCUSSION_DATA message)
        {
            if (_socket?.State == WebSocketState.Open)
            {
                var messageString = JsonConvert.SerializeObject(message);
                var buffer = Encoding.UTF8.GetBytes(messageString);
                await _socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                Console.WriteLine($"sendMsg: {messageString}");
            }
            else
            {
                Console.WriteLine("WebSocket not open");
            }
        }

        public async Task SendMessages(DAT_REQ_END_DISCUSSION message)
        {
            if (_socket?.State == WebSocketState.Open)
            {
                var messageString = JsonConvert.SerializeObject(message);
                var buffer = Encoding.UTF8.GetBytes(messageString);
                await _socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                Console.WriteLine($"sendMsg: {messageString}");
            }
            else
            {
                Console.WriteLine("WebSocket not open");
            }
        }
        public IObservable<object> GetMessages()
        {
            return _messageSubject;
        }



        public async Task HandleRegister(Dictionary<string, object> message)
        {

        }

        public void Dispose()
        {
            _socket?.Dispose();
            _messageSubject.Dispose();
        }
    }
}