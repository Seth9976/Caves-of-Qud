using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using AiUnity.Common.Extensions;
using AiUnity.Common.Log;
using AiUnity.Common.Types;
using AiUnity.NLog.Core.Filters;
using AiUnity.NLog.Core.Targets;
using UnityEngine;

namespace AiUnity.NLog.Core.Config
{
	// Token: 0x02000293 RID: 659
	[NLogConfigurationItem]
	public class LoggingRule
	{
		// Token: 0x06001761 RID: 5985 RVA: 0x0007ADA4 File Offset: 0x00078FA4
		public LoggingRule()
		{
			this.Filters = new List<Filter>();
			this.ChildRules = new List<LoggingRule>();
			this.Targets = new List<Target>();
			this.namePatternMatch = new PatternMatch("*");
			this.namespacePatternMatch = new PatternMatch("*");
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x0007ADF8 File Offset: 0x00078FF8
		public LoggingRule(string loggerNamePattern, string loggerNamespacePattern, LogLevels minLevel, Target target)
		{
			this.Filters = new List<Filter>();
			this.ChildRules = new List<LoggingRule>();
			this.Targets = new List<Target>();
			this.namePatternMatch = new PatternMatch(loggerNamePattern);
			this.namespacePatternMatch = new PatternMatch(loggerNamespacePattern);
			this.Targets.Add(target);
			this.logLevels = LogLevels.Everything & (minLevel - 1);
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x0007AE5C File Offset: 0x0007905C
		public LoggingRule(string loggerNamePattern, string loggerNamespacePattern, Target target)
		{
			this.Filters = new List<Filter>();
			this.ChildRules = new List<LoggingRule>();
			this.Targets = new List<Target>();
			this.namePatternMatch = new PatternMatch(loggerNamePattern);
			this.namespacePatternMatch = new PatternMatch(loggerNamespacePattern);
			this.Targets.Add(target);
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06001764 RID: 5988 RVA: 0x0007AEB4 File Offset: 0x000790B4
		// (set) Token: 0x06001765 RID: 5989 RVA: 0x0007AEBC File Offset: 0x000790BC
		public IList<Target> Targets { get; private set; }

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06001766 RID: 5990 RVA: 0x0007AEC5 File Offset: 0x000790C5
		// (set) Token: 0x06001767 RID: 5991 RVA: 0x0007AECD File Offset: 0x000790CD
		public IList<LoggingRule> ChildRules { get; private set; }

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06001768 RID: 5992 RVA: 0x0007AED6 File Offset: 0x000790D6
		// (set) Token: 0x06001769 RID: 5993 RVA: 0x0007AEDE File Offset: 0x000790DE
		public IList<Filter> Filters { get; private set; }

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x0600176A RID: 5994 RVA: 0x0007AEE7 File Offset: 0x000790E7
		// (set) Token: 0x0600176B RID: 5995 RVA: 0x0007AEEF File Offset: 0x000790EF
		public bool Final { get; set; }

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x0600176C RID: 5996 RVA: 0x0007AEF8 File Offset: 0x000790F8
		// (set) Token: 0x0600176D RID: 5997 RVA: 0x0007AF00 File Offset: 0x00079100
		public PlatformEnumFlagWrapper<RuntimePlatform> TargetPlatforms { get; set; }

		// Token: 0x0600176E RID: 5998 RVA: 0x0007AF09 File Offset: 0x00079109
		public void EnableLoggingForLevel(LogLevels level)
		{
			this.logLevels |= level;
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x0007AF19 File Offset: 0x00079119
		public void DisableLoggingForLevel(LogLevels level)
		{
			this.logLevels &= ~level;
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x0007AF2C File Offset: 0x0007912C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, this.namePatternMatch.ToString(), Array.Empty<object>());
			stringBuilder.Append(" levels: [ ");
			stringBuilder.Append(this.logLevels);
			stringBuilder.Append("] appendTo: [ ");
			foreach (Target target in this.Targets)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0} ", target.Name);
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x0007AFE8 File Offset: 0x000791E8
		public bool IsLoggingEnabledForLevel(LogLevels level)
		{
			return this.logLevels.Has(level);
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x0007AFFC File Offset: 0x000791FC
		public bool NameMatches(string loggerFullName)
		{
			int num = loggerFullName.LastIndexOf('.');
			string text = loggerFullName.Substring(num + 1);
			string text2 = loggerFullName.Substring(0, Math.Max(0, num));
			return this.namePatternMatch.NameMatches(text) && this.namespacePatternMatch.NameMatches(text2);
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x0007B047 File Offset: 0x00079247
		internal bool isPlatformMatch()
		{
			return this.TargetPlatforms.Has(Application.platform);
		}

		// Token: 0x04000E1E RID: 3614
		public LogLevels logLevels;

		// Token: 0x04000E1F RID: 3615
		public PatternMatch namePatternMatch;

		// Token: 0x04000E20 RID: 3616
		public PatternMatch namespacePatternMatch;
	}
}
