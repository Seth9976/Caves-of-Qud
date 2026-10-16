using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battlehub.UIControls
{
	// Token: 0x020003BE RID: 958
	public class ExternalDragItem : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IDropHandler, IEndDragHandler
	{
		// Token: 0x06002386 RID: 9094 RVA: 0x000B4597 File Offset: 0x000B2797
		void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
		{
			this.TreeView.ExternalBeginDrag(eventData.position);
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x000B45AF File Offset: 0x000B27AF
		void IDragHandler.OnDrag(PointerEventData eventData)
		{
			this.TreeView.ExternalItemDrag(eventData.position);
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x000B45C7 File Offset: 0x000B27C7
		void IDropHandler.OnDrop(PointerEventData eventData)
		{
			if (this.TreeView.DropTarget != null)
			{
				this.TreeView.AddChild(this.TreeView.DropTarget, new GameObject());
			}
			this.TreeView.ExternalItemDrop();
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x000B45FC File Offset: 0x000B27FC
		void IEndDragHandler.OnEndDrag(PointerEventData eventData)
		{
			if (this.TreeView.DropTarget != null)
			{
				GameObject gameObject = (GameObject)this.TreeView.DropTarget;
				GameObject gameObject2 = new GameObject();
				TreeViewItem treeViewItem = (TreeViewItem)this.TreeView.GetItemContainer(this.TreeView.DropTarget);
				if (this.TreeView.DropAction == ItemDropAction.SetLastChild)
				{
					gameObject2.transform.SetParent(gameObject.transform);
					this.TreeView.AddChild(this.TreeView.DropTarget, gameObject2);
					treeViewItem.CanExpand = true;
					treeViewItem.IsExpanded = true;
				}
				else if (this.TreeView.DropAction != ItemDropAction.None)
				{
					int num;
					if (this.TreeView.DropAction == ItemDropAction.SetNextSibling)
					{
						num = this.TreeView.IndexOf(gameObject) + 1;
					}
					else
					{
						num = this.TreeView.IndexOf(gameObject);
					}
					gameObject2.transform.SetParent(gameObject.transform.parent);
					gameObject2.transform.SetSiblingIndex(num);
					((TreeViewItem)this.TreeView.Insert(num, gameObject2)).Parent = treeViewItem.Parent;
				}
			}
			this.TreeView.ExternalItemDrop();
		}

		// Token: 0x040011E1 RID: 4577
		public TreeView TreeView;
	}
}
