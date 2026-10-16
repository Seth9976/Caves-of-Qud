using System;
using System.Collections.Generic;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200013F RID: 319
	public static class Observable
	{
		// Token: 0x060011B2 RID: 4530 RVA: 0x00054CA4 File Offset: 0x00052EA4
		public static IObservable<TValue> Where<TValue>(this IObservable<TValue> source, Func<TValue, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new WhereObservable<TValue>(source, predicate);
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x00054CC9 File Offset: 0x00052EC9
		public static IObservable<TResult> Select<TSource, TResult>(this IObservable<TSource> source, Func<TSource, TResult> filter)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			return new SelectObservable<TSource, TResult>(source, filter);
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x00054CEE File Offset: 0x00052EEE
		public static IObservable<TResult> SelectMany<TSource, TResult>(this IObservable<TSource> source, Func<TSource, IEnumerable<TResult>> filter)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			return new SelectManyObservable<TSource, TResult>(source, filter);
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x00054D13 File Offset: 0x00052F13
		public static IObservable<TValue> Take<TValue>(this IObservable<TValue> source, int count)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return new TakeNObservable<TValue>(source, count);
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x00054D39 File Offset: 0x00052F39
		public static IObservable<InputEventPtr> ForDevice(this IObservable<InputEventPtr> source, InputDevice device)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new ForDeviceEventObservable(source, null, device);
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00054D51 File Offset: 0x00052F51
		public static IObservable<InputEventPtr> ForDevice<TDevice>(this IObservable<InputEventPtr> source) where TDevice : InputDevice
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new ForDeviceEventObservable(source, typeof(TDevice), null);
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00054D74 File Offset: 0x00052F74
		public static IDisposable CallOnce<TValue>(this IObservable<TValue> source, Action<TValue> action)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			IDisposable subscription = null;
			subscription = source.Take<TValue>(1).Subscribe(new Observer<TValue>(action, delegate
			{
				IDisposable subscription2 = subscription;
				if (subscription2 == null)
				{
					return;
				}
				subscription2.Dispose();
			}));
			return subscription;
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00054DD4 File Offset: 0x00052FD4
		public static IDisposable Call<TValue>(this IObservable<TValue> source, Action<TValue> action)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			return source.Subscribe(new Observer<TValue>(action, null));
		}
	}
}
