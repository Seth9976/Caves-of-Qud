using System;
using System.Collections.Generic;
using System.Text;
using AiUnity.NLog.Core.Common;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Targets.Wrappers
{
	// Token: 0x02000234 RID: 564
	[Preserve]
	public abstract class CompoundTargetBase : Target
	{
		// Token: 0x060014DE RID: 5342 RVA: 0x00074D10 File Offset: 0x00072F10
		protected CompoundTargetBase(params Target[] targets)
		{
			this.Targets = new List<Target>(targets);
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060014DF RID: 5343 RVA: 0x00074D24 File Offset: 0x00072F24
		// (set) Token: 0x060014E0 RID: 5344 RVA: 0x00074D2C File Offset: 0x00072F2C
		public IList<Target> Targets { get; private set; }

		// Token: 0x060014E1 RID: 5345 RVA: 0x00074D38 File Offset: 0x00072F38
		public override string ToString()
		{
			string text = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.ToString());
			stringBuilder.Append("(");
			foreach (Target target in this.Targets)
			{
				stringBuilder.Append(text);
				stringBuilder.Append(target.ToString());
				text = ", ";
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x00074DD0 File Offset: 0x00072FD0
		protected override void Write(LogEventInfo logEvent)
		{
			throw new NotSupportedException("This target must not be invoked in a synchronous way.");
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x00074DDC File Offset: 0x00072FDC
		protected override void FlushAsync(AsyncContinuation asyncContinuation)
		{
			AsyncHelpers.ForEachItemInParallel<Target>(this.Targets, asyncContinuation, delegate(Target t, AsyncContinuation c)
			{
				t.Flush(c);
			});
		}
	}
}
