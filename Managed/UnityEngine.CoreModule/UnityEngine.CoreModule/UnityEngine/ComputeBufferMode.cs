using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200016B RID: 363
	[NativeType("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public enum ComputeBufferMode
	{
		// Token: 0x040004A6 RID: 1190
		Immutable,
		// Token: 0x040004A7 RID: 1191
		Dynamic,
		// Token: 0x040004A8 RID: 1192
		[Obsolete("ComputeBufferMode.Circular is deprecated (legacy mode)")]
		Circular,
		// Token: 0x040004A9 RID: 1193
		[Obsolete("ComputeBufferMode.StreamOut is deprecated (internal use only)")]
		StreamOut,
		// Token: 0x040004AA RID: 1194
		SubUpdates
	}
}
