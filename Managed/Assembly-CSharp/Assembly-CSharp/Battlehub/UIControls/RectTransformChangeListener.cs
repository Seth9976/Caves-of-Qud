using System;
using UnityEngine.EventSystems;

namespace Battlehub.UIControls
{
	// Token: 0x020003D5 RID: 981
	public class RectTransformChangeListener : UIBehaviour
	{
		// Token: 0x1400001F RID: 31
		// (add) Token: 0x0600248D RID: 9357 RVA: 0x000B8394 File Offset: 0x000B6594
		// (remove) Token: 0x0600248E RID: 9358 RVA: 0x000B83CC File Offset: 0x000B65CC
		public event RectTransformChanged RectTransformChanged;

		// Token: 0x0600248F RID: 9359 RVA: 0x000B8401 File Offset: 0x000B6601
		protected override void OnRectTransformDimensionsChange()
		{
			if (this.RectTransformChanged != null)
			{
				this.RectTransformChanged();
			}
		}
	}
}
