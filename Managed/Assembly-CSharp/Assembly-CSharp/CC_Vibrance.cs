using System;
using UnityEngine;

// Token: 0x02000029 RID: 41
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Vibrance")]
public class CC_Vibrance : CC_Base
{
	// Token: 0x06000093 RID: 147 RVA: 0x00004CA4 File Offset: 0x00002EA4
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.amount == 0f)
		{
			Graphics.Blit(source, destination);
			return;
		}
		if (this.advanced)
		{
			base.material.SetFloat("_Amount", this.amount * 0.01f);
			base.material.SetVector("_Channels", new Vector3(this.redChannel, this.greenChannel, this.blueChannel));
			Graphics.Blit(source, destination, base.material, 1);
			return;
		}
		base.material.SetFloat("_Amount", this.amount * 0.02f);
		Graphics.Blit(source, destination, base.material, 0);
	}

	// Token: 0x040000C6 RID: 198
	[Range(-100f, 100f)]
	public float amount;

	// Token: 0x040000C7 RID: 199
	[Range(-5f, 5f)]
	public float redChannel = 1f;

	// Token: 0x040000C8 RID: 200
	[Range(-5f, 5f)]
	public float greenChannel = 1f;

	// Token: 0x040000C9 RID: 201
	[Range(-5f, 5f)]
	public float blueChannel = 1f;

	// Token: 0x040000CA RID: 202
	public bool advanced;
}
