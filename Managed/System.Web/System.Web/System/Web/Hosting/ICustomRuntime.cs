using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x02000535 RID: 1333
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("692D0723-C338-4D09-9057-C71F0F47DA87")]
	[ComImport]
	internal interface ICustomRuntime
	{
		// Token: 0x06003A33 RID: 14899
		void Start([In] IntPtr reserved0, [In] int reserved1);

		// Token: 0x06003A34 RID: 14900
		void ResolveModules([In] IntPtr pResolveModuleData, [In] int resolveModuleDataSize);

		// Token: 0x06003A35 RID: 14901
		void Stop([In] IntPtr reserved0, [In] int reserved1);
	}
}
