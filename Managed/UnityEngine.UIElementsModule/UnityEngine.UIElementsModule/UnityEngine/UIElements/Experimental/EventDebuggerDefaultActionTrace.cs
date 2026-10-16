using System;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x0200070E RID: 1806
	internal class EventDebuggerDefaultActionTrace : EventDebuggerTrace
	{
		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06003438 RID: 13368 RVA: 0x000CC186 File Offset: 0x000CA386
		public PropagationPhase phase { get; }

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06003439 RID: 13369 RVA: 0x000CC190 File Offset: 0x000CA390
		public string targetName
		{
			get
			{
				return base.eventBase.target.GetType().FullName;
			}
		}

		// Token: 0x0600343A RID: 13370 RVA: 0x000CC1B7 File Offset: 0x000CA3B7
		public EventDebuggerDefaultActionTrace(IPanel panel, EventBase evt, PropagationPhase phase, long duration, IEventHandler mouseCapture)
			: base(panel, evt, duration, mouseCapture)
		{
			this.phase = phase;
		}
	}
}
