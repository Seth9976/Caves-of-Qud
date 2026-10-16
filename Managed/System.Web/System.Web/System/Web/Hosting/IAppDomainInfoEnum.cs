using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	/// <summary>Provides access to an application domain.</summary>
	// Token: 0x0200075C RID: 1884
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("F79648FB-558B-4a09-88F1-1E3BCB30E34F")]
	public interface IAppDomainInfoEnum
	{
		/// <summary>Retrieves the number of application domains.</summary>
		/// <returns>The number of application domains.</returns>
		// Token: 0x06004CED RID: 19693
		[return: MarshalAs(UnmanagedType.I4)]
		int Count();

		/// <summary>Gets an <see cref="T:System.Web.Hosting.IAppDomainInfo" /> interface.</summary>
		/// <returns>An <see cref="T:System.Web.Hosting.IAppDomainInfo" /> interface.</returns>
		// Token: 0x06004CEE RID: 19694
		[return: MarshalAs(UnmanagedType.Interface)]
		IAppDomainInfo GetData();

		/// <summary>Moves to the next <see cref="T:System.Web.Hosting.IAppDomainInfo" /> interface.</summary>
		/// <returns>true if a new interface is available; otherwise, false.</returns>
		// Token: 0x06004CEF RID: 19695
		[return: MarshalAs(UnmanagedType.Bool)]
		bool MoveNext();

		/// <summary>Initializes the <see cref="T:System.Web.Hosting.IAppDomainInfo" /> interface.</summary>
		// Token: 0x06004CF0 RID: 19696
		void Reset();
	}
}
