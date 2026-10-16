using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000016 RID: 22
	public struct TMP_Vertex
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002AB8 File Offset: 0x00000CB8
		public static TMP_Vertex zero
		{
			get
			{
				return TMP_Vertex.k_Zero;
			}
		}

		// Token: 0x04000034 RID: 52
		public Vector3 position;

		// Token: 0x04000035 RID: 53
		public Vector4 uv;

		// Token: 0x04000036 RID: 54
		public Vector2 uv2;

		// Token: 0x04000037 RID: 55
		public Color32 color;

		// Token: 0x04000038 RID: 56
		private static readonly TMP_Vertex k_Zero;
	}
}
