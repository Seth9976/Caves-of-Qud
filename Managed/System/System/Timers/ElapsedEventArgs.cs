using System;
using Unity;

namespace System.Timers
{
	/// <summary>Provides data for the <see cref="E:System.Timers.Timer.Elapsed" /> event.</summary>
	// Token: 0x02000198 RID: 408
	public class ElapsedEventArgs : EventArgs
	{
		// Token: 0x06000AAB RID: 2731 RVA: 0x0002DBF8 File Offset: 0x0002BDF8
		internal ElapsedEventArgs(DateTime time)
		{
			this.time = time;
		}

		/// <summary>Gets the time the <see cref="E:System.Timers.Timer.Elapsed" /> event was raised.</summary>
		/// <returns>The time the <see cref="E:System.Timers.Timer.Elapsed" /> event was raised.</returns>
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x0002DC07 File Offset: 0x0002BE07
		public DateTime SignalTime
		{
			get
			{
				return this.time;
			}
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal ElapsedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000743 RID: 1859
		private DateTime time;
	}
}
