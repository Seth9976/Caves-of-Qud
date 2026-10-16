using System;
using UnityEngine.Events;

namespace UnityEngine
{
	// Token: 0x0200027A RID: 634
	public static class UnityEventAwaitableExtensions
	{
		// Token: 0x06001FFA RID: 8186 RVA: 0x0003D97C File Offset: 0x0003BB7C
		public static Awaitable.Awaiter GetAwaiter(this UnityEvent ev)
		{
			Awaitable awaitable = Awaitable.NewManagedAwaitable();
			UnityAction cb = null;
			cb = delegate
			{
				ev.RemoveListener(cb);
				awaitable.RaiseManagedCompletion();
			};
			ev.AddListener(cb);
			return awaitable.GetAwaiter();
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x0003D9DC File Offset: 0x0003BBDC
		public static Awaitable<T>.Awaiter GetAwaiter<T>(this UnityEvent<T> ev)
		{
			Awaitable<T> awaitable = Awaitable<T>.GetManaged();
			UnityAction<T> cb = null;
			cb = delegate(T arg)
			{
				ev.RemoveListener(cb);
				awaitable.SetResultAndRaiseContinuation(arg);
			};
			ev.AddListener(cb);
			return awaitable.GetAwaiter();
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x0003DA3C File Offset: 0x0003BC3C
		public static Awaitable<ValueTuple<T0, T1>>.Awaiter GetAwaiter<T0, T1>(this UnityEvent<T0, T1> ev)
		{
			Awaitable<ValueTuple<T0, T1>> awaitable = Awaitable<ValueTuple<T0, T1>>.GetManaged();
			UnityAction<T0, T1> cb = null;
			cb = delegate(T0 arg0, T1 arg1)
			{
				ev.RemoveListener(cb);
				awaitable.SetResultAndRaiseContinuation(new ValueTuple<T0, T1>(arg0, arg1));
			};
			ev.AddListener(cb);
			return awaitable.GetAwaiter();
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x0003DA9C File Offset: 0x0003BC9C
		public static Awaitable<ValueTuple<T0, T1, T2>>.Awaiter GetAwaiter<T0, T1, T2>(this UnityEvent<T0, T1, T2> ev)
		{
			Awaitable<ValueTuple<T0, T1, T2>> awaitable = Awaitable<ValueTuple<T0, T1, T2>>.GetManaged();
			UnityAction<T0, T1, T2> cb = null;
			cb = delegate(T0 arg0, T1 arg1, T2 arg2)
			{
				ev.RemoveListener(cb);
				awaitable.SetResultAndRaiseContinuation(new ValueTuple<T0, T1, T2>(arg0, arg1, arg2));
			};
			ev.AddListener(cb);
			return awaitable.GetAwaiter();
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x0003DAFC File Offset: 0x0003BCFC
		public static Awaitable<ValueTuple<T0, T1, T2, T3>>.Awaiter GetAwaiter<T0, T1, T2, T3>(this UnityEvent<T0, T1, T2, T3> ev)
		{
			Awaitable<ValueTuple<T0, T1, T2, T3>> awaitable = Awaitable<ValueTuple<T0, T1, T2, T3>>.GetManaged();
			UnityAction<T0, T1, T2, T3> cb = null;
			cb = delegate(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
			{
				ev.RemoveListener(cb);
				awaitable.SetResultAndRaiseContinuation(new ValueTuple<T0, T1, T2, T3>(arg0, arg1, arg2, arg3));
			};
			ev.AddListener(cb);
			return awaitable.GetAwaiter();
		}
	}
}
