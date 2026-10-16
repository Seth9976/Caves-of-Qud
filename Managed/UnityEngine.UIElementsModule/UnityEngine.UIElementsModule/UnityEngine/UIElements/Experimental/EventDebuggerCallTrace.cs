using System;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x0200070D RID: 1805
	internal class EventDebuggerCallTrace : EventDebuggerTrace
	{
		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x06003433 RID: 13363 RVA: 0x000CC137 File Offset: 0x000CA337
		public int callbackHashCode { get; }

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x06003434 RID: 13364 RVA: 0x000CC13F File Offset: 0x000CA33F
		public string callbackName { get; }

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06003435 RID: 13365 RVA: 0x000CC147 File Offset: 0x000CA347
		public bool propagationHasStopped { get; }

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06003436 RID: 13366 RVA: 0x000CC14F File Offset: 0x000CA34F
		public bool immediatePropagationHasStopped { get; }

		// Token: 0x06003437 RID: 13367 RVA: 0x000CC157 File Offset: 0x000CA357
		public EventDebuggerCallTrace(IPanel panel, EventBase evt, int cbHashCode, string cbName, bool propagationHasStopped, bool immediatePropagationHasStopped, long duration, IEventHandler mouseCapture)
			: base(panel, evt, duration, mouseCapture)
		{
			this.callbackHashCode = cbHashCode;
			this.callbackName = cbName;
			this.propagationHasStopped = propagationHasStopped;
			this.immediatePropagationHasStopped = immediatePropagationHasStopped;
		}
	}
}
