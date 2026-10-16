using System;
using AiUnity.Common.Log;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Targets;

namespace AiUnity.NLog.Core.Config
{
	// Token: 0x02000299 RID: 665
	public static class SimpleConfigurator
	{
		// Token: 0x06001797 RID: 6039 RVA: 0x0007B681 File Offset: 0x00079881
		public static void ConfigureForTargetLogging(Target target)
		{
			SimpleConfigurator.ConfigureForTargetLogging(target, LogLevels.Info);
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x0007B68C File Offset: 0x0007988C
		public static void ConfigureForTargetLogging(Target target, LogLevels minLevel)
		{
			LoggingConfiguration loggingConfiguration = new LoggingConfiguration();
			LoggingRule loggingRule = new LoggingRule("*", "*", minLevel, target);
			loggingConfiguration.LoggingRules.Add(loggingRule);
			Singleton<NLogManager>.Instance.Configuration = loggingConfiguration;
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x0007B6C8 File Offset: 0x000798C8
		public static void ConfigureForFileLogging(string fileName)
		{
			SimpleConfigurator.ConfigureForFileLogging(fileName, LogLevels.Info);
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x0007B6D2 File Offset: 0x000798D2
		public static void ConfigureForFileLogging(string fileName, LogLevels minLevel)
		{
			SimpleConfigurator.ConfigureForTargetLogging(new FileTarget
			{
				FileName = fileName
			}, minLevel);
		}
	}
}
