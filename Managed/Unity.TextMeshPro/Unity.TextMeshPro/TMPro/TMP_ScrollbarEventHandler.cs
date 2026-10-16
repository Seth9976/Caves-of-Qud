using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TMPro
{
	// Token: 0x02000044 RID: 68
	public class TMP_ScrollbarEventHandler : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, ISelectHandler, IDeselectHandler
	{
		// Token: 0x060002EB RID: 747 RVA: 0x00011F63 File Offset: 0x00010163
		public void OnPointerClick(PointerEventData eventData)
		{
			Debug.Log("Scrollbar click...");
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00011F6F File Offset: 0x0001016F
		public void OnSelect(BaseEventData eventData)
		{
			Debug.Log("Scrollbar selected");
			this.isSelected = true;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00011F82 File Offset: 0x00010182
		public void OnDeselect(BaseEventData eventData)
		{
			Debug.Log("Scrollbar De-Selected");
			this.isSelected = false;
		}

		// Token: 0x0400028C RID: 652
		public bool isSelected;
	}
}
