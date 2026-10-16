using System;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x0200070F RID: 1807
	internal class EventDebuggerPathTrace : EventDebuggerTrace
	{
		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x0600343B RID: 13371 RVA: 0x000CC1CE File Offset: 0x000CA3CE
		public PropagationPaths paths { get; }

		// Token: 0x0600343C RID: 13372 RVA: 0x000CC1D6 File Offset: 0x000CA3D6
		public EventDebuggerPathTrace(IPanel panel, EventBase evt, PropagationPaths paths)
			: base(panel, evt, -1L, null)
		{
			this.paths = paths;
		}
	}
}
