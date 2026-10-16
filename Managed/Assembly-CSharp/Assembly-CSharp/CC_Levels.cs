using System;
using UnityEngine;

// Token: 0x0200001F RID: 31
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Levels")]
public class CC_Levels : CC_Base
{
	// Token: 0x1700002E RID: 46
	// (get) Token: 0x0600007D RID: 125 RVA: 0x000044FC File Offset: 0x000026FC
	// (set) Token: 0x0600007E RID: 126 RVA: 0x00004509 File Offset: 0x00002709
	public int mode
	{
		get
		{
			if (!this.isRGB)
			{
				return 0;
			}
			return 1;
		}
		set
		{
			this.isRGB = value > 0;
		}
	}

	// Token: 0x0600007F RID: 127 RVA: 0x0000451C File Offset: 0x0000271C
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.isRGB)
		{
			base.material.SetVector("_InputMin", new Vector4(this.inputMinL / 255f, this.inputMinL / 255f, this.inputMinL / 255f, 1f));
			base.material.SetVector("_InputMax", new Vector4(this.inputMaxL / 255f, this.inputMaxL / 255f, this.inputMaxL / 255f, 1f));
			base.material.SetVector("_InputGamma", new Vector4(this.inputGammaL, this.inputGammaL, this.inputGammaL, 1f));
			base.material.SetVector("_OutputMin", new Vector4(this.outputMinL / 255f, this.outputMinL / 255f, this.outputMinL / 255f, 1f));
			base.material.SetVector("_OutputMax", new Vector4(this.outputMaxL / 255f, this.outputMaxL / 255f, this.outputMaxL / 255f, 1f));
		}
		else
		{
			base.material.SetVector("_InputMin", new Vector4(this.inputMinR / 255f, this.inputMinG / 255f, this.inputMinB / 255f, 1f));
			base.material.SetVector("_InputMax", new Vector4(this.inputMaxR / 255f, this.inputMaxG / 255f, this.inputMaxB / 255f, 1f));
			base.material.SetVector("_InputGamma", new Vector4(this.inputGammaR, this.inputGammaG, this.inputGammaB, 1f));
			base.material.SetVector("_OutputMin", new Vector4(this.outputMinR / 255f, this.outputMinG / 255f, this.outputMinB / 255f, 1f));
			base.material.SetVector("_OutputMax", new Vector4(this.outputMaxR / 255f, this.outputMaxG / 255f, this.outputMaxB / 255f, 1f));
		}
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x0400009C RID: 156
	public bool isRGB;

	// Token: 0x0400009D RID: 157
	public float inputMinL;

	// Token: 0x0400009E RID: 158
	public float inputMaxL = 255f;

	// Token: 0x0400009F RID: 159
	public float inputGammaL = 1f;

	// Token: 0x040000A0 RID: 160
	public float inputMinR;

	// Token: 0x040000A1 RID: 161
	public float inputMaxR = 255f;

	// Token: 0x040000A2 RID: 162
	public float inputGammaR = 1f;

	// Token: 0x040000A3 RID: 163
	public float inputMinG;

	// Token: 0x040000A4 RID: 164
	public float inputMaxG = 255f;

	// Token: 0x040000A5 RID: 165
	public float inputGammaG = 1f;

	// Token: 0x040000A6 RID: 166
	public float inputMinB;

	// Token: 0x040000A7 RID: 167
	public float inputMaxB = 255f;

	// Token: 0x040000A8 RID: 168
	public float inputGammaB = 1f;

	// Token: 0x040000A9 RID: 169
	public float outputMinL;

	// Token: 0x040000AA RID: 170
	public float outputMaxL = 255f;

	// Token: 0x040000AB RID: 171
	public float outputMinR;

	// Token: 0x040000AC RID: 172
	public float outputMaxR = 255f;

	// Token: 0x040000AD RID: 173
	public float outputMinG;

	// Token: 0x040000AE RID: 174
	public float outputMaxG = 255f;

	// Token: 0x040000AF RID: 175
	public float outputMinB;

	// Token: 0x040000B0 RID: 176
	public float outputMaxB = 255f;

	// Token: 0x040000B1 RID: 177
	public int currentChannel;

	// Token: 0x040000B2 RID: 178
	public bool logarithmic;
}
