

# Initialize Display Language
```csharp
this.UserSetting = this.JSN_RES_MOBILE_LOGIN.DAT_USER_SETTING[0];
string l_userPreferredLanguage = this.UserSetting.LanguageCode_0_50;
Common.mCommon.UpdateLanguage(l_userPreferredLanguage);
Common.mCommon.UpdateMessageManager(l_userPreferredLanguage);
```


# Declare LanguageExtension class variable
```xaml
<ContentView
    xmlns:ext="clr-namespace:CS.ERP_MOB.Extensions">
```

# Bind LanguageExtension Key,Value
```xaml
<controls:TabItem
    Caption="{Binding Source={ext:LanguageExtension Key=SYS.Common.loading}, Path=Value}"
```

# Display dynamic message base on language setting
```csharp
WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("DAT.MsgSaveSuccess"));
```


# Declare Common class variable
```xaml
<ContentPage 
    xmlns:general="clr-namespace:CS.ERP_MOB.General">
```

# Bind dynamic color
```xaml
<StackLayout
    BackgroundColor="{Binding UserSetting.ActionBarBgColor, Source={x:Static general:Common.mCommon}}">
```
