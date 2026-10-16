using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RedShadow.CommonDialogs
{
	// Token: 0x020001F2 RID: 498
	public class ResizePanel : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IDragHandler
	{
		// Token: 0x060011CA RID: 4554 RVA: 0x0006A62F File Offset: 0x0006882F
		public Vector2 getSize()
		{
			return this._panelRectTransform.sizeDelta;
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x0006A63C File Offset: 0x0006883C
		public void setSize(Vector2 size)
		{
			this._panelRectTransform.sizeDelta = size;
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0006A64C File Offset: 0x0006884C
		protected void Awake()
		{
			if (this.TargetPanel == null)
			{
				this._panelRectTransform = base.GetComponent<RectTransform>();
				this._parentRectTransform = this._panelRectTransform.parent.GetComponent<RectTransform>();
			}
			else
			{
				this._panelRectTransform = this.TargetPanel;
				this._parentRectTransform = this.TargetPanel.parent.GetComponent<RectTransform>();
			}
			if (this.MaxSize.x <= 0f)
			{
				this.MaxSize.x = (float)Screen.width;
			}
			if (this.MaxSize.y <= 0f)
			{
				this.MaxSize.y = (float)Screen.height;
			}
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0006A6F3 File Offset: 0x000688F3
		public void OnPointerDown(PointerEventData data)
		{
			this._originalSizeDelta = this._panelRectTransform.sizeDelta;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this._panelRectTransform, data.position, data.pressEventCamera, out this._originalLocalPointerPosition);
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0006A724 File Offset: 0x00068924
		public void OnDrag(PointerEventData data)
		{
			if (this._panelRectTransform == null)
			{
				return;
			}
			Vector2 vector;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this._panelRectTransform, data.position, data.pressEventCamera, out vector);
			Vector3 vector2 = vector - this._originalLocalPointerPosition;
			Vector2 vector3 = this._originalSizeDelta + new Vector2(vector2.x, -vector2.y);
			vector3 = new Vector2(Mathf.Clamp(vector3.x, this.MinSize.x, this.MaxSize.x), Mathf.Clamp(vector3.y, this.MinSize.y, this.MaxSize.y));
			this._panelRectTransform.sizeDelta = vector3;
			this.clampToWindow();
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0006A7E8 File Offset: 0x000689E8
		private void clampToWindow()
		{
			Vector3 localPosition = this._panelRectTransform.localPosition;
			Vector3 vector = this._parentRectTransform.rect.min - this._panelRectTransform.rect.min;
			Vector3 vector2 = this._parentRectTransform.rect.max - this._panelRectTransform.rect.max;
			localPosition.x = Mathf.Clamp(localPosition.x, vector.x, vector2.x);
			localPosition.y = Mathf.Clamp(localPosition.y, vector.y, vector2.y);
			Vector3 vector3 = localPosition - this._panelRectTransform.localPosition;
			this._panelRectTransform.sizeDelta += new Vector2(vector3.x, -vector3.y);
		}

		// Token: 0x04000BD5 RID: 3029
		public RectTransform TargetPanel;

		// Token: 0x04000BD6 RID: 3030
		public Vector2 MinSize = new Vector2(100f, 100f);

		// Token: 0x04000BD7 RID: 3031
		public Vector2 MaxSize = new Vector2(400f, 400f);

		// Token: 0x04000BD8 RID: 3032
		private RectTransform _panelRectTransform;

		// Token: 0x04000BD9 RID: 3033
		private RectTransform _parentRectTransform;

		// Token: 0x04000BDA RID: 3034
		private Vector2 _originalLocalPointerPosition;

		// Token: 0x04000BDB RID: 3035
		private Vector2 _originalSizeDelta;
	}
}
