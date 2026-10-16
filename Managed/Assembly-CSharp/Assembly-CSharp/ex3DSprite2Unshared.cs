using System;
using UnityEngine;

// Token: 0x020000E9 RID: 233
[AddComponentMenu("ex2D/3D Sprite2 Unshared")]
public class ex3DSprite2Unshared : ex3DSprite2
{
	// Token: 0x060008D1 RID: 2257 RVA: 0x00038E2A File Offset: 0x0003702A
	protected override void UpdateMaterial()
	{
		this.material_ = null;
		base.cachedRenderer.sharedMaterial = global::UnityEngine.Object.Instantiate<Material>(base.material);
	}

	// Token: 0x040005FC RID: 1532
	public bool ShareMaterial;
}
