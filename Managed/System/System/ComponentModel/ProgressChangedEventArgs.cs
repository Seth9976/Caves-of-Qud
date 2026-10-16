using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" /> event.</summary>
	// Token: 0x0200041D RID: 1053
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class ProgressChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ProgressChangedEventArgs" /> class.</summary>
		/// <param name="progressPercentage">The percentage of an asynchronous task that has been completed.</param>
		/// <param name="userState">A unique user state.</param>
		// Token: 0x060021F6 RID: 8694 RVA: 0x00074396 File Offset: 0x00072596
		public ProgressChangedEventArgs(int progressPercentage, object userState)
		{
			this.progressPercentage = progressPercentage;
			this.userState = userState;
		}

		/// <summary>Gets the asynchronous task progress percentage.</summary>
		/// <returns>A percentage value indicating the asynchronous task progress.</returns>
		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x060021F7 RID: 8695 RVA: 0x000743AC File Offset: 0x000725AC
		[SRDescription("Percentage progress made in operation.")]
		public int ProgressPercentage
		{
			get
			{
				return this.progressPercentage;
			}
		}

		/// <summary>Gets a unique user state.</summary>
		/// <returns>A unique <see cref="T:System.Object" /> indicating the user state.</returns>
		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x060021F8 RID: 8696 RVA: 0x000743B4 File Offset: 0x000725B4
		[SRDescription("User-supplied state to identify operation.")]
		public object UserState
		{
			get
			{
				return this.userState;
			}
		}

		// Token: 0x04001059 RID: 4185
		private readonly int progressPercentage;

		// Token: 0x0400105A RID: 4186
		private readonly object userState;
	}
}
