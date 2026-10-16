using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.Analytics
{
	// Token: 0x02000007 RID: 7
	[ExcludeFromDocs]
	[RequiredByNativeCode(GenerateProxy = true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class VRDeviceAnalyticAspect : VRDeviceAnalyticBase
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000020F4 File Offset: 0x000002F4
		[RequiredByNativeCode]
		internal static VRDeviceAnalyticAspect CreateVRDeviceAnalyticAspect()
		{
			return new VRDeviceAnalyticAspect();
		}

		// Token: 0x04000006 RID: 6
		public float vr_aspect_ratio;
	}
}
