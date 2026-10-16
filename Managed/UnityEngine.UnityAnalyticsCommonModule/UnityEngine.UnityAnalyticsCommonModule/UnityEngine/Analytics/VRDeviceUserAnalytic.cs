using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.Analytics
{
	// Token: 0x02000009 RID: 9
	[ExcludeFromDocs]
	[RequiredByNativeCode(GenerateProxy = true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class VRDeviceUserAnalytic : VRDeviceAnalyticBase
	{
		// Token: 0x0600000D RID: 13 RVA: 0x0000212C File Offset: 0x0000032C
		[RequiredByNativeCode]
		internal static VRDeviceUserAnalytic CreateVRDeviceUserAnalytic()
		{
			return new VRDeviceUserAnalytic();
		}

		// Token: 0x04000008 RID: 8
		public int vr_user_presence;
	}
}
