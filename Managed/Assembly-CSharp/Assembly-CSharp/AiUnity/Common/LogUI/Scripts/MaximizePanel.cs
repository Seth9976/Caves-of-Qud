using System;
using UnityEngine;

namespace AiUnity.Common.LogUI.Scripts
{
	// Token: 0x020002CD RID: 717
	public class MaximizePanel : MonoBehaviour
	{
		// Token: 0x060018D4 RID: 6356 RVA: 0x0007FFE8 File Offset: 0x0007E1E8
		public void MaxHandler()
		{
			if (this.isMaximized)
			{
				this.WindowStorage.Restore(this.GameConsolePanelTransform);
				this.isMaximized = false;
				return;
			}
			this.WindowStorage.Store(this.GameConsolePanelTransform);
			this.GameConsolePanelTransform.anchorMin = new Vector2(0f, 1f);
			this.GameConsolePanelTransform.anchorMax = new Vector2(1f, 1f);
			this.GameConsolePanelTransform.sizeDelta = new Vector2(0f, this.GameConsolePanelTransform.sizeDelta.y);
			this.GameConsolePanelTransform.anchoredPosition = Vector3.zero;
			this.isMaximized = true;
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x0008009C File Offset: 0x0007E29C
		private void Start()
		{
			this.GameConsolePanelTransform = (RectTransform)base.transform;
		}

		// Token: 0x04000EB5 RID: 3765
		private RectTransform GameConsolePanelTransform;

		// Token: 0x04000EB6 RID: 3766
		private bool isMaximized;

		// Token: 0x04000EB7 RID: 3767
		private RectTransformStorage WindowStorage;
	}
}
