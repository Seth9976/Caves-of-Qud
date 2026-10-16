using System;
using UnityEngine;

namespace Battlehub.UIControls
{
	// Token: 0x020003C4 RID: 964
	[RequireComponent(typeof(RectTransform))]
	public class ItemDropMarker : MonoBehaviour
	{
		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060023D3 RID: 9171 RVA: 0x000B5694 File Offset: 0x000B3894
		protected Canvas ParentCanvas
		{
			get
			{
				return this.m_parentCanvas;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060023D4 RID: 9172 RVA: 0x000B569C File Offset: 0x000B389C
		// (set) Token: 0x060023D5 RID: 9173 RVA: 0x000B56A4 File Offset: 0x000B38A4
		public virtual ItemDropAction Action
		{
			get
			{
				return this.m_action;
			}
			set
			{
				this.m_action = value;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060023D6 RID: 9174 RVA: 0x000B56AD File Offset: 0x000B38AD
		public RectTransform RectTransform
		{
			get
			{
				return this.m_rectTransform;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060023D7 RID: 9175 RVA: 0x000B56B5 File Offset: 0x000B38B5
		protected ItemContainer Item
		{
			get
			{
				return this.m_item;
			}
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x000B56BD File Offset: 0x000B38BD
		private void Awake()
		{
			this.m_rectTransform = base.GetComponent<RectTransform>();
			this.SiblingGraphics.SetActive(true);
			this.m_parentCanvas = base.GetComponentInParent<Canvas>();
			this.m_itemsControl = base.GetComponentInParent<ItemsControl>();
			this.AwakeOverride();
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x000B56F5 File Offset: 0x000B38F5
		protected virtual void AwakeOverride()
		{
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x000B56F7 File Offset: 0x000B38F7
		public virtual void SetTraget(ItemContainer item)
		{
			base.gameObject.SetActive(item != null);
			this.m_item = item;
			if (this.m_item == null)
			{
				this.Action = ItemDropAction.None;
			}
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x000B5728 File Offset: 0x000B3928
		public virtual void SetPosition(Vector2 position)
		{
			if (this.m_item == null)
			{
				return;
			}
			if (!this.m_itemsControl.CanReorder)
			{
				return;
			}
			RectTransform rectTransform = this.Item.RectTransform;
			Camera camera = null;
			if (this.ParentCanvas.renderMode == RenderMode.WorldSpace || this.ParentCanvas.renderMode == RenderMode.ScreenSpaceCamera)
			{
				camera = this.m_itemsControl.Camera;
			}
			Vector2 vector;
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, position, camera, out vector))
			{
				if (vector.y > -rectTransform.rect.height / 2f)
				{
					this.Action = ItemDropAction.SetPrevSibling;
					this.RectTransform.position = rectTransform.position;
					return;
				}
				this.Action = ItemDropAction.SetNextSibling;
				this.RectTransform.position = rectTransform.position;
				this.RectTransform.localPosition = this.RectTransform.localPosition - new Vector3(0f, rectTransform.rect.height * this.ParentCanvas.scaleFactor, 0f);
			}
		}

		// Token: 0x04001204 RID: 4612
		private Canvas m_parentCanvas;

		// Token: 0x04001205 RID: 4613
		private ItemsControl m_itemsControl;

		// Token: 0x04001206 RID: 4614
		public GameObject SiblingGraphics;

		// Token: 0x04001207 RID: 4615
		private ItemDropAction m_action;

		// Token: 0x04001208 RID: 4616
		private RectTransform m_rectTransform;

		// Token: 0x04001209 RID: 4617
		private ItemContainer m_item;
	}
}
