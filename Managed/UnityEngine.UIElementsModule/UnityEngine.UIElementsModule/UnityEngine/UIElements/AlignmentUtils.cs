using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000004 RID: 4
	public static class AlignmentUtils
	{
		// Token: 0x06000003 RID: 3 RVA: 0x0000205C File Offset: 0x0000025C
		internal static float RoundToPixelGrid(float v, float pixelsPerPoint, float offset = 0.02f)
		{
			return Mathf.Floor(v * pixelsPerPoint + 0.5f + offset) / pixelsPerPoint;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002080 File Offset: 0x00000280
		internal static float CeilToPixelGrid(float v, float pixelsPerPoint, float offset = -0.02f)
		{
			return Mathf.Ceil(v * pixelsPerPoint + offset) / pixelsPerPoint;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020A0 File Offset: 0x000002A0
		internal static float FloorToPixelGrid(float v, float pixelsPerPoint, float offset = 0.02f)
		{
			return Mathf.Floor(v * pixelsPerPoint + offset) / pixelsPerPoint;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020C0 File Offset: 0x000002C0
		public static float RoundToPanelPixelSize(this VisualElement ve, float v)
		{
			return AlignmentUtils.RoundToPixelGrid(v, ve.scaledPixelsPerPoint, 0.02f);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020E4 File Offset: 0x000002E4
		public static float CeilToPanelPixelSize(this VisualElement ve, float v)
		{
			return AlignmentUtils.CeilToPixelGrid(v, ve.scaledPixelsPerPoint, -0.02f);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		public static float FloorToPanelPixelSize(this VisualElement ve, float v)
		{
			return AlignmentUtils.FloorToPixelGrid(v, ve.scaledPixelsPerPoint, 0.02f);
		}
	}
}
