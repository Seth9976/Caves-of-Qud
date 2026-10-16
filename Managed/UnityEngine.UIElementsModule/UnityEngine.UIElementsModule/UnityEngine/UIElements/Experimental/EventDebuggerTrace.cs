using System;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x0200070C RID: 1804
	internal class EventDebuggerTrace
	{
		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x0600342D RID: 13357 RVA: 0x000CC0BE File Offset: 0x000CA2BE
		public EventDebuggerEventRecord eventBase { get; }

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x0600342E RID: 13358 RVA: 0x000CC0C6 File Offset: 0x000CA2C6
		public IEventHandler focusedElement { get; }

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x0600342F RID: 13359 RVA: 0x000CC0CE File Offset: 0x000CA2CE
		public IEventHandler mouseCapture { get; }

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x06003430 RID: 13360 RVA: 0x000CC0D6 File Offset: 0x000CA2D6
		// (set) Token: 0x06003431 RID: 13361 RVA: 0x000CC0DE File Offset: 0x000CA2DE
		public long duration { get; set; }

		// Token: 0x06003432 RID: 13362 RVA: 0x000CC0E8 File Offset: 0x000CA2E8
		public EventDebuggerTrace(IPanel panel, EventBase evt, long duration, IEventHandler mouseCapture)
		{
			this.eventBase = new EventDebuggerEventRecord(evt);
			IEventHandler eventHandler;
			if (panel == null)
			{
				eventHandler = null;
			}
			else
			{
				FocusController focusController = panel.focusController;
				eventHandler = ((focusController != null) ? focusController.focusedElement : null);
			}
			this.focusedElement = eventHandler;
			this.mouseCapture = mouseCapture;
			this.duration = duration;
		}
	}
}
