using System;
using System.ComponentModel;
using System.Threading;
using AiUnity.Common.Attributes;
using AiUnity.NLog.Core.Common;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Targets.Wrappers
{
	// Token: 0x02000233 RID: 563
	[Target("BufferingWrapper", IsWrapper = true)]
	[Preserve]
	public class BufferingTargetWrapper : WrapperTargetBase
	{
		// Token: 0x060014CF RID: 5327 RVA: 0x00074B0E File Offset: 0x00072D0E
		public BufferingTargetWrapper()
			: this(null)
		{
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x00074B17 File Offset: 0x00072D17
		public BufferingTargetWrapper(Target wrappedTarget)
			: this(wrappedTarget, 100)
		{
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x00074B22 File Offset: 0x00072D22
		public BufferingTargetWrapper(Target wrappedTarget, int bufferSize)
			: this(wrappedTarget, bufferSize, -1)
		{
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x00074B2D File Offset: 0x00072D2D
		public BufferingTargetWrapper(Target wrappedTarget, int bufferSize, int flushTimeout)
		{
			base.WrappedTarget = wrappedTarget;
			this.BufferSize = bufferSize;
			this.FlushTimeout = flushTimeout;
			this.SlidingTimeout = true;
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x00074B51 File Offset: 0x00072D51
		// (set) Token: 0x060014D4 RID: 5332 RVA: 0x00074B59 File Offset: 0x00072D59
		[DefaultValue(100)]
		[Display("Buffer size", "Buffer size to fill before write to inner target occurs.", false, 0)]
		public int BufferSize { get; set; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x00074B62 File Offset: 0x00072D62
		// (set) Token: 0x060014D6 RID: 5334 RVA: 0x00074B6A File Offset: 0x00072D6A
		[DefaultValue(-1)]
		[Display("Timeout", "Timeout (milliseconds) after which buffer contents is flushed.  Disabled with a value of -1.", false, 0)]
		public int FlushTimeout { get; set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x00074B73 File Offset: 0x00072D73
		// (set) Token: 0x060014D8 RID: 5336 RVA: 0x00074B7B File Offset: 0x00072D7B
		[DefaultValue(true)]
		[Display("Slide Timeout", "When enabled timer reset on each write.  If disabled timer will start uninterrupted upon first write.", false, 0)]
		public bool SlidingTimeout { get; set; }

		// Token: 0x060014D9 RID: 5337 RVA: 0x00074B84 File Offset: 0x00072D84
		protected override void FlushAsync(AsyncContinuation asyncContinuation)
		{
			AsyncLogEventInfo[] eventsAndClear = this.buffer.GetEventsAndClear();
			if (eventsAndClear.Length == 0)
			{
				base.WrappedTarget.Flush(asyncContinuation);
				return;
			}
			base.WrappedTarget.WriteAsyncLogEvents(eventsAndClear, delegate(Exception ex)
			{
				this.WrappedTarget.Flush(asyncContinuation);
			});
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x00074BDF File Offset: 0x00072DDF
		protected override void InitializeTarget()
		{
			base.InitializeTarget();
			this.buffer = new LogEventInfoBuffer(this.BufferSize, false, 0);
			this.flushTimer = new Timer(new TimerCallback(this.FlushCallback), null, -1, -1);
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x00074C14 File Offset: 0x00072E14
		protected override void CloseTarget()
		{
			base.CloseTarget();
			if (this.flushTimer != null)
			{
				this.flushTimer.Dispose();
				this.flushTimer = null;
			}
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x00074C38 File Offset: 0x00072E38
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			base.WrappedTarget.PrecalculateVolatileLayouts(logEvent.LogEvent);
			int num = this.buffer.Append(logEvent);
			if (num >= this.BufferSize)
			{
				AsyncLogEventInfo[] eventsAndClear = this.buffer.GetEventsAndClear();
				base.WrappedTarget.WriteAsyncLogEvents(eventsAndClear);
				return;
			}
			if (this.FlushTimeout > 0 && (this.SlidingTimeout || num == 1))
			{
				this.flushTimer.Change(this.FlushTimeout, -1);
			}
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x00074CB0 File Offset: 0x00072EB0
		private void FlushCallback(object state)
		{
			object syncRoot = base.SyncRoot;
			lock (syncRoot)
			{
				if (base.IsInitialized)
				{
					AsyncLogEventInfo[] eventsAndClear = this.buffer.GetEventsAndClear();
					if (eventsAndClear.Length != 0)
					{
						base.WrappedTarget.WriteAsyncLogEvents(eventsAndClear);
					}
				}
			}
		}

		// Token: 0x04000D66 RID: 3430
		private LogEventInfoBuffer buffer;

		// Token: 0x04000D67 RID: 3431
		private Timer flushTimer;
	}
}
