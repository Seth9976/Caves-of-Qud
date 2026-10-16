using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for the MethodNameCompleted event.</summary>
	// Token: 0x02000422 RID: 1058
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class RunWorkerCompletedEventArgs : AsyncCompletedEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RunWorkerCompletedEventArgs" /> class.</summary>
		/// <param name="result">The result of an asynchronous operation.</param>
		/// <param name="error">Any error that occurred during the asynchronous operation.</param>
		/// <param name="cancelled">A value indicating whether the asynchronous operation was canceled.</param>
		// Token: 0x0600225D RID: 8797 RVA: 0x00077886 File Offset: 0x00075A86
		public RunWorkerCompletedEventArgs(object result, Exception error, bool cancelled)
			: base(error, cancelled, null)
		{
			this.result = result;
		}

		/// <summary>Gets a value that represents the result of an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the result of an asynchronous operation.</returns>
		/// <exception cref="T:System.Reflection.TargetInvocationException">
		///   <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Error" /> is not null. The <see cref="P:System.Exception.InnerException" /> property holds a reference to <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Error" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Cancelled" /> is true.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x0600225E RID: 8798 RVA: 0x00077898 File Offset: 0x00075A98
		public object Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.result;
			}
		}

		/// <summary>Gets a value that represents the user state.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the user state.</returns>
		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x0600225F RID: 8799 RVA: 0x000778A6 File Offset: 0x00075AA6
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public new object UserState
		{
			get
			{
				return base.UserState;
			}
		}

		// Token: 0x0400108E RID: 4238
		private object result;
	}
}
