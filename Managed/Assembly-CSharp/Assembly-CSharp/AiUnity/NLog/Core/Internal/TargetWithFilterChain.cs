using System;
using System.Collections.Generic;
using AiUnity.NLog.Core.Config;
using AiUnity.NLog.Core.Filters;
using AiUnity.NLog.Core.Targets;

namespace AiUnity.NLog.Core.Internal
{
	// Token: 0x02000270 RID: 624
	[NLogConfigurationItem]
	internal class TargetWithFilterChain
	{
		// Token: 0x0600166F RID: 5743 RVA: 0x00078C28 File Offset: 0x00076E28
		public TargetWithFilterChain(Target target, IList<Filter> filterChain)
		{
			this.Target = target;
			this.FilterChain = filterChain;
			this.stackTraceUsage = StackTraceUsage.None;
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001670 RID: 5744 RVA: 0x00078C45 File Offset: 0x00076E45
		// (set) Token: 0x06001671 RID: 5745 RVA: 0x00078C4D File Offset: 0x00076E4D
		public Target Target { get; private set; }

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06001672 RID: 5746 RVA: 0x00078C56 File Offset: 0x00076E56
		// (set) Token: 0x06001673 RID: 5747 RVA: 0x00078C5E File Offset: 0x00076E5E
		public IList<Filter> FilterChain { get; private set; }

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06001674 RID: 5748 RVA: 0x00078C67 File Offset: 0x00076E67
		// (set) Token: 0x06001675 RID: 5749 RVA: 0x00078C6F File Offset: 0x00076E6F
		public TargetWithFilterChain NextInChain { get; set; }

		// Token: 0x06001676 RID: 5750 RVA: 0x00078C78 File Offset: 0x00076E78
		public StackTraceUsage GetStackTraceUsage()
		{
			return this.stackTraceUsage;
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x00078C80 File Offset: 0x00076E80
		internal void PrecalculateStackTraceUsage()
		{
			this.stackTraceUsage = StackTraceUsage.None;
			IUsesStackTrace[] array = ObjectGraphScanner.FindReachableObjects<IUsesStackTrace>(new object[] { this });
			for (int i = 0; i < array.Length; i++)
			{
				StackTraceUsage stackTraceUsage = array[i].StackTraceUsage;
				if (stackTraceUsage > this.stackTraceUsage)
				{
					this.stackTraceUsage = stackTraceUsage;
					if (this.stackTraceUsage >= StackTraceUsage.WithSource)
					{
						break;
					}
				}
			}
		}

		// Token: 0x04000DDA RID: 3546
		private StackTraceUsage stackTraceUsage;
	}
}
