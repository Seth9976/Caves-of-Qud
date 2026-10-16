using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	/// <summary>Provides a method to retrieve an <see cref="T:System.Web.Hosting.IProcessHost" /> interface.</summary>
	// Token: 0x02000768 RID: 1896
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("02fd465d-5c5d-4b7e-95b6-82faa031b74a")]
	public interface IProcessHostFactoryHelper
	{
		/// <summary>Gets the process host.</summary>
		/// <returns>A process host object.</returns>
		/// <param name="functions">Functions that are declared by the <see cref="T:System.Web.Hosting.IProcessHostSupportFunctions" /> interface.</param>
		// Token: 0x06004D16 RID: 19734
		[return: MarshalAs(UnmanagedType.Interface)]
		object GetProcessHost(IProcessHostSupportFunctions functions);
	}
}
