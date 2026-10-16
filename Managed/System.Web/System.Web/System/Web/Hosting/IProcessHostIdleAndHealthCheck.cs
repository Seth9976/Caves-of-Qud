using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	/// <summary>Provides ways to check on the state of a process.</summary>
	// Token: 0x02000769 RID: 1897
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("9d98b251-453e-44f6-9cec-8b5aed970129")]
	public interface IProcessHostIdleAndHealthCheck
	{
		/// <summary>Gets the state of the application domain.</summary>
		/// <returns>true if the application domain is idle; otherwise, false.</returns>
		// Token: 0x06004D17 RID: 19735
		[return: MarshalAs(UnmanagedType.Bool)]
		bool IsIdle();

		/// <summary>Pings a process.</summary>
		/// <param name="callback">The callback to handle the ping response.</param>
		// Token: 0x06004D18 RID: 19736
		void Ping(IProcessPingCallback callback);
	}
}
