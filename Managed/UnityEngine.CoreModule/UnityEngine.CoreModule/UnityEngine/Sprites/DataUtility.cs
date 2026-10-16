using System;

namespace UnityEngine.Sprites
{
	// Token: 0x020005A4 RID: 1444
	public sealed class DataUtility
	{
		// Token: 0x060037C9 RID: 14281 RVA: 0x0006D754 File Offset: 0x0006B954
		public static Vector4 GetInnerUV(Sprite sprite)
		{
			return sprite.GetInnerUVs();
		}

		// Token: 0x060037CA RID: 14282 RVA: 0x0006D76C File Offset: 0x0006B96C
		public static Vector4 GetOuterUV(Sprite sprite)
		{
			return sprite.GetOuterUVs();
		}

		// Token: 0x060037CB RID: 14283 RVA: 0x0006D784 File Offset: 0x0006B984
		public static Vector4 GetPadding(Sprite sprite)
		{
			return sprite.GetPadding();
		}

		// Token: 0x060037CC RID: 14284 RVA: 0x0006D79C File Offset: 0x0006B99C
		public static Vector2 GetMinSize(Sprite sprite)
		{
			Vector2 vector;
			vector.x = sprite.border.x + sprite.border.z;
			vector.y = sprite.border.y + sprite.border.w;
			return vector;
		}
	}
}
