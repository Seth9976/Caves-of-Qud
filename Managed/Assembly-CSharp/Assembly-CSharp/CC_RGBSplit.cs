using System;
using UnityEngine;

// Token: 0x02000025 RID: 37
[ExecuteInEditMode]
[AddComponentMenu("Colorful/RGB Split")]
public class CC_RGBSplit : CC_Base
{
	// Token: 0x0600008B RID: 139 RVA: 0x00004A50 File Offset: 0x00002C50
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.amount == 0f)
		{
			Graphics.Blit(source, destination);
			return;
		}
		base.material.SetFloat("_RGBShiftAmount", this.amount * 0.001f);
		base.material.SetFloat("_RGBShiftAngleCos", Mathf.Cos(this.angle));
		base.material.SetFloat("_RGBShiftAngleSin", Mathf.Sin(this.angle));
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x040000BC RID: 188
	public float amount;

	// Token: 0x040000BD RID: 189
	public float angle;
}
