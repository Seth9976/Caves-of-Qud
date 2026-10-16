using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Directs tracing or debugging output to either the standard output or the standard error stream.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000216 RID: 534
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class ConsoleTraceListener : TextWriterTraceListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ConsoleTraceListener" /> class with trace output written to the standard output stream.</summary>
		// Token: 0x06000F54 RID: 3924 RVA: 0x00044B24 File Offset: 0x00042D24
		public ConsoleTraceListener()
			: base(Console.Out)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ConsoleTraceListener" /> class with an option to write trace output to the standard output stream or the standard error stream.</summary>
		/// <param name="useErrorStream">true to write tracing and debugging output to the standard error stream; false to write tracing and debugging output to the standard output stream.</param>
		// Token: 0x06000F55 RID: 3925 RVA: 0x00044B31 File Offset: 0x00042D31
		public ConsoleTraceListener(bool useErrorStream)
			: base(useErrorStream ? Console.Error : Console.Out)
		{
		}

		/// <summary>Closes the output to the stream specified for this trace listener.</summary>
		// Token: 0x06000F56 RID: 3926 RVA: 0x00003917 File Offset: 0x00001B17
		public override void Close()
		{
		}
	}
}
