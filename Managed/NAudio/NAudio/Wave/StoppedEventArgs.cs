using System;

namespace NAudio.Wave
{
	// Token: 0x020001AC RID: 428
	public class StoppedEventArgs : EventArgs
	{
		// Token: 0x060008FD RID: 2301 RVA: 0x00019EED File Offset: 0x000180ED
		public StoppedEventArgs(Exception exception = null)
		{
			this.exception = exception;
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x00019EFC File Offset: 0x000180FC
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x04000A7D RID: 2685
		private readonly Exception exception;
	}
}
