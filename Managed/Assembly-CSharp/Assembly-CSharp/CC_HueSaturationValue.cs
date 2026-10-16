using System;
using UnityEngine;

// Token: 0x0200001D RID: 29
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Hue, Saturation, Value")]
public class CC_HueSaturationValue : CC_Base
{
	// Token: 0x1700002B RID: 43
	// (get) Token: 0x06000073 RID: 115 RVA: 0x000041FA File Offset: 0x000023FA
	// (set) Token: 0x06000074 RID: 116 RVA: 0x00004202 File Offset: 0x00002402
	public float hue
	{
		get
		{
			return this.masterHue;
		}
		set
		{
			this.masterHue = value;
		}
	}

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x06000075 RID: 117 RVA: 0x0000420B File Offset: 0x0000240B
	// (set) Token: 0x06000076 RID: 118 RVA: 0x00004213 File Offset: 0x00002413
	public float saturation
	{
		get
		{
			return this.masterSaturation;
		}
		set
		{
			this.masterSaturation = value;
		}
	}

	// Token: 0x1700002D RID: 45
	// (get) Token: 0x06000077 RID: 119 RVA: 0x0000421C File Offset: 0x0000241C
	// (set) Token: 0x06000078 RID: 120 RVA: 0x00004224 File Offset: 0x00002424
	public float value
	{
		get
		{
			return this.masterValue;
		}
		set
		{
			this.masterValue = value;
		}
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00004230 File Offset: 0x00002430
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetVector("_Master", new Vector3(this.masterHue / 360f, this.masterSaturation * 0.01f, this.masterValue * 0.01f));
		if (this.advanced)
		{
			base.material.SetVector("_Reds", new Vector3(this.redsHue / 360f, this.redsSaturation * 0.01f, this.redsValue * 0.01f));
			base.material.SetVector("_Yellows", new Vector3(this.yellowsHue / 360f, this.yellowsSaturation * 0.01f, this.yellowsValue * 0.01f));
			base.material.SetVector("_Greens", new Vector3(this.greensHue / 360f, this.greensSaturation * 0.01f, this.greensValue * 0.01f));
			base.material.SetVector("_Cyans", new Vector3(this.cyansHue / 360f, this.cyansSaturation * 0.01f, this.cyansValue * 0.01f));
			base.material.SetVector("_Blues", new Vector3(this.bluesHue / 360f, this.bluesSaturation * 0.01f, this.bluesValue * 0.01f));
			base.material.SetVector("_Magentas", new Vector3(this.magentasHue / 360f, this.magentasSaturation * 0.01f, this.magentasValue * 0.01f));
			Graphics.Blit(source, destination, base.material, 1);
			return;
		}
		Graphics.Blit(source, destination, base.material, 0);
	}

	// Token: 0x04000080 RID: 128
	[Range(-180f, 180f)]
	public float masterHue;

	// Token: 0x04000081 RID: 129
	[Range(-100f, 100f)]
	public float masterSaturation;

	// Token: 0x04000082 RID: 130
	[Range(-100f, 100f)]
	public float masterValue;

	// Token: 0x04000083 RID: 131
	[Range(-180f, 180f)]
	public float redsHue;

	// Token: 0x04000084 RID: 132
	[Range(-100f, 100f)]
	public float redsSaturation;

	// Token: 0x04000085 RID: 133
	[Range(-100f, 100f)]
	public float redsValue;

	// Token: 0x04000086 RID: 134
	[Range(-180f, 180f)]
	public float yellowsHue;

	// Token: 0x04000087 RID: 135
	[Range(-100f, 100f)]
	public float yellowsSaturation;

	// Token: 0x04000088 RID: 136
	[Range(-100f, 100f)]
	public float yellowsValue;

	// Token: 0x04000089 RID: 137
	[Range(-180f, 180f)]
	public float greensHue;

	// Token: 0x0400008A RID: 138
	[Range(-100f, 100f)]
	public float greensSaturation;

	// Token: 0x0400008B RID: 139
	[Range(-100f, 100f)]
	public float greensValue;

	// Token: 0x0400008C RID: 140
	[Range(-180f, 180f)]
	public float cyansHue;

	// Token: 0x0400008D RID: 141
	[Range(-100f, 100f)]
	public float cyansSaturation;

	// Token: 0x0400008E RID: 142
	[Range(-100f, 100f)]
	public float cyansValue;

	// Token: 0x0400008F RID: 143
	[Range(-180f, 180f)]
	public float bluesHue;

	// Token: 0x04000090 RID: 144
	[Range(-100f, 100f)]
	public float bluesSaturation;

	// Token: 0x04000091 RID: 145
	[Range(-100f, 100f)]
	public float bluesValue;

	// Token: 0x04000092 RID: 146
	[Range(-180f, 180f)]
	public float magentasHue;

	// Token: 0x04000093 RID: 147
	[Range(-100f, 100f)]
	public float magentasSaturation;

	// Token: 0x04000094 RID: 148
	[Range(-100f, 100f)]
	public float magentasValue;

	// Token: 0x04000095 RID: 149
	public bool advanced;

	// Token: 0x04000096 RID: 150
	public int currentChannel;
}
