using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.PooledObjects
{
	// Token: 0x0200029C RID: 668
	[NullableContext(2)]
	[Nullable(0)]
	internal static class PooledDelegates
	{
		// Token: 0x06001C52 RID: 7250 RVA: 0x000539A8 File Offset: 0x00051BA8
		[NullableContext(1)]
		private static PooledDelegates.Releaser GetPooledDelegate<[Nullable(0)] TPooled, [Nullable(2)] TArg, [Nullable(0)] TUnboundDelegate, [Nullable(0)] TBoundDelegate>(TUnboundDelegate unboundDelegate, TArg argument, out TBoundDelegate boundDelegate) where TPooled : PooledDelegates.AbstractDelegateWithBoundArgument<TPooled, TArg, TUnboundDelegate, TBoundDelegate>, new() where TUnboundDelegate : Delegate where TBoundDelegate : Delegate
		{
			TPooled tpooled = PooledDelegates.DefaultDelegatePool<TPooled>.Instance.Allocate();
			tpooled.Initialize(unboundDelegate, argument);
			boundDelegate = tpooled.BoundDelegate;
			return new PooledDelegates.Releaser(tpooled);
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x000539E9 File Offset: 0x00051BE9
		[NullableContext(1)]
		public static PooledDelegates.Releaser GetPooledAction<[Nullable(2)] TArg>(Action<TArg> unboundAction, TArg argument, out Action boundAction)
		{
			return PooledDelegates.GetPooledDelegate<PooledDelegates.ActionWithBoundArgument<TArg>, TArg, Action<TArg>, Action>(unboundAction, argument, out boundAction);
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x000539F3 File Offset: 0x00051BF3
		[NullableContext(1)]
		public static PooledDelegates.Releaser GetPooledAction<[Nullable(2)] T1, [Nullable(2)] TArg>(Action<T1, TArg> unboundAction, TArg argument, out Action<T1> boundAction)
		{
			return PooledDelegates.GetPooledDelegate<PooledDelegates.ActionWithBoundArgument<T1, TArg>, TArg, Action<T1, TArg>, Action<T1>>(unboundAction, argument, out boundAction);
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x000539FD File Offset: 0x00051BFD
		[NullableContext(1)]
		public static PooledDelegates.Releaser GetPooledAction<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] TArg>(Action<T1, T2, TArg> unboundAction, TArg argument, out Action<T1, T2> boundAction)
		{
			return PooledDelegates.GetPooledDelegate<PooledDelegates.ActionWithBoundArgument<T1, T2, TArg>, TArg, Action<T1, T2, TArg>, Action<T1, T2>>(unboundAction, argument, out boundAction);
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x00053A07 File Offset: 0x00051C07
		public static PooledDelegates.Releaser GetPooledAction<T1, T2, T3, TArg>([Nullable(1)] Action<T1, T2, T3, TArg> unboundAction, [Nullable(1)] TArg argument, [Nullable(1)] out Action<T1, T2, T3> boundAction)
		{
			return PooledDelegates.GetPooledDelegate<PooledDelegates.ActionWithBoundArgument<T1, T2, T3, TArg>, TArg, Action<T1, T2, T3, TArg>, Action<T1, T2, T3>>(unboundAction, argument, out boundAction);
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x00053A11 File Offset: 0x00051C11
		[NullableContext(1)]
		public static PooledDelegates.Releaser GetPooledFunction<[Nullable(2)] TArg, [Nullable(2)] TResult>(Func<TArg, TResult> unboundFunction, TArg argument, out Func<TResult> boundFunction)
		{
			return PooledDelegates.GetPooledDelegate<PooledDelegates.FuncWithBoundArgument<TArg, TResult>, TArg, Func<TArg, TResult>, Func<TResult>>(unboundFunction, argument, out boundFunction);
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x00053A1B File Offset: 0x00051C1B
		[NullableContext(1)]
		public static PooledDelegates.Releaser GetPooledFunction<[Nullable(2)] T1, [Nullable(2)] TArg, [Nullable(2)] TResult>(Func<T1, TArg, TResult> unboundFunction, TArg argument, out Func<T1, TResult> boundFunction)
		{
			return PooledDelegates.GetPooledDelegate<PooledDelegates.FuncWithBoundArgument<T1, TArg, TResult>, TArg, Func<T1, TArg, TResult>, Func<T1, TResult>>(unboundFunction, argument, out boundFunction);
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x00053A25 File Offset: 0x00051C25
		public static PooledDelegates.Releaser GetPooledFunction<T1, T2, TArg, TResult>([Nullable(1)] Func<T1, T2, TArg, TResult> unboundFunction, [Nullable(1)] TArg argument, [Nullable(1)] out Func<T1, T2, TResult> boundFunction)
		{
			return PooledDelegates.GetPooledDelegate<PooledDelegates.FuncWithBoundArgument<T1, T2, TArg, TResult>, TArg, Func<T1, T2, TArg, TResult>, Func<T1, T2, TResult>>(unboundFunction, argument, out boundFunction);
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x00053A2F File Offset: 0x00051C2F
		public static PooledDelegates.Releaser GetPooledFunction<T1, T2, T3, TArg, TResult>([Nullable(1)] Func<T1, T2, T3, TArg, TResult> unboundFunction, [Nullable(1)] TArg argument, [Nullable(1)] out Func<T1, T2, T3, TResult> boundFunction)
		{
			return PooledDelegates.GetPooledDelegate<PooledDelegates.FuncWithBoundArgument<T1, T2, T3, TArg, TResult>, TArg, Func<T1, T2, T3, TArg, TResult>, Func<T1, T2, T3, TResult>>(unboundFunction, argument, out boundFunction);
		}

		// Token: 0x0200063E RID: 1598
		[NullableContext(1)]
		[Nullable(0)]
		private static class DefaultDelegatePool<T> where T : class, new()
		{
			// Token: 0x0400184B RID: 6219
			public static readonly ObjectPool<T> Instance = new ObjectPool<T>(() => new T(), 20);
		}

		// Token: 0x0200063F RID: 1599
		[NullableContext(0)]
		[PooledDelegates.NonCopyableAttribute]
		public struct Releaser : IDisposable
		{
			// Token: 0x06003870 RID: 14448 RVA: 0x0009D1B8 File Offset: 0x0009B3B8
			[NullableContext(1)]
			internal Releaser(PooledDelegates.Poolable pooledObject)
			{
				this._pooledObject = pooledObject;
			}

			// Token: 0x06003871 RID: 14449 RVA: 0x0009D1C1 File Offset: 0x0009B3C1
			public void Dispose()
			{
				this._pooledObject.ClearAndFree();
			}

			// Token: 0x0400184C RID: 6220
			[Nullable(1)]
			private readonly PooledDelegates.Poolable _pooledObject;
		}

		// Token: 0x02000640 RID: 1600
		[NullableContext(0)]
		internal abstract class Poolable
		{
			// Token: 0x06003872 RID: 14450
			public abstract void ClearAndFree();
		}

		// Token: 0x02000641 RID: 1601
		[NullableContext(1)]
		[Nullable(0)]
		private abstract class AbstractDelegateWithBoundArgument<[Nullable(0)] TSelf, [Nullable(2)] TArg, [Nullable(0)] TUnboundDelegate, [Nullable(0)] TBoundDelegate> : PooledDelegates.Poolable where TSelf : PooledDelegates.AbstractDelegateWithBoundArgument<TSelf, TArg, TUnboundDelegate, TBoundDelegate>, new() where TUnboundDelegate : Delegate where TBoundDelegate : Delegate
		{
			// Token: 0x06003874 RID: 14452 RVA: 0x0009D1D8 File Offset: 0x0009B3D8
			protected AbstractDelegateWithBoundArgument()
			{
				this.BoundDelegate = this.Bind();
				this.UnboundDelegate = default(TUnboundDelegate);
				this.Argument = default(TArg);
			}

			// Token: 0x17000FC1 RID: 4033
			// (get) Token: 0x06003875 RID: 14453 RVA: 0x0009D215 File Offset: 0x0009B415
			public TBoundDelegate BoundDelegate { get; }

			// Token: 0x17000FC2 RID: 4034
			// (get) Token: 0x06003876 RID: 14454 RVA: 0x0009D21D File Offset: 0x0009B41D
			// (set) Token: 0x06003877 RID: 14455 RVA: 0x0009D225 File Offset: 0x0009B425
			public TUnboundDelegate UnboundDelegate { get; private set; }

			// Token: 0x17000FC3 RID: 4035
			// (get) Token: 0x06003878 RID: 14456 RVA: 0x0009D22E File Offset: 0x0009B42E
			// (set) Token: 0x06003879 RID: 14457 RVA: 0x0009D236 File Offset: 0x0009B436
			public TArg Argument { get; private set; }

			// Token: 0x0600387A RID: 14458 RVA: 0x0009D23F File Offset: 0x0009B43F
			public void Initialize(TUnboundDelegate unboundDelegate, TArg argument)
			{
				this.UnboundDelegate = unboundDelegate;
				this.Argument = argument;
			}

			// Token: 0x0600387B RID: 14459 RVA: 0x0009D250 File Offset: 0x0009B450
			public sealed override void ClearAndFree()
			{
				this.Argument = default(TArg);
				this.UnboundDelegate = default(TUnboundDelegate);
				PooledDelegates.DefaultDelegatePool<TSelf>.Instance.Free((TSelf)((object)this));
			}

			// Token: 0x0600387C RID: 14460
			protected abstract TBoundDelegate Bind();
		}

		// Token: 0x02000642 RID: 1602
		[NullableContext(1)]
		[Nullable(new byte[] { 0, 1, 1, 1, 1, 1, 1 })]
		private sealed class ActionWithBoundArgument<[Nullable(2)] TArg> : PooledDelegates.AbstractDelegateWithBoundArgument<PooledDelegates.ActionWithBoundArgument<TArg>, TArg, Action<TArg>, Action>
		{
			// Token: 0x0600387D RID: 14461 RVA: 0x0009D28B File Offset: 0x0009B48B
			protected override Action Bind()
			{
				return delegate
				{
					base.UnboundDelegate.Invoke(base.Argument);
				};
			}
		}

		// Token: 0x02000643 RID: 1603
		[Nullable(new byte[] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1 })]
		private sealed class ActionWithBoundArgument<T1, TArg> : PooledDelegates.AbstractDelegateWithBoundArgument<PooledDelegates.ActionWithBoundArgument<T1, TArg>, TArg, Action<T1, TArg>, Action<T1>>
		{
			// Token: 0x06003880 RID: 14464 RVA: 0x0009D2B4 File Offset: 0x0009B4B4
			[NullableContext(1)]
			protected override Action<T1> Bind()
			{
				return delegate(T1 arg1)
				{
					base.UnboundDelegate.Invoke(arg1, base.Argument);
				};
			}
		}

		// Token: 0x02000644 RID: 1604
		[Nullable(new byte[]
		{
			0, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1
		})]
		private sealed class ActionWithBoundArgument<T1, T2, TArg> : PooledDelegates.AbstractDelegateWithBoundArgument<PooledDelegates.ActionWithBoundArgument<T1, T2, TArg>, TArg, Action<T1, T2, TArg>, Action<T1, T2>>
		{
			// Token: 0x06003883 RID: 14467 RVA: 0x0009D2DE File Offset: 0x0009B4DE
			[NullableContext(1)]
			protected override Action<T1, T2> Bind()
			{
				return delegate(T1 arg1, T2 arg2)
				{
					base.UnboundDelegate.Invoke(arg1, arg2, base.Argument);
				};
			}
		}

		// Token: 0x02000645 RID: 1605
		[Nullable(new byte[]
		{
			0, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1
		})]
		private sealed class ActionWithBoundArgument<T1, T2, T3, TArg> : PooledDelegates.AbstractDelegateWithBoundArgument<PooledDelegates.ActionWithBoundArgument<T1, T2, T3, TArg>, TArg, Action<T1, T2, T3, TArg>, Action<T1, T2, T3>>
		{
			// Token: 0x06003886 RID: 14470 RVA: 0x0009D309 File Offset: 0x0009B509
			[NullableContext(1)]
			protected override Action<T1, T2, T3> Bind()
			{
				return delegate(T1 arg1, T2 arg2, T3 arg3)
				{
					base.UnboundDelegate.Invoke(arg1, arg2, arg3, base.Argument);
				};
			}
		}

		// Token: 0x02000646 RID: 1606
		[Nullable(new byte[] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1 })]
		private sealed class FuncWithBoundArgument<TArg, TResult> : PooledDelegates.AbstractDelegateWithBoundArgument<PooledDelegates.FuncWithBoundArgument<TArg, TResult>, TArg, Func<TArg, TResult>, Func<TResult>>
		{
			// Token: 0x06003889 RID: 14473 RVA: 0x0009D335 File Offset: 0x0009B535
			[NullableContext(1)]
			protected override Func<TResult> Bind()
			{
				return () => base.UnboundDelegate.Invoke(base.Argument);
			}
		}

		// Token: 0x02000647 RID: 1607
		[Nullable(new byte[]
		{
			0, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1
		})]
		private sealed class FuncWithBoundArgument<T1, TArg, TResult> : PooledDelegates.AbstractDelegateWithBoundArgument<PooledDelegates.FuncWithBoundArgument<T1, TArg, TResult>, TArg, Func<T1, TArg, TResult>, Func<T1, TResult>>
		{
			// Token: 0x0600388C RID: 14476 RVA: 0x0009D35E File Offset: 0x0009B55E
			[NullableContext(1)]
			protected override Func<T1, TResult> Bind()
			{
				return (T1 arg1) => base.UnboundDelegate.Invoke(arg1, base.Argument);
			}
		}

		// Token: 0x02000648 RID: 1608
		[Nullable(new byte[]
		{
			0, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1
		})]
		private sealed class FuncWithBoundArgument<T1, T2, TArg, TResult> : PooledDelegates.AbstractDelegateWithBoundArgument<PooledDelegates.FuncWithBoundArgument<T1, T2, TArg, TResult>, TArg, Func<T1, T2, TArg, TResult>, Func<T1, T2, TResult>>
		{
			// Token: 0x0600388F RID: 14479 RVA: 0x0009D388 File Offset: 0x0009B588
			[NullableContext(1)]
			protected override Func<T1, T2, TResult> Bind()
			{
				return (T1 arg1, T2 arg2) => base.UnboundDelegate.Invoke(arg1, arg2, base.Argument);
			}
		}

		// Token: 0x02000649 RID: 1609
		[Nullable(new byte[]
		{
			0, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1
		})]
		private sealed class FuncWithBoundArgument<T1, T2, T3, TArg, TResult> : PooledDelegates.AbstractDelegateWithBoundArgument<PooledDelegates.FuncWithBoundArgument<T1, T2, T3, TArg, TResult>, TArg, Func<T1, T2, T3, TArg, TResult>, Func<T1, T2, T3, TResult>>
		{
			// Token: 0x06003892 RID: 14482 RVA: 0x0009D3B3 File Offset: 0x0009B5B3
			[NullableContext(1)]
			protected override Func<T1, T2, T3, TResult> Bind()
			{
				return (T1 arg1, T2 arg2, T3 arg3) => base.UnboundDelegate.Invoke(arg1, arg2, arg3, base.Argument);
			}
		}

		// Token: 0x0200064A RID: 1610
		[NullableContext(0)]
		[AttributeUsage(8)]
		private sealed class NonCopyableAttribute : Attribute
		{
		}
	}
}
