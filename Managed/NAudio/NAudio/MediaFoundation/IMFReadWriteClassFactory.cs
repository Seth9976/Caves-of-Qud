using System;
using System.Runtime.InteropServices;

namespace NAudio.MediaFoundation
{
	// Token: 0x02000041 RID: 65
	[Guid("E7FE2E12-661C-40DA-92F9-4F002AB67627")]
	[InterfaceType(1)]
	[ComImport]
	public interface IMFReadWriteClassFactory
	{
		// Token: 0x06000122 RID: 290
		void CreateInstanceFromURL([MarshalAs(43)] [In] Guid clsid, [MarshalAs(21)] [In] string pwszURL, [MarshalAs(28)] [In] IMFAttributes pAttributes, [MarshalAs(43)] [In] Guid riid, [MarshalAs(28)] out object ppvObject);

		// Token: 0x06000123 RID: 291
		void CreateInstanceFromObject([MarshalAs(43)] [In] Guid clsid, [MarshalAs(25)] [In] object punkObject, [MarshalAs(28)] [In] IMFAttributes pAttributes, [MarshalAs(43)] [In] Guid riid, [MarshalAs(28)] out object ppvObject);
	}
}
