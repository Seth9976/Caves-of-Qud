using System;
using UnityEngine;
using UnityEngine.UI;

namespace Battlehub.UIControls
{
	// Token: 0x020003DC RID: 988
	public class TreeViewItem : ItemContainer
	{
		// Token: 0x14000021 RID: 33
		// (add) Token: 0x060024C8 RID: 9416 RVA: 0x000B9238 File Offset: 0x000B7438
		// (remove) Token: 0x060024C9 RID: 9417 RVA: 0x000B926C File Offset: 0x000B746C
		public static event EventHandler<ParentChangedEventArgs> ParentChanged;

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x060024CA RID: 9418 RVA: 0x000B929F File Offset: 0x000B749F
		private TreeView TreeView
		{
			get
			{
				if (this.m_treeView == null)
				{
					this.m_treeView = base.GetComponentInParent<TreeView>();
				}
				return this.m_treeView;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x060024CB RID: 9419 RVA: 0x000B92C1 File Offset: 0x000B74C1
		public int Indent
		{
			get
			{
				return this.m_indent;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060024CC RID: 9420 RVA: 0x000B92C9 File Offset: 0x000B74C9
		// (set) Token: 0x060024CD RID: 9421 RVA: 0x000B92D4 File Offset: 0x000B74D4
		public TreeViewItem Parent
		{
			get
			{
				return this.m_parent;
			}
			set
			{
				if (this.m_parent == value)
				{
					return;
				}
				TreeViewItem parent = this.m_parent;
				this.m_parent = value;
				if (this.m_parent != null && this.TreeView != null && this.m_itemLayout != null)
				{
					this.m_indent = this.m_parent.m_indent + this.TreeView.Indent;
					this.m_itemLayout.padding = new RectOffset(this.m_indent, this.m_itemLayout.padding.right, this.m_itemLayout.padding.top, this.m_itemLayout.padding.bottom);
					int siblingIndex = base.transform.GetSiblingIndex();
					this.SetIndent(this, ref siblingIndex);
				}
				else
				{
					this.ZeroIndent();
				}
				if (this.TreeView != null && TreeViewItem.ParentChanged != null)
				{
					TreeViewItem.ParentChanged(this, new ParentChangedEventArgs(parent, this.m_parent));
				}
			}
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x000B93DC File Offset: 0x000B75DC
		private void ZeroIndent()
		{
			this.m_indent = 0;
			if (this.m_itemLayout != null)
			{
				this.m_itemLayout.padding = new RectOffset(this.m_indent, this.m_itemLayout.padding.right, this.m_itemLayout.padding.top, this.m_itemLayout.padding.bottom);
			}
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x000B9444 File Offset: 0x000B7644
		private void SetIndent(TreeViewItem parent, ref int siblingIndex)
		{
			for (;;)
			{
				TreeViewItem treeViewItem = (TreeViewItem)this.TreeView.GetItemContainer(siblingIndex + 1);
				if (treeViewItem == null)
				{
					break;
				}
				if (treeViewItem.Parent != parent)
				{
					return;
				}
				treeViewItem.m_indent = parent.m_indent + this.TreeView.Indent;
				treeViewItem.m_itemLayout.padding.left = treeViewItem.m_indent;
				siblingIndex++;
				this.SetIndent(treeViewItem, ref siblingIndex);
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060024D0 RID: 9424 RVA: 0x000B94BC File Offset: 0x000B76BC
		// (set) Token: 0x060024D1 RID: 9425 RVA: 0x000B94C4 File Offset: 0x000B76C4
		public override bool IsSelected
		{
			get
			{
				return base.IsSelected;
			}
			set
			{
				if (base.IsSelected != value)
				{
					this.m_toggle.isOn = value;
					base.IsSelected = value;
				}
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060024D2 RID: 9426 RVA: 0x000B94E2 File Offset: 0x000B76E2
		// (set) Token: 0x060024D3 RID: 9427 RVA: 0x000B94EA File Offset: 0x000B76EA
		public bool CanExpand
		{
			get
			{
				return this.m_canExpand;
			}
			set
			{
				if (this.m_canExpand != value)
				{
					this.m_canExpand = value;
					if (this.m_expander != null)
					{
						this.m_expander.CanExpand = this.m_canExpand;
					}
					if (!this.m_canExpand)
					{
						this.IsExpanded = false;
					}
				}
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060024D4 RID: 9428 RVA: 0x000B952A File Offset: 0x000B772A
		// (set) Token: 0x060024D5 RID: 9429 RVA: 0x000B9534 File Offset: 0x000B7734
		public bool IsExpanded
		{
			get
			{
				return this.m_isExpanded;
			}
			set
			{
				if (this.m_isExpanded != value)
				{
					this.m_isExpanded = value && this.m_canExpand;
					if (this.m_expander != null)
					{
						this.m_expander.IsOn = value && this.m_canExpand;
					}
					if (this.TreeView != null)
					{
						if (this.m_isExpanded)
						{
							this.TreeView.Expand(this);
							return;
						}
						this.TreeView.Collapse(this);
					}
				}
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060024D6 RID: 9430 RVA: 0x000B95B0 File Offset: 0x000B77B0
		public bool HasChildren
		{
			get
			{
				int siblingIndex = base.transform.GetSiblingIndex();
				if (this.TreeView == null)
				{
					return false;
				}
				TreeViewItem treeViewItem = (TreeViewItem)this.TreeView.GetItemContainer(siblingIndex + 1);
				return treeViewItem != null && treeViewItem.Parent == this;
			}
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x000B9604 File Offset: 0x000B7804
		public bool IsDescendantOf(TreeViewItem ancestor)
		{
			if (ancestor == null)
			{
				return true;
			}
			if (ancestor == this)
			{
				return false;
			}
			TreeViewItem treeViewItem = this;
			while (treeViewItem != null)
			{
				if (ancestor == treeViewItem)
				{
					return true;
				}
				treeViewItem = treeViewItem.Parent;
			}
			return false;
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x000B9648 File Offset: 0x000B7848
		public TreeViewItem FirstChild()
		{
			if (!this.HasChildren)
			{
				return null;
			}
			int num = base.transform.GetSiblingIndex();
			num++;
			return (TreeViewItem)this.TreeView.GetItemContainer(num);
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x000B9680 File Offset: 0x000B7880
		public TreeViewItem NextChild(TreeViewItem currentChild)
		{
			if (currentChild == null)
			{
				throw new ArgumentNullException("currentChild");
			}
			int num = currentChild.transform.GetSiblingIndex();
			num++;
			TreeViewItem treeViewItem = (TreeViewItem)this.TreeView.GetItemContainer(num);
			while (treeViewItem != null && treeViewItem.IsDescendantOf(this))
			{
				if (treeViewItem.Parent == this)
				{
					return treeViewItem;
				}
				num++;
				treeViewItem = (TreeViewItem)this.TreeView.GetItemContainer(num);
			}
			return null;
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x000B9700 File Offset: 0x000B7900
		public TreeViewItem LastChild()
		{
			if (!this.HasChildren)
			{
				return null;
			}
			int num = base.transform.GetSiblingIndex();
			TreeViewItem treeViewItem = null;
			for (;;)
			{
				num++;
				TreeViewItem treeViewItem2 = (TreeViewItem)this.TreeView.GetItemContainer(num);
				if (treeViewItem2 == null || !treeViewItem2.IsDescendantOf(this))
				{
					break;
				}
				if (treeViewItem2.Parent == this)
				{
					treeViewItem = treeViewItem2;
				}
			}
			return treeViewItem;
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x000B9760 File Offset: 0x000B7960
		public TreeViewItem LastDescendant()
		{
			if (!this.HasChildren)
			{
				return null;
			}
			int num = base.transform.GetSiblingIndex();
			TreeViewItem treeViewItem = null;
			for (;;)
			{
				num++;
				TreeViewItem treeViewItem2 = (TreeViewItem)this.TreeView.GetItemContainer(num);
				if (treeViewItem2 == null || !treeViewItem2.IsDescendantOf(this))
				{
					break;
				}
				treeViewItem = treeViewItem2;
			}
			return treeViewItem;
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x000B97B4 File Offset: 0x000B79B4
		protected override void AwakeOverride()
		{
			this.m_toggle = base.GetComponent<Toggle>();
			this.m_toggle.interactable = false;
			this.m_toggle.isOn = this.IsSelected;
			this.m_expander = base.GetComponentInChildren<TreeViewExpander>();
			if (this.m_expander != null)
			{
				this.m_expander.CanExpand = this.m_canExpand;
			}
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x000B9818 File Offset: 0x000B7A18
		protected override void StartOverride()
		{
			if (this.TreeView != null)
			{
				this.m_toggle.isOn = this.TreeView.IsItemSelected(base.Item);
				this.m_isSelected = this.m_toggle.isOn;
			}
			if (this.Parent != null)
			{
				this.m_indent = this.Parent.m_indent + this.TreeView.Indent;
				this.m_itemLayout.padding = new RectOffset(this.m_indent, this.m_itemLayout.padding.right, this.m_itemLayout.padding.top, this.m_itemLayout.padding.bottom);
			}
			if (this.CanExpand && this.TreeView.AutoExpand)
			{
				this.IsExpanded = true;
			}
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x000B98F0 File Offset: 0x000B7AF0
		public override void Clear()
		{
			base.Clear();
			this.m_parent = null;
			this.ZeroIndent();
			this.m_isSelected = false;
			this.m_toggle.isOn = this.m_isSelected;
			this.m_isExpanded = false;
			this.m_canExpand = false;
			this.m_expander.IsOn = false;
			this.m_expander.CanExpand = false;
		}

		// Token: 0x04001269 RID: 4713
		private TreeViewExpander m_expander;

		// Token: 0x0400126A RID: 4714
		[SerializeField]
		private HorizontalLayoutGroup m_itemLayout;

		// Token: 0x0400126B RID: 4715
		private Toggle m_toggle;

		// Token: 0x0400126C RID: 4716
		private TreeView m_treeView;

		// Token: 0x0400126D RID: 4717
		private int m_indent;

		// Token: 0x0400126E RID: 4718
		private TreeViewItem m_parent;

		// Token: 0x0400126F RID: 4719
		private bool m_canExpand;

		// Token: 0x04001270 RID: 4720
		private bool m_isExpanded;
	}
}
