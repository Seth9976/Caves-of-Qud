using System;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200055E RID: 1374
	internal class ContextAwareResult : LazyAsyncResult
	{
		// Token: 0x06002C8A RID: 11402 RVA: 0x00097AD6 File Offset: 0x00095CD6
		private void SafeCaptureIdentity()
		{
			this._windowsIdentity = WindowsIdentity.GetCurrent();
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06002C8B RID: 11403 RVA: 0x00097AE4 File Offset: 0x00095CE4
		internal WindowsIdentity Identity
		{
			get
			{
				if (base.InternalPeekCompleted)
				{
					if ((this._flags & ContextAwareResult.StateFlags.ThreadSafeContextCopy) == ContextAwareResult.StateFlags.None)
					{
						NetEventSource.Fail(this, "Called on completed result.", "Identity");
					}
					throw new InvalidOperationException("This operation cannot be performed on a completed asynchronous result object.");
				}
				if (this._windowsIdentity != null)
				{
					return this._windowsIdentity;
				}
				if ((this._flags & ContextAwareResult.StateFlags.CaptureIdentity) == ContextAwareResult.StateFlags.None)
				{
					NetEventSource.Fail(this, "No identity captured - specify captureIdentity.", "Identity");
				}
				if ((this._flags & ContextAwareResult.StateFlags.PostBlockFinished) == ContextAwareResult.StateFlags.None)
				{
					if (this._lock == null)
					{
						NetEventSource.Fail(this, "Must lock (StartPostingAsyncOp()) { ... FinishPostingAsyncOp(); } when calling Identity (unless it's only called after FinishPostingAsyncOp).", "Identity");
					}
					object @lock = this._lock;
					lock (@lock)
					{
					}
				}
				if (base.InternalPeekCompleted)
				{
					if ((this._flags & ContextAwareResult.StateFlags.ThreadSafeContextCopy) == ContextAwareResult.StateFlags.None)
					{
						NetEventSource.Fail(this, "Result became completed during call.", "Identity");
					}
					throw new InvalidOperationException("This operation cannot be performed on a completed asynchronous result object.");
				}
				return this._windowsIdentity;
			}
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x00097BCC File Offset: 0x00095DCC
		private void CleanupInternal()
		{
			if (this._windowsIdentity != null)
			{
				this._windowsIdentity.Dispose();
				this._windowsIdentity = null;
			}
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x00097BE8 File Offset: 0x00095DE8
		internal ContextAwareResult(object myObject, object myState, AsyncCallback myCallBack)
			: this(false, false, myObject, myState, myCallBack)
		{
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x00097BF5 File Offset: 0x00095DF5
		internal ContextAwareResult(bool captureIdentity, bool forceCaptureContext, object myObject, object myState, AsyncCallback myCallBack)
			: this(captureIdentity, forceCaptureContext, false, myObject, myState, myCallBack)
		{
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x00097C05 File Offset: 0x00095E05
		internal ContextAwareResult(bool captureIdentity, bool forceCaptureContext, bool threadSafeContextCopy, object myObject, object myState, AsyncCallback myCallBack)
			: base(myObject, myState, myCallBack)
		{
			if (forceCaptureContext)
			{
				this._flags = ContextAwareResult.StateFlags.CaptureContext;
			}
			if (captureIdentity)
			{
				this._flags |= ContextAwareResult.StateFlags.CaptureIdentity;
			}
			if (threadSafeContextCopy)
			{
				this._flags |= ContextAwareResult.StateFlags.ThreadSafeContextCopy;
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06002C90 RID: 11408 RVA: 0x00097C40 File Offset: 0x00095E40
		internal ExecutionContext ContextCopy
		{
			get
			{
				if (base.InternalPeekCompleted)
				{
					if ((this._flags & ContextAwareResult.StateFlags.ThreadSafeContextCopy) == ContextAwareResult.StateFlags.None)
					{
						NetEventSource.Fail(this, "Called on completed result.", "ContextCopy");
					}
					throw new InvalidOperationException("This operation cannot be performed on a completed asynchronous result object.");
				}
				ExecutionContext context = this._context;
				if (context != null)
				{
					return context;
				}
				if (base.AsyncCallback == null && (this._flags & ContextAwareResult.StateFlags.CaptureContext) == ContextAwareResult.StateFlags.None)
				{
					NetEventSource.Fail(this, "No context captured - specify a callback or forceCaptureContext.", "ContextCopy");
				}
				if ((this._flags & ContextAwareResult.StateFlags.PostBlockFinished) == ContextAwareResult.StateFlags.None)
				{
					if (this._lock == null)
					{
						NetEventSource.Fail(this, "Must lock (StartPostingAsyncOp()) { ... FinishPostingAsyncOp(); } when calling ContextCopy (unless it's only called after FinishPostingAsyncOp).", "ContextCopy");
					}
					object @lock = this._lock;
					lock (@lock)
					{
					}
				}
				if (base.InternalPeekCompleted)
				{
					if ((this._flags & ContextAwareResult.StateFlags.ThreadSafeContextCopy) == ContextAwareResult.StateFlags.None)
					{
						NetEventSource.Fail(this, "Result became completed during call.", "ContextCopy");
					}
					throw new InvalidOperationException("This operation cannot be performed on a completed asynchronous result object.");
				}
				return this._context;
			}
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x00097D30 File Offset: 0x00095F30
		internal object StartPostingAsyncOp()
		{
			return this.StartPostingAsyncOp(true);
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x00097D39 File Offset: 0x00095F39
		internal object StartPostingAsyncOp(bool lockCapture)
		{
			if (base.InternalPeekCompleted)
			{
				NetEventSource.Fail(this, "Called on completed result.", "StartPostingAsyncOp");
			}
			this._lock = (lockCapture ? new object() : null);
			this._flags |= ContextAwareResult.StateFlags.PostBlockStarted;
			return this._lock;
		}

		// Token: 0x06002C93 RID: 11411 RVA: 0x00097D78 File Offset: 0x00095F78
		internal bool FinishPostingAsyncOp()
		{
			if ((this._flags & (ContextAwareResult.StateFlags.PostBlockStarted | ContextAwareResult.StateFlags.PostBlockFinished)) != ContextAwareResult.StateFlags.PostBlockStarted)
			{
				return false;
			}
			this._flags |= ContextAwareResult.StateFlags.PostBlockFinished;
			ExecutionContext executionContext = null;
			return this.CaptureOrComplete(ref executionContext, false);
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x00097DB0 File Offset: 0x00095FB0
		internal bool FinishPostingAsyncOp(ref CallbackClosure closure)
		{
			if ((this._flags & (ContextAwareResult.StateFlags.PostBlockStarted | ContextAwareResult.StateFlags.PostBlockFinished)) != ContextAwareResult.StateFlags.PostBlockStarted)
			{
				return false;
			}
			this._flags |= ContextAwareResult.StateFlags.PostBlockFinished;
			CallbackClosure callbackClosure = closure;
			ExecutionContext executionContext;
			if (callbackClosure == null)
			{
				executionContext = null;
			}
			else if (!callbackClosure.IsCompatible(base.AsyncCallback))
			{
				closure = null;
				executionContext = null;
			}
			else
			{
				base.AsyncCallback = callbackClosure.AsyncCallback;
				executionContext = callbackClosure.Context;
			}
			bool flag = this.CaptureOrComplete(ref executionContext, true);
			if (closure == null && base.AsyncCallback != null && executionContext != null)
			{
				closure = new CallbackClosure(executionContext, base.AsyncCallback);
			}
			return flag;
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x00097E32 File Offset: 0x00096032
		protected override void Cleanup()
		{
			base.Cleanup();
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, null, "Cleanup");
			}
			this.CleanupInternal();
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x00097E54 File Offset: 0x00096054
		private bool CaptureOrComplete(ref ExecutionContext cachedContext, bool returnContext)
		{
			if ((this._flags & ContextAwareResult.StateFlags.PostBlockStarted) == ContextAwareResult.StateFlags.None)
			{
				NetEventSource.Fail(this, "Called without calling StartPostingAsyncOp.", "CaptureOrComplete");
			}
			bool flag = base.AsyncCallback != null || (this._flags & ContextAwareResult.StateFlags.CaptureContext) > ContextAwareResult.StateFlags.None;
			if ((this._flags & ContextAwareResult.StateFlags.CaptureIdentity) != ContextAwareResult.StateFlags.None && !base.InternalPeekCompleted && !flag)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, "starting identity capture", "CaptureOrComplete");
				}
				this.SafeCaptureIdentity();
			}
			if (flag && !base.InternalPeekCompleted)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, "starting capture", "CaptureOrComplete");
				}
				if (cachedContext == null)
				{
					cachedContext = ExecutionContext.Capture();
				}
				if (cachedContext != null)
				{
					if (!returnContext)
					{
						this._context = cachedContext;
						cachedContext = null;
					}
					else
					{
						this._context = cachedContext;
					}
				}
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("_context:{0}", new object[] { this._context }), "CaptureOrComplete");
				}
			}
			else
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, "Skipping capture", "CaptureOrComplete");
				}
				cachedContext = null;
				if (base.AsyncCallback != null && !base.CompletedSynchronously)
				{
					NetEventSource.Fail(this, "Didn't capture context, but didn't complete synchronously!", "CaptureOrComplete");
				}
			}
			if (base.CompletedSynchronously)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, "Completing synchronously", "CaptureOrComplete");
				}
				base.Complete(IntPtr.Zero);
				return true;
			}
			return false;
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x00097FA8 File Offset: 0x000961A8
		protected override void Complete(IntPtr userToken)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, FormattableStringFactory.Create("_context(set):{0} userToken:{1}", new object[]
				{
					this._context != null,
					userToken
				}), "Complete");
			}
			if ((this._flags & ContextAwareResult.StateFlags.PostBlockStarted) == ContextAwareResult.StateFlags.None)
			{
				base.Complete(userToken);
				return;
			}
			if (base.CompletedSynchronously)
			{
				return;
			}
			ExecutionContext context = this._context;
			if (userToken != IntPtr.Zero || context == null)
			{
				base.Complete(userToken);
				return;
			}
			ExecutionContext.Run(context, delegate(object s)
			{
				((ContextAwareResult)s).CompleteCallback();
			}, this);
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x00098055 File Offset: 0x00096255
		private void CompleteCallback()
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, "Context set, calling callback.", "CompleteCallback");
			}
			base.Complete(IntPtr.Zero);
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06002C99 RID: 11417 RVA: 0x00002F6A File Offset: 0x0000116A
		internal virtual EndPoint RemoteEndPoint
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400180C RID: 6156
		private WindowsIdentity _windowsIdentity;

		// Token: 0x0400180D RID: 6157
		private volatile ExecutionContext _context;

		// Token: 0x0400180E RID: 6158
		private object _lock;

		// Token: 0x0400180F RID: 6159
		private ContextAwareResult.StateFlags _flags;

		// Token: 0x0200055F RID: 1375
		[Flags]
		private enum StateFlags : byte
		{
			// Token: 0x04001811 RID: 6161
			None = 0,
			// Token: 0x04001812 RID: 6162
			CaptureIdentity = 1,
			// Token: 0x04001813 RID: 6163
			CaptureContext = 2,
			// Token: 0x04001814 RID: 6164
			ThreadSafeContextCopy = 4,
			// Token: 0x04001815 RID: 6165
			PostBlockStarted = 8,
			// Token: 0x04001816 RID: 6166
			PostBlockFinished = 16
		}
	}
}
