using System;
using UnityEngine;

// Token: 0x0200010E RID: 270
[Serializable]
public class LTSpline
{
	// Token: 0x06000A9A RID: 2714 RVA: 0x00044C9C File Offset: 0x00042E9C
	public LTSpline(params Vector3[] pts)
	{
		this.pts = new Vector3[pts.Length];
		Array.Copy(pts, this.pts, pts.Length);
		this.numSections = pts.Length - 3;
		float num = float.PositiveInfinity;
		Vector3 vector = this.pts[1];
		float num2 = 0f;
		for (int i = 2; i < this.pts.Length - 2; i++)
		{
			float num3 = Vector3.Distance(this.pts[i], vector);
			if (num3 < num)
			{
				num = num3;
			}
			num2 += num3;
		}
		float num4 = num / (float)LTSpline.SUBLINE_COUNT;
		int num5 = (int)Mathf.Ceil(num2 / num4) * LTSpline.DISTANCE_COUNT;
		this.ptsAdj = new Vector3[num5];
		vector = this.interp(0f);
		int num6 = 0;
		for (int j = 0; j < num5; j++)
		{
			float num7 = ((float)j + 1f) / (float)num5;
			Vector3 vector2 = this.interp(num7);
			if (Vector3.Distance(vector2, vector) >= num4)
			{
				this.ptsAdj[num6] = vector2;
				vector = vector2;
				num6++;
			}
		}
		this.ptsAdjLength = num6;
	}

	// Token: 0x06000A9B RID: 2715 RVA: 0x00044DB8 File Offset: 0x00042FB8
	public Vector3 map(float u)
	{
		if (u >= 1f)
		{
			return this.pts[this.pts.Length - 2];
		}
		float num = u * (float)(this.ptsAdjLength - 1);
		int num2 = (int)Mathf.Floor(num);
		int num3 = (int)Mathf.Ceil(num);
		Vector3 vector = this.ptsAdj[num2];
		Vector3 vector2 = this.ptsAdj[num3];
		float num4 = num - (float)num2;
		return vector + (vector2 - vector) * num4;
	}

	// Token: 0x06000A9C RID: 2716 RVA: 0x00044E34 File Offset: 0x00043034
	public Vector3 interp(float t)
	{
		this.currPt = Mathf.Min(Mathf.FloorToInt(t * (float)this.numSections), this.numSections - 1);
		float num = t * (float)this.numSections - (float)this.currPt;
		Vector3 vector = this.pts[this.currPt];
		Vector3 vector2 = this.pts[this.currPt + 1];
		Vector3 vector3 = this.pts[this.currPt + 2];
		Vector3 vector4 = this.pts[this.currPt + 3];
		return 0.5f * ((-vector + 3f * vector2 - 3f * vector3 + vector4) * (num * num * num) + (2f * vector - 5f * vector2 + 4f * vector3 - vector4) * (num * num) + (-vector + vector3) * num + 2f * vector2);
	}

	// Token: 0x06000A9D RID: 2717 RVA: 0x00044F6C File Offset: 0x0004316C
	public Vector3 point(float ratio)
	{
		float num = ((ratio > 1f) ? 1f : ratio);
		return this.map(num);
	}

	// Token: 0x06000A9E RID: 2718 RVA: 0x00044F94 File Offset: 0x00043194
	public void place2d(Transform transform, float ratio)
	{
		transform.position = this.point(ratio);
		ratio += 0.001f;
		if (ratio <= 1f)
		{
			Vector3 vector = this.point(ratio) - transform.position;
			float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
			transform.eulerAngles = new Vector3(0f, 0f, num);
		}
	}

	// Token: 0x06000A9F RID: 2719 RVA: 0x00045000 File Offset: 0x00043200
	public void placeLocal2d(Transform transform, float ratio)
	{
		transform.localPosition = this.point(ratio);
		ratio += 0.001f;
		if (ratio <= 1f)
		{
			Vector3 vector = transform.parent.TransformPoint(this.point(ratio)) - transform.localPosition;
			float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
			transform.eulerAngles = new Vector3(0f, 0f, num);
		}
	}

	// Token: 0x06000AA0 RID: 2720 RVA: 0x00045077 File Offset: 0x00043277
	public void place(Transform transform, float ratio)
	{
		this.place(transform, ratio, Vector3.up);
	}

	// Token: 0x06000AA1 RID: 2721 RVA: 0x00045086 File Offset: 0x00043286
	public void place(Transform transform, float ratio, Vector3 worldUp)
	{
		transform.position = this.point(ratio);
		ratio += 0.001f;
		if (ratio <= 1f)
		{
			transform.LookAt(this.point(ratio), worldUp);
		}
	}

	// Token: 0x06000AA2 RID: 2722 RVA: 0x000450B4 File Offset: 0x000432B4
	public void placeLocal(Transform transform, float ratio)
	{
		this.placeLocal(transform, ratio, Vector3.up);
	}

	// Token: 0x06000AA3 RID: 2723 RVA: 0x000450C3 File Offset: 0x000432C3
	public void placeLocal(Transform transform, float ratio, Vector3 worldUp)
	{
		transform.localPosition = this.point(ratio);
		ratio += 0.001f;
		if (ratio <= 1f)
		{
			transform.LookAt(transform.parent.TransformPoint(this.point(ratio)), worldUp);
		}
	}

	// Token: 0x06000AA4 RID: 2724 RVA: 0x000450FC File Offset: 0x000432FC
	public void gizmoDraw(float t = -1f)
	{
		Vector3 vector = this.point(0f);
		for (int i = 1; i <= 120; i++)
		{
			float num = (float)i / 120f;
			Vector3 vector2 = this.point(num);
			Gizmos.DrawLine(vector2, vector);
			vector = vector2;
		}
	}

	// Token: 0x04000717 RID: 1815
	public static int DISTANCE_COUNT = 30;

	// Token: 0x04000718 RID: 1816
	public static int SUBLINE_COUNT = 50;

	// Token: 0x04000719 RID: 1817
	public Vector3[] pts;

	// Token: 0x0400071A RID: 1818
	public Vector3[] ptsAdj;

	// Token: 0x0400071B RID: 1819
	public int ptsAdjLength;

	// Token: 0x0400071C RID: 1820
	public bool orientToPath;

	// Token: 0x0400071D RID: 1821
	public bool orientToPath2d;

	// Token: 0x0400071E RID: 1822
	private int numSections;

	// Token: 0x0400071F RID: 1823
	private int currPt;

	// Token: 0x04000720 RID: 1824
	private float totalLength;
}
