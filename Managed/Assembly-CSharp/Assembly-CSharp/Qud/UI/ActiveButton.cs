using System;
using UnityEngine;
using UnityEngine.UI;

namespace Qud.UI
{
	// Token: 0x0200049F RID: 1183
	[ExecuteAlways]
	public class ActiveButton : MonoBehaviour
	{
		// Token: 0x06002D5C RID: 11612 RVA: 0x000F557C File Offset: 0x000F377C
		public void Update()
		{
			bool? wasActive = this._wasActive;
			bool isActive = this.IsActive;
			if (!((wasActive.GetValueOrDefault() == isActive) & (wasActive != null)))
			{
				this._wasActive = new bool?(this.IsActive);
				Button component = base.gameObject.GetComponent<Button>();
				if (component != null)
				{
					ColorBlock colors = component.colors;
					colors.normalColor = (this.IsActive ? this.ActiveColor : this.InactiveColor);
					colors.selectedColor = (this.IsActive ? this.ActiveColor : this.InactiveColor);
					component.colors = colors;
					if (this.TargetImage != null && this.ActiveImage != null && this.DisabledImage != null)
					{
						this.TargetImage.sprite = (this.IsActive ? this.ActiveImage : this.DisabledImage);
					}
				}
			}
		}

		// Token: 0x0400196B RID: 6507
		public Image TargetImage;

		// Token: 0x0400196C RID: 6508
		public Sprite ActiveImage;

		// Token: 0x0400196D RID: 6509
		public Sprite DisabledImage;

		// Token: 0x0400196E RID: 6510
		public Color InactiveColor;

		// Token: 0x0400196F RID: 6511
		public Color ActiveColor;

		// Token: 0x04001970 RID: 6512
		public bool IsActive;

		// Token: 0x04001971 RID: 6513
		public bool? _wasActive;
	}
}
