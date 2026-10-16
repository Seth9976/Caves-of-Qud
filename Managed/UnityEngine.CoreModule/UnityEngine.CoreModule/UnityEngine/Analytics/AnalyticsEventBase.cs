using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Analytics
{
	// Token: 0x02000473 RID: 1139
	[RequiredByNativeCode(GenerateProxy = true)]
	[StructLayout(LayoutKind.Sequential)]
	public class AnalyticsEventBase
	{
		// Token: 0x06002CEE RID: 11502 RVA: 0x00055074 File Offset: 0x00053274
		public string EventName()
		{
			return this.eventName;
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x0005508C File Offset: 0x0005328C
		public int EventVersion()
		{
			return this.eventVersion;
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x000550A4 File Offset: 0x000532A4
		public string EventPrefix()
		{
			return this.eventPrefix;
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x000550BC File Offset: 0x000532BC
		public AnalyticsEventBase(string eventName, int eventVersion, SendEventOptions sendEventOptions = SendEventOptions.kAppendNone, string eventPrefix = "")
		{
			this.eventName = eventName;
			this.eventVersion = eventVersion;
			this.sendEventOptions = sendEventOptions;
			this.eventPrefix = eventPrefix;
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x000550E3 File Offset: 0x000532E3
		public AnalyticsEventBase(AnalyticsEventBase e)
			: this(e.eventName, e.eventVersion, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x00030B93 File Offset: 0x0002ED93
		public AnalyticsEventBase()
		{
		}

		// Token: 0x04000C64 RID: 3172
		private string eventName;

		// Token: 0x04000C65 RID: 3173
		private int eventVersion;

		// Token: 0x04000C66 RID: 3174
		private string eventPrefix;

		// Token: 0x04000C67 RID: 3175
		private SendEventOptions sendEventOptions;
	}
}
