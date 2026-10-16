using System;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x02000580 RID: 1408
	public struct Cookie
	{
		// Token: 0x0600368B RID: 13963 RVA: 0x0006B2E8 File Offset: 0x000694E8
		public static Cookie Defaults()
		{
			Cookie cookie;
			cookie.instanceID = 0;
			cookie.scale = 1f;
			cookie.sizes = new Vector2(1f, 1f);
			return cookie;
		}

		// Token: 0x040012C6 RID: 4806
		public int instanceID;

		// Token: 0x040012C7 RID: 4807
		public float scale;

		// Token: 0x040012C8 RID: 4808
		public Vector2 sizes;
	}
}
