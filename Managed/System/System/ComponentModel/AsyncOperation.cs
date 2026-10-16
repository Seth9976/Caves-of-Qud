using System;
using System.Threading;
using Unity;

namespace System.ComponentModel
{
	/// <summary>Tracks the lifetime of an asynchronous operation.</summary>
	// Token: 0x02000364 RID: 868
	public sealed class AsyncOperation
	{
		// Token: 0x06001CB5 RID: 7349 RVA: 0x000679AD File Offset: 0x00065BAD
		private AsyncOperation(object userSuppliedState, SynchronizationContext syncContext)
		{
			this._userSuppliedState = userSuppliedState;
			this._syncContext = syncContext;
			this._alreadyCompleted = false;
			this._syncContext.OperationStarted();
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x000679D8 File Offset: 0x00065BD8
		~AsyncOperation()
		{
			if (!this._alreadyCompleted && this._syncContext != null)
			{
				this._syncContext.OperationCompleted();
			}
		}

		/// <summary>Gets or sets an object used to uniquely identify an asynchronous operation.</summary>
		/// <returns>The state object passed to the asynchronous method invocation.</returns>
		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001CB7 RID: 7351 RVA: 0x00067A1C File Offset: 0x00065C1C
		public object UserSuppliedState
		{
			get
			{
				return this._userSuppliedState;
			}
		}

		/// <summary>Gets the <see cref="T:System.Threading.SynchronizationContext" /> object that was passed to the constructor.</summary>
		/// <returns>The <see cref="T:System.Threading.SynchronizationContext" /> object that was passed to the constructor.</returns>
		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001CB8 RID: 7352 RVA: 0x00067A24 File Offset: 0x00065C24
		public SynchronizationContext SynchronizationContext
		{
			get
			{
				return this._syncContext;
			}
		}

		/// <summary>Invokes a delegate on the thread or context appropriate for the application model.</summary>
		/// <param name="d">A <see cref="T:System.Threading.SendOrPostCallback" /> object that wraps the delegate to be called when the operation ends. </param>
		/// <param name="arg">An argument for the delegate contained in the <paramref name="d" /> parameter. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.ComponentModel.AsyncOperation.PostOperationCompleted(System.Threading.SendOrPostCallback,System.Object)" /> method has been called previously for this task. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		// Token: 0x06001CB9 RID: 7353 RVA: 0x00067A2C File Offset: 0x00065C2C
		public void Post(SendOrPostCallback d, object arg)
		{
			this.PostCore(d, arg, false);
		}

		/// <summary>Ends the lifetime of an asynchronous operation.</summary>
		/// <param name="d">A <see cref="T:System.Threading.SendOrPostCallback" /> object that wraps the delegate to be called when the operation ends. </param>
		/// <param name="arg">An argument for the delegate contained in the <paramref name="d" /> parameter. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.ComponentModel.AsyncOperation.OperationCompleted" /> has been called previously for this task. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		// Token: 0x06001CBA RID: 7354 RVA: 0x00067A37 File Offset: 0x00065C37
		public void PostOperationCompleted(SendOrPostCallback d, object arg)
		{
			this.PostCore(d, arg, true);
			this.OperationCompletedCore();
		}

		/// <summary>Ends the lifetime of an asynchronous operation.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.ComponentModel.AsyncOperation.OperationCompleted" /> has been called previously for this task. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001CBB RID: 7355 RVA: 0x00067A48 File Offset: 0x00065C48
		public void OperationCompleted()
		{
			this.VerifyNotCompleted();
			this._alreadyCompleted = true;
			this.OperationCompletedCore();
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x00067A5D File Offset: 0x00065C5D
		private void PostCore(SendOrPostCallback d, object arg, bool markCompleted)
		{
			this.VerifyNotCompleted();
			this.VerifyDelegateNotNull(d);
			if (markCompleted)
			{
				this._alreadyCompleted = true;
			}
			this._syncContext.Post(d, arg);
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x00067A84 File Offset: 0x00065C84
		private void OperationCompletedCore()
		{
			try
			{
				this._syncContext.OperationCompleted();
			}
			finally
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x00067AB8 File Offset: 0x00065CB8
		private void VerifyNotCompleted()
		{
			if (this._alreadyCompleted)
			{
				throw new InvalidOperationException("This operation has already had OperationCompleted called on it and further calls are illegal.");
			}
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x00067ACD File Offset: 0x00065CCD
		private void VerifyDelegateNotNull(SendOrPostCallback d)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d", "A non-null SendOrPostCallback must be supplied.");
			}
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x00067AE2 File Offset: 0x00065CE2
		internal static AsyncOperation CreateOperation(object userSuppliedState, SynchronizationContext syncContext)
		{
			return new AsyncOperation(userSuppliedState, syncContext);
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal AsyncOperation()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000EB3 RID: 3763
		private readonly SynchronizationContext _syncContext;

		// Token: 0x04000EB4 RID: 3764
		private readonly object _userSuppliedState;

		// Token: 0x04000EB5 RID: 3765
		private bool _alreadyCompleted;
	}
}
