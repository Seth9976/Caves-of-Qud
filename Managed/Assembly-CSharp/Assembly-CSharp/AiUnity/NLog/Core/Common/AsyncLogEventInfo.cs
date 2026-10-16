using System;

namespace AiUnity.NLog.Core.Common
{
	// Token: 0x020002B4 RID: 692
	public struct AsyncLogEventInfo
	{
		// Token: 0x0600184B RID: 6219 RVA: 0x0007EB32 File Offset: 0x0007CD32
		public AsyncLogEventInfo(LogEventInfo logEvent, AsyncContinuation continuation)
		{
			this = default(AsyncLogEventInfo);
			this.LogEvent = logEvent;
			this.Continuation = continuation;
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x0600184C RID: 6220 RVA: 0x0007EB49 File Offset: 0x0007CD49
		// (set) Token: 0x0600184D RID: 6221 RVA: 0x0007EB51 File Offset: 0x0007CD51
		public LogEventInfo LogEvent { readonly get; private set; }

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x0600184E RID: 6222 RVA: 0x0007EB5A File Offset: 0x0007CD5A
		// (set) Token: 0x0600184F RID: 6223 RVA: 0x0007EB62 File Offset: 0x0007CD62
		public AsyncContinuation Continuation { readonly get; internal set; }

		// Token: 0x06001850 RID: 6224 RVA: 0x0007EB6B File Offset: 0x0007CD6B
		public static bool operator ==(AsyncLogEventInfo eventInfo1, AsyncLogEventInfo eventInfo2)
		{
			return eventInfo1.Continuation == eventInfo2.Continuation && eventInfo1.LogEvent == eventInfo2.LogEvent;
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x0007EB8F File Offset: 0x0007CD8F
		public static bool operator !=(AsyncLogEventInfo eventInfo1, AsyncLogEventInfo eventInfo2)
		{
			return eventInfo1.Continuation != eventInfo2.Continuation || eventInfo1.LogEvent != eventInfo2.LogEvent;
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x0007EBB8 File Offset: 0x0007CDB8
		public override bool Equals(object obj)
		{
			AsyncLogEventInfo asyncLogEventInfo = (AsyncLogEventInfo)obj;
			return this == asyncLogEventInfo;
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x0007EBD8 File Offset: 0x0007CDD8
		public override int GetHashCode()
		{
			return this.LogEvent.GetHashCode() ^ this.Continuation.GetHashCode();
		}
	}
}
