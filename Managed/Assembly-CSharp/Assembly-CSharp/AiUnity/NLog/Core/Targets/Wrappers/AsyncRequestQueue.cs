using System;
using System.Collections.Generic;
using System.Threading;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Targets.Wrappers
{
	// Token: 0x0200022F RID: 559
	[Preserve]
	internal class AsyncRequestQueue
	{
		// Token: 0x060014AD RID: 5293 RVA: 0x00074478 File Offset: 0x00072678
		public AsyncRequestQueue(int requestLimit, AsyncTargetWrapperOverflowAction overflowAction)
		{
			this.RequestLimit = requestLimit;
			this.OnOverflow = overflowAction;
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060014AE RID: 5294 RVA: 0x00074499 File Offset: 0x00072699
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x000744A0 File Offset: 0x000726A0
		// (set) Token: 0x060014B0 RID: 5296 RVA: 0x000744A8 File Offset: 0x000726A8
		public int RequestLimit { get; set; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060014B1 RID: 5297 RVA: 0x000744B1 File Offset: 0x000726B1
		// (set) Token: 0x060014B2 RID: 5298 RVA: 0x000744B9 File Offset: 0x000726B9
		public AsyncTargetWrapperOverflowAction OnOverflow { get; set; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x000744C2 File Offset: 0x000726C2
		public int RequestCount
		{
			get
			{
				return this.logEventInfoQueue.Count;
			}
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x000744D0 File Offset: 0x000726D0
		public void Enqueue(AsyncLogEventInfo logEventInfo)
		{
			lock (this)
			{
				if (this.logEventInfoQueue.Count >= this.RequestLimit)
				{
					AsyncRequestQueue.Logger.Debug("Async queue is full", Array.Empty<object>());
					switch (this.OnOverflow)
					{
					case AsyncTargetWrapperOverflowAction.Grow:
						AsyncRequestQueue.Logger.Debug("The overflow action is Grow, adding element anyway", Array.Empty<object>());
						break;
					case AsyncTargetWrapperOverflowAction.Discard:
						AsyncRequestQueue.Logger.Debug("Discarding one element from queue", Array.Empty<object>());
						this.logEventInfoQueue.Dequeue();
						break;
					case AsyncTargetWrapperOverflowAction.Block:
						while (this.logEventInfoQueue.Count >= this.RequestLimit)
						{
							AsyncRequestQueue.Logger.Debug("Blocking because the overflow action is Block...", Array.Empty<object>());
							Monitor.Wait(this);
							AsyncRequestQueue.Logger.Trace("Entered critical section.", Array.Empty<object>());
						}
						AsyncRequestQueue.Logger.Trace("Limit ok.", Array.Empty<object>());
						break;
					}
				}
				this.logEventInfoQueue.Enqueue(logEventInfo);
			}
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x000745EC File Offset: 0x000727EC
		public AsyncLogEventInfo[] DequeueBatch(int count)
		{
			List<AsyncLogEventInfo> list = new List<AsyncLogEventInfo>();
			lock (this)
			{
				int num = 0;
				while (num < count && this.logEventInfoQueue.Count > 0)
				{
					list.Add(this.logEventInfoQueue.Dequeue());
					num++;
				}
				if (this.OnOverflow == AsyncTargetWrapperOverflowAction.Block)
				{
					Monitor.PulseAll(this);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x00074668 File Offset: 0x00072868
		public void Clear()
		{
			lock (this)
			{
				this.logEventInfoQueue.Clear();
			}
		}

		// Token: 0x04000D58 RID: 3416
		private readonly Queue<AsyncLogEventInfo> logEventInfoQueue = new Queue<AsyncLogEventInfo>();
	}
}
