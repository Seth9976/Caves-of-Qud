using System;
using UnityEngine;

// Token: 0x0200002B RID: 43
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Wiggle")]
public class CC_Wiggle : CC_Base
{
	// Token: 0x06000097 RID: 151 RVA: 0x00004DCC File Offset: 0x00002FCC
	private void Update()
	{
		if (this.autoTimer)
		{
			this.timer += this.speed * Time.deltaTime;
		}
	}

	// Token: 0x06000098 RID: 152 RVA: 0x00004DEF File Offset: 0x00002FEF
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_Timer", this.timer);
		base.material.SetFloat("_Scale", this.scale);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x040000CC RID: 204
	public float timer;

	// Token: 0x040000CD RID: 205
	public float speed = 1f;

	// Token: 0x040000CE RID: 206
	public float scale = 12f;

	// Token: 0x040000CF RID: 207
	public bool autoTimer = true;
}
