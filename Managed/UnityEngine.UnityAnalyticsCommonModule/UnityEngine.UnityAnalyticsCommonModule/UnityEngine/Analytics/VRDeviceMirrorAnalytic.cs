using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.Analytics
{
	// Token: 0x02000008 RID: 8
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class VRDeviceMirrorAnalytic : VRDeviceAnalyticBase
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002114 File Offset: 0x00000314
		[RequiredByNativeCode]
		internal static VRDeviceMirrorAnalytic CreateVRDeviceMirrorAnalytic()
		{
			return new VRDeviceMirrorAnalytic();
		}

		// Token: 0x04000007 RID: 7
		public bool vr_device_mirror_mode;
	}
}
