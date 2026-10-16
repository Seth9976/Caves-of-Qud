using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x02000537 RID: 1335
	[Guid("3A8E9CED-D3C9-4C4B-8956-6F15E2F559D9")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ICustomRuntimeRegistrationToken
	{
		// Token: 0x06003A37 RID: 14903
		void Unregister();
	}
}
