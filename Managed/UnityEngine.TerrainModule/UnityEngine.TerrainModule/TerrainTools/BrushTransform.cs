using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.TerrainTools
{
	// Token: 0x0200003A RID: 58
	[MovedFrom("UnityEngine.Experimental.TerrainAPI")]
	public struct BrushTransform
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000286 RID: 646 RVA: 0x000072FD File Offset: 0x000054FD
		public readonly Vector2 brushOrigin { get; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00007305 File Offset: 0x00005505
		public readonly Vector2 brushU { get; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000730D File Offset: 0x0000550D
		public readonly Vector2 brushV { get; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00007315 File Offset: 0x00005515
		public readonly Vector2 targetOrigin { get; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000731D File Offset: 0x0000551D
		public readonly Vector2 targetX { get; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00007325 File Offset: 0x00005525
		public readonly Vector2 targetY { get; }

		// Token: 0x0600028C RID: 652 RVA: 0x00007330 File Offset: 0x00005530
		public BrushTransform(Vector2 brushOrigin, Vector2 brushU, Vector2 brushV)
		{
			float num = brushU.x * brushV.y - brushU.y * brushV.x;
			float num2 = (Mathf.Approximately(num, 0f) ? 1f : (1f / num));
			Vector2 vector = new Vector2(brushV.y, -brushU.y) * num2;
			Vector2 vector2 = new Vector2(-brushV.x, brushU.x) * num2;
			Vector2 vector3 = -brushOrigin.x * vector - brushOrigin.y * vector2;
			this.brushOrigin = brushOrigin;
			this.brushU = brushU;
			this.brushV = brushV;
			this.targetOrigin = vector3;
			this.targetX = vector;
			this.targetY = vector2;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x000073F4 File Offset: 0x000055F4
		public Rect GetBrushXYBounds()
		{
			Vector2 vector = this.brushOrigin + this.brushU;
			Vector2 vector2 = this.brushOrigin + this.brushV;
			Vector2 vector3 = this.brushOrigin + this.brushU + this.brushV;
			float num = Mathf.Min(Mathf.Min(this.brushOrigin.x, vector.x), Mathf.Min(vector2.x, vector3.x));
			float num2 = Mathf.Max(Mathf.Max(this.brushOrigin.x, vector.x), Mathf.Max(vector2.x, vector3.x));
			float num3 = Mathf.Min(Mathf.Min(this.brushOrigin.y, vector.y), Mathf.Min(vector2.y, vector3.y));
			float num4 = Mathf.Max(Mathf.Max(this.brushOrigin.y, vector.y), Mathf.Max(vector2.y, vector3.y));
			return Rect.MinMaxRect(num, num3, num2, num4);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000750C File Offset: 0x0000570C
		public static BrushTransform FromRect(Rect brushRect)
		{
			Vector2 min = brushRect.min;
			Vector2 vector = new Vector2(brushRect.width, 0f);
			Vector2 vector2 = new Vector2(0f, brushRect.height);
			return new BrushTransform(min, vector, vector2);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00007554 File Offset: 0x00005754
		public Vector2 ToBrushUV(Vector2 targetXY)
		{
			return targetXY.x * this.targetX + targetXY.y * this.targetY + this.targetOrigin;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00007598 File Offset: 0x00005798
		public Vector2 FromBrushUV(Vector2 brushUV)
		{
			return brushUV.x * this.brushU + brushUV.y * this.brushV + this.brushOrigin;
		}
	}
}
