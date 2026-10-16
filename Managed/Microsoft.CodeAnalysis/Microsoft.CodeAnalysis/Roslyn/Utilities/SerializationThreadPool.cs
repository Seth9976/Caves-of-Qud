using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Roslyn.Utilities
{
	// Token: 0x02000075 RID: 117
	internal static class SerializationThreadPool
	{
		// Token: 0x06000307 RID: 775 RVA: 0x0000B69A File Offset: 0x0000989A
		[return: Nullable(new byte[] { 1, 2 })]
		public static Task<object> RunOnBackgroundThreadAsync([Nullable(new byte[] { 1, 2 })] Func<object> start)
		{
			return SerializationThreadPool.ImmediateBackgroundThreadPool.QueueAsync(start);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000B6A2 File Offset: 0x000098A2
		[NullableContext(2)]
		[return: Nullable(new byte[] { 1, 2 })]
		public static Task<object> RunOnBackgroundThreadAsync([Nullable(new byte[] { 1, 2, 2 })] Func<object, object> start, object obj)
		{
			return SerializationThreadPool.ImmediateBackgroundThreadPool.QueueAsync(start, obj);
		}

		// Token: 0x0200051C RID: 1308
		private static class ImmediateBackgroundThreadPool
		{
			// Token: 0x06003338 RID: 13112 RVA: 0x00091219 File Offset: 0x0008F419
			[return: Nullable(new byte[] { 1, 2 })]
			public static Task<object> QueueAsync([Nullable(new byte[] { 1, 2 })] Func<object> threadStart)
			{
				return SerializationThreadPool.ImmediateBackgroundThreadPool.QueueAsync(threadStart, null);
			}

			// Token: 0x06003339 RID: 13113 RVA: 0x00091222 File Offset: 0x0008F422
			[NullableContext(2)]
			[return: Nullable(new byte[] { 1, 2 })]
			public static Task<object> QueueAsync([Nullable(new byte[] { 1, 2, 2 })] Func<object, object> threadStart, object state)
			{
				return SerializationThreadPool.ImmediateBackgroundThreadPool.QueueAsync(threadStart, state);
			}

			// Token: 0x0600333A RID: 13114 RVA: 0x0009122C File Offset: 0x0008F42C
			[NullableContext(1)]
			[return: Nullable(new byte[] { 1, 2 })]
			private static Task<object> QueueAsync(Delegate threadStart, [Nullable(2)] object state)
			{
				TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>(64);
				SerializationThreadPool.ImmediateBackgroundThreadPool.<QueueAsync>g__enqueue|5_1(new ValueTuple<Delegate, object, TaskCompletionSource<object>>(threadStart, state, taskCompletionSource));
				return taskCompletionSource.Task;
			}

			// Token: 0x0600333C RID: 13116 RVA: 0x00091279 File Offset: 0x0008F479
			[CompilerGenerated]
			internal static void <QueueAsync>g__createThread|5_0()
			{
				new Thread(delegate
				{
					ValueTuple<Delegate, object, TaskCompletionSource<object>> valueTuple;
					while (SerializationThreadPool.ImmediateBackgroundThreadPool.<QueueAsync>g__tryDequeue|5_2(out valueTuple))
					{
						try
						{
							Func<object, object> func = valueTuple.Item1 as Func<object, object>;
							if (func != null)
							{
								valueTuple.Item3.SetResult(func.Invoke(valueTuple.Item2));
							}
							else
							{
								valueTuple.Item3.SetResult(((Func<object>)valueTuple.Item1).Invoke());
							}
							continue;
						}
						catch (OperationCanceledException ex)
						{
							valueTuple.Item3.TrySetCanceled(ex.CancellationToken);
							continue;
						}
						catch (Exception ex2)
						{
							valueTuple.Item3.TrySetException(ex2);
							continue;
						}
						break;
					}
				})
				{
					IsBackground = true
				}.Start();
			}

			// Token: 0x0600333D RID: 13117 RVA: 0x000912AC File Offset: 0x0008F4AC
			[CompilerGenerated]
			internal static void <QueueAsync>g__enqueue|5_1([TupleElementNames(new string[] { "function", "state", "tcs" })] [Nullable(new byte[] { 0, 1, 2, 1, 2 })] ValueTuple<Delegate, object, TaskCompletionSource<object>> item)
			{
				Queue<ValueTuple<Delegate, object, TaskCompletionSource<object>>> queue = SerializationThreadPool.ImmediateBackgroundThreadPool.s_queue;
				lock (queue)
				{
					SerializationThreadPool.ImmediateBackgroundThreadPool.s_queue.Enqueue(item);
					if (SerializationThreadPool.ImmediateBackgroundThreadPool.s_queue.Count <= SerializationThreadPool.ImmediateBackgroundThreadPool.s_availableThreads)
					{
						Monitor.Pulse(SerializationThreadPool.ImmediateBackgroundThreadPool.s_queue);
						return;
					}
				}
				SerializationThreadPool.ImmediateBackgroundThreadPool.<QueueAsync>g__createThread|5_0();
			}

			// Token: 0x0600333E RID: 13118 RVA: 0x00091314 File Offset: 0x0008F514
			[CompilerGenerated]
			internal static bool <QueueAsync>g__tryDequeue|5_2([TupleElementNames(new string[] { "function", "state", "tcs" })] [Nullable(new byte[] { 0, 1, 2, 1, 2 })] out ValueTuple<Delegate, object, TaskCompletionSource<object>> item)
			{
				Queue<ValueTuple<Delegate, object, TaskCompletionSource<object>>> queue = SerializationThreadPool.ImmediateBackgroundThreadPool.s_queue;
				bool flag2;
				lock (queue)
				{
					SerializationThreadPool.ImmediateBackgroundThreadPool.s_availableThreads++;
					try
					{
						while (SerializationThreadPool.ImmediateBackgroundThreadPool.s_queue.Count == 0)
						{
							if (!Monitor.Wait(SerializationThreadPool.ImmediateBackgroundThreadPool.s_queue, SerializationThreadPool.ImmediateBackgroundThreadPool.s_idleTimeout))
							{
								if (SerializationThreadPool.ImmediateBackgroundThreadPool.s_queue.Count > 0)
								{
									break;
								}
								item = default(ValueTuple<Delegate, object, TaskCompletionSource<object>>);
								return false;
							}
						}
					}
					finally
					{
						SerializationThreadPool.ImmediateBackgroundThreadPool.s_availableThreads--;
					}
					item = SerializationThreadPool.ImmediateBackgroundThreadPool.s_queue.Dequeue();
					flag2 = true;
				}
				return flag2;
			}

			// Token: 0x0400148D RID: 5261
			private static readonly TimeSpan s_idleTimeout = TimeSpan.FromSeconds(1.0);

			// Token: 0x0400148E RID: 5262
			[TupleElementNames(new string[] { "function", "state", "tcs" })]
			[Nullable(new byte[] { 1, 0, 1, 2, 1, 2 })]
			private static readonly Queue<ValueTuple<Delegate, object, TaskCompletionSource<object>>> s_queue = new Queue<ValueTuple<Delegate, object, TaskCompletionSource<object>>>();

			// Token: 0x0400148F RID: 5263
			private static int s_availableThreads = 0;
		}
	}
}
