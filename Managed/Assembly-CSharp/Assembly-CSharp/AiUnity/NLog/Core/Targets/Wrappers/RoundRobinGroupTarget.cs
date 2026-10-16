using System;
using AiUnity.NLog.Core.Common;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Targets.Wrappers
{
	// Token: 0x0200023A RID: 570
	[Target("RoundRobinGroup", IsCompound = true, IsWrapper = true)]
	[Preserve]
	public class RoundRobinGroupTarget : CompoundTargetBase
	{
		// Token: 0x06001500 RID: 5376 RVA: 0x00075142 File Offset: 0x00073342
		public RoundRobinGroupTarget()
			: this(new Target[0])
		{
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x00075150 File Offset: 0x00073350
		public RoundRobinGroupTarget(params Target[] targets)
			: base(targets)
		{
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x00075164 File Offset: 0x00073364
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			if (base.Targets.Count == 0)
			{
				logEvent.Continuation(null);
				return;
			}
			object obj = this.lockObject;
			int num;
			lock (obj)
			{
				num = this.currentTarget;
				this.currentTarget = (this.currentTarget + 1) % base.Targets.Count;
			}
			base.Targets[num].WriteAsyncLogEvent(logEvent);
		}

		// Token: 0x04000D75 RID: 3445
		private int currentTarget;

		// Token: 0x04000D76 RID: 3446
		private object lockObject = new object();
	}
}
