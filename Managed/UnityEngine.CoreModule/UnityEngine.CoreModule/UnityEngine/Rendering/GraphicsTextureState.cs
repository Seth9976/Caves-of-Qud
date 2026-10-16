using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020004CC RID: 1228
	[NativeType("Runtime/Export/Graphics/GraphicsTexture.bindings.h")]
	[UsedByNativeCode]
	public enum GraphicsTextureState
	{
		// Token: 0x04000EC5 RID: 3781
		Constructed,
		// Token: 0x04000EC6 RID: 3782
		Initializing,
		// Token: 0x04000EC7 RID: 3783
		InitializedOnRenderThread,
		// Token: 0x04000EC8 RID: 3784
		DestroyQueued,
		// Token: 0x04000EC9 RID: 3785
		Destroyed
	}
}
