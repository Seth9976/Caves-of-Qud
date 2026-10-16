using System;

namespace System.Runtime.ExceptionServices
{
	/// <summary>Provides data for the notification event that is raised when a managed exception first occurs, before the common language runtime begins searching for event handlers.</summary>
	// Token: 0x020007D0 RID: 2000
	public class FirstChanceExceptionEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs" /> class with a specified exception.</summary>
		/// <param name="exception">The exception that was just thrown by managed code, and that will be examined by the <see cref="E:System.AppDomain.UnhandledException" /> event. </param>
		// Token: 0x060045AB RID: 17835 RVA: 0x000E50AB File Offset: 0x000E32AB
		public FirstChanceExceptionEventArgs(Exception exception)
		{
			this.Exception = exception;
		}

		/// <summary>The managed exception object that corresponds to the exception thrown in managed code.</summary>
		/// <returns>The newly thrown exception.</returns>
		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x060045AC RID: 17836 RVA: 0x000E50BA File Offset: 0x000E32BA
		public Exception Exception { get; }
	}
}
