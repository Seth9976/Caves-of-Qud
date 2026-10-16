using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.Analytics
{
	// Token: 0x02000006 RID: 6
	[ExcludeFromDocs]
	[RequiredByNativeCode(GenerateProxy = true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class VRDeviceAnalyticBase : AnalyticsEventBase
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000020DB File Offset: 0x000002DB
		public VRDeviceAnalyticBase()
			: base("deviceStatus", 1, SendEventOptions.kAppendNone, "")
		{
		}
	}
}
