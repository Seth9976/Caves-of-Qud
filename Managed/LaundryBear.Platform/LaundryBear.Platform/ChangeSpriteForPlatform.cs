using System;
using UnityEngine;

namespace LaundryBear.PlatformServices
{
	// Token: 0x0200001C RID: 28
	public class ChangeSpriteForPlatform : ChangeForPlatform<Sprite>
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002373 File Offset: 0x00000573
		private void Start()
		{
			this.UpdateSpriteForPlatform();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000237C File Offset: 0x0000057C
		public void UpdateSpriteForPlatform()
		{
			SpriteRenderer spriteRenderer;
			Sprite sprite;
			if (base.TryGetComponent<SpriteRenderer>(out spriteRenderer) && base.GetPlatformSpecificObject(Utilities.GetCurrentPlatform(), out sprite))
			{
				spriteRenderer.sprite = sprite;
			}
		}
	}
}
