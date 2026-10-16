using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000011 RID: 17
	[UsedByNativeCode]
	public struct UIVertex
	{
		// Token: 0x0400004C RID: 76
		public Vector3 position;

		// Token: 0x0400004D RID: 77
		public Vector3 normal;

		// Token: 0x0400004E RID: 78
		public Vector4 tangent;

		// Token: 0x0400004F RID: 79
		public Color32 color;

		// Token: 0x04000050 RID: 80
		public Vector4 uv0;

		// Token: 0x04000051 RID: 81
		public Vector4 uv1;

		// Token: 0x04000052 RID: 82
		public Vector4 uv2;

		// Token: 0x04000053 RID: 83
		public Vector4 uv3;

		// Token: 0x04000054 RID: 84
		private static readonly Color32 s_DefaultColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

		// Token: 0x04000055 RID: 85
		private static readonly Vector4 s_DefaultTangent = new Vector4(1f, 0f, 0f, -1f);

		// Token: 0x04000056 RID: 86
		public static UIVertex simpleVert = new UIVertex
		{
			position = Vector3.zero,
			normal = Vector3.back,
			tangent = UIVertex.s_DefaultTangent,
			color = UIVertex.s_DefaultColor,
			uv0 = Vector4.zero,
			uv1 = Vector4.zero,
			uv2 = Vector4.zero,
			uv3 = Vector4.zero
		};
	}
}
