using System;

namespace System.Diagnostics
{
	// Token: 0x02000287 RID: 647
	internal class TraceImplSettings
	{
		// Token: 0x0600146E RID: 5230 RVA: 0x00053255 File Offset: 0x00051455
		public TraceImplSettings()
		{
			this.Listeners.Add(new DefaultTraceListener
			{
				IndentSize = this.IndentSize
			});
		}

		// Token: 0x04000BA9 RID: 2985
		public const string Key = ".__TraceInfoSettingsKey__.";

		// Token: 0x04000BAA RID: 2986
		public bool AutoFlush;

		// Token: 0x04000BAB RID: 2987
		public int IndentSize = 4;

		// Token: 0x04000BAC RID: 2988
		public TraceListenerCollection Listeners = new TraceListenerCollection();
	}
}
