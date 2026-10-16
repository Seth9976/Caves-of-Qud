using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x02000536 RID: 1334
	[Guid("A0BBBDFF-5AF5-42E3-9753-34441F764A6B")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ICustomRuntimeManager
	{
		// Token: 0x06003A36 RID: 14902
		[return: MarshalAs(UnmanagedType.Interface)]
		ICustomRuntimeRegistrationToken Register([MarshalAs(UnmanagedType.Interface)] [In] ICustomRuntime customRuntime);
	}
}
