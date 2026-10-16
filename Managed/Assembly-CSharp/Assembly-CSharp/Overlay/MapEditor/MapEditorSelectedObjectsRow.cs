using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XRL.EditorFormats.Map;

namespace Overlay.MapEditor
{
	// Token: 0x020003DE RID: 990
	public class MapEditorSelectedObjectsRow : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
	{
		// Token: 0x060024E2 RID: 9442 RVA: 0x000B9C75 File Offset: 0x000B7E75
		public void Delete()
		{
			MapEditorView.Instance.DeleteInRegion(this.bp);
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x000B9C87 File Offset: 0x000B7E87
		public void Replace()
		{
			MapEditorView.Instance.ReplaceInRegion(this.bp);
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x000B9C99 File Offset: 0x000B7E99
		public void SetOwner()
		{
			MapEditorView.Instance.SetOwnerInRegion(this.bp);
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x000B9CAB File Offset: 0x000B7EAB
		public void OnPointerEnter(PointerEventData eventData)
		{
			MapEditorView.Instance.SetHoverBlueprint(this.bp);
			base.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.25f);
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x000B9CE1 File Offset: 0x000B7EE1
		public void OnPointerExit(PointerEventData eventData)
		{
			MapEditorView.Instance.SetHoverBlueprint(null);
			base.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x000B9D12 File Offset: 0x000B7F12
		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Right)
			{
				return;
			}
			MapEditorView.Instance.DisplayContextInRegion(this.bp);
		}

		// Token: 0x04001276 RID: 4726
		public MapFileObjectBlueprint bp;

		// Token: 0x04001277 RID: 4727
		public TextMeshProUGUI BlueprintName;

		// Token: 0x04001278 RID: 4728
		public TextMeshProUGUI Count;

		// Token: 0x04001279 RID: 4729
		public TextMeshProUGUI Owner;

		// Token: 0x0400127A RID: 4730
		public GameObject ReplaceButton;
	}
}
