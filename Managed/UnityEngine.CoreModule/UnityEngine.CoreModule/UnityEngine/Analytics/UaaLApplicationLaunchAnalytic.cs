using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Analytics
{
	// Token: 0x02000475 RID: 1141
	[RequiredByNativeCode(GenerateProxy = true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class UaaLApplicationLaunchAnalytic : AnalyticsEventBase
	{
		// Token: 0x06002CF6 RID: 11510 RVA: 0x0005512F File Offset: 0x0005332F
		public UaaLApplicationLaunchAnalytic()
			: base("UaaLApplicationLaunch", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x00055148 File Offset: 0x00053348
		[RequiredByNativeCode]
		public static UaaLApplicationLaunchAnalytic CreateUaaLApplicationLaunchAnalytic()
		{
			return new UaaLApplicationLaunchAnalytic();
		}

		// Token: 0x04000C6C RID: 3180
		public int launch_type;

		// Token: 0x04000C6D RID: 3181
		public int launch_process_type;
	}
}
