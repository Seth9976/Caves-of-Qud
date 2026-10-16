using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.U2D
{
	// Token: 0x020005A2 RID: 1442
	[NativeHeader("Runtime/2D/Renderer/SpriteRendererGroup.h")]
	[RequiredByNativeCode]
	internal struct SpriteIntermediateRendererInfo
	{
		// Token: 0x040013C7 RID: 5063
		public int SpriteID;

		// Token: 0x040013C8 RID: 5064
		public int TextureID;

		// Token: 0x040013C9 RID: 5065
		public int MaterialID;

		// Token: 0x040013CA RID: 5066
		public Color Color;

		// Token: 0x040013CB RID: 5067
		public Matrix4x4 Transform;

		// Token: 0x040013CC RID: 5068
		public Bounds Bounds;

		// Token: 0x040013CD RID: 5069
		public int Layer;

		// Token: 0x040013CE RID: 5070
		public int SortingLayer;

		// Token: 0x040013CF RID: 5071
		public int SortingOrder;

		// Token: 0x040013D0 RID: 5072
		public ulong SceneCullingMask;

		// Token: 0x040013D1 RID: 5073
		public IntPtr IndexData;

		// Token: 0x040013D2 RID: 5074
		public IntPtr VertexData;

		// Token: 0x040013D3 RID: 5075
		public int IndexCount;

		// Token: 0x040013D4 RID: 5076
		public int VertexCount;

		// Token: 0x040013D5 RID: 5077
		public int ShaderChannelMask;
	}
}
