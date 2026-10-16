using System;
using UnityEngine;

namespace AiUnity.Common.LogUI.Scripts
{
	// Token: 0x020002CE RID: 718
	public struct RectTransformStorage
	{
		// Token: 0x060018D7 RID: 6359 RVA: 0x000800B8 File Offset: 0x0007E2B8
		public void Restore(RectTransform t)
		{
			t.anchorMin = this.anchorMin;
			t.anchorMax = this.anchorMax;
			t.sizeDelta = this.sizeDelta;
			t.position = this.position;
			t.rotation = this.rotation;
			t.localScale = this.localScale;
			t.anchoredPosition = this.anchoredPosition;
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x00080120 File Offset: 0x0007E320
		public void Store(RectTransform t)
		{
			this.anchorMin = t.anchorMin;
			this.anchorMax = t.anchorMax;
			this.sizeDelta = t.sizeDelta;
			this.position = t.position;
			this.rotation = t.rotation;
			this.localScale = t.localScale;
			this.anchoredPosition = t.anchoredPosition;
		}

		// Token: 0x04000EB8 RID: 3768
		public Vector3 anchoredPosition;

		// Token: 0x04000EB9 RID: 3769
		public Vector2 anchorMax;

		// Token: 0x04000EBA RID: 3770
		public Vector2 anchorMin;

		// Token: 0x04000EBB RID: 3771
		public Vector3 localScale;

		// Token: 0x04000EBC RID: 3772
		public Vector3 position;

		// Token: 0x04000EBD RID: 3773
		public Quaternion rotation;

		// Token: 0x04000EBE RID: 3774
		public Vector2 sizeDelta;
	}
}
