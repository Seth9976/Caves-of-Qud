using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Qud.UI
{
	// Token: 0x02000465 RID: 1125
	public class DraggableInventoryArea : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		// Token: 0x06002AA0 RID: 10912 RVA: 0x000E0D1D File Offset: 0x000DEF1D
		public void Awake()
		{
			if (this.highlight != null)
			{
				this.highlight.enabled = false;
			}
		}

		// Token: 0x06002AA1 RID: 10913 RVA: 0x000E0D3C File Offset: 0x000DEF3C
		public void Update()
		{
			if (this.highlight != null)
			{
				this.highlight.enabled = this.highlight != null && DraggableInventoryArea.inside == this && ((this.ID == "Inventory" && EquipmentLine.dragging && InventoryLine.dragObject == null) || (this.ID == "Equipment" && InventoryLine.dragging && EquipmentLine.dragObject == null));
			}
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x000E0DD0 File Offset: 0x000DEFD0
		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			DraggableInventoryArea.inside = this;
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x000E0DD8 File Offset: 0x000DEFD8
		public virtual void OnPointerExit(PointerEventData eventData)
		{
			DraggableInventoryArea.inside = null;
		}

		// Token: 0x0400169B RID: 5787
		public string ID;

		// Token: 0x0400169C RID: 5788
		public static DraggableInventoryArea inside;

		// Token: 0x0400169D RID: 5789
		public Image highlight;
	}
}
