using System;
using UnityEngine;

// Token: 0x02000020 RID: 32
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Lookup Filter (Color Grading)")]
public class CC_LookupFilter : CC_Base
{
	// Token: 0x06000081 RID: 129 RVA: 0x00004828 File Offset: 0x00002A28
	protected virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.lookupTexture == null)
		{
			Graphics.Blit(source, destination);
			return;
		}
		base.material.SetTexture("_LookupTex", this.lookupTexture);
		Graphics.Blit(source, destination, base.material, CC_Base.IsLinear ? 1 : 0);
	}

	// Token: 0x040000B3 RID: 179
	public Texture lookupTexture;
}
