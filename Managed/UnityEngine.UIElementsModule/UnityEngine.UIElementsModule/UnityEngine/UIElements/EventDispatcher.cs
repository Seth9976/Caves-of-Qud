using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;

namespace UnityEngine.UIElements
{
	// Token: 0x02000210 RID: 528
	public sealed class EventDispatcher
	{
		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x00046E61 File Offset: 0x00045061
		internal PointerDispatchState pointerState { get; } = new PointerDispatchState();

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06001007 RID: 4103 RVA: 0x00046E69 File Offset: 0x00045069
		internal uint GateDepth
		{
			get
			{
				return this.m_GateDepth;
			}
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x00046E74 File Offset: 0x00045074
		internal static EventDispatcher CreateDefault()
		{
			return new EventDispatcher();
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x00046E8C File Offset: 0x0004508C
		[Obsolete("Please use EventDispatcher.CreateDefault().")]
		internal EventDispatcher()
		{
			this.m_Queue = EventDispatcher.k_EventQueuePool.Get();
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x0600100A RID: 4106 RVA: 0x00046EE8 File Offset: 0x000450E8
		private bool dispatchImmediately
		{
			get
			{
				return this.m_Immediate || this.m_GateCount == 0U;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x00046F0E File Offset: 0x0004510E
		// (set) Token: 0x0600100C RID: 4108 RVA: 0x00046F16 File Offset: 0x00045116
		internal bool processingEvents { get; private set; }

		// Token: 0x0600100D RID: 4109 RVA: 0x00046F20 File Offset: 0x00045120
		internal void Dispatch(EventBase evt, [NotNull] BaseVisualElementPanel panel, DispatchMode dispatchMode)
		{
			evt.MarkReceivedByDispatcher();
			bool flag = evt.eventTypeId == EventBase<IMGUIEvent>.TypeId();
			if (flag)
			{
				Event imguiEvent = evt.imguiEvent;
				bool flag2 = imguiEvent.rawType == EventType.Repaint;
				if (flag2)
				{
					return;
				}
			}
			bool flag3 = this.dispatchImmediately || dispatchMode == DispatchMode.Immediate;
			if (flag3)
			{
				this.ProcessEvent(evt, panel);
			}
			else
			{
				bool flag4 = this.HandleRecursiveState(evt);
				if (!flag4)
				{
					evt.Acquire();
					this.m_Queue.Enqueue(new EventDispatcher.EventRecord
					{
						m_Event = evt,
						m_Panel = panel
					});
				}
			}
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x00046FC0 File Offset: 0x000451C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool HandleRecursiveState(EventBase evt)
		{
			bool flag = this.m_GateDepth <= 400U;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this.m_DispatchStackFrame != 0;
				if (flag3)
				{
					StackTrace stackTrace = new StackTrace(1, true);
					StringBuilder stringBuilder = new StringBuilder();
					int num = stackTrace.FrameCount - this.m_DispatchStackFrame;
					stringBuilder.AppendLine(string.Format("Recursively dispatching event {0} from another event {1} (depth = {2})", evt, this.m_CurrentEvent, this.m_GateDepth));
					for (int i = 0; i < num; i++)
					{
						StackFrame frame = stackTrace.GetFrame(i);
						stringBuilder.Append(frame.GetMethod()).AppendFormat("({0}:{1}", frame.GetFileName(), frame.GetFileLineNumber()).AppendLine(")");
					}
					Debug.LogFormat(LogType.Error, LogOption.NoStacktrace, null, stringBuilder.ToString(), Array.Empty<object>());
				}
				else
				{
					Debug.LogFormat(LogType.Error, LogOption.NoStacktrace, null, string.Format("Recursively dispatching event {0} from another event {1} (depth = {2})", evt, this.m_CurrentEvent, this.m_GateDepth), Array.Empty<object>());
				}
				bool flag4 = this.m_GateDepth > 500U;
				if (flag4)
				{
					Debug.LogErrorFormat("Ignoring event {0}: too many events dispatched recurively", new object[] { evt });
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x0004710C File Offset: 0x0004530C
		internal void PushDispatcherContext()
		{
			this.ProcessEventQueue();
			this.m_DispatchContexts.Push(new EventDispatcher.DispatchContext
			{
				m_GateCount = this.m_GateCount,
				m_Queue = this.m_Queue
			});
			this.m_GateCount = 0U;
			this.m_Queue = EventDispatcher.k_EventQueuePool.Get();
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x00047168 File Offset: 0x00045368
		internal void PopDispatcherContext()
		{
			Debug.Assert(this.m_GateCount == 0U, "All gates should have been opened before popping dispatch context.");
			Debug.Assert(this.m_Queue.Count == 0, "Queue should be empty when popping dispatch context.");
			EventDispatcher.k_EventQueuePool.Release(this.m_Queue);
			this.m_GateCount = this.m_DispatchContexts.Peek().m_GateCount;
			this.m_Queue = this.m_DispatchContexts.Peek().m_Queue;
			this.m_DispatchContexts.Pop();
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x000471EC File Offset: 0x000453EC
		internal void CloseGate()
		{
			this.m_GateCount += 1U;
			this.m_GateDepth += 1U;
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x0004720C File Offset: 0x0004540C
		internal void OpenGate()
		{
			Debug.Assert(this.m_GateCount > 0U, "m_GateCount > 0");
			bool flag = this.m_GateCount > 0U;
			if (flag)
			{
				this.m_GateCount -= 1U;
			}
			try
			{
				bool flag2 = this.m_GateCount == 0U;
				if (flag2)
				{
					this.ProcessEventQueue();
				}
			}
			finally
			{
				Debug.Assert(this.m_GateDepth > 0U, "m_GateDepth > 0");
				bool flag3 = this.m_GateDepth > 0U;
				if (flag3)
				{
					this.m_GateDepth -= 1U;
				}
			}
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x000472AC File Offset: 0x000454AC
		private void ProcessEventQueue()
		{
			Queue<EventDispatcher.EventRecord> queue = this.m_Queue;
			this.m_Queue = EventDispatcher.k_EventQueuePool.Get();
			ExitGUIException ex = null;
			try
			{
				this.processingEvents = true;
				while (queue.Count > 0)
				{
					EventDispatcher.EventRecord eventRecord = queue.Dequeue();
					EventBase @event = eventRecord.m_Event;
					BaseVisualElementPanel panel = eventRecord.m_Panel;
					try
					{
						this.ProcessEvent(@event, panel);
					}
					catch (ExitGUIException ex2)
					{
						Debug.Assert(ex == null);
						ex = ex2;
					}
					finally
					{
						@event.Dispose();
					}
				}
			}
			finally
			{
				this.processingEvents = false;
				EventDispatcher.k_EventQueuePool.Release(queue);
			}
			bool flag = ex != null;
			if (flag)
			{
				throw ex;
			}
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x00047384 File Offset: 0x00045584
		private void ProcessEvent(EventBase evt, [NotNull] BaseVisualElementPanel panel)
		{
			bool disposed = panel.disposed;
			if (!disposed)
			{
				Event imguiEvent = evt.imguiEvent;
				bool flag = imguiEvent != null && imguiEvent.rawType == EventType.Used;
				using (new EventDispatcherGate(this))
				{
					evt.PreDispatch(panel);
					try
					{
						this.m_CurrentEvent = evt;
						this.m_DispatchStackFrame = ((this.m_GateDepth > 490U) ? new StackTrace().FrameCount : 0);
						evt.Dispatch(panel);
					}
					finally
					{
						this.m_CurrentEvent = null;
					}
					evt.PostDispatch(panel);
					Debug.Assert(flag || evt.isPropagationStopped || imguiEvent == null || imguiEvent.rawType != EventType.Used, "Event is used but not stopped.");
				}
			}
		}

		// Token: 0x040009B9 RID: 2489
		internal ClickDetector m_ClickDetector = new ClickDetector();

		// Token: 0x040009BA RID: 2490
		private static readonly ObjectPool<Queue<EventDispatcher.EventRecord>> k_EventQueuePool = new ObjectPool<Queue<EventDispatcher.EventRecord>>(() => new Queue<EventDispatcher.EventRecord>(), 100);

		// Token: 0x040009BB RID: 2491
		private Queue<EventDispatcher.EventRecord> m_Queue;

		// Token: 0x040009BD RID: 2493
		private uint m_GateCount;

		// Token: 0x040009BE RID: 2494
		private uint m_GateDepth = 0U;

		// Token: 0x040009BF RID: 2495
		internal const int k_MaxGateDepth = 500;

		// Token: 0x040009C0 RID: 2496
		internal const int k_NumberOfEventsWithStackInfo = 10;

		// Token: 0x040009C1 RID: 2497
		internal const int k_NumberOfEventsWithEventInfo = 100;

		// Token: 0x040009C2 RID: 2498
		private int m_DispatchStackFrame = 0;

		// Token: 0x040009C3 RID: 2499
		private EventBase m_CurrentEvent;

		// Token: 0x040009C4 RID: 2500
		private Stack<EventDispatcher.DispatchContext> m_DispatchContexts = new Stack<EventDispatcher.DispatchContext>();

		// Token: 0x040009C5 RID: 2501
		private bool m_Immediate = false;

		// Token: 0x02000211 RID: 529
		private struct EventRecord
		{
			// Token: 0x040009C7 RID: 2503
			public EventBase m_Event;

			// Token: 0x040009C8 RID: 2504
			public BaseVisualElementPanel m_Panel;
		}

		// Token: 0x02000212 RID: 530
		private struct DispatchContext
		{
			// Token: 0x040009C9 RID: 2505
			public uint m_GateCount;

			// Token: 0x040009CA RID: 2506
			public Queue<EventDispatcher.EventRecord> m_Queue;
		}
	}
}
