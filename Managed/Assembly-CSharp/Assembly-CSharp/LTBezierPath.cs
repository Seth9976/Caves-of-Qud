using System;
using UnityEngine;

// Token: 0x0200010D RID: 269
public class LTBezierPath
{
	// Token: 0x06000A8F RID: 2703 RVA: 0x00044926 File Offset: 0x00042B26
	public LTBezierPath()
	{
	}

	// Token: 0x06000A90 RID: 2704 RVA: 0x0004492E File Offset: 0x00042B2E
	public LTBezierPath(Vector3[] pts_)
	{
		this.setPoints(pts_);
	}

	// Token: 0x06000A91 RID: 2705 RVA: 0x00044940 File Offset: 0x00042B40
	public void setPoints(Vector3[] pts_)
	{
		if (pts_.Length < 4)
		{
			LeanTween.logError("LeanTween - When passing values for a vector path, you must pass four or more values!");
		}
		if (pts_.Length % 4 != 0)
		{
			LeanTween.logError("LeanTween - When passing values for a vector path, they must be in sets of four: controlPoint1, controlPoint2, endPoint2, controlPoint2, controlPoint2...");
		}
		this.pts = pts_;
		int num = 0;
		this.beziers = new LTBezier[this.pts.Length / 4];
		this.lengthRatio = new float[this.beziers.Length];
		this.length = 0f;
		for (int i = 0; i < this.pts.Length; i += 4)
		{
			this.beziers[num] = new LTBezier(this.pts[i], this.pts[i + 2], this.pts[i + 1], this.pts[i + 3], 0.05f);
			this.length += this.beziers[num].length;
			num++;
		}
		for (int i = 0; i < this.beziers.Length; i++)
		{
			this.lengthRatio[i] = this.beziers[i].length / this.length;
		}
	}

	// Token: 0x06000A92 RID: 2706 RVA: 0x00044A54 File Offset: 0x00042C54
	public Vector3 point(float ratio)
	{
		float num = 0f;
		for (int i = 0; i < this.lengthRatio.Length; i++)
		{
			num += this.lengthRatio[i];
			if (num >= ratio)
			{
				return this.beziers[i].point((ratio - (num - this.lengthRatio[i])) / this.lengthRatio[i]);
			}
		}
		return this.beziers[this.lengthRatio.Length - 1].point(1f);
	}

	// Token: 0x06000A93 RID: 2707 RVA: 0x00044AC8 File Offset: 0x00042CC8
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

	// Token: 0x06000A94 RID: 2708 RVA: 0x00044B34 File Offset: 0x00042D34
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

	// Token: 0x06000A95 RID: 2709 RVA: 0x00044BAB File Offset: 0x00042DAB
	public void place(Transform transform, float ratio)
	{
		this.place(transform, ratio, Vector3.up);
	}

	// Token: 0x06000A96 RID: 2710 RVA: 0x00044BBA File Offset: 0x00042DBA
	public void place(Transform transform, float ratio, Vector3 worldUp)
	{
		transform.position = this.point(ratio);
		ratio += 0.001f;
		if (ratio <= 1f)
		{
			transform.LookAt(this.point(ratio), worldUp);
		}
	}

	// Token: 0x06000A97 RID: 2711 RVA: 0x00044BE8 File Offset: 0x00042DE8
	public void placeLocal(Transform transform, float ratio)
	{
		this.placeLocal(transform, ratio, Vector3.up);
	}

	// Token: 0x06000A98 RID: 2712 RVA: 0x00044BF7 File Offset: 0x00042DF7
	public void placeLocal(Transform transform, float ratio, Vector3 worldUp)
	{
		transform.localPosition = this.point(ratio);
		ratio += 0.001f;
		if (ratio <= 1f)
		{
			transform.LookAt(transform.parent.TransformPoint(this.point(ratio)), worldUp);
		}
	}

	// Token: 0x06000A99 RID: 2713 RVA: 0x00044C30 File Offset: 0x00042E30
	public void gizmoDraw(float t = -1f)
	{
		Vector3 vector = this.point(0f);
		for (int i = 1; i <= 120; i++)
		{
			float num = (float)i / 120f;
			Vector3 vector2 = this.point(num);
			Gizmos.color = ((this.previousBezier == this.currentBezier) ? Color.magenta : Color.grey);
			Gizmos.DrawLine(vector2, vector);
			vector = vector2;
			this.previousBezier = this.currentBezier;
		}
	}

	// Token: 0x0400070F RID: 1807
	public Vector3[] pts;

	// Token: 0x04000710 RID: 1808
	public float length;

	// Token: 0x04000711 RID: 1809
	public bool orientToPath;

	// Token: 0x04000712 RID: 1810
	public bool orientToPath2d;

	// Token: 0x04000713 RID: 1811
	private LTBezier[] beziers;

	// Token: 0x04000714 RID: 1812
	private float[] lengthRatio;

	// Token: 0x04000715 RID: 1813
	private int currentBezier;

	// Token: 0x04000716 RID: 1814
	private int previousBezier;
}
