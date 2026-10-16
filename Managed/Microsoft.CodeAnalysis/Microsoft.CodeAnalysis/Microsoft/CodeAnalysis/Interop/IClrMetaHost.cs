using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Microsoft.CodeAnalysis.Interop
{
	// Token: 0x020002CC RID: 716
	[InterfaceType(1)]
	[Guid("D332DB9E-B9B3-4125-8207-A14884F53216")]
	[SuppressUnmanagedCodeSecurity]
	[ComImport]
	internal interface IClrMetaHost
	{
		// Token: 0x06001DF1 RID: 7665
		[return: MarshalAs(28)]
		object GetRuntime([MarshalAs(21)] [In] string version, [MarshalAs(43)] [In] Guid interfaceId);

		// Token: 0x06001DF2 RID: 7666
		[PreserveSig]
		int GetVersionFromFile([MarshalAs(21)] [In] string filePath, [MarshalAs(21)] [Out] StringBuilder buffer, [MarshalAs(8)] [In] [Out] ref int bufferLength);

		// Token: 0x06001DF3 RID: 7667
		[return: MarshalAs(28)]
		object EnumerateInstalledRuntimes();

		// Token: 0x06001DF4 RID: 7668
		[return: MarshalAs(28)]
		object EnumerateLoadedRuntimes([In] IntPtr processHandle);

		// Token: 0x06001DF5 RID: 7669
		[PreserveSig]
		int Reserved01([In] IntPtr reserved1);

		// Token: 0x06001DF6 RID: 7670
		[return: MarshalAs(28)]
		object QueryLegacyV2RuntimeBinding([MarshalAs(43)] [In] Guid interfaceId);

		// Token: 0x06001DF7 RID: 7671
		void ExitProcess([MarshalAs(8)] [In] int exitCode);
	}
}
