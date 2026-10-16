using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020005A8 RID: 1448
	[Serializable]
	public sealed class VectorImage : ScriptableObject
	{
		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06002B52 RID: 11090 RVA: 0x0009C9EB File Offset: 0x0009ABEB
		public float width
		{
			get
			{
				return this.size.x;
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06002B53 RID: 11091 RVA: 0x0009C9F8 File Offset: 0x0009ABF8
		public float height
		{
			get
			{
				return this.size.y;
			}
		}

		// Token: 0x0400137F RID: 4991
		[SerializeField]
		internal int version = 0;

		// Token: 0x04001380 RID: 4992
		[SerializeField]
		internal Texture2D atlas = null;

		// Token: 0x04001381 RID: 4993
		[SerializeField]
		internal VectorImageVertex[] vertices = null;

		// Token: 0x04001382 RID: 4994
		[SerializeField]
		internal ushort[] indices = null;

		// Token: 0x04001383 RID: 4995
		[SerializeField]
		internal GradientSettings[] settings = null;

		// Token: 0x04001384 RID: 4996
		[SerializeField]
		internal Vector2 size = Vector2.zero;
	}
}
