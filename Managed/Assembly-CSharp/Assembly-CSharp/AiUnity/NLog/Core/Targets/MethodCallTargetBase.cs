using System;
using System.Collections.Generic;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Config;
using AiUnity.NLog.Core.Internal;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Targets
{
	// Token: 0x02000221 RID: 545
	[Preserve]
	public abstract class MethodCallTargetBase : Target
	{
		// Token: 0x06001430 RID: 5168 RVA: 0x00072BCF File Offset: 0x00070DCF
		protected MethodCallTargetBase()
		{
			this.Parameters = new List<MethodCallParameter>();
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x00072BE2 File Offset: 0x00070DE2
		// (set) Token: 0x06001432 RID: 5170 RVA: 0x00072BEA File Offset: 0x00070DEA
		[ArrayParameter(typeof(MethodCallParameter), "parameter")]
		public IList<MethodCallParameter> Parameters { get; private set; }

		// Token: 0x06001433 RID: 5171 RVA: 0x00072BF4 File Offset: 0x00070DF4
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			object[] array = new object[this.Parameters.Count];
			int num = 0;
			foreach (MethodCallParameter methodCallParameter in this.Parameters)
			{
				array[num++] = methodCallParameter.GetValue(logEvent.LogEvent);
			}
			this.DoInvoke(array, logEvent.Continuation);
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x00072C70 File Offset: 0x00070E70
		protected virtual void DoInvoke(object[] parameters, AsyncContinuation continuation)
		{
			try
			{
				this.DoInvoke(parameters);
				continuation(null);
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrown())
				{
					throw;
				}
				continuation(ex);
			}
		}

		// Token: 0x06001435 RID: 5173
		protected abstract void DoInvoke(object[] parameters);
	}
}
