using System;
using System.Collections.Concurrent;

namespace UnityEngine.Purchasing.Telemetry
{
	// Token: 0x02000043 RID: 67
	internal class TelemetryQueue<TTelemetryEventParams>
	{
		// Token: 0x0600012C RID: 300 RVA: 0x00004080 File Offset: 0x00002280
		public TelemetryQueue(Action<TTelemetryEventParams> sendTelemetryEvent)
		{
			this.m_SendTelemetryEvent = sendTelemetryEvent;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004090 File Offset: 0x00002290
		internal void QueueEvent(TTelemetryEventParams telemetryEvent)
		{
			if (this.m_Queue == null)
			{
				this.m_Queue = new ConcurrentQueue<TTelemetryEventParams>();
			}
			this.m_Queue.Enqueue(telemetryEvent);
			if (this.m_Queue.Count > 10)
			{
				TTelemetryEventParams ttelemetryEventParams;
				this.m_Queue.TryDequeue(out ttelemetryEventParams);
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000040DC File Offset: 0x000022DC
		internal void SendQueuedEvents()
		{
			if (this.m_SendTelemetryEvent == null || this.m_Queue == null)
			{
				return;
			}
			TTelemetryEventParams ttelemetryEventParams;
			while (this.m_Queue.TryDequeue(out ttelemetryEventParams))
			{
				this.m_SendTelemetryEvent(ttelemetryEventParams);
			}
		}

		// Token: 0x0400009A RID: 154
		private readonly Action<TTelemetryEventParams> m_SendTelemetryEvent;

		// Token: 0x0400009B RID: 155
		private ConcurrentQueue<TTelemetryEventParams> m_Queue;

		// Token: 0x0400009C RID: 156
		internal const int k_maxQueueSize = 10;
	}
}
