using System;
using UnityEngine;

namespace PlayFab.Internal
{
	// Token: 0x020001A3 RID: 419
	[Obsolete("This logging utility has been deprecated. Use UnityEngine.Debug.Log")]
	public static class Log
	{
		// Token: 0x06000503 RID: 1283 RVA: 0x00013A9C File Offset: 0x00011C9C
		[Obsolete("Debug is deprecated.")]
		public static void Debug(string text, params object[] args)
		{
			if ((PlayFabSettings.LogLevel & PlayFabLogLevel.Debug) != PlayFabLogLevel.None)
			{
				global::UnityEngine.Debug.Log(PlayFabUtil.timeStamp + " DEBUG: " + PlayFabUtil.Format(text, args));
			}
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00013AC2 File Offset: 0x00011CC2
		[Obsolete("Info is deprecated.")]
		public static void Info(string text, params object[] args)
		{
			if ((PlayFabSettings.LogLevel & PlayFabLogLevel.Info) != PlayFabLogLevel.None)
			{
				global::UnityEngine.Debug.Log(PlayFabUtil.timeStamp + " INFO: " + PlayFabUtil.Format(text, args));
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00013AE8 File Offset: 0x00011CE8
		[Obsolete("Warning is deprecated.")]
		public static void Warning(string text, params object[] args)
		{
			if ((PlayFabSettings.LogLevel & PlayFabLogLevel.Warning) != PlayFabLogLevel.None)
			{
				global::UnityEngine.Debug.LogWarning(PlayFabUtil.timeStamp + " WARNING: " + PlayFabUtil.Format(text, args));
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00013B0E File Offset: 0x00011D0E
		[Obsolete("Error is deprecated.")]
		public static void Error(string text, params object[] args)
		{
			if ((PlayFabSettings.LogLevel & PlayFabLogLevel.Error) != PlayFabLogLevel.None)
			{
				global::UnityEngine.Debug.LogError(PlayFabUtil.timeStamp + " ERROR: " + PlayFabUtil.Format(text, args));
			}
		}
	}
}
