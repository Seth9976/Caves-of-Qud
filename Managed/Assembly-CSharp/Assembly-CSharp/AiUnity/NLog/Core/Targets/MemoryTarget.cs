using System;
using System.Collections.Generic;
using AiUnity.NLog.Core.Common;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Targets
{
	// Token: 0x0200021E RID: 542
	[Target("Memory")]
	[Preserve]
	public sealed class MemoryTarget : TargetWithLayout
	{
		// Token: 0x06001418 RID: 5144 RVA: 0x00072A1B File Offset: 0x00070C1B
		public MemoryTarget()
		{
			this.Logs = new List<string>();
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x00072A2E File Offset: 0x00070C2E
		// (set) Token: 0x0600141A RID: 5146 RVA: 0x00072A36 File Offset: 0x00070C36
		public IList<string> Logs { get; private set; }

		// Token: 0x0600141B RID: 5147 RVA: 0x00072A40 File Offset: 0x00070C40
		protected override void Write(LogEventInfo logEvent)
		{
			string text = this.Layout.Render(logEvent);
			this.Logs.Add(text);
		}
	}
}
