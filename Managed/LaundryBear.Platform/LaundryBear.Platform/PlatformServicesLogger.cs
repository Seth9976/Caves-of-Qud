using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class PlatformServicesLogger
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	[Conditional("PLATFORM_LOGGING")]
	public static void Log(object message)
	{
		global::UnityEngine.Debug.Log(message);
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
	[Conditional("PLATFORM_LOGGING")]
	public static void Log(object message, global::UnityEngine.Object context)
	{
		global::UnityEngine.Debug.Log(message, context);
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
	[Conditional("PLATFORM_LOGGING")]
	public static void LogWarning(object message)
	{
		global::UnityEngine.Debug.LogWarning(message);
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00002069 File Offset: 0x00000269
	[Conditional("PLATFORM_LOGGING")]
	public static void LogWarning(object message, global::UnityEngine.Object context)
	{
		global::UnityEngine.Debug.LogWarning(message, context);
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002072 File Offset: 0x00000272
	[Conditional("PLATFORM_LOGGING")]
	public static void LogError(object message)
	{
		global::UnityEngine.Debug.LogError(message);
	}

	// Token: 0x06000006 RID: 6 RVA: 0x0000207A File Offset: 0x0000027A
	[Conditional("PLATFORM_LOGGING")]
	public static void LogError(object message, global::UnityEngine.Object context)
	{
		global::UnityEngine.Debug.Log(message, context);
	}

	// Token: 0x04000001 RID: 1
	public const string LOGGING_DEFINE = "PLATFORM_LOGGING";
}
