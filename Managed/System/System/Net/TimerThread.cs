using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200063F RID: 1599
	internal static class TimerThread
	{
		// Token: 0x06003255 RID: 12885 RVA: 0x000AE110 File Offset: 0x000AC310
		static TimerThread()
		{
			AppDomain.CurrentDomain.DomainUnload += TimerThread.OnDomainUnload;
		}

		// Token: 0x06003256 RID: 12886 RVA: 0x000AE188 File Offset: 0x000AC388
		internal static TimerThread.Queue CreateQueue(int durationMilliseconds)
		{
			if (durationMilliseconds == -1)
			{
				return new TimerThread.InfiniteTimerQueue();
			}
			if (durationMilliseconds < 0)
			{
				throw new ArgumentOutOfRangeException("durationMilliseconds");
			}
			LinkedList<WeakReference> linkedList = TimerThread.s_NewQueues;
			TimerThread.TimerQueue timerQueue;
			lock (linkedList)
			{
				timerQueue = new TimerThread.TimerQueue(durationMilliseconds);
				WeakReference weakReference = new WeakReference(timerQueue);
				TimerThread.s_NewQueues.AddLast(weakReference);
			}
			return timerQueue;
		}

		// Token: 0x06003257 RID: 12887 RVA: 0x000AE1F8 File Offset: 0x000AC3F8
		internal static TimerThread.Queue GetOrCreateQueue(int durationMilliseconds)
		{
			if (durationMilliseconds == -1)
			{
				return new TimerThread.InfiniteTimerQueue();
			}
			if (durationMilliseconds < 0)
			{
				throw new ArgumentOutOfRangeException("durationMilliseconds");
			}
			WeakReference weakReference = (WeakReference)TimerThread.s_QueuesCache[durationMilliseconds];
			TimerThread.TimerQueue timerQueue;
			if (weakReference == null || (timerQueue = (TimerThread.TimerQueue)weakReference.Target) == null)
			{
				LinkedList<WeakReference> linkedList = TimerThread.s_NewQueues;
				lock (linkedList)
				{
					weakReference = (WeakReference)TimerThread.s_QueuesCache[durationMilliseconds];
					if (weakReference == null || (timerQueue = (TimerThread.TimerQueue)weakReference.Target) == null)
					{
						timerQueue = new TimerThread.TimerQueue(durationMilliseconds);
						weakReference = new WeakReference(timerQueue);
						TimerThread.s_NewQueues.AddLast(weakReference);
						TimerThread.s_QueuesCache[durationMilliseconds] = weakReference;
						if (++TimerThread.s_CacheScanIteration % 32 == 0)
						{
							List<int> list = new List<int>();
							foreach (object obj in TimerThread.s_QueuesCache)
							{
								DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
								if (((WeakReference)dictionaryEntry.Value).Target == null)
								{
									list.Add((int)dictionaryEntry.Key);
								}
							}
							for (int i = 0; i < list.Count; i++)
							{
								TimerThread.s_QueuesCache.Remove(list[i]);
							}
						}
					}
				}
			}
			return timerQueue;
		}

		// Token: 0x06003258 RID: 12888 RVA: 0x000AE39C File Offset: 0x000AC59C
		private static void Prod()
		{
			TimerThread.s_ThreadReadyEvent.Set();
			if (Interlocked.CompareExchange(ref TimerThread.s_ThreadState, 1, 0) == 0)
			{
				new Thread(new ThreadStart(TimerThread.ThreadProc)).Start();
			}
		}

		// Token: 0x06003259 RID: 12889 RVA: 0x000AE3D0 File Offset: 0x000AC5D0
		private static void ThreadProc()
		{
			Thread.CurrentThread.IsBackground = true;
			LinkedList<WeakReference> linkedList = TimerThread.s_Queues;
			lock (linkedList)
			{
				if (Interlocked.CompareExchange(ref TimerThread.s_ThreadState, 1, 1) == 1)
				{
					bool flag2 = true;
					while (flag2)
					{
						try
						{
							TimerThread.s_ThreadReadyEvent.Reset();
							for (;;)
							{
								if (TimerThread.s_NewQueues.Count > 0)
								{
									LinkedList<WeakReference> linkedList2 = TimerThread.s_NewQueues;
									lock (linkedList2)
									{
										for (LinkedListNode<WeakReference> linkedListNode = TimerThread.s_NewQueues.First; linkedListNode != null; linkedListNode = TimerThread.s_NewQueues.First)
										{
											TimerThread.s_NewQueues.Remove(linkedListNode);
											TimerThread.s_Queues.AddLast(linkedListNode);
										}
									}
								}
								int tickCount = Environment.TickCount;
								int num = 0;
								bool flag4 = false;
								LinkedListNode<WeakReference> linkedListNode2 = TimerThread.s_Queues.First;
								while (linkedListNode2 != null)
								{
									TimerThread.TimerQueue timerQueue = (TimerThread.TimerQueue)linkedListNode2.Value.Target;
									if (timerQueue == null)
									{
										LinkedListNode<WeakReference> next = linkedListNode2.Next;
										TimerThread.s_Queues.Remove(linkedListNode2);
										linkedListNode2 = next;
									}
									else
									{
										int num2;
										if (timerQueue.Fire(out num2) && (!flag4 || TimerThread.IsTickBetween(tickCount, num, num2)))
										{
											num = num2;
											flag4 = true;
										}
										linkedListNode2 = linkedListNode2.Next;
									}
								}
								int tickCount2 = Environment.TickCount;
								int num3 = (int)(flag4 ? (TimerThread.IsTickBetween(tickCount, num, tickCount2) ? (Math.Min((uint)(num - tickCount2), 2147483632U) + 15U) : 0U) : 30000U);
								int num4 = WaitHandle.WaitAny(TimerThread.s_ThreadEvents, num3, false);
								if (num4 == 0)
								{
									break;
								}
								if (num4 == 258 && !flag4)
								{
									Interlocked.CompareExchange(ref TimerThread.s_ThreadState, 0, 1);
									if (!TimerThread.s_ThreadReadyEvent.WaitOne(0, false) || Interlocked.CompareExchange(ref TimerThread.s_ThreadState, 1, 0) != 0)
									{
										goto IL_01A8;
									}
								}
							}
							flag2 = false;
							continue;
							IL_01A8:
							flag2 = false;
						}
						catch (Exception ex)
						{
							if (NclUtilities.IsFatal(ex))
							{
								throw;
							}
							bool on = Logging.On;
							Thread.Sleep(1000);
						}
					}
				}
			}
		}

		// Token: 0x0600325A RID: 12890 RVA: 0x000AE604 File Offset: 0x000AC804
		private static void StopTimerThread()
		{
			Interlocked.Exchange(ref TimerThread.s_ThreadState, 2);
			TimerThread.s_ThreadShutdownEvent.Set();
		}

		// Token: 0x0600325B RID: 12891 RVA: 0x000AE61D File Offset: 0x000AC81D
		private static bool IsTickBetween(int start, int end, int comparand)
		{
			return start <= comparand == end <= comparand != start <= end;
		}

		// Token: 0x0600325C RID: 12892 RVA: 0x000AE63C File Offset: 0x000AC83C
		private static void OnDomainUnload(object sender, EventArgs e)
		{
			try
			{
				TimerThread.StopTimerThread();
			}
			catch
			{
			}
		}

		// Token: 0x04001D87 RID: 7559
		private const int c_ThreadIdleTimeoutMilliseconds = 30000;

		// Token: 0x04001D88 RID: 7560
		private const int c_CacheScanPerIterations = 32;

		// Token: 0x04001D89 RID: 7561
		private const int c_TickCountResolution = 15;

		// Token: 0x04001D8A RID: 7562
		private static LinkedList<WeakReference> s_Queues = new LinkedList<WeakReference>();

		// Token: 0x04001D8B RID: 7563
		private static LinkedList<WeakReference> s_NewQueues = new LinkedList<WeakReference>();

		// Token: 0x04001D8C RID: 7564
		private static int s_ThreadState = 0;

		// Token: 0x04001D8D RID: 7565
		private static AutoResetEvent s_ThreadReadyEvent = new AutoResetEvent(false);

		// Token: 0x04001D8E RID: 7566
		private static ManualResetEvent s_ThreadShutdownEvent = new ManualResetEvent(false);

		// Token: 0x04001D8F RID: 7567
		private static WaitHandle[] s_ThreadEvents = new WaitHandle[]
		{
			TimerThread.s_ThreadShutdownEvent,
			TimerThread.s_ThreadReadyEvent
		};

		// Token: 0x04001D90 RID: 7568
		private static int s_CacheScanIteration;

		// Token: 0x04001D91 RID: 7569
		private static Hashtable s_QueuesCache = new Hashtable();

		// Token: 0x02000640 RID: 1600
		internal abstract class Queue
		{
			// Token: 0x0600325D RID: 12893 RVA: 0x000AE664 File Offset: 0x000AC864
			internal Queue(int durationMilliseconds)
			{
				this.m_DurationMilliseconds = durationMilliseconds;
			}

			// Token: 0x17000A16 RID: 2582
			// (get) Token: 0x0600325E RID: 12894 RVA: 0x000AE673 File Offset: 0x000AC873
			internal int Duration
			{
				get
				{
					return this.m_DurationMilliseconds;
				}
			}

			// Token: 0x0600325F RID: 12895 RVA: 0x000AE67B File Offset: 0x000AC87B
			internal TimerThread.Timer CreateTimer()
			{
				return this.CreateTimer(null, null);
			}

			// Token: 0x06003260 RID: 12896
			internal abstract TimerThread.Timer CreateTimer(TimerThread.Callback callback, object context);

			// Token: 0x04001D92 RID: 7570
			private readonly int m_DurationMilliseconds;
		}

		// Token: 0x02000641 RID: 1601
		internal abstract class Timer : IDisposable
		{
			// Token: 0x06003261 RID: 12897 RVA: 0x000AE685 File Offset: 0x000AC885
			internal Timer(int durationMilliseconds)
			{
				this.m_DurationMilliseconds = durationMilliseconds;
				this.m_StartTimeMilliseconds = Environment.TickCount;
			}

			// Token: 0x17000A17 RID: 2583
			// (get) Token: 0x06003262 RID: 12898 RVA: 0x000AE69F File Offset: 0x000AC89F
			internal int Duration
			{
				get
				{
					return this.m_DurationMilliseconds;
				}
			}

			// Token: 0x17000A18 RID: 2584
			// (get) Token: 0x06003263 RID: 12899 RVA: 0x000AE6A7 File Offset: 0x000AC8A7
			internal int StartTime
			{
				get
				{
					return this.m_StartTimeMilliseconds;
				}
			}

			// Token: 0x17000A19 RID: 2585
			// (get) Token: 0x06003264 RID: 12900 RVA: 0x000AE6AF File Offset: 0x000AC8AF
			internal int Expiration
			{
				get
				{
					return this.m_StartTimeMilliseconds + this.m_DurationMilliseconds;
				}
			}

			// Token: 0x17000A1A RID: 2586
			// (get) Token: 0x06003265 RID: 12901 RVA: 0x000AE6C0 File Offset: 0x000AC8C0
			internal int TimeRemaining
			{
				get
				{
					if (this.HasExpired)
					{
						return 0;
					}
					if (this.Duration == -1)
					{
						return -1;
					}
					int tickCount = Environment.TickCount;
					int num = (int)(TimerThread.IsTickBetween(this.StartTime, this.Expiration, tickCount) ? Math.Min((uint)(this.Expiration - tickCount), 2147483647U) : 0U);
					if (num >= 2)
					{
						return num;
					}
					return num + 1;
				}
			}

			// Token: 0x06003266 RID: 12902
			internal abstract bool Cancel();

			// Token: 0x17000A1B RID: 2587
			// (get) Token: 0x06003267 RID: 12903
			internal abstract bool HasExpired { get; }

			// Token: 0x06003268 RID: 12904 RVA: 0x000AE71B File Offset: 0x000AC91B
			public void Dispose()
			{
				this.Cancel();
			}

			// Token: 0x04001D93 RID: 7571
			private readonly int m_StartTimeMilliseconds;

			// Token: 0x04001D94 RID: 7572
			private readonly int m_DurationMilliseconds;
		}

		// Token: 0x02000642 RID: 1602
		// (Invoke) Token: 0x0600326A RID: 12906
		internal delegate void Callback(TimerThread.Timer timer, int timeNoticed, object context);

		// Token: 0x02000643 RID: 1603
		private enum TimerThreadState
		{
			// Token: 0x04001D96 RID: 7574
			Idle,
			// Token: 0x04001D97 RID: 7575
			Running,
			// Token: 0x04001D98 RID: 7576
			Stopped
		}

		// Token: 0x02000644 RID: 1604
		private class TimerQueue : TimerThread.Queue
		{
			// Token: 0x0600326D RID: 12909 RVA: 0x000AE724 File Offset: 0x000AC924
			internal TimerQueue(int durationMilliseconds)
				: base(durationMilliseconds)
			{
				this.m_Timers = new TimerThread.TimerNode();
				this.m_Timers.Next = this.m_Timers;
				this.m_Timers.Prev = this.m_Timers;
			}

			// Token: 0x0600326E RID: 12910 RVA: 0x000AE75C File Offset: 0x000AC95C
			internal override TimerThread.Timer CreateTimer(TimerThread.Callback callback, object context)
			{
				TimerThread.TimerNode timerNode = new TimerThread.TimerNode(callback, context, base.Duration, this.m_Timers);
				bool flag = false;
				TimerThread.TimerNode timers = this.m_Timers;
				lock (timers)
				{
					if (this.m_Timers.Next == this.m_Timers)
					{
						if (this.m_ThisHandle == IntPtr.Zero)
						{
							this.m_ThisHandle = (IntPtr)GCHandle.Alloc(this);
						}
						flag = true;
					}
					timerNode.Next = this.m_Timers;
					timerNode.Prev = this.m_Timers.Prev;
					this.m_Timers.Prev.Next = timerNode;
					this.m_Timers.Prev = timerNode;
				}
				if (flag)
				{
					TimerThread.Prod();
				}
				return timerNode;
			}

			// Token: 0x0600326F RID: 12911 RVA: 0x000AE828 File Offset: 0x000ACA28
			internal bool Fire(out int nextExpiration)
			{
				TimerThread.TimerNode timerNode;
				do
				{
					timerNode = this.m_Timers.Next;
					if (timerNode == this.m_Timers)
					{
						TimerThread.TimerNode timers = this.m_Timers;
						lock (timers)
						{
							timerNode = this.m_Timers.Next;
							if (timerNode == this.m_Timers)
							{
								if (this.m_ThisHandle != IntPtr.Zero)
								{
									((GCHandle)this.m_ThisHandle).Free();
									this.m_ThisHandle = IntPtr.Zero;
								}
								nextExpiration = 0;
								return false;
							}
						}
					}
				}
				while (timerNode.Fire());
				nextExpiration = timerNode.Expiration;
				return true;
			}

			// Token: 0x04001D99 RID: 7577
			private IntPtr m_ThisHandle;

			// Token: 0x04001D9A RID: 7578
			private readonly TimerThread.TimerNode m_Timers;
		}

		// Token: 0x02000645 RID: 1605
		private class InfiniteTimerQueue : TimerThread.Queue
		{
			// Token: 0x06003270 RID: 12912 RVA: 0x000AE8DC File Offset: 0x000ACADC
			internal InfiniteTimerQueue()
				: base(-1)
			{
			}

			// Token: 0x06003271 RID: 12913 RVA: 0x000AE8E5 File Offset: 0x000ACAE5
			internal override TimerThread.Timer CreateTimer(TimerThread.Callback callback, object context)
			{
				return new TimerThread.InfiniteTimer();
			}
		}

		// Token: 0x02000646 RID: 1606
		private class TimerNode : TimerThread.Timer
		{
			// Token: 0x06003272 RID: 12914 RVA: 0x000AE8EC File Offset: 0x000ACAEC
			internal TimerNode(TimerThread.Callback callback, object context, int durationMilliseconds, object queueLock)
				: base(durationMilliseconds)
			{
				if (callback != null)
				{
					this.m_Callback = callback;
					this.m_Context = context;
				}
				this.m_TimerState = TimerThread.TimerNode.TimerState.Ready;
				this.m_QueueLock = queueLock;
			}

			// Token: 0x06003273 RID: 12915 RVA: 0x000AE915 File Offset: 0x000ACB15
			internal TimerNode()
				: base(0)
			{
				this.m_TimerState = TimerThread.TimerNode.TimerState.Sentinel;
			}

			// Token: 0x17000A1C RID: 2588
			// (get) Token: 0x06003274 RID: 12916 RVA: 0x000AE925 File Offset: 0x000ACB25
			internal override bool HasExpired
			{
				get
				{
					return this.m_TimerState == TimerThread.TimerNode.TimerState.Fired;
				}
			}

			// Token: 0x17000A1D RID: 2589
			// (get) Token: 0x06003275 RID: 12917 RVA: 0x000AE930 File Offset: 0x000ACB30
			// (set) Token: 0x06003276 RID: 12918 RVA: 0x000AE938 File Offset: 0x000ACB38
			internal TimerThread.TimerNode Next
			{
				get
				{
					return this.next;
				}
				set
				{
					this.next = value;
				}
			}

			// Token: 0x17000A1E RID: 2590
			// (get) Token: 0x06003277 RID: 12919 RVA: 0x000AE941 File Offset: 0x000ACB41
			// (set) Token: 0x06003278 RID: 12920 RVA: 0x000AE949 File Offset: 0x000ACB49
			internal TimerThread.TimerNode Prev
			{
				get
				{
					return this.prev;
				}
				set
				{
					this.prev = value;
				}
			}

			// Token: 0x06003279 RID: 12921 RVA: 0x000AE954 File Offset: 0x000ACB54
			internal override bool Cancel()
			{
				if (this.m_TimerState == TimerThread.TimerNode.TimerState.Ready)
				{
					object queueLock = this.m_QueueLock;
					lock (queueLock)
					{
						if (this.m_TimerState == TimerThread.TimerNode.TimerState.Ready)
						{
							this.Next.Prev = this.Prev;
							this.Prev.Next = this.Next;
							this.Next = null;
							this.Prev = null;
							this.m_Callback = null;
							this.m_Context = null;
							this.m_TimerState = TimerThread.TimerNode.TimerState.Cancelled;
							return true;
						}
					}
					return false;
				}
				return false;
			}

			// Token: 0x0600327A RID: 12922 RVA: 0x000AE9EC File Offset: 0x000ACBEC
			internal bool Fire()
			{
				if (this.m_TimerState != TimerThread.TimerNode.TimerState.Ready)
				{
					return true;
				}
				int tickCount = Environment.TickCount;
				if (TimerThread.IsTickBetween(base.StartTime, base.Expiration, tickCount))
				{
					return false;
				}
				bool flag = false;
				object queueLock = this.m_QueueLock;
				lock (queueLock)
				{
					if (this.m_TimerState == TimerThread.TimerNode.TimerState.Ready)
					{
						this.m_TimerState = TimerThread.TimerNode.TimerState.Fired;
						this.Next.Prev = this.Prev;
						this.Prev.Next = this.Next;
						this.Next = null;
						this.Prev = null;
						flag = this.m_Callback != null;
					}
				}
				if (flag)
				{
					try
					{
						TimerThread.Callback callback = this.m_Callback;
						object context = this.m_Context;
						this.m_Callback = null;
						this.m_Context = null;
						callback(this, tickCount, context);
					}
					catch (Exception ex)
					{
						if (NclUtilities.IsFatal(ex))
						{
							throw;
						}
						bool on = Logging.On;
					}
				}
				return true;
			}

			// Token: 0x04001D9B RID: 7579
			private TimerThread.TimerNode.TimerState m_TimerState;

			// Token: 0x04001D9C RID: 7580
			private TimerThread.Callback m_Callback;

			// Token: 0x04001D9D RID: 7581
			private object m_Context;

			// Token: 0x04001D9E RID: 7582
			private object m_QueueLock;

			// Token: 0x04001D9F RID: 7583
			private TimerThread.TimerNode next;

			// Token: 0x04001DA0 RID: 7584
			private TimerThread.TimerNode prev;

			// Token: 0x02000647 RID: 1607
			private enum TimerState
			{
				// Token: 0x04001DA2 RID: 7586
				Ready,
				// Token: 0x04001DA3 RID: 7587
				Fired,
				// Token: 0x04001DA4 RID: 7588
				Cancelled,
				// Token: 0x04001DA5 RID: 7589
				Sentinel
			}
		}

		// Token: 0x02000648 RID: 1608
		private class InfiniteTimer : TimerThread.Timer
		{
			// Token: 0x0600327B RID: 12923 RVA: 0x000AEAE0 File Offset: 0x000ACCE0
			internal InfiniteTimer()
				: base(-1)
			{
			}

			// Token: 0x17000A1F RID: 2591
			// (get) Token: 0x0600327C RID: 12924 RVA: 0x00003062 File Offset: 0x00001262
			internal override bool HasExpired
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600327D RID: 12925 RVA: 0x000AEAE9 File Offset: 0x000ACCE9
			internal override bool Cancel()
			{
				return Interlocked.Exchange(ref this.cancelled, 1) == 0;
			}

			// Token: 0x04001DA6 RID: 7590
			private int cancelled;
		}
	}
}
