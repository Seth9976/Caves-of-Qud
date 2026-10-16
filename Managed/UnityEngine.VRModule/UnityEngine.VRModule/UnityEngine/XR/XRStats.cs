using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.XR
{
	// Token: 0x02000008 RID: 8
	[NativeConditional("ENABLE_VR")]
	public static class XRStats
	{
		// Token: 0x0600002D RID: 45
		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool TryGetGPUTimeLastFrame(out float gpuTimeLastFrame);

		// Token: 0x0600002E RID: 46
		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool TryGetDroppedFrameCount(out int droppedFrameCount);

		// Token: 0x0600002F RID: 47
		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool TryGetFramePresentCount(out int framePresentCount);
	}
}
