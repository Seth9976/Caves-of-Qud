using System;
using UnityEngine;

// Token: 0x02000013 RID: 19
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Channel Mixer")]
public class CC_ChannelMixer : CC_Base
{
	// Token: 0x0600005F RID: 95 RVA: 0x00003A24 File Offset: 0x00001C24
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetVector("_Red", new Vector4(this.redR * 0.01f, this.greenR * 0.01f, this.blueR * 0.01f));
		base.material.SetVector("_Green", new Vector4(this.redG * 0.01f, this.greenG * 0.01f, this.blueG * 0.01f));
		base.material.SetVector("_Blue", new Vector4(this.redB * 0.01f, this.greenB * 0.01f, this.blueB * 0.01f));
		base.material.SetVector("_Constant", new Vector4(this.constantR * 0.01f, this.constantG * 0.01f, this.constantB * 0.01f));
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x0400004E RID: 78
	[Range(-200f, 200f)]
	public float redR = 100f;

	// Token: 0x0400004F RID: 79
	[Range(-200f, 200f)]
	public float redG;

	// Token: 0x04000050 RID: 80
	[Range(-200f, 200f)]
	public float redB;

	// Token: 0x04000051 RID: 81
	[Range(-200f, 200f)]
	public float greenR;

	// Token: 0x04000052 RID: 82
	[Range(-200f, 200f)]
	public float greenG = 100f;

	// Token: 0x04000053 RID: 83
	[Range(-200f, 200f)]
	public float greenB;

	// Token: 0x04000054 RID: 84
	[Range(-200f, 200f)]
	public float blueR;

	// Token: 0x04000055 RID: 85
	[Range(-200f, 200f)]
	public float blueG;

	// Token: 0x04000056 RID: 86
	[Range(-200f, 200f)]
	public float blueB = 100f;

	// Token: 0x04000057 RID: 87
	[Range(-200f, 200f)]
	public float constantR;

	// Token: 0x04000058 RID: 88
	[Range(-200f, 200f)]
	public float constantG;

	// Token: 0x04000059 RID: 89
	[Range(-200f, 200f)]
	public float constantB;

	// Token: 0x0400005A RID: 90
	public int currentChannel;
}
