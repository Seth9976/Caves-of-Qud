using System;
using System.Diagnostics;
using UnityEngine;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000037 RID: 55
	internal static class CoreLogger
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00002AE4 File Offset: 0x00000CE4
		public static void Log(object message)
		{
			global::UnityEngine.Debug.unityLogger.Log("[ServicesCore]", message);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00002AF6 File Offset: 0x00000CF6
		public static void LogWarning(object message)
		{
			global::UnityEngine.Debug.unityLogger.LogWarning("[ServicesCore]", message);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00002B08 File Offset: 0x00000D08
		public static void LogError(object message)
		{
			global::UnityEngine.Debug.unityLogger.LogError("[ServicesCore]", message);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00002B1A File Offset: 0x00000D1A
		public static void LogException(Exception exception)
		{
			global::UnityEngine.Debug.unityLogger.Log(LogType.Exception, "[ServicesCore]", exception);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00002B2D File Offset: 0x00000D2D
		[Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertion(object message)
		{
			global::UnityEngine.Debug.unityLogger.Log(LogType.Assert, "[ServicesCore]", message);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00002B40 File Offset: 0x00000D40
		[Conditional("ENABLE_UNITY_SERVICES_CORE_VERBOSE_LOGGING")]
		public static void LogVerbose(object message)
		{
			global::UnityEngine.Debug.unityLogger.Log("[ServicesCore]", message);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00002B52 File Offset: 0x00000D52
		[Conditional("ENABLE_UNITY_SERVICES_CORE_TELEMETRY_LOGGING")]
		public static void LogTelemetry(object message)
		{
			global::UnityEngine.Debug.unityLogger.Log("[ServicesCore]", message);
		}

		// Token: 0x04000034 RID: 52
		internal const string Tag = "[ServicesCore]";

		// Token: 0x04000035 RID: 53
		internal const string VerboseLoggingDefine = "ENABLE_UNITY_SERVICES_CORE_VERBOSE_LOGGING";

		// Token: 0x04000036 RID: 54
		private const string k_TelemetryLoggingDefine = "ENABLE_UNITY_SERVICES_CORE_TELEMETRY_LOGGING";
	}
}
