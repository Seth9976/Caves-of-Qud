using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Overlay.MapEditor
{
	// Token: 0x020003E0 RID: 992
	public class MapEditorTileAreaHandler : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler, IScrollHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
	{
		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060024EA RID: 9450 RVA: 0x000B9D49 File Offset: 0x000B7F49
		private MapEditorView MapEditor
		{
			get
			{
				return MapEditorView.instance;
			}
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x000B9D50 File Offset: 0x000B7F50
		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.dragging)
			{
				return;
			}
			this.MapEditor.OnClicked(eventData);
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x000B9D6C File Offset: 0x000B7F6C
		public void OnScroll(PointerEventData eventData)
		{
			GameManager.Instance.OnScroll(eventData.scrollDelta);
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x000B9D7E File Offset: 0x000B7F7E
		public void OnPointerEnter(PointerEventData eventData)
		{
			this.PointerInside = true;
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x000B9D87 File Offset: 0x000B7F87
		public void OnPointerExit(PointerEventData eventData)
		{
			this.PointerInside = false;
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x000B9D90 File Offset: 0x000B7F90
		private void Start()
		{
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x000B9D94 File Offset: 0x000B7F94
		private void Update()
		{
			if (this.PointerInside)
			{
				foreach (RaycastHit raycastHit in Physics.RaycastAll(GameManager.MainCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition)))
				{
					TileBehavior component = raycastHit.collider.gameObject.GetComponent<TileBehavior>();
					if (component != null)
					{
						MapEditorView.Instance.UpdateHoverXY(component.x, component.y);
					}
				}
			}
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x000B9E0C File Offset: 0x000B800C
		public void OnBeginDrag(PointerEventData eventData)
		{
			TileBehavior tileBehavior = null;
			if (this.PointerInside)
			{
				foreach (RaycastHit raycastHit in Physics.RaycastAll(GameManager.MainCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition)))
				{
					TileBehavior component = raycastHit.collider.gameObject.GetComponent<TileBehavior>();
					if (component != null)
					{
						tileBehavior = component;
					}
				}
			}
			this.MapEditor.OnBeginDrag(eventData, tileBehavior);
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x000B9E80 File Offset: 0x000B8080
		public void OnEndDrag(PointerEventData eventData)
		{
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x000B9E82 File Offset: 0x000B8082
		public void OnDrag(PointerEventData eventData)
		{
			MapEditorView.instance.OnDrag(eventData);
		}

		// Token: 0x0400127D RID: 4733
		public bool PointerInside;
	}
}
