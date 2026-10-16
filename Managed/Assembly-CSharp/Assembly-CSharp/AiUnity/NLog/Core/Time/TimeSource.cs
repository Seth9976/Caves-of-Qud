using System;
using AiUnity.NLog.Core.Config;

namespace AiUnity.NLog.Core.Time
{
	// Token: 0x02000216 RID: 534
	[NLogConfigurationItem]
	public abstract class TimeSource
	{
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600137B RID: 4987
		public abstract DateTime Time { get; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x0600137C RID: 4988 RVA: 0x000706A9 File Offset: 0x0006E8A9
		// (set) Token: 0x0600137D RID: 4989 RVA: 0x000706B0 File Offset: 0x0006E8B0
		public static TimeSource Current
		{
			get
			{
				return TimeSource.currentSource;
			}
			set
			{
				TimeSource.currentSource = value;
			}
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x000706B8 File Offset: 0x0006E8B8
		public override string ToString()
		{
			TimeSourceAttribute timeSourceAttribute = (TimeSourceAttribute)Attribute.GetCustomAttribute(base.GetType(), typeof(TimeSourceAttribute));
			if (timeSourceAttribute != null)
			{
				return timeSourceAttribute.DisplayName + " (time source)";
			}
			return base.GetType().Name;
		}

		// Token: 0x04000CC1 RID: 3265
		private static TimeSource currentSource = new FastLocalTimeSource();
	}
}
