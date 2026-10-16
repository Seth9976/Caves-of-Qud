using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Pool;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000267 RID: 615
	[NativeHeader("Runtime/Mono/DelayedCallAwaitable.h")]
	[AsyncMethodBuilder(typeof(Awaitable.AwaitableAsyncMethodBuilder))]
	[NativeHeader("Runtime/Mono/Awaitable.h")]
	[NativeHeader("Runtime/Mono/AsyncOperationAwaitable.h")]
	public class Awaitable : IEnumerator
	{
		// Token: 0x06001F70 RID: 8048 RVA: 0x0003C540 File Offset: 0x0003A740
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Awaitable FromAsyncOperation(AsyncOperation op, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			IntPtr intPtr = Awaitable.FromAsyncOperationInternal(op.m_Ptr);
			return Awaitable.FromNativeAwaitableHandle(intPtr, cancellationToken);
		}

		// Token: 0x06001F71 RID: 8049
		[FreeFunction("Scripting::Awaitables::FromAsyncOperation", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr FromAsyncOperationInternal(IntPtr asyncOperation);

		// Token: 0x06001F72 RID: 8050 RVA: 0x0003C56D File Offset: 0x0003A76D
		[ExcludeFromDocs]
		public Awaitable.Awaiter GetAwaiter()
		{
			return new Awaitable.Awaiter(this);
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x0003C578 File Offset: 0x0003A778
		[RequiredByNativeCode(GenerateProxy = true)]
		private void SetExceptionFromNative(Exception ex)
		{
			bool flag = false;
			try
			{
				this._spinLock.Enter(ref flag);
				this._exceptionToRethrow = ExceptionDispatchInfo.Capture(ex);
			}
			finally
			{
				bool flag2 = flag;
				if (flag2)
				{
					this._spinLock.Exit();
				}
			}
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x0003C5CC File Offset: 0x0003A7CC
		[RequiredByNativeCode(GenerateProxy = true)]
		private void RunContinuation()
		{
			Action action = null;
			bool flag = false;
			try
			{
				this._spinLock.Enter(ref flag);
				action = this._continuation;
				this._continuation = null;
			}
			finally
			{
				bool flag2 = flag;
				if (flag2)
				{
					this._spinLock.Exit();
				}
			}
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x06001F75 RID: 8053
		[FreeFunction("Scripting::Awaitables::AttachManagedWrapper", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AttachManagedGCHandleToNativeAwaitable(IntPtr nativeAwaitable, UIntPtr gcHandle);

		// Token: 0x06001F76 RID: 8054
		[FreeFunction("Scripting::Awaitables::Release", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseNativeAwaitable(IntPtr nativeAwaitable);

		// Token: 0x06001F77 RID: 8055
		[FreeFunction("Scripting::Awaitables::Cancel", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CancelNativeAwaitable(IntPtr nativeAwaitable);

		// Token: 0x06001F78 RID: 8056
		[FreeFunction("Scripting::Awaitables::IsCompleted", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int IsNativeAwaitableCompleted(IntPtr nativeAwaitable);

		// Token: 0x06001F79 RID: 8057 RVA: 0x0003C630 File Offset: 0x0003A830
		private Awaitable()
		{
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x0003C648 File Offset: 0x0003A848
		internal static Awaitable NewManagedAwaitable()
		{
			Awaitable awaitable = Awaitable._pool.Value.Get();
			awaitable._handle = Awaitable.AwaitableHandle.ManagedHandle;
			return awaitable;
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x0003C678 File Offset: 0x0003A878
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static Awaitable FromNativeAwaitableHandle(IntPtr nativeHandle, CancellationToken cancellationToken)
		{
			Awaitable awaitable = Awaitable._pool.Value.Get();
			awaitable._handle = nativeHandle;
			Awaitable.AttachManagedGCHandleToNativeAwaitable(nativeHandle, (UIntPtr)((void*)GCHandle.ToIntPtr(GCHandle.Alloc(awaitable))));
			bool canBeCanceled = cancellationToken.CanBeCanceled;
			if (canBeCanceled)
			{
				Awaitable.WireupCancellation(awaitable, cancellationToken);
			}
			return awaitable;
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x0003C6DC File Offset: 0x0003A8DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void WireupCancellation(Awaitable awaitable, CancellationToken cancellationToken)
		{
			bool flag = awaitable == null;
			if (flag)
			{
				throw new ArgumentNullException("awaitable");
			}
			bool flag2 = false;
			try
			{
				awaitable._spinLock.Enter(ref flag2);
				using (ExecutionContext.SuppressFlow())
				{
					awaitable._cancelTokenRegistration = new CancellationTokenRegistration?(cancellationToken.Register(delegate(object coroutine)
					{
						((Awaitable)coroutine).Cancel();
					}, awaitable, false));
				}
			}
			finally
			{
				bool flag3 = flag2;
				if (flag3)
				{
					awaitable._spinLock.Exit();
				}
			}
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x0003C790 File Offset: 0x0003A990
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool MatchCompletionThreadAffinity(Awaitable.AwaiterCompletionThreadAffinity awaiterCompletionThreadAffinity)
		{
			if (!true)
			{
			}
			bool flag;
			if (awaiterCompletionThreadAffinity != Awaitable.AwaiterCompletionThreadAffinity.MainThread)
			{
				flag = awaiterCompletionThreadAffinity != Awaitable.AwaiterCompletionThreadAffinity.BackgroundThread || Thread.CurrentThread.ManagedThreadId != Awaitable._mainThreadId;
			}
			else
			{
				flag = Thread.CurrentThread.ManagedThreadId == Awaitable._mainThreadId;
			}
			if (!true)
			{
			}
			return flag;
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x0003C7E8 File Offset: 0x0003A9E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void RaiseManagedCompletion(Exception exception)
		{
			Action action = null;
			bool flag = false;
			Awaitable.AwaiterCompletionThreadAffinity completionThreadAffinity;
			try
			{
				this._spinLock.Enter(ref flag);
				bool flag2 = exception != null;
				if (flag2)
				{
					this._exceptionToRethrow = ExceptionDispatchInfo.Capture(exception);
				}
				this._managedAwaitableDone = true;
				action = this._continuation;
				completionThreadAffinity = this._completionThreadAffinity;
				this._continuation = null;
			}
			finally
			{
				bool flag3 = flag;
				if (flag3)
				{
					this._spinLock.Exit();
				}
			}
			bool flag4 = action != null;
			if (flag4)
			{
				this.RunOrScheduleContinuation(completionThreadAffinity, action);
			}
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x0003C87C File Offset: 0x0003AA7C
		private void RunOrScheduleContinuation(Awaitable.AwaiterCompletionThreadAffinity awaiterCompletionThreadAffinity, Action continuation)
		{
			bool flag = Awaitable.MatchCompletionThreadAffinity(awaiterCompletionThreadAffinity);
			if (flag)
			{
				continuation();
			}
			else
			{
				bool flag2 = awaiterCompletionThreadAffinity == Awaitable.AwaiterCompletionThreadAffinity.MainThread;
				if (flag2)
				{
					Awaitable._synchronizationContext.Post(new SendOrPostCallback(Awaitable.DoRunContinuationOnSynchonizationContext), continuation);
				}
				else
				{
					bool flag3 = awaiterCompletionThreadAffinity == Awaitable.AwaiterCompletionThreadAffinity.BackgroundThread;
					if (flag3)
					{
						Task.Run(continuation);
					}
				}
			}
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x0003C8D4 File Offset: 0x0003AAD4
		private static void DoRunContinuationOnSynchonizationContext(object continuation)
		{
			Action action = continuation as Action;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x0003C8EC File Offset: 0x0003AAEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void RaiseManagedCompletion()
		{
			Action action = null;
			bool flag = false;
			Awaitable.AwaiterCompletionThreadAffinity completionThreadAffinity;
			try
			{
				this._spinLock.Enter(ref flag);
				this._managedAwaitableDone = true;
				completionThreadAffinity = this._completionThreadAffinity;
				action = this._continuation;
				this._continuation = null;
				this._managedCompletionQueue = null;
			}
			finally
			{
				bool flag2 = flag;
				if (flag2)
				{
					this._spinLock.Exit();
				}
			}
			bool flag3 = action != null;
			if (flag3)
			{
				this.RunOrScheduleContinuation(completionThreadAffinity, action);
			}
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x0003C970 File Offset: 0x0003AB70
		internal void PropagateExceptionAndRelease()
		{
			bool flag = false;
			try
			{
				this._spinLock.Enter(ref flag);
				this.CheckPointerValidity();
				bool flag2 = this._cancelTokenRegistration != null;
				if (flag2)
				{
					this._cancelTokenRegistration.Value.Dispose();
					this._cancelTokenRegistration = null;
				}
				this._managedAwaitableDone = false;
				this._completionThreadAffinity = Awaitable.AwaiterCompletionThreadAffinity.None;
				Awaitable.AwaitableHandle handle = this._handle;
				this._handle = Awaitable.AwaitableHandle.NullHandle;
				ExceptionDispatchInfo exceptionToRethrow = this._exceptionToRethrow;
				this._exceptionToRethrow = null;
				this._managedCompletionQueue = null;
				this._continuation = null;
				bool flag3 = !handle.IsManaged && !handle.IsNull;
				if (flag3)
				{
					Awaitable.ReleaseNativeAwaitable(handle);
				}
				Awaitable._pool.Value.Release(this);
				if (exceptionToRethrow != null)
				{
					exceptionToRethrow.Throw();
				}
			}
			finally
			{
				bool flag4 = flag;
				if (flag4)
				{
					this._spinLock.Exit();
				}
			}
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x0003CA78 File Offset: 0x0003AC78
		public void Cancel()
		{
			Awaitable.AwaitableHandle awaitableHandle = this.CheckPointerValidity();
			bool isManaged = awaitableHandle.IsManaged;
			if (isManaged)
			{
				Awaitable.DoubleBufferedAwaitableList managedCompletionQueue = this._managedCompletionQueue;
				if (managedCompletionQueue != null)
				{
					managedCompletionQueue.Remove(this);
				}
				this.RaiseManagedCompletion(new OperationCanceledException());
			}
			else
			{
				Awaitable.CancelNativeAwaitable(awaitableHandle);
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001F84 RID: 8068 RVA: 0x0003CACC File Offset: 0x0003ACCC
		private bool IsCompletedNoLock
		{
			get
			{
				this.CheckPointerValidity();
				bool isManaged = this._handle.IsManaged;
				bool flag;
				if (isManaged)
				{
					flag = this._managedAwaitableDone && Awaitable.MatchCompletionThreadAffinity(this._completionThreadAffinity);
				}
				else
				{
					flag = Awaitable.IsNativeAwaitableCompleted(this._handle) != 0;
				}
				return flag;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001F85 RID: 8069 RVA: 0x0003CB24 File Offset: 0x0003AD24
		private bool IsLogicallyCompletedNoLock
		{
			get
			{
				this.CheckPointerValidity();
				bool isManaged = this._handle.IsManaged;
				bool flag;
				if (isManaged)
				{
					flag = this._managedAwaitableDone;
				}
				else
				{
					flag = Awaitable.IsNativeAwaitableCompleted(this._handle) != 0;
				}
				return flag;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001F86 RID: 8070 RVA: 0x0003CB6C File Offset: 0x0003AD6C
		public bool IsCompleted
		{
			get
			{
				bool flag = false;
				bool isCompletedNoLock;
				try
				{
					this._spinLock.Enter(ref flag);
					isCompletedNoLock = this.IsCompletedNoLock;
				}
				finally
				{
					bool flag2 = flag;
					if (flag2)
					{
						this._spinLock.Exit();
					}
				}
				return isCompletedNoLock;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001F87 RID: 8071 RVA: 0x0003CBBC File Offset: 0x0003ADBC
		internal bool IsDettachedOrCompleted
		{
			get
			{
				bool flag = false;
				bool flag2;
				try
				{
					this._spinLock.Enter(ref flag);
					bool isNull = this._handle.IsNull;
					if (isNull)
					{
						flag2 = true;
					}
					else
					{
						this.CheckPointerValidity();
						bool isManaged = this._handle.IsManaged;
						if (isManaged)
						{
							flag2 = this._managedAwaitableDone;
						}
						else
						{
							flag2 = Awaitable.IsNativeAwaitableCompleted(this._handle) != 0;
						}
					}
				}
				finally
				{
					bool flag3 = flag;
					if (flag3)
					{
						this._spinLock.Exit();
					}
				}
				return flag2;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001F88 RID: 8072 RVA: 0x0003CC50 File Offset: 0x0003AE50
		// (set) Token: 0x06001F89 RID: 8073 RVA: 0x0003CCA0 File Offset: 0x0003AEA0
		internal Awaitable.AwaiterCompletionThreadAffinity CompletionThreadAffinity
		{
			get
			{
				bool flag = false;
				Awaitable.AwaiterCompletionThreadAffinity completionThreadAffinity;
				try
				{
					this._spinLock.Enter(ref flag);
					completionThreadAffinity = this._completionThreadAffinity;
				}
				finally
				{
					bool flag2 = flag;
					if (flag2)
					{
						this._spinLock.Exit();
					}
				}
				return completionThreadAffinity;
			}
			set
			{
				bool flag = false;
				try
				{
					this._spinLock.Enter(ref flag);
					this._completionThreadAffinity = value;
				}
				finally
				{
					bool flag2 = flag;
					if (flag2)
					{
						this._spinLock.Exit();
					}
				}
			}
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x0003CCF0 File Offset: 0x0003AEF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private Awaitable.AwaitableHandle CheckPointerValidity()
		{
			Awaitable.AwaitableHandle handle = this._handle;
			bool isNull = handle.IsNull;
			if (isNull)
			{
				throw new InvalidOperationException("Awaitable is in detached state");
			}
			return handle;
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x0003CD24 File Offset: 0x0003AF24
		internal void SetContinuation(Action continuation)
		{
			bool flag = false;
			bool flag2 = false;
			Awaitable.AwaiterCompletionThreadAffinity awaiterCompletionThreadAffinity = Awaitable.AwaiterCompletionThreadAffinity.None;
			try
			{
				this._spinLock.Enter(ref flag2);
				bool isLogicallyCompletedNoLock = this.IsLogicallyCompletedNoLock;
				if (isLogicallyCompletedNoLock)
				{
					flag = true;
					awaiterCompletionThreadAffinity = this._completionThreadAffinity;
				}
				else
				{
					this._continuation = continuation;
				}
			}
			finally
			{
				bool flag3 = flag2;
				if (flag3)
				{
					this._spinLock.Exit();
				}
			}
			bool flag4 = flag;
			if (flag4)
			{
				this.RunOrScheduleContinuation(awaiterCompletionThreadAffinity, continuation);
			}
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x0003CDA4 File Offset: 0x0003AFA4
		bool IEnumerator.MoveNext()
		{
			bool isCompleted = this.IsCompleted;
			bool flag;
			if (isCompleted)
			{
				this.PropagateExceptionAndRelease();
				flag = false;
			}
			else
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x00002925 File Offset: 0x00000B25
		void IEnumerator.Reset()
		{
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001F8E RID: 8078 RVA: 0x0003CDCD File Offset: 0x0003AFCD
		object IEnumerator.Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x0003CDD0 File Offset: 0x0003AFD0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void ThrowIfNotMainThread()
		{
			bool flag = Thread.CurrentThread.ManagedThreadId != Awaitable._mainThreadId;
			if (flag)
			{
				throw new InvalidOperationException("This operation can only be performed on the main thread.");
			}
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x0003CE04 File Offset: 0x0003B004
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Awaitable NextFrameAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			Awaitable.ThrowIfNotMainThread();
			cancellationToken.ThrowIfCancellationRequested();
			Awaitable.EnsureDelayedCallWiredUp();
			Awaitable awaitable = Awaitable.NewManagedAwaitable();
			Awaitable._nextFrameAwaitables.Add(awaitable, Time.frameCount + 1);
			awaitable._managedCompletionQueue = Awaitable._nextFrameAwaitables;
			bool canBeCanceled = cancellationToken.CanBeCanceled;
			if (canBeCanceled)
			{
				Awaitable.WireupCancellation(awaitable, cancellationToken);
			}
			return awaitable;
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x0003CE64 File Offset: 0x0003B064
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Awaitable WaitForSecondsAsync(float seconds, CancellationToken cancellationToken = default(CancellationToken))
		{
			Awaitable.ThrowIfNotMainThread();
			cancellationToken.ThrowIfCancellationRequested();
			IntPtr intPtr = Awaitable.WaitForScondsInternal(seconds);
			return Awaitable.FromNativeAwaitableHandle(intPtr, cancellationToken);
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x0003CE94 File Offset: 0x0003B094
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Awaitable FixedUpdateAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			Awaitable.ThrowIfNotMainThread();
			cancellationToken.ThrowIfCancellationRequested();
			IntPtr intPtr = Awaitable.FixedUpdateInternal();
			return Awaitable.FromNativeAwaitableHandle(intPtr, cancellationToken);
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x0003CEC4 File Offset: 0x0003B0C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Awaitable EndOfFrameAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			Awaitable.ThrowIfNotMainThread();
			cancellationToken.ThrowIfCancellationRequested();
			Awaitable.EnsureDelayedCallWiredUp();
			Awaitable awaitable = Awaitable.NewManagedAwaitable();
			Awaitable._endOfFrameAwaitables.Add(awaitable, -1);
			awaitable._managedCompletionQueue = Awaitable._endOfFrameAwaitables;
			bool canBeCanceled = cancellationToken.CanBeCanceled;
			if (canBeCanceled)
			{
				Awaitable.WireupCancellation(awaitable, cancellationToken);
			}
			return awaitable;
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x0003CF20 File Offset: 0x0003B120
		private static void EnsureDelayedCallWiredUp()
		{
			bool nextFrameAndEndOfFrameWiredUp = Awaitable._nextFrameAndEndOfFrameWiredUp;
			if (!nextFrameAndEndOfFrameWiredUp)
			{
				Awaitable._nextFrameAndEndOfFrameWiredUp = true;
				Awaitable.WireupNextFrameAndEndOfFrameCallbacks();
				Awaitable._nextFrameAndEndOfFrameWiredUpCTRegistration = Application.exitCancellationToken.Register(new Action(Awaitable.OnDelayedCallManagerCleared));
			}
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x0003CF64 File Offset: 0x0003B164
		[RequiredByNativeCode]
		private static void OnDelayedCallManagerCleared()
		{
			Awaitable._nextFrameAndEndOfFrameWiredUp = false;
			Awaitable._nextFrameAwaitables.Clear();
			Awaitable._endOfFrameAwaitables.Clear();
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x0003CF83 File Offset: 0x0003B183
		[RequiredByNativeCode]
		private static void OnUpdate()
		{
			Awaitable._nextFrameAwaitables.SwapAndComplete();
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x0003CF91 File Offset: 0x0003B191
		[RequiredByNativeCode]
		private static void OnEndOfFrame()
		{
			Awaitable._endOfFrameAwaitables.SwapAndComplete();
		}

		// Token: 0x06001F98 RID: 8088
		[FreeFunction("Scripting::Awaitables::NextFrameAwaitable")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr NextFrameInternal();

		// Token: 0x06001F99 RID: 8089
		[FreeFunction("Scripting::Awaitables::WaitForSecondsAwaitable")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr WaitForScondsInternal(float seconds);

		// Token: 0x06001F9A RID: 8090
		[FreeFunction("Scripting::Awaitables::FixedUpdateAwaitable")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr FixedUpdateInternal();

		// Token: 0x06001F9B RID: 8091
		[FreeFunction("Scripting::Awaitables::EndOfFrameAwaitable")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr EndOfFrameInternal();

		// Token: 0x06001F9C RID: 8092
		[FreeFunction("Scripting::Awaitables::WireupNextFrameAndEndOfFrameCallbacks")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WireupNextFrameAndEndOfFrameCallbacks();

		// Token: 0x06001F9D RID: 8093 RVA: 0x0003CF9F File Offset: 0x0003B19F
		internal static void SetSynchronizationContext(UnitySynchronizationContext synchronizationContext)
		{
			Awaitable._synchronizationContext = synchronizationContext;
			Awaitable._mainThreadId = synchronizationContext.MainThreadId;
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x0003CFB4 File Offset: 0x0003B1B4
		public static MainThreadAwaitable MainThreadAsync()
		{
			return new MainThreadAwaitable(Awaitable._synchronizationContext, Awaitable._mainThreadId);
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x0003CFD8 File Offset: 0x0003B1D8
		public static BackgroundThreadAwaitable BackgroundThreadAsync()
		{
			return new BackgroundThreadAwaitable(Awaitable._synchronizationContext, Awaitable._mainThreadId);
		}

		// Token: 0x04000946 RID: 2374
		private SpinLock _spinLock = default(SpinLock);

		// Token: 0x04000947 RID: 2375
		private static readonly ThreadLocal<ObjectPool<Awaitable>> _pool = new ThreadLocal<ObjectPool<Awaitable>>(() => new ObjectPool<Awaitable>(() => new Awaitable(), null, null, null, false, 10, 10000));

		// Token: 0x04000948 RID: 2376
		private Awaitable.AwaitableHandle _handle;

		// Token: 0x04000949 RID: 2377
		private ExceptionDispatchInfo _exceptionToRethrow;

		// Token: 0x0400094A RID: 2378
		private bool _managedAwaitableDone;

		// Token: 0x0400094B RID: 2379
		private Awaitable.AwaiterCompletionThreadAffinity _completionThreadAffinity;

		// Token: 0x0400094C RID: 2380
		private Action _continuation;

		// Token: 0x0400094D RID: 2381
		private CancellationTokenRegistration? _cancelTokenRegistration;

		// Token: 0x0400094E RID: 2382
		private Awaitable.DoubleBufferedAwaitableList _managedCompletionQueue;

		// Token: 0x0400094F RID: 2383
		private static bool _nextFrameAndEndOfFrameWiredUp = false;

		// Token: 0x04000950 RID: 2384
		private static CancellationTokenRegistration _nextFrameAndEndOfFrameWiredUpCTRegistration = default(CancellationTokenRegistration);

		// Token: 0x04000951 RID: 2385
		private static readonly Awaitable.DoubleBufferedAwaitableList _nextFrameAwaitables = new Awaitable.DoubleBufferedAwaitableList();

		// Token: 0x04000952 RID: 2386
		private static readonly Awaitable.DoubleBufferedAwaitableList _endOfFrameAwaitables = new Awaitable.DoubleBufferedAwaitableList();

		// Token: 0x04000953 RID: 2387
		private static SynchronizationContext _synchronizationContext;

		// Token: 0x04000954 RID: 2388
		private static int _mainThreadId;

		// Token: 0x02000268 RID: 616
		internal enum AwaiterCompletionThreadAffinity
		{
			// Token: 0x04000956 RID: 2390
			None,
			// Token: 0x04000957 RID: 2391
			MainThread,
			// Token: 0x04000958 RID: 2392
			BackgroundThread
		}

		// Token: 0x02000269 RID: 617
		[ExcludeFromDocs]
		public struct AwaitableAsyncMethodBuilder
		{
			// Token: 0x06001FA1 RID: 8097 RVA: 0x0003D048 File Offset: 0x0003B248
			private Awaitable.AwaitableAsyncMethodBuilder.IStateMachineBox EnsureStateMachineBox<TStateMachine>() where TStateMachine : IAsyncStateMachine
			{
				bool flag = this._stateMachineBox != null;
				Awaitable.AwaitableAsyncMethodBuilder.IStateMachineBox stateMachineBox;
				if (flag)
				{
					stateMachineBox = this._stateMachineBox;
				}
				else
				{
					this._stateMachineBox = Awaitable.AwaitableAsyncMethodBuilder.StateMachineBox<TStateMachine>.GetOne();
					this._stateMachineBox.ResultingCoroutine = this._resultingCoroutine;
					stateMachineBox = this._stateMachineBox;
				}
				return stateMachineBox;
			}

			// Token: 0x06001FA2 RID: 8098 RVA: 0x0003D094 File Offset: 0x0003B294
			public static Awaitable.AwaitableAsyncMethodBuilder Create()
			{
				return default(Awaitable.AwaitableAsyncMethodBuilder);
			}

			// Token: 0x17000509 RID: 1289
			// (get) Token: 0x06001FA3 RID: 8099 RVA: 0x0003D0AC File Offset: 0x0003B2AC
			public Awaitable Task
			{
				get
				{
					bool flag = this._resultingCoroutine != null;
					Awaitable awaitable;
					if (flag)
					{
						awaitable = this._resultingCoroutine;
					}
					else
					{
						bool flag2 = this._stateMachineBox != null;
						if (flag2)
						{
							Awaitable.AwaitableAsyncMethodBuilder.IStateMachineBox stateMachineBox = this._stateMachineBox;
							Awaitable awaitable2;
							if ((awaitable2 = stateMachineBox.ResultingCoroutine) == null)
							{
								awaitable2 = (stateMachineBox.ResultingCoroutine = Awaitable.NewManagedAwaitable());
							}
							awaitable = (this._resultingCoroutine = awaitable2);
						}
						else
						{
							awaitable = (this._resultingCoroutine = Awaitable.NewManagedAwaitable());
						}
					}
					return awaitable;
				}
			}

			// Token: 0x06001FA4 RID: 8100 RVA: 0x0003D124 File Offset: 0x0003B324
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
			{
				Awaitable.AwaitableAsyncMethodBuilder.IStateMachineBox stateMachineBox = this.EnsureStateMachineBox<TStateMachine>();
				Awaitable.AwaitableAsyncMethodBuilder.StateMachineBox<TStateMachine> stateMachineBox2 = (Awaitable.AwaitableAsyncMethodBuilder.StateMachineBox<TStateMachine>)stateMachineBox;
				this.Task.CompletionThreadAffinity = ((Thread.CurrentThread.ManagedThreadId == Awaitable._mainThreadId) ? Awaitable.AwaiterCompletionThreadAffinity.MainThread : Awaitable.AwaiterCompletionThreadAffinity.BackgroundThread);
				stateMachineBox2.StateMachine = stateMachine;
				stateMachineBox2.MoveNext();
			}

			// Token: 0x06001FA5 RID: 8101 RVA: 0x00002925 File Offset: 0x00000B25
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x06001FA6 RID: 8102 RVA: 0x0003D17C File Offset: 0x0003B37C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
			{
				Awaitable.AwaitableAsyncMethodBuilder.IStateMachineBox stateMachineBox = this.EnsureStateMachineBox<TStateMachine>();
				Awaitable.AwaitableAsyncMethodBuilder.StateMachineBox<TStateMachine> stateMachineBox2 = (Awaitable.AwaitableAsyncMethodBuilder.StateMachineBox<TStateMachine>)stateMachineBox;
				stateMachineBox2.StateMachine = stateMachine;
				awaiter.OnCompleted(stateMachineBox.MoveNext);
			}

			// Token: 0x06001FA7 RID: 8103 RVA: 0x0003D1B8 File Offset: 0x0003B3B8
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
			{
				Awaitable.AwaitableAsyncMethodBuilder.IStateMachineBox stateMachineBox = this.EnsureStateMachineBox<TStateMachine>();
				Awaitable.AwaitableAsyncMethodBuilder.StateMachineBox<TStateMachine> stateMachineBox2 = (Awaitable.AwaitableAsyncMethodBuilder.StateMachineBox<TStateMachine>)stateMachineBox;
				stateMachineBox2.StateMachine = stateMachine;
				awaiter.UnsafeOnCompleted(stateMachineBox.MoveNext);
			}

			// Token: 0x06001FA8 RID: 8104 RVA: 0x0003D1F4 File Offset: 0x0003B3F4
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void SetException(Exception e)
			{
				this.Task.RaiseManagedCompletion(e);
				this._stateMachineBox.Dispose();
				this._stateMachineBox = null;
			}

			// Token: 0x06001FA9 RID: 8105 RVA: 0x0003D217 File Offset: 0x0003B417
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void SetResult()
			{
				this.Task.RaiseManagedCompletion();
				this._stateMachineBox.Dispose();
				this._stateMachineBox = null;
			}

			// Token: 0x04000959 RID: 2393
			private Awaitable.AwaitableAsyncMethodBuilder.IStateMachineBox _stateMachineBox;

			// Token: 0x0400095A RID: 2394
			private Awaitable _resultingCoroutine;

			// Token: 0x0200026A RID: 618
			private interface IStateMachineBox : IDisposable
			{
				// Token: 0x1700050A RID: 1290
				// (get) Token: 0x06001FAA RID: 8106
				// (set) Token: 0x06001FAB RID: 8107
				Awaitable ResultingCoroutine { get; set; }

				// Token: 0x1700050B RID: 1291
				// (get) Token: 0x06001FAC RID: 8108
				Action MoveNext { get; }
			}

			// Token: 0x0200026B RID: 619
			private class StateMachineBox<TStateMachine> : Awaitable.AwaitableAsyncMethodBuilder.IStateMachineBox, IDisposable where TStateMachine : IAsyncStateMachine
			{
				// Token: 0x06001FAD RID: 8109 RVA: 0x0003D239 File Offset: 0x0003B439
				public static Awaitable.AwaitableAsyncMethodBuilder.StateMachineBox<TStateMachine> GetOne()
				{
					return Awaitable.AwaitableAsyncMethodBuilder.StateMachineBox<TStateMachine>._pool.Value.Get();
				}

				// Token: 0x06001FAE RID: 8110 RVA: 0x0003D24C File Offset: 0x0003B44C
				public void Dispose()
				{
					this.StateMachine = default(TStateMachine);
					this.ResultingCoroutine = null;
					Awaitable.AwaitableAsyncMethodBuilder.StateMachineBox<TStateMachine>._pool.Value.Release(this);
				}

				// Token: 0x1700050C RID: 1292
				// (get) Token: 0x06001FAF RID: 8111 RVA: 0x0003D283 File Offset: 0x0003B483
				// (set) Token: 0x06001FB0 RID: 8112 RVA: 0x0003D28B File Offset: 0x0003B48B
				public TStateMachine StateMachine { get; set; }

				// Token: 0x06001FB1 RID: 8113 RVA: 0x0003D294 File Offset: 0x0003B494
				private void DoMoveNext()
				{
					TStateMachine stateMachine = this.StateMachine;
					stateMachine.MoveNext();
				}

				// Token: 0x1700050D RID: 1293
				// (get) Token: 0x06001FB2 RID: 8114 RVA: 0x0003D2B7 File Offset: 0x0003B4B7
				public Action MoveNext { get; }

				// Token: 0x06001FB3 RID: 8115 RVA: 0x0003D2BF File Offset: 0x0003B4BF
				public StateMachineBox()
				{
					this.MoveNext = new Action(this.DoMoveNext);
				}

				// Token: 0x1700050E RID: 1294
				// (get) Token: 0x06001FB4 RID: 8116 RVA: 0x0003D2DB File Offset: 0x0003B4DB
				// (set) Token: 0x06001FB5 RID: 8117 RVA: 0x0003D2E3 File Offset: 0x0003B4E3
				public Awaitable ResultingCoroutine { get; set; }

				// Token: 0x0400095B RID: 2395
				private static readonly ThreadLocal<ObjectPool<Awaitable.AwaitableAsyncMethodBuilder.StateMachineBox<TStateMachine>>> _pool = new ThreadLocal<ObjectPool<Awaitable.AwaitableAsyncMethodBuilder.StateMachineBox<TStateMachine>>>(() => new ObjectPool<Awaitable.AwaitableAsyncMethodBuilder.StateMachineBox<TStateMachine>>(() => new Awaitable.AwaitableAsyncMethodBuilder.StateMachineBox<TStateMachine>(), null, null, null, false, 10, 10000));
			}
		}

		// Token: 0x0200026D RID: 621
		[ExcludeFromDocs]
		public struct AwaitableAsyncMethodBuilder<T>
		{
			// Token: 0x06001FBB RID: 8123 RVA: 0x0003D34C File Offset: 0x0003B54C
			private Awaitable.AwaitableAsyncMethodBuilder<T>.IStateMachineBox EnsureStateMachineBox<TStateMachine>() where TStateMachine : IAsyncStateMachine
			{
				bool flag = this._stateMachineBox != null;
				Awaitable.AwaitableAsyncMethodBuilder<T>.IStateMachineBox stateMachineBox;
				if (flag)
				{
					stateMachineBox = this._stateMachineBox;
				}
				else
				{
					this._stateMachineBox = Awaitable.AwaitableAsyncMethodBuilder<T>.StateMachineBox<TStateMachine>.GetOne();
					this._stateMachineBox.ResultingCoroutine = this._resultingCoroutine;
					stateMachineBox = this._stateMachineBox;
				}
				return stateMachineBox;
			}

			// Token: 0x06001FBC RID: 8124 RVA: 0x0003D398 File Offset: 0x0003B598
			public static Awaitable.AwaitableAsyncMethodBuilder<T> Create()
			{
				return default(Awaitable.AwaitableAsyncMethodBuilder<T>);
			}

			// Token: 0x1700050F RID: 1295
			// (get) Token: 0x06001FBD RID: 8125 RVA: 0x0003D3B0 File Offset: 0x0003B5B0
			public Awaitable<T> Task
			{
				get
				{
					bool flag = this._resultingCoroutine != null;
					Awaitable<T> awaitable;
					if (flag)
					{
						awaitable = this._resultingCoroutine;
					}
					else
					{
						bool flag2 = this._stateMachineBox != null;
						if (flag2)
						{
							Awaitable.AwaitableAsyncMethodBuilder<T>.IStateMachineBox stateMachineBox = this._stateMachineBox;
							Awaitable<T> awaitable2;
							if ((awaitable2 = stateMachineBox.ResultingCoroutine) == null)
							{
								awaitable2 = (stateMachineBox.ResultingCoroutine = Awaitable<T>.GetManaged());
							}
							awaitable = (this._resultingCoroutine = awaitable2);
						}
						else
						{
							awaitable = (this._resultingCoroutine = Awaitable<T>.GetManaged());
						}
					}
					return awaitable;
				}
			}

			// Token: 0x06001FBE RID: 8126 RVA: 0x0003D427 File Offset: 0x0003B627
			public void SetResult(T value)
			{
				this.Task.SetResultAndRaiseContinuation(value);
				this._stateMachineBox.Dispose();
				this._stateMachineBox = null;
			}

			// Token: 0x06001FBF RID: 8127 RVA: 0x0003D44A File Offset: 0x0003B64A
			public void SetException(Exception e)
			{
				this.Task.SetExceptionAndRaiseContinuation(e);
				this._stateMachineBox.Dispose();
				this._stateMachineBox = null;
			}

			// Token: 0x06001FC0 RID: 8128 RVA: 0x0003D470 File Offset: 0x0003B670
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
			{
				Awaitable.AwaitableAsyncMethodBuilder<T>.IStateMachineBox stateMachineBox = this.EnsureStateMachineBox<TStateMachine>();
				Awaitable.AwaitableAsyncMethodBuilder<T>.StateMachineBox<TStateMachine> stateMachineBox2 = (Awaitable.AwaitableAsyncMethodBuilder<T>.StateMachineBox<TStateMachine>)stateMachineBox;
				this.Task.CompletionThreadAffinity = ((Thread.CurrentThread.ManagedThreadId == Awaitable._mainThreadId) ? Awaitable.AwaiterCompletionThreadAffinity.MainThread : Awaitable.AwaiterCompletionThreadAffinity.BackgroundThread);
				stateMachineBox2.StateMachine = stateMachine;
				stateMachineBox2.MoveNext();
			}

			// Token: 0x06001FC1 RID: 8129 RVA: 0x00002925 File Offset: 0x00000B25
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x06001FC2 RID: 8130 RVA: 0x0003D4C8 File Offset: 0x0003B6C8
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
			{
				Awaitable.AwaitableAsyncMethodBuilder<T>.IStateMachineBox stateMachineBox = this.EnsureStateMachineBox<TStateMachine>();
				Awaitable.AwaitableAsyncMethodBuilder<T>.StateMachineBox<TStateMachine> stateMachineBox2 = (Awaitable.AwaitableAsyncMethodBuilder<T>.StateMachineBox<TStateMachine>)stateMachineBox;
				stateMachineBox2.StateMachine = stateMachine;
				awaiter.OnCompleted(stateMachineBox.MoveNext);
			}

			// Token: 0x06001FC3 RID: 8131 RVA: 0x0003D504 File Offset: 0x0003B704
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
			{
				Awaitable.AwaitableAsyncMethodBuilder<T>.IStateMachineBox stateMachineBox = this.EnsureStateMachineBox<TStateMachine>();
				((Awaitable.AwaitableAsyncMethodBuilder<T>.StateMachineBox<TStateMachine>)stateMachineBox).StateMachine = stateMachine;
				awaiter.UnsafeOnCompleted(stateMachineBox.MoveNext);
			}

			// Token: 0x04000961 RID: 2401
			private Awaitable.AwaitableAsyncMethodBuilder<T>.IStateMachineBox _stateMachineBox;

			// Token: 0x04000962 RID: 2402
			private Awaitable<T> _resultingCoroutine;

			// Token: 0x0200026E RID: 622
			private interface IStateMachineBox : IDisposable
			{
				// Token: 0x17000510 RID: 1296
				// (get) Token: 0x06001FC4 RID: 8132
				// (set) Token: 0x06001FC5 RID: 8133
				Awaitable<T> ResultingCoroutine { get; set; }

				// Token: 0x17000511 RID: 1297
				// (get) Token: 0x06001FC6 RID: 8134
				Action MoveNext { get; }
			}

			// Token: 0x0200026F RID: 623
			private class StateMachineBox<TStateMachine> : Awaitable.AwaitableAsyncMethodBuilder<T>.IStateMachineBox, IDisposable where TStateMachine : IAsyncStateMachine
			{
				// Token: 0x06001FC7 RID: 8135 RVA: 0x0003D540 File Offset: 0x0003B740
				public static Awaitable.AwaitableAsyncMethodBuilder<T>.StateMachineBox<TStateMachine> GetOne()
				{
					return Awaitable.AwaitableAsyncMethodBuilder<T>.StateMachineBox<TStateMachine>._pool.Value.Get();
				}

				// Token: 0x06001FC8 RID: 8136 RVA: 0x0003D564 File Offset: 0x0003B764
				public void Dispose()
				{
					this.StateMachine = default(TStateMachine);
					this.ResultingCoroutine = null;
					Awaitable.AwaitableAsyncMethodBuilder<T>.StateMachineBox<TStateMachine>._pool.Value.Release(this);
				}

				// Token: 0x17000512 RID: 1298
				// (get) Token: 0x06001FC9 RID: 8137 RVA: 0x0003D59B File Offset: 0x0003B79B
				// (set) Token: 0x06001FCA RID: 8138 RVA: 0x0003D5A3 File Offset: 0x0003B7A3
				public TStateMachine StateMachine { get; set; }

				// Token: 0x17000513 RID: 1299
				// (get) Token: 0x06001FCB RID: 8139 RVA: 0x0003D5AC File Offset: 0x0003B7AC
				public Action MoveNext { get; }

				// Token: 0x06001FCC RID: 8140 RVA: 0x0003D5B4 File Offset: 0x0003B7B4
				private void DoMoveNext()
				{
					TStateMachine stateMachine = this.StateMachine;
					stateMachine.MoveNext();
				}

				// Token: 0x06001FCD RID: 8141 RVA: 0x0003D5D7 File Offset: 0x0003B7D7
				public StateMachineBox()
				{
					this.MoveNext = new Action(this.DoMoveNext);
				}

				// Token: 0x17000514 RID: 1300
				// (get) Token: 0x06001FCE RID: 8142 RVA: 0x0003D5F3 File Offset: 0x0003B7F3
				// (set) Token: 0x06001FCF RID: 8143 RVA: 0x0003D5FB File Offset: 0x0003B7FB
				public Awaitable<T> ResultingCoroutine { get; set; }

				// Token: 0x04000963 RID: 2403
				private static readonly ThreadLocal<ObjectPool<Awaitable.AwaitableAsyncMethodBuilder<T>.StateMachineBox<TStateMachine>>> _pool = new ThreadLocal<ObjectPool<Awaitable.AwaitableAsyncMethodBuilder<T>.StateMachineBox<TStateMachine>>>(() => new ObjectPool<Awaitable.AwaitableAsyncMethodBuilder<T>.StateMachineBox<TStateMachine>>(() => new Awaitable.AwaitableAsyncMethodBuilder<T>.StateMachineBox<TStateMachine>(), null, null, null, false, 10, 10000));
			}
		}

		// Token: 0x02000271 RID: 625
		[ExcludeFromDocs]
		public struct Awaiter : INotifyCompletion
		{
			// Token: 0x06001FD5 RID: 8149 RVA: 0x0003D664 File Offset: 0x0003B864
			internal Awaiter(Awaitable awaited)
			{
				this._awaited = awaited;
			}

			// Token: 0x06001FD6 RID: 8150 RVA: 0x0003D66D File Offset: 0x0003B86D
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void OnCompleted(Action continuation)
			{
				this._awaited.SetContinuation(continuation);
			}

			// Token: 0x17000515 RID: 1301
			// (get) Token: 0x06001FD7 RID: 8151 RVA: 0x0003D67D File Offset: 0x0003B87D
			public bool IsCompleted
			{
				get
				{
					return this._awaited.IsCompleted;
				}
			}

			// Token: 0x06001FD8 RID: 8152 RVA: 0x0003D68A File Offset: 0x0003B88A
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void GetResult()
			{
				this._awaited.PropagateExceptionAndRelease();
			}

			// Token: 0x04000969 RID: 2409
			private readonly Awaitable _awaited;
		}

		// Token: 0x02000272 RID: 626
		private readonly struct AwaitableHandle
		{
			// Token: 0x17000516 RID: 1302
			// (get) Token: 0x06001FD9 RID: 8153 RVA: 0x0003D698 File Offset: 0x0003B898
			public bool IsNull
			{
				get
				{
					return this._handle == IntPtr.Zero;
				}
			}

			// Token: 0x17000517 RID: 1303
			// (get) Token: 0x06001FDA RID: 8154 RVA: 0x0003D6AA File Offset: 0x0003B8AA
			public bool IsManaged
			{
				get
				{
					return this._handle == Awaitable.AwaitableHandle.ManagedHandle._handle;
				}
			}

			// Token: 0x06001FDB RID: 8155 RVA: 0x0003D6C1 File Offset: 0x0003B8C1
			public AwaitableHandle(IntPtr handle)
			{
				this._handle = handle;
			}

			// Token: 0x06001FDC RID: 8156 RVA: 0x0003D6CA File Offset: 0x0003B8CA
			public static implicit operator IntPtr(Awaitable.AwaitableHandle handle)
			{
				return handle._handle;
			}

			// Token: 0x06001FDD RID: 8157 RVA: 0x0003D6D2 File Offset: 0x0003B8D2
			public static implicit operator Awaitable.AwaitableHandle(IntPtr handle)
			{
				return new Awaitable.AwaitableHandle(handle);
			}

			// Token: 0x0400096A RID: 2410
			private readonly IntPtr _handle;

			// Token: 0x0400096B RID: 2411
			public static Awaitable.AwaitableHandle ManagedHandle = new Awaitable.AwaitableHandle(new IntPtr(-1));

			// Token: 0x0400096C RID: 2412
			public static Awaitable.AwaitableHandle NullHandle = new Awaitable.AwaitableHandle(IntPtr.Zero);
		}

		// Token: 0x02000273 RID: 627
		private struct AwaitableAndFrameIndex
		{
			// Token: 0x17000518 RID: 1304
			// (get) Token: 0x06001FDF RID: 8159 RVA: 0x0003D6FB File Offset: 0x0003B8FB
			public readonly Awaitable Awaitable { get; }

			// Token: 0x17000519 RID: 1305
			// (get) Token: 0x06001FE0 RID: 8160 RVA: 0x0003D703 File Offset: 0x0003B903
			public readonly int FrameIndex { get; }

			// Token: 0x06001FE1 RID: 8161 RVA: 0x0003D70B File Offset: 0x0003B90B
			public AwaitableAndFrameIndex(Awaitable awaitable, int frameIndex)
			{
				this.Awaitable = awaitable;
				this.FrameIndex = frameIndex;
			}
		}

		// Token: 0x02000274 RID: 628
		private class DoubleBufferedAwaitableList
		{
			// Token: 0x06001FE2 RID: 8162 RVA: 0x0003D71C File Offset: 0x0003B91C
			public void SwapAndComplete()
			{
				List<Awaitable.AwaitableAndFrameIndex> scratch = this._scratch;
				List<Awaitable.AwaitableAndFrameIndex> awaitables = this._awaitables;
				this._awaitables = scratch;
				this._scratch = awaitables;
				try
				{
					foreach (Awaitable.AwaitableAndFrameIndex awaitableAndFrameIndex in awaitables)
					{
						bool flag = !awaitableAndFrameIndex.Awaitable.IsDettachedOrCompleted;
						if (flag)
						{
							bool flag2 = Time.frameCount >= awaitableAndFrameIndex.FrameIndex || awaitableAndFrameIndex.FrameIndex == -1;
							if (flag2)
							{
								awaitableAndFrameIndex.Awaitable.RaiseManagedCompletion();
							}
							else
							{
								scratch.Add(awaitableAndFrameIndex);
							}
						}
					}
				}
				finally
				{
					awaitables.Clear();
				}
			}

			// Token: 0x06001FE3 RID: 8163 RVA: 0x0003D7F4 File Offset: 0x0003B9F4
			public void Add(Awaitable item, int frameIndex)
			{
				this._awaitables.Add(new Awaitable.AwaitableAndFrameIndex(item, frameIndex));
			}

			// Token: 0x06001FE4 RID: 8164 RVA: 0x0003D80C File Offset: 0x0003BA0C
			public void Remove(Awaitable item)
			{
				this._awaitables.RemoveAll((Awaitable.AwaitableAndFrameIndex x) => x.Awaitable == item);
			}

			// Token: 0x06001FE5 RID: 8165 RVA: 0x0003D83F File Offset: 0x0003BA3F
			public void Clear()
			{
				this._awaitables.Clear();
			}

			// Token: 0x0400096F RID: 2415
			private List<Awaitable.AwaitableAndFrameIndex> _awaitables = new List<Awaitable.AwaitableAndFrameIndex>();

			// Token: 0x04000970 RID: 2416
			private List<Awaitable.AwaitableAndFrameIndex> _scratch = new List<Awaitable.AwaitableAndFrameIndex>();
		}
	}
}
