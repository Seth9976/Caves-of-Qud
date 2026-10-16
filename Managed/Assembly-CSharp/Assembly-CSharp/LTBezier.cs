using System;
using UnityEngine;

// Token: 0x0200010C RID: 268
public class LTBezier
{
	// Token: 0x06000A8B RID: 2699 RVA: 0x00044750 File Offset: 0x00042950
	public LTBezier(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float precision)
	{
		this.a = a;
		this.aa = -a + 3f * (b - c) + d;
		this.bb = 3f * (a + c) - 6f * b;
		this.cc = 3f * (b - a);
		this.len = 1f / precision;
		this.arcLengths = new float[(int)this.len + 1];
		this.arcLengths[0] = 0f;
		Vector3 vector = a;
		float num = 0f;
		int num2 = 1;
		while ((float)num2 <= this.len)
		{
			Vector3 vector2 = this.bezierPoint((float)num2 * precision);
			num += (vector - vector2).magnitude;
			this.arcLengths[num2] = num;
			vector = vector2;
			num2++;
		}
		this.length = num;
	}

	// Token: 0x06000A8C RID: 2700 RVA: 0x0004484C File Offset: 0x00042A4C
	private float map(float u)
	{
		float num = u * this.arcLengths[(int)this.len];
		int i = 0;
		int num2 = (int)this.len;
		int num3 = 0;
		while (i < num2)
		{
			num3 = i + ((int)((float)(num2 - i) / 2f) | 0);
			if (this.arcLengths[num3] < num)
			{
				i = num3 + 1;
			}
			else
			{
				num2 = num3;
			}
		}
		if (this.arcLengths[num3] > num)
		{
			num3--;
		}
		if (num3 < 0)
		{
			num3 = 0;
		}
		return ((float)num3 + (num - this.arcLengths[num3]) / (this.arcLengths[num3 + 1] - this.arcLengths[num3])) / this.len;
	}

	// Token: 0x06000A8D RID: 2701 RVA: 0x000448DC File Offset: 0x00042ADC
	private Vector3 bezierPoint(float t)
	{
		return ((this.aa * t + this.bb) * t + this.cc) * t + this.a;
	}

	// Token: 0x06000A8E RID: 2702 RVA: 0x00044917 File Offset: 0x00042B17
	public Vector3 point(float t)
	{
		return this.bezierPoint(this.map(t));
	}

	// Token: 0x04000708 RID: 1800
	public float length;

	// Token: 0x04000709 RID: 1801
	private Vector3 a;

	// Token: 0x0400070A RID: 1802
	private Vector3 aa;

	// Token: 0x0400070B RID: 1803
	private Vector3 bb;

	// Token: 0x0400070C RID: 1804
	private Vector3 cc;

	// Token: 0x0400070D RID: 1805
	private float len;

	// Token: 0x0400070E RID: 1806
	private float[] arcLengths;
}
