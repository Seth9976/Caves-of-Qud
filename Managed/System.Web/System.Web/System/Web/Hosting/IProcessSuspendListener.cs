using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x0200053B RID: 1339
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("406E6C4C-1C5D-4357-9DFE-EF4BE00D654B")]
	[ComImport]
	internal interface IProcessSuspendListener
	{
		// Token: 0x06003A40 RID: 14912
		[return: MarshalAs(UnmanagedType.Interface)]
		IProcessResumeCallback Suspend();
	}
}
