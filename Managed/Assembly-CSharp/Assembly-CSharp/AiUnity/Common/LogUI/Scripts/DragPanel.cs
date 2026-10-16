using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AiUnity.Common.LogUI.Scripts
{
	// Token: 0x020002C7 RID: 711
	public class DragPanel : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IDragHandler
	{
		// Token: 0x0600189D RID: 6301 RVA: 0x0007F828 File Offset: 0x0007DA28
		public void OnDrag(PointerEventData data)
		{
			if (base.enabled)
			{
				if (this.panelRectTransform == null)
				{
					return;
				}
				Vector2 vector = this.ClampToWindow(data);
				Vector2 vector2;
				if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.canvasRectTransform, vector, data.pressEventCamera, out vector2))
				{
					this.panelRectTransform.localPosition = vector2 - this.pointerOffset;
				}
			}
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x0007F886 File Offset: 0x0007DA86
		public void OnPointerDown(PointerEventData data)
		{
			if (base.enabled)
			{
				this.panelRectTransform.SetAsLastSibling();
				RectTransformUtility.ScreenPointToLocalPointInRectangle(this.panelRectTransform, data.position, data.pressEventCamera, out this.pointerOffset);
			}
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x0007F8BC File Offset: 0x0007DABC
		private void Awake()
		{
			Canvas componentInParent = base.GetComponentInParent<Canvas>();
			if (componentInParent != null)
			{
				this.canvasRectTransform = componentInParent.transform as RectTransform;
			}
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x0007F8EC File Offset: 0x0007DAEC
		private Vector2 ClampToWindow(PointerEventData data)
		{
			Vector2 position = data.position;
			Vector3[] array = new Vector3[4];
			this.canvasRectTransform.GetWorldCorners(array);
			float num = Mathf.Clamp(position.x, array[0].x, array[2].x);
			float num2 = Mathf.Clamp(position.y, array[0].y, array[2].y);
			return new Vector2(num, num2);
		}

		// Token: 0x04000E98 RID: 3736
		public RectTransform panelRectTransform;

		// Token: 0x04000E99 RID: 3737
		private RectTransform canvasRectTransform;

		// Token: 0x04000E9A RID: 3738
		private Vector2 pointerOffset;
	}
}
