using System;
using System.Security.Permissions;

namespace Microsoft.Win32
{
	/// <summary>Provides data for the <see cref="E:Microsoft.Win32.SystemEvents.TimerElapsed" /> event.</summary>
	// Token: 0x0200012E RID: 302
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public class TimerElapsedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.TimerElapsedEventArgs" /> class.</summary>
		/// <param name="timerId">The ID number for the timer. </param>
		// Token: 0x06000709 RID: 1801 RVA: 0x0001382B File Offset: 0x00011A2B
		public TimerElapsedEventArgs(IntPtr timerId)
		{
			this.mytimerId = timerId;
		}

		/// <summary>Gets the ID number for the timer.</summary>
		/// <returns>The ID number for the timer.</returns>
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x0001383A File Offset: 0x00011A3A
		public IntPtr TimerId
		{
			get
			{
				return this.mytimerId;
			}
		}

		// Token: 0x04000507 RID: 1287
		private IntPtr mytimerId;
	}
}
