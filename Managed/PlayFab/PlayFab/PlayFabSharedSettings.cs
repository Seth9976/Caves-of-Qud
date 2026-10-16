using System;
using PlayFab;
using UnityEngine;

// Token: 0x02000002 RID: 2
[CreateAssetMenu(fileName = "PlayFabSharedSettings", menuName = "PlayFab/CreateSharedSettings", order = 1)]
public class PlayFabSharedSettings : ScriptableObject
{
	// Token: 0x04000001 RID: 1
	public string TitleId;

	// Token: 0x04000002 RID: 2
	internal string VerticalName;

	// Token: 0x04000003 RID: 3
	public string ProductionEnvironmentUrl = "";

	// Token: 0x04000004 RID: 4
	public WebRequestType RequestType;

	// Token: 0x04000005 RID: 5
	public string AdvertisingIdType;

	// Token: 0x04000006 RID: 6
	public string AdvertisingIdValue;

	// Token: 0x04000007 RID: 7
	public bool DisableAdvertising;

	// Token: 0x04000008 RID: 8
	public bool DisableDeviceInfo;

	// Token: 0x04000009 RID: 9
	public bool DisableFocusTimeCollection;

	// Token: 0x0400000A RID: 10
	public int RequestTimeout = 2000;

	// Token: 0x0400000B RID: 11
	public bool RequestKeepAlive = true;

	// Token: 0x0400000C RID: 12
	public bool CompressApiData = true;

	// Token: 0x0400000D RID: 13
	public PlayFabLogLevel LogLevel = PlayFabLogLevel.Warning | PlayFabLogLevel.Error;

	// Token: 0x0400000E RID: 14
	public string LoggerHost = "";

	// Token: 0x0400000F RID: 15
	public int LoggerPort;

	// Token: 0x04000010 RID: 16
	public bool EnableRealTimeLogging;

	// Token: 0x04000011 RID: 17
	public int LogCapLimit = 30;
}
