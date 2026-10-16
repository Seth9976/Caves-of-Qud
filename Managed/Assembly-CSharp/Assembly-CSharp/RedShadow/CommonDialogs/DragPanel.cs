using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RedShadow.CommonDialogs
{
	// Token: 0x020001EB RID: 491
	public class DragPanel : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IDragHandler
	{
		// Token: 0x06001194 RID: 4500 RVA: 0x00069E9B File Offset: 0x0006809B
		public Vector2 getPosition()
		{
			return this._panelRectTransform.localPosition;
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x00069EAD File Offset: 0x000680AD
		public void setPosition(Vector2 pos)
		{
			this._panelRectTransform.localPosition = pos;
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x00069EC0 File Offset: 0x000680C0
		protected void Awake()
		{
			this._panelRectTransform = this.TargetPanel;
			this._parentRectTransform = this.TargetPanel.parent.GetComponent<RectTransform>();
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x00069EE4 File Offset: 0x000680E4
		public void OnPointerDown(PointerEventData data)
		{
			this._originalPanelLocalPosition = this._panelRectTransform.localPosition;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this._parentRectTransform, data.position, data.pressEventCamera, out this._originalLocalPointerPosition);
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x00069F18 File Offset: 0x00068118
		public void OnDrag(PointerEventData data)
		{
			if (this._panelRectTransform == null || this._parentRectTransform == null)
			{
				return;
			}
			Vector2 vector;
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this._parentRectTransform, data.position, data.pressEventCamera, out vector))
			{
				Vector3 vector2 = vector - this._originalLocalPointerPosition;
				this._panelRectTransform.localPosition = this._originalPanelLocalPosition + vector2;
			}
			this.clampToWindow();
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x00069F8C File Offset: 0x0006818C
		public void clampToWindow()
		{
			Vector3 localPosition = this._panelRectTransform.localPosition;
			Vector3 vector = this._parentRectTransform.rect.min - this._panelRectTransform.rect.min;
			Vector3 vector2 = this._parentRectTransform.rect.max - this._panelRectTransform.rect.max;
			localPosition.x = Mathf.Clamp(localPosition.x, vector.x, vector2.x);
			localPosition.y = Mathf.Clamp(localPosition.y, vector.y, vector2.y);
			this._panelRectTransform.localPosition = localPosition;
		}

		// Token: 0x04000BB3 RID: 2995
		public RectTransform TargetPanel;

		// Token: 0x04000BB4 RID: 2996
		private Vector2 _originalLocalPointerPosition;

		// Token: 0x04000BB5 RID: 2997
		private Vector3 _originalPanelLocalPosition;

		// Token: 0x04000BB6 RID: 2998
		private RectTransform _panelRectTransform;

		// Token: 0x04000BB7 RID: 2999
		private RectTransform _parentRectTransform;
	}
}
