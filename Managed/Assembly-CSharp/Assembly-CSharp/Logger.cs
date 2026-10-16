using System;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core;
using AiUnity.NLog.Core.Config;
using Platform.IO;
using UnityEngine;

// Token: 0x02000066 RID: 102
public static class Logger
{
	// Token: 0x0600037F RID: 895 RVA: 0x000176B4 File Offset: 0x000158B4
	public static void Exception(string context, Exception ex)
	{
		global::Logger.gameLog.Error(context + " - " + ex.ToString(), Array.Empty<object>());
	}

	// Token: 0x06000380 RID: 896 RVA: 0x000176D6 File Offset: 0x000158D6
	public static void Exception(Exception ex)
	{
		global::Logger.gameLog.Error(ex.ToString(), Array.Empty<object>());
	}

	// Token: 0x06000381 RID: 897 RVA: 0x000176F0 File Offset: 0x000158F0
	static Logger()
	{
		new LoggingConfiguration();
		global::Logger.gameLog = Singleton<NLogManager>.Instance.GetLogger("GameLog", new global::UnityEngine.Object(), null);
		global::Logger.buildLog = SimpleFileLogger.Create("build_log.txt").AwaitResult<SimpleFileLogger>();
		global::Logger.traceLog = Singleton<NLogManager>.Instance.GetLogger("TraceLog", new global::UnityEngine.Object(), null);
		global::Logger.log = global::Logger.gameLog;
		global::Logger.gameLog.Info("GameLogger initialized...", Array.Empty<object>());
		global::Logger.buildLog.Info("BuildLogger initialized...");
		global::Logger.traceLog.Info("TraceLogger initialized...", Array.Empty<object>());
	}

	// Token: 0x040002BC RID: 700
	public static NLogger log;

	// Token: 0x040002BD RID: 701
	public static NLogger traceLog;

	// Token: 0x040002BE RID: 702
	public static NLogger gameLog;

	// Token: 0x040002BF RID: 703
	public static SimpleFileLogger buildLog;
}
