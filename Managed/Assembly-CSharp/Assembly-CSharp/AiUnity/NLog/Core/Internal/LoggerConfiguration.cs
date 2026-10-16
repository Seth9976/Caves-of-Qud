using System;
using System.Collections.Generic;
using AiUnity.Common.Log;

namespace AiUnity.NLog.Core.Internal
{
	// Token: 0x02000268 RID: 616
	internal class LoggerConfiguration
	{
		// Token: 0x06001640 RID: 5696 RVA: 0x00078084 File Offset: 0x00076284
		public LoggerConfiguration(Dictionary<LogLevels, TargetWithFilterChain> targetsByLevel)
		{
			this.targetsByLevel = targetsByLevel;
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x00078094 File Offset: 0x00076294
		public TargetWithFilterChain GetTargetsForLevel(LogLevels level)
		{
			TargetWithFilterChain targetWithFilterChain;
			this.targetsByLevel.TryGetValue(level, out targetWithFilterChain);
			return targetWithFilterChain;
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x000780B1 File Offset: 0x000762B1
		public bool IsEnabled(LogLevels level)
		{
			return this.targetsByLevel.ContainsKey(level);
		}

		// Token: 0x04000DD5 RID: 3541
		private readonly Dictionary<LogLevels, TargetWithFilterChain> targetsByLevel;
	}
}
