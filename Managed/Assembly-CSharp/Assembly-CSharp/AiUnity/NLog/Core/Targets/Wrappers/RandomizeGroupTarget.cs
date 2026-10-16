using System;
using AiUnity.NLog.Core.Common;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Targets.Wrappers
{
	// Token: 0x02000237 RID: 567
	[Target("RandomizeGroup", IsCompound = true, IsWrapper = true)]
	[Preserve]
	public class RandomizeGroupTarget : CompoundTargetBase
	{
		// Token: 0x060014F0 RID: 5360 RVA: 0x00074F70 File Offset: 0x00073170
		public RandomizeGroupTarget()
			: this(new Target[0])
		{
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x00074F7E File Offset: 0x0007317E
		public RandomizeGroupTarget(params Target[] targets)
			: base(targets)
		{
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x00074F94 File Offset: 0x00073194
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			if (base.Targets.Count == 0)
			{
				logEvent.Continuation(null);
				return;
			}
			Random random = this.random;
			int num;
			lock (random)
			{
				num = this.random.Next(base.Targets.Count);
			}
			base.Targets[num].WriteAsyncLogEvent(logEvent);
		}

		// Token: 0x04000D71 RID: 3441
		private readonly Random random = new Random();
	}
}
