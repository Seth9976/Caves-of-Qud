using System;
using System.Collections.Generic;
using System.Text;
using PlayFab.Internal;
using UnityEngine;

namespace PlayFab
{
	// Token: 0x02000028 RID: 40
	public static class PlayFabSettings
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000FFAA File Offset: 0x0000E1AA
		private static PlayFabSharedSettings PlayFabSharedPrivate
		{
			get
			{
				if (PlayFabSettings._playFabShared == null)
				{
					PlayFabSettings._playFabShared = PlayFabSettings.GetSharedSettingsObjectPrivate();
				}
				return PlayFabSettings._playFabShared;
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000FFC8 File Offset: 0x0000E1C8
		private static PlayFabSharedSettings GetSharedSettingsObjectPrivate()
		{
			PlayFabSharedSettings[] array = Resources.LoadAll<PlayFabSharedSettings>("PlayFabSharedSettings");
			if (array.Length != 1)
			{
				Debug.LogWarning("The number of PlayFabSharedSettings objects should be 1: " + array.Length.ToString());
				Debug.LogWarning("If you are upgrading your SDK, you can ignore this warning as PlayFabSharedSettings will be imported soon. If you are not upgrading your SDK and you see this message, you should re-download the latest PlayFab source code.");
			}
			return array[0];
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0001000D File Offset: 0x0000E20D
		public static string DeviceUniqueIdentifier
		{
			get
			{
				return SystemInfo.deviceUniqueIdentifier;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00010014 File Offset: 0x0000E214
		// (set) Token: 0x060002EE RID: 750 RVA: 0x00010020 File Offset: 0x0000E220
		public static string TitleId
		{
			get
			{
				return PlayFabSettings.staticSettings.TitleId;
			}
			set
			{
				PlayFabSettings.staticSettings.TitleId = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0001002D File Offset: 0x0000E22D
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x00010039 File Offset: 0x0000E239
		internal static string VerticalName
		{
			get
			{
				return PlayFabSettings.staticSettings.VerticalName;
			}
			set
			{
				PlayFabSettings.staticSettings.VerticalName = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00010046 File Offset: 0x0000E246
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x00010052 File Offset: 0x0000E252
		public static bool DisableAdvertising
		{
			get
			{
				return PlayFabSettings.staticSettings.DisableAdvertising;
			}
			set
			{
				PlayFabSettings.staticSettings.DisableAdvertising = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0001005F File Offset: 0x0000E25F
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x0001006B File Offset: 0x0000E26B
		public static bool DisableDeviceInfo
		{
			get
			{
				return PlayFabSettings.staticSettings.DisableDeviceInfo;
			}
			set
			{
				PlayFabSettings.staticSettings.DisableDeviceInfo = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00010078 File Offset: 0x0000E278
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x00010084 File Offset: 0x0000E284
		public static bool DisableFocusTimeCollection
		{
			get
			{
				return PlayFabSettings.staticSettings.DisableFocusTimeCollection;
			}
			set
			{
				PlayFabSettings.staticSettings.DisableFocusTimeCollection = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00010091 File Offset: 0x0000E291
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x0001009D File Offset: 0x0000E29D
		[Obsolete("LogLevel has been deprecated, please use UnityEngine.Debug.Log for your logging needs.")]
		public static PlayFabLogLevel LogLevel
		{
			get
			{
				return PlayFabSettings.PlayFabSharedPrivate.LogLevel;
			}
			set
			{
				PlayFabSettings.PlayFabSharedPrivate.LogLevel = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x000100AA File Offset: 0x0000E2AA
		// (set) Token: 0x060002FA RID: 762 RVA: 0x000100B6 File Offset: 0x0000E2B6
		public static WebRequestType RequestType
		{
			get
			{
				return PlayFabSettings.PlayFabSharedPrivate.RequestType;
			}
			set
			{
				PlayFabSettings.PlayFabSharedPrivate.RequestType = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060002FB RID: 763 RVA: 0x000100C3 File Offset: 0x0000E2C3
		// (set) Token: 0x060002FC RID: 764 RVA: 0x000100CF File Offset: 0x0000E2CF
		public static int RequestTimeout
		{
			get
			{
				return PlayFabSettings.PlayFabSharedPrivate.RequestTimeout;
			}
			set
			{
				PlayFabSettings.PlayFabSharedPrivate.RequestTimeout = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060002FD RID: 765 RVA: 0x000100DC File Offset: 0x0000E2DC
		// (set) Token: 0x060002FE RID: 766 RVA: 0x000100E8 File Offset: 0x0000E2E8
		public static bool RequestKeepAlive
		{
			get
			{
				return PlayFabSettings.PlayFabSharedPrivate.RequestKeepAlive;
			}
			set
			{
				PlayFabSettings.PlayFabSharedPrivate.RequestKeepAlive = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060002FF RID: 767 RVA: 0x000100F5 File Offset: 0x0000E2F5
		// (set) Token: 0x06000300 RID: 768 RVA: 0x00010101 File Offset: 0x0000E301
		public static bool CompressApiData
		{
			get
			{
				return PlayFabSettings.PlayFabSharedPrivate.CompressApiData;
			}
			set
			{
				PlayFabSettings.PlayFabSharedPrivate.CompressApiData = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0001010E File Offset: 0x0000E30E
		// (set) Token: 0x06000302 RID: 770 RVA: 0x0001011A File Offset: 0x0000E31A
		public static string LoggerHost
		{
			get
			{
				return PlayFabSettings.PlayFabSharedPrivate.LoggerHost;
			}
			set
			{
				PlayFabSettings.PlayFabSharedPrivate.LoggerHost = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00010127 File Offset: 0x0000E327
		// (set) Token: 0x06000304 RID: 772 RVA: 0x00010133 File Offset: 0x0000E333
		public static int LoggerPort
		{
			get
			{
				return PlayFabSettings.PlayFabSharedPrivate.LoggerPort;
			}
			set
			{
				PlayFabSettings.PlayFabSharedPrivate.LoggerPort = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00010140 File Offset: 0x0000E340
		// (set) Token: 0x06000306 RID: 774 RVA: 0x0001014C File Offset: 0x0000E34C
		public static bool EnableRealTimeLogging
		{
			get
			{
				return PlayFabSettings.PlayFabSharedPrivate.EnableRealTimeLogging;
			}
			set
			{
				PlayFabSettings.PlayFabSharedPrivate.EnableRealTimeLogging = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00010159 File Offset: 0x0000E359
		// (set) Token: 0x06000308 RID: 776 RVA: 0x00010165 File Offset: 0x0000E365
		public static int LogCapLimit
		{
			get
			{
				return PlayFabSettings.PlayFabSharedPrivate.LogCapLimit;
			}
			set
			{
				PlayFabSettings.PlayFabSharedPrivate.LogCapLimit = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00010172 File Offset: 0x0000E372
		// (set) Token: 0x0600030A RID: 778 RVA: 0x00010187 File Offset: 0x0000E387
		public static string LocalApiServer
		{
			get
			{
				return PlayFabSettings._localApiServer ?? PlayFabUtil.GetLocalSettingsFileProperty("LocalApiServer");
			}
			set
			{
				PlayFabSettings._localApiServer = value;
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00010190 File Offset: 0x0000E390
		public static string GetFullUrl(string apiCall, Dictionary<string, string> getParams, PlayFabApiSettings apiSettings = null)
		{
			StringBuilder stringBuilder = new StringBuilder(1000);
			string text = null;
			string text2 = null;
			string text3 = null;
			if (apiSettings != null)
			{
				if (!string.IsNullOrEmpty(apiSettings.ProductionEnvironmentUrl))
				{
					text = apiSettings.ProductionEnvironmentUrl;
				}
				if (!string.IsNullOrEmpty(apiSettings.VerticalName))
				{
					text2 = apiSettings.VerticalName;
				}
				if (!string.IsNullOrEmpty(apiSettings.TitleId))
				{
					text3 = apiSettings.TitleId;
				}
			}
			if (text == null)
			{
				text = ((!string.IsNullOrEmpty(PlayFabSettings.PlayFabSharedPrivate.ProductionEnvironmentUrl)) ? PlayFabSettings.PlayFabSharedPrivate.ProductionEnvironmentUrl : "playfabapi.com");
			}
			if (text2 == null && apiSettings != null && !string.IsNullOrEmpty(apiSettings.VerticalName))
			{
				text2 = apiSettings.VerticalName;
			}
			if (text3 == null)
			{
				text3 = PlayFabSettings.PlayFabSharedPrivate.TitleId;
			}
			string text4 = text;
			if (!text4.StartsWith("http"))
			{
				stringBuilder.Append("https://");
				if (!string.IsNullOrEmpty(text3))
				{
					stringBuilder.Append(text3).Append(".");
				}
				if (!string.IsNullOrEmpty(text2))
				{
					stringBuilder.Append(text2).Append(".");
				}
			}
			stringBuilder.Append(text4).Append(apiCall);
			if (getParams != null)
			{
				bool flag = true;
				foreach (KeyValuePair<string, string> keyValuePair in getParams)
				{
					if (flag)
					{
						stringBuilder.Append("?");
						flag = false;
					}
					else
					{
						stringBuilder.Append("&");
					}
					stringBuilder.Append(keyValuePair.Key).Append("=").Append(keyValuePair.Value);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040002BA RID: 698
		private static PlayFabSharedSettings _playFabShared = null;

		// Token: 0x040002BB RID: 699
		public static readonly PlayFabApiSettings staticSettings = new PlayFabSettingsRedirect(() => PlayFabSettings.PlayFabSharedPrivate);

		// Token: 0x040002BC RID: 700
		public static readonly PlayFabAuthenticationContext staticPlayer = new PlayFabAuthenticationContext();

		// Token: 0x040002BD RID: 701
		public const string SdkVersion = "2.90.200713";

		// Token: 0x040002BE RID: 702
		public const string BuildIdentifier = "jbuild_unitysdk__sdk-unity-3-slave_0";

		// Token: 0x040002BF RID: 703
		public const string VersionString = "UnitySDK-2.90.200713";

		// Token: 0x040002C0 RID: 704
		public const string AD_TYPE_IDFA = "Idfa";

		// Token: 0x040002C1 RID: 705
		public const string AD_TYPE_ANDROID_ID = "Adid";

		// Token: 0x040002C2 RID: 706
		public const string DefaultPlayFabApiUrl = "playfabapi.com";

		// Token: 0x040002C3 RID: 707
		private static string _localApiServer;
	}
}
