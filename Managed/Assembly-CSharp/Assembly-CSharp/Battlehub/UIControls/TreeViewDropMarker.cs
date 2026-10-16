using System;
using UnityEngine;

namespace Battlehub.UIControls
{
	// Token: 0x020003D9 RID: 985
	[RequireComponent(typeof(RectTransform))]
	public class TreeViewDropMarker : ItemDropMarker
	{
		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060024B4 RID: 9396 RVA: 0x000B8D10 File Offset: 0x000B6F10
		// (set) Token: 0x060024B5 RID: 9397 RVA: 0x000B8D18 File Offset: 0x000B6F18
		public override ItemDropAction Action
		{
			get
			{
				return base.Action;
			}
			set
			{
				base.Action = value;
				this.ChildGraphics.SetActive(base.Action == ItemDropAction.SetLastChild);
				this.SiblingGraphics.SetActive(base.Action != ItemDropAction.SetLastChild);
			}
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x000B8D4C File Offset: 0x000B6F4C
		protected override void AwakeOverride()
		{
			base.AwakeOverride();
			this.m_treeView = base.GetComponentInParent<TreeView>();
			this.m_siblingGraphicsRectTransform = this.SiblingGraphics.GetComponent<RectTransform>();
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x000B8D74 File Offset: 0x000B6F74
		public override void SetTraget(ItemContainer item)
		{
			base.SetTraget(item);
			if (item == null)
			{
				return;
			}
			TreeViewItem treeViewItem = (TreeViewItem)item;
			if (treeViewItem != null)
			{
				this.m_siblingGraphicsRectTransform.offsetMin = new Vector2((float)treeViewItem.Indent, this.m_siblingGraphicsRectTransform.offsetMin.y);
				return;
			}
			this.m_siblingGraphicsRectTransform.offsetMin = new Vector2(0f, this.m_siblingGraphicsRectTransform.offsetMin.y);
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x000B8DF0 File Offset: 0x000B6FF0
		public override void SetPosition(Vector2 position)
		{
			if (base.Item == null)
			{
				return;
			}
			if (!this.m_treeView.CanReparent)
			{
				base.SetPosition(position);
				return;
			}
			RectTransform rectTransform = base.Item.RectTransform;
			TreeViewItem treeViewItem = (TreeViewItem)base.Item;
			Camera camera = null;
			if (base.ParentCanvas.renderMode == RenderMode.WorldSpace || base.ParentCanvas.renderMode == RenderMode.ScreenSpaceCamera)
			{
				camera = this.m_treeView.Camera;
			}
			if (this.m_treeView.CanReorder)
			{
				Vector2 vector;
				if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, position, camera, out vector))
				{
					if (vector.y > -rectTransform.rect.height / 4f)
					{
						this.Action = ItemDropAction.SetPrevSibling;
						base.RectTransform.position = rectTransform.position;
						return;
					}
					if (vector.y < rectTransform.rect.height / 4f - rectTransform.rect.height && !treeViewItem.HasChildren)
					{
						this.Action = ItemDropAction.SetNextSibling;
						base.RectTransform.position = rectTransform.position;
						base.RectTransform.localPosition = base.RectTransform.localPosition - new Vector3(0f, rectTransform.rect.height * base.ParentCanvas.scaleFactor, 0f);
						return;
					}
					if (!treeViewItem.CanDrop)
					{
						return;
					}
					this.Action = ItemDropAction.SetLastChild;
					base.RectTransform.position = rectTransform.position;
				}
				return;
			}
			if (!treeViewItem.CanDrop)
			{
				return;
			}
			this.Action = ItemDropAction.SetLastChild;
			base.RectTransform.position = rectTransform.position;
		}

		// Token: 0x04001260 RID: 4704
		private TreeView m_treeView;

		// Token: 0x04001261 RID: 4705
		private RectTransform m_siblingGraphicsRectTransform;

		// Token: 0x04001262 RID: 4706
		public GameObject ChildGraphics;
	}
}
