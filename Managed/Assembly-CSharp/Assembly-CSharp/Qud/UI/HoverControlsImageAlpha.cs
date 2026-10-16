using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Qud.UI
{
	// Token: 0x0200048F RID: 1167
	public class HoverControlsImageAlpha : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		// Token: 0x06002CCB RID: 11467 RVA: 0x000F1F68 File Offset: 0x000F0168
		private void Awake()
		{
			if (this.image != null)
			{
				this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, 0f);
			}
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x000F1FC8 File Offset: 0x000F01C8
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (this.image != null)
			{
				this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, 1f);
			}
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x000F2028 File Offset: 0x000F0228
		public void OnPointerExit(PointerEventData eventData)
		{
			if (this.image != null)
			{
				this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, 0f);
			}
		}

		// Token: 0x040018FD RID: 6397
		public Image image;
	}
}
