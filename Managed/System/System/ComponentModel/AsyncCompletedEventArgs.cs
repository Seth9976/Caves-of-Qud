using System;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for the MethodNameCompleted event.</summary>
	// Token: 0x0200040B RID: 1035
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class AsyncCompletedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" /> class.</summary>
		// Token: 0x06002161 RID: 8545 RVA: 0x0000C759 File Offset: 0x0000A959
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		public AsyncCompletedEventArgs()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" /> class. </summary>
		/// <param name="error">Any error that occurred during the asynchronous operation.</param>
		/// <param name="cancelled">A value indicating whether the asynchronous operation was canceled.</param>
		/// <param name="userState">The optional user-supplied state object passed to the <see cref="M:System.ComponentModel.BackgroundWorker.RunWorkerAsync(System.Object)" /> method.</param>
		// Token: 0x06002162 RID: 8546 RVA: 0x00072205 File Offset: 0x00070405
		public AsyncCompletedEventArgs(Exception error, bool cancelled, object userState)
		{
			this.error = error;
			this.cancelled = cancelled;
			this.userState = userState;
		}

		/// <summary>Gets a value indicating whether an asynchronous operation has been canceled.</summary>
		/// <returns>true if the background operation has been canceled; otherwise false. The default is false.</returns>
		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06002163 RID: 8547 RVA: 0x00072222 File Offset: 0x00070422
		[SRDescription("True if operation was cancelled.")]
		public bool Cancelled
		{
			get
			{
				return this.cancelled;
			}
		}

		/// <summary>Gets a value indicating which error occurred during an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Exception" /> instance, if an error occurred during an asynchronous operation; otherwise null.</returns>
		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06002164 RID: 8548 RVA: 0x0007222A File Offset: 0x0007042A
		[SRDescription("Exception that occurred during operation.  Null if no error.")]
		public Exception Error
		{
			get
			{
				return this.error;
			}
		}

		/// <summary>Gets the unique identifier for the asynchronous task.</summary>
		/// <returns>An object reference that uniquely identifies the asynchronous task; otherwise, null if no value has been set.</returns>
		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06002165 RID: 8549 RVA: 0x00072232 File Offset: 0x00070432
		[SRDescription("User-supplied state to identify operation.")]
		public object UserState
		{
			get
			{
				return this.userState;
			}
		}

		/// <summary>Raises a user-supplied exception if an asynchronous operation failed.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Cancelled" /> property is true. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Error" /> property has been set by the asynchronous operation. The <see cref="P:System.Exception.InnerException" /> property holds a reference to <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Error" />. </exception>
		// Token: 0x06002166 RID: 8550 RVA: 0x0007223A File Offset: 0x0007043A
		protected void RaiseExceptionIfNecessary()
		{
			if (this.Error != null)
			{
				throw new TargetInvocationException(SR.GetString("An exception occurred during the operation, making the result invalid.  Check InnerException for exception details."), this.Error);
			}
			if (this.Cancelled)
			{
				throw new InvalidOperationException(SR.GetString("Operation has been cancelled."));
			}
		}

		// Token: 0x04001023 RID: 4131
		private readonly Exception error;

		// Token: 0x04001024 RID: 4132
		private readonly bool cancelled;

		// Token: 0x04001025 RID: 4133
		private readonly object userState;
	}
}
