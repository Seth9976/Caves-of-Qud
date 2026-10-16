using System;
using AiUnity.Common.Attributes;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Targets.Wrappers
{
	// Token: 0x02000235 RID: 565
	[Target("FallbackGroup", IsCompound = true, IsWrapper = true)]
	[Preserve]
	public class FallbackGroupTarget : CompoundTargetBase
	{
		// Token: 0x060014E4 RID: 5348 RVA: 0x00074E09 File Offset: 0x00073009
		public FallbackGroupTarget()
			: this(new Target[0])
		{
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x00074E17 File Offset: 0x00073017
		public FallbackGroupTarget(params Target[] targets)
			: base(targets)
		{
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x00074E2B File Offset: 0x0007302B
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060014E7 RID: 5351 RVA: 0x00074E32 File Offset: 0x00073032
		// (set) Token: 0x060014E8 RID: 5352 RVA: 0x00074E3A File Offset: 0x0007303A
		[Display("Return start", "Return to first target after any successful write.", false, 0)]
		public bool ReturnToFirstOnSuccess { get; set; }

		// Token: 0x060014E9 RID: 5353 RVA: 0x00074E44 File Offset: 0x00073044
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			AsyncContinuation continuation = null;
			int tryCounter = 0;
			int targetToInvoke;
			continuation = delegate(Exception ex)
			{
				object obj2;
				if (ex == null)
				{
					obj2 = this.lockObject;
					lock (obj2)
					{
						if (this.currentTarget != 0 && this.ReturnToFirstOnSuccess)
						{
							FallbackGroupTarget.Logger.Debug("Fallback: target '{0}' succeeded. Returning to the first one.", new object[] { this.Targets[this.currentTarget] });
							this.currentTarget = 0;
						}
					}
					logEvent.Continuation(null);
					return;
				}
				obj2 = this.lockObject;
				lock (obj2)
				{
					FallbackGroupTarget.Logger.Warn("Fallback: target '{0}' failed. Proceeding to the next one. Error was: {1}", new object[]
					{
						this.Targets[this.currentTarget],
						ex
					});
					this.currentTarget = (this.currentTarget + 1) % this.Targets.Count;
					int tryCounter2 = tryCounter;
					tryCounter = tryCounter2 + 1;
					targetToInvoke = this.currentTarget;
					if (tryCounter >= this.Targets.Count)
					{
						targetToInvoke = -1;
					}
				}
				if (targetToInvoke >= 0)
				{
					this.Targets[targetToInvoke].WriteAsyncLogEvent(logEvent.LogEvent.WithContinuation(continuation));
					return;
				}
				logEvent.Continuation(ex);
			};
			object obj = this.lockObject;
			lock (obj)
			{
				targetToInvoke = this.currentTarget;
			}
			base.Targets[targetToInvoke].WriteAsyncLogEvent(logEvent.LogEvent.WithContinuation(continuation));
		}

		// Token: 0x04000D6C RID: 3436
		private int currentTarget;

		// Token: 0x04000D6D RID: 3437
		private object lockObject = new object();
	}
}
