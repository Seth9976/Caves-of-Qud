using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering
{
	// Token: 0x02000545 RID: 1349
	[NativeHeader("Runtime/Graphics/ScriptableRenderLoop/RendererList.h")]
	[MovedFrom("UnityEngine.Rendering.RendererUtils")]
	public struct RendererList
	{
		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06003396 RID: 13206
		public extern bool isValid
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06003397 RID: 13207 RVA: 0x00064E1D File Offset: 0x0006301D
		internal RendererList(UIntPtr ctx, uint indx)
		{
			this.context = ctx;
			this.index = indx;
			this.frame = 0U;
			this.type = 0U;
			this.contextID = 0U;
		}

		// Token: 0x0400115C RID: 4444
		internal UIntPtr context;

		// Token: 0x0400115D RID: 4445
		internal uint index;

		// Token: 0x0400115E RID: 4446
		internal uint frame;

		// Token: 0x0400115F RID: 4447
		internal uint type;

		// Token: 0x04001160 RID: 4448
		internal uint contextID;

		// Token: 0x04001161 RID: 4449
		public static readonly RendererList nullRendererList = new RendererList(UIntPtr.Zero, uint.MaxValue);
	}
}
