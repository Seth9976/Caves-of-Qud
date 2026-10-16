using System;

namespace NAudio.Wave.Asio
{
	// Token: 0x0200014A RID: 330
	internal class ASIOException : Exception
	{
		// Token: 0x06000710 RID: 1808 RVA: 0x0001523F File Offset: 0x0001343F
		public ASIOException()
		{
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00015247 File Offset: 0x00013447
		public ASIOException(string message)
			: base(message)
		{
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00015250 File Offset: 0x00013450
		public ASIOException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x0001525A File Offset: 0x0001345A
		// (set) Token: 0x06000714 RID: 1812 RVA: 0x00015262 File Offset: 0x00013462
		public ASIOError Error
		{
			get
			{
				return this.error;
			}
			set
			{
				this.error = value;
				this.Data["ASIOError"] = this.error;
			}
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00015286 File Offset: 0x00013486
		public static string getErrorName(ASIOError error)
		{
			return Enum.GetName(typeof(ASIOError), error);
		}

		// Token: 0x04000750 RID: 1872
		private ASIOError error;
	}
}
