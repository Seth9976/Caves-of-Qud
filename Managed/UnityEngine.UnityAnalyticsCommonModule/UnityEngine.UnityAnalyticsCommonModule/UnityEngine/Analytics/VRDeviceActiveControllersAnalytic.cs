using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.Analytics
{
	// Token: 0x0200000A RID: 10
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class VRDeviceActiveControllersAnalytic : VRDeviceAnalyticBase
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002144 File Offset: 0x00000344
		[RequiredByNativeCode]
		internal static VRDeviceActiveControllersAnalytic CreateVRDeviceActiveControllersAnalytic()
		{
			return new VRDeviceActiveControllersAnalytic();
		}

		// Token: 0x04000009 RID: 9
		public string[] vr_active_controllers;
	}
}
