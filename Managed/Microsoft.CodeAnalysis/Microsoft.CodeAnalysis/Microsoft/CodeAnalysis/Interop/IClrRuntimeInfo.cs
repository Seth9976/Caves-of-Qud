using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Microsoft.CodeAnalysis.Interop
{
	// Token: 0x020002CD RID: 717
	[InterfaceType(1)]
	[Guid("BD39D1D2-BA2F-486A-89B0-B4B0CB466891")]
	[SuppressUnmanagedCodeSecurity]
	[ComImport]
	internal interface IClrRuntimeInfo
	{
		// Token: 0x06001DF8 RID: 7672
		[PreserveSig]
		int GetVersionString([MarshalAs(21)] [Out] StringBuilder buffer, [MarshalAs(8)] [In] [Out] ref int bufferLength);

		// Token: 0x06001DF9 RID: 7673
		[PreserveSig]
		int GetRuntimeDirectory([MarshalAs(21)] [Out] StringBuilder buffer, [MarshalAs(8)] [In] [Out] ref int bufferLength);

		// Token: 0x06001DFA RID: 7674
		[return: MarshalAs(2)]
		bool IsLoaded([In] IntPtr processHandle);

		// Token: 0x06001DFB RID: 7675
		[PreserveSig]
		int LoadErrorString([MarshalAs(8)] [In] int resourceId, [MarshalAs(21)] [Out] StringBuilder buffer, [MarshalAs(8)] [In] [Out] ref int bufferLength);

		// Token: 0x06001DFC RID: 7676
		IntPtr LoadLibrary([MarshalAs(21)] [In] string dllName);

		// Token: 0x06001DFD RID: 7677
		IntPtr GetProcAddress([MarshalAs(20)] [In] string procName);

		// Token: 0x06001DFE RID: 7678
		[return: MarshalAs(28)]
		object GetInterface([MarshalAs(43)] [In] Guid coClassId, [MarshalAs(43)] [In] Guid interfaceId);

		// Token: 0x06001DFF RID: 7679
		[return: MarshalAs(2)]
		bool IsLoadable();

		// Token: 0x06001E00 RID: 7680
		void SetDefaultStartupFlags([MarshalAs(8)] [In] int startupFlags, [MarshalAs(20)] [In] string hostConfigFile);

		// Token: 0x06001E01 RID: 7681
		[PreserveSig]
		int GetDefaultStartupFlags([MarshalAs(8)] out int startupFlags, [MarshalAs(21)] [Out] StringBuilder hostConfigFile, [MarshalAs(8)] [In] [Out] ref int hostConfigFileLength);

		// Token: 0x06001E02 RID: 7682
		void BindAsLegacyV2Runtime();

		// Token: 0x06001E03 RID: 7683
		void IsStarted([MarshalAs(2)] out bool started, [MarshalAs(8)] out int startupFlags);
	}
}
