using System;
using System.Runtime.InteropServices;
using System.Security;

namespace NAudio.Dmo
{
	// Token: 0x0200008D RID: 141
	[InterfaceType(1)]
	[SuppressUnmanagedCodeSecurity]
	[Guid("6d6cbb60-a223-44aa-842f-a2f06750be6d")]
	[ComImport]
	internal interface IMediaParamInfo
	{
		// Token: 0x06000301 RID: 769
		[PreserveSig]
		int GetParamCount(out int paramCount);

		// Token: 0x06000302 RID: 770
		[PreserveSig]
		int GetParamInfo(int paramIndex, ref MediaParamInfo paramInfo);

		// Token: 0x06000303 RID: 771
		[PreserveSig]
		int GetParamText(int paramIndex, out IntPtr paramText);

		// Token: 0x06000304 RID: 772
		[PreserveSig]
		int GetNumTimeFormats(out int numTimeFormats);

		// Token: 0x06000305 RID: 773
		[PreserveSig]
		int GetSupportedTimeFormat(int formatIndex, out Guid guidTimeFormat);

		// Token: 0x06000306 RID: 774
		[PreserveSig]
		int GetCurrentTimeFormat(out Guid guidTimeFormat, out int mediaTimeData);
	}
}
