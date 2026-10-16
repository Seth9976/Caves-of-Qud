using System;

namespace System.Threading
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Application.ThreadException" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000183 RID: 387
	public class ThreadExceptionEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadExceptionEventArgs" /> class.</summary>
		/// <param name="t">The <see cref="T:System.Exception" /> that occurred. </param>
		// Token: 0x06000A5A RID: 2650 RVA: 0x0002D0E9 File Offset: 0x0002B2E9
		public ThreadExceptionEventArgs(Exception t)
		{
			this.exception = t;
		}

		/// <summary>Gets the <see cref="T:System.Exception" /> that occurred.</summary>
		/// <returns>The <see cref="T:System.Exception" /> that occurred.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0002D0F8 File Offset: 0x0002B2F8
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x040006FE RID: 1790
		private Exception exception;
	}
}
