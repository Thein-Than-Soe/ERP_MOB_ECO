using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.ERP_MOB.Services.NTF
{
    internal class Ntf_Name
    {
        public Ntf_Name()
        {
        }
        #region "Frame"
        public static string wssaveRegistration = "saveRegistration";

        #endregion

        #region "Socket"
        public static string wsStartSocketServer = "StartSocketServer";
        #endregion
        #region "Get"
        public static string wsGetChat = "getChat";
        public static string wsGetChatData = "getChatData";
        #endregion
        #region "Save"
        public static string wsSaveCity = "saveCity";
        public static string wsSaveChat = "saveChat";
        public static string wsSaveChatDataBookmark = "saveChatDataBookmark";
        public static string wsEndDiscussion = "endDiscussion";
        #endregion
        #region "Load"
        public static string wsLoadCity = "loadCity";
        public static string wsLoadChat = "loadChat";
        #endregion
    }
}
