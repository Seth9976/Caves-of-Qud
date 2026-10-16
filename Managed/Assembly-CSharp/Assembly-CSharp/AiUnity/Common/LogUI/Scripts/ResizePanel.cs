using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AiUnity.Common.LogUI.Scripts
{
	// Token: 0x020002CF RID: 719
	public class ResizePanel : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
	{
		// Token: 0x060018D9 RID: 6361 RVA: 0x00080188 File Offset: 0x0007E388
		public void OnDrag(PointerEventData data)
		{
			if (this.rectTransform == null)
			{
				return;
			}
			Vector2 vector = this.rectTransform.sizeDelta;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.rectTransform, data.position, data.pressEventCamera, out this.currentPointerPosition);
			Vector2 vector2 = this.currentPointerPosition - this.previousPointerPosition;
			vector += new Vector2(vector2.x, -vector2.y);
			vector = new Vector2(Mathf.Clamp(vector.x, this.minSize.x, this.maxSize.x), Mathf.Clamp(vector.y, this.minSize.y, this.maxSize.y));
			this.rectTransform.sizeDelta = vector;
			this.previousPointerPosition = this.currentPointerPosition;
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x0008025A File Offset: 0x0007E45A
		public void OnPointerEnter(PointerEventData eventData)
		{
			Cursor.SetCursor(this.cursorTexture, this.cursorHotspot, CursorMode.Auto);
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x0008026E File Offset: 0x0007E46E
		public void OnPointerExit(PointerEventData eventData)
		{
			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x0008027C File Offset: 0x0007E47C
		public void OnPointerDown(PointerEventData data)
		{
			this.rectTransform.SetAsLastSibling();
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.rectTransform, data.position, data.pressEventCamera, out this.previousPointerPosition);
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x000802A7 File Offset: 0x0007E4A7
		private void Awake()
		{
			this.rectTransform = base.transform.parent.GetComponent<RectTransform>();
			this.cursorHotspot = new Vector2((float)(this.cursorTexture.width / 4), (float)(this.cursorTexture.height / 4));
		}

		// Token: 0x04000EBF RID: 3775
		public Vector2 maxSize;

		// Token: 0x04000EC0 RID: 3776
		public Vector2 minSize;

		// Token: 0x04000EC1 RID: 3777
		public Texture2D cursorTexture;

		// Token: 0x04000EC2 RID: 3778
		private Vector2 cursorHotspot;

		// Token: 0x04000EC3 RID: 3779
		private Vector2 currentPointerPosition;

		// Token: 0x04000EC4 RID: 3780
		private Vector2 previousPointerPosition;

		// Token: 0x04000EC5 RID: 3781
		private RectTransform rectTransform;
	}
}
