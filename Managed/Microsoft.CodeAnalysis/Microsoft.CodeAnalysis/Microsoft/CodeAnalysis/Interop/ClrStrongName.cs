using System;
using System.Runtime.InteropServices;

namespace Microsoft.CodeAnalysis.Interop
{
	// Token: 0x020002CB RID: 715
	internal static class ClrStrongName
	{
		// Token: 0x06001DEE RID: 7662
		[DllImport("mscoree.dll", EntryPoint = "CLRCreateInstance", PreserveSig = false)]
		[return: MarshalAs(28)]
		private static extern object nCreateInterface([MarshalAs(43)] Guid clsid, [MarshalAs(43)] Guid riid);

		// Token: 0x06001DEF RID: 7663 RVA: 0x00056B48 File Offset: 0x00054D48
		internal static IClrStrongName GetInstance()
		{
			Guid guid = new Guid(-1837098867, 3726, 18535, 179, 12, 127, 168, 56, 132, 232, 222);
			Guid guid2;
			guid2..ctor(-751641698, -17997, 16677, 130, 7, 161, 72, 132, 245, 50, 22);
			Guid guid3;
			guid3..ctor(-1214575923, -2611, 16539, 181, 165, 161, 98, 68, 97, 11, 146);
			Guid guid4;
			guid4..ctor(-1120284206, -17873, 18538, 137, 176, 180, 176, 203, 70, 104, 145);
			Guid guid5;
			guid5..ctor(-1613153073, 12928, 17297, 179, 169, 150, 225, 205, 231, 124, 141);
			return (IClrStrongName)((IClrRuntimeInfo)((IClrMetaHost)ClrStrongName.nCreateInterface(guid, guid2)).GetRuntime(ClrStrongName.GetRuntimeVersion(), guid4)).GetInterface(guid3, guid5);
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x00056C88 File Offset: 0x00054E88
		internal static string GetRuntimeVersion()
		{
			if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("COMPLUS_InstallRoot")))
			{
				string environmentVariable = Environment.GetEnvironmentVariable("COMPLUS_Version");
				if (!string.IsNullOrEmpty(environmentVariable))
				{
					return environmentVariable;
				}
			}
			return "v4.0.30319";
		}
	}
}
