using System;

namespace System.IO
{
	/// <summary>Provides data for the <see cref="E:System.IO.FileSystemWatcher.Error" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200050D RID: 1293
	public class ErrorEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.ErrorEventArgs" /> class.</summary>
		/// <param name="exception">An <see cref="T:System.Exception" /> that represents the error that occurred. </param>
		// Token: 0x060029F2 RID: 10738 RVA: 0x00090262 File Offset: 0x0008E462
		public ErrorEventArgs(Exception exception)
		{
			this.exception = exception;
		}

		/// <summary>Gets the <see cref="T:System.Exception" /> that represents the error that occurred.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that represents the error that occurred.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060029F3 RID: 10739 RVA: 0x00090271 File Offset: 0x0008E471
		public virtual Exception GetException()
		{
			return this.exception;
		}

		// Token: 0x04001659 RID: 5721
		private Exception exception;
	}
}
