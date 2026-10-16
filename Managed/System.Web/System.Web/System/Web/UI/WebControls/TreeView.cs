using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays hierarchical data, such as a table of contents, in a tree structure.</summary>
	// Token: 0x0200042E RID: 1070
	[SupportsEventValidation]
	[ControlValueProperty("SelectedValue")]
	[DefaultEvent("SelectedNodeChanged")]
	[Designer("System.Web.UI.Design.WebControls.TreeViewDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TreeView : HierarchicalDataBoundControl, IPostBackEventHandler, IPostBackDataHandler, ICallbackEventHandler
	{
		// Token: 0x060030D9 RID: 12505 RVA: 0x0007EBC8 File Offset: 0x0007CDC8
		static TreeView()
		{
			TreeView.TreeNodeCheckChangedEvent = new object();
			TreeView.SelectedNodeChangedEvent = new object();
			TreeView.TreeNodeCollapsedEvent = new object();
			TreeView.TreeNodeDataBoundEvent = new object();
			TreeView.TreeNodeExpandedEvent = new object();
			TreeView.TreeNodePopulateEvent = new object();
			TreeView.imageStyles = new Hashtable();
			TreeView.imageStyles[TreeViewImageSet.Arrows] = new TreeView.ImageStyle("arrow_plus", "arrow_minus", "arrow_noexpand", null, null, null);
			TreeView.imageStyles[TreeViewImageSet.BulletedList] = new TreeView.ImageStyle(null, null, null, "dot_full", "dot_empty", "dot_full");
			TreeView.imageStyles[TreeViewImageSet.BulletedList2] = new TreeView.ImageStyle(null, null, null, "box_full", "box_empty", "box_full");
			TreeView.imageStyles[TreeViewImageSet.BulletedList3] = new TreeView.ImageStyle(null, null, null, "star_full", "star_empty", "star_full");
			TreeView.imageStyles[TreeViewImageSet.BulletedList4] = new TreeView.ImageStyle(null, null, null, "star_full", "star_empty", "dots");
			TreeView.imageStyles[TreeViewImageSet.Contacts] = new TreeView.ImageStyle("TreeView_plus", "TreeView_minus", "contact", null, null, null);
			TreeView.imageStyles[TreeViewImageSet.Events] = new TreeView.ImageStyle(null, null, null, "warning", "warning", "warning");
			TreeView.imageStyles[TreeViewImageSet.Inbox] = new TreeView.ImageStyle(null, null, null, "inbox", "inbox", "inbox");
			TreeView.imageStyles[TreeViewImageSet.Msdn] = new TreeView.ImageStyle("box_plus", "box_minus", "box_noexpand", null, null, null);
			TreeView.imageStyles[TreeViewImageSet.Simple] = new TreeView.ImageStyle(null, null, "box_full", null, null, null);
			TreeView.imageStyles[TreeViewImageSet.Simple2] = new TreeView.ImageStyle(null, null, "box_empty", null, null, null);
			TreeView.imageStyles[TreeViewImageSet.News] = new TreeView.ImageStyle("TreeView_plus", "TreeView_minus", "TreeView_noexpand", null, null, null);
			TreeView.imageStyles[TreeViewImageSet.Faq] = new TreeView.ImageStyle("TreeView_plus", "TreeView_minus", "TreeView_noexpand", null, null, null);
			TreeView.imageStyles[TreeViewImageSet.WindowsHelp] = new TreeView.ImageStyle("TreeView_plus", "TreeView_minus", "TreeView_noexpand", null, null, null);
			TreeView.imageStyles[TreeViewImageSet.XPFileExplorer] = new TreeView.ImageStyle("TreeView_plus", "TreeView_minus", "TreeView_noexpand", "folder", "file", "computer");
		}

		/// <summary>Occurs when a check box in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control changes state between posts to the server.</summary>
		// Token: 0x140000EE RID: 238
		// (add) Token: 0x060030DA RID: 12506 RVA: 0x0007EE77 File Offset: 0x0007D077
		// (remove) Token: 0x060030DB RID: 12507 RVA: 0x0007EE8A File Offset: 0x0007D08A
		public event TreeNodeEventHandler TreeNodeCheckChanged
		{
			add
			{
				base.Events.AddHandler(TreeView.TreeNodeCheckChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TreeView.TreeNodeCheckChangedEvent, value);
			}
		}

		/// <summary>Occurs when a node is selected in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		// Token: 0x140000EF RID: 239
		// (add) Token: 0x060030DC RID: 12508 RVA: 0x0007EE9D File Offset: 0x0007D09D
		// (remove) Token: 0x060030DD RID: 12509 RVA: 0x0007EEB0 File Offset: 0x0007D0B0
		public event EventHandler SelectedNodeChanged
		{
			add
			{
				base.Events.AddHandler(TreeView.SelectedNodeChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TreeView.SelectedNodeChangedEvent, value);
			}
		}

		/// <summary>Occurs when a node is collapsed in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		// Token: 0x140000F0 RID: 240
		// (add) Token: 0x060030DE RID: 12510 RVA: 0x0007EEC3 File Offset: 0x0007D0C3
		// (remove) Token: 0x060030DF RID: 12511 RVA: 0x0007EED6 File Offset: 0x0007D0D6
		public event TreeNodeEventHandler TreeNodeCollapsed
		{
			add
			{
				base.Events.AddHandler(TreeView.TreeNodeCollapsedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TreeView.TreeNodeCollapsedEvent, value);
			}
		}

		/// <summary>Occurs when a data item is bound to a node in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		// Token: 0x140000F1 RID: 241
		// (add) Token: 0x060030E0 RID: 12512 RVA: 0x0007EEE9 File Offset: 0x0007D0E9
		// (remove) Token: 0x060030E1 RID: 12513 RVA: 0x0007EEFC File Offset: 0x0007D0FC
		public event TreeNodeEventHandler TreeNodeDataBound
		{
			add
			{
				base.Events.AddHandler(TreeView.TreeNodeDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TreeView.TreeNodeDataBoundEvent, value);
			}
		}

		/// <summary>Occurs when a node is expanded in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		// Token: 0x140000F2 RID: 242
		// (add) Token: 0x060030E2 RID: 12514 RVA: 0x0007EF0F File Offset: 0x0007D10F
		// (remove) Token: 0x060030E3 RID: 12515 RVA: 0x0007EF22 File Offset: 0x0007D122
		public event TreeNodeEventHandler TreeNodeExpanded
		{
			add
			{
				base.Events.AddHandler(TreeView.TreeNodeExpandedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TreeView.TreeNodeExpandedEvent, value);
			}
		}

		/// <summary>Occurs when a node with its <see cref="P:System.Web.UI.WebControls.TreeNode.PopulateOnDemand" /> property set to true is expanded in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		// Token: 0x140000F3 RID: 243
		// (add) Token: 0x060030E4 RID: 12516 RVA: 0x0007EF35 File Offset: 0x0007D135
		// (remove) Token: 0x060030E5 RID: 12517 RVA: 0x0007EF48 File Offset: 0x0007D148
		public event TreeNodeEventHandler TreeNodePopulate
		{
			add
			{
				base.Events.AddHandler(TreeView.TreeNodePopulateEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TreeView.TreeNodePopulateEvent, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodeCheckChanged" /> event of the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.TreeNodeEventArgs" /> that contains event data. </param>
		// Token: 0x060030E6 RID: 12518 RVA: 0x0007EF5C File Offset: 0x0007D15C
		protected virtual void OnTreeNodeCheckChanged(TreeNodeEventArgs e)
		{
			if (base.Events != null)
			{
				TreeNodeEventHandler treeNodeEventHandler = (TreeNodeEventHandler)base.Events[TreeView.TreeNodeCheckChangedEvent];
				if (treeNodeEventHandler != null)
				{
					treeNodeEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.TreeView.SelectedNodeChanged" /> event of the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
		// Token: 0x060030E7 RID: 12519 RVA: 0x0007EF94 File Offset: 0x0007D194
		protected virtual void OnSelectedNodeChanged(EventArgs e)
		{
			if (base.Events != null)
			{
				EventHandler eventHandler = (EventHandler)base.Events[TreeView.SelectedNodeChangedEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodeCollapsed" /> event of the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.TreeNodeEventArgs" /> that contains event data. </param>
		// Token: 0x060030E8 RID: 12520 RVA: 0x0007EFCC File Offset: 0x0007D1CC
		protected virtual void OnTreeNodeCollapsed(TreeNodeEventArgs e)
		{
			if (base.Events != null)
			{
				TreeNodeEventHandler treeNodeEventHandler = (TreeNodeEventHandler)base.Events[TreeView.TreeNodeCollapsedEvent];
				if (treeNodeEventHandler != null)
				{
					treeNodeEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodeDataBound" /> event of the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.TreeNodeEventArgs" /> that contains event data. </param>
		// Token: 0x060030E9 RID: 12521 RVA: 0x0007F004 File Offset: 0x0007D204
		protected virtual void OnTreeNodeDataBound(TreeNodeEventArgs e)
		{
			if (base.Events != null)
			{
				TreeNodeEventHandler treeNodeEventHandler = (TreeNodeEventHandler)base.Events[TreeView.TreeNodeDataBoundEvent];
				if (treeNodeEventHandler != null)
				{
					treeNodeEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodeExpanded" /> event of the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.TreeNodeEventArgs" /> that contains event data. </param>
		// Token: 0x060030EA RID: 12522 RVA: 0x0007F03C File Offset: 0x0007D23C
		protected virtual void OnTreeNodeExpanded(TreeNodeEventArgs e)
		{
			if (base.Events != null)
			{
				TreeNodeEventHandler treeNodeEventHandler = (TreeNodeEventHandler)base.Events[TreeView.TreeNodeExpandedEvent];
				if (treeNodeEventHandler != null)
				{
					treeNodeEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodePopulate" /> event of the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.TreeNodeEventArgs" /> that contains event data. </param>
		// Token: 0x060030EB RID: 12523 RVA: 0x0007F074 File Offset: 0x0007D274
		protected virtual void OnTreeNodePopulate(TreeNodeEventArgs e)
		{
			if (base.Events != null)
			{
				TreeNodeEventHandler treeNodeEventHandler = (TreeNodeEventHandler)base.Events[TreeView.TreeNodePopulateEvent];
				if (treeNodeEventHandler != null)
				{
					treeNodeEventHandler(this, e);
				}
			}
		}

		/// <summary>Gets or sets the ToolTip for the image that is displayed for the collapsible node indicator.</summary>
		/// <returns>The ToolTip for the image displayed for the collapsible node indicator.</returns>
		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x060030EC RID: 12524 RVA: 0x0007F0AA File Offset: 0x0007D2AA
		// (set) Token: 0x060030ED RID: 12525 RVA: 0x0007F0C1 File Offset: 0x0007D2C1
		[Localizable(true)]
		public string CollapseImageToolTip
		{
			get
			{
				return this.ViewState.GetString("CollapseImageToolTip", "Collapse {0}");
			}
			set
			{
				this.ViewState["CollapseImageToolTip"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.TreeView" /> control automatically generates tree node bindings.</summary>
		/// <returns>true to have the <see cref="T:System.Web.UI.WebControls.TreeView" /> control automatically generate tree node bindings; otherwise, false. The default is true.</returns>
		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x060030EE RID: 12526 RVA: 0x0007F0D4 File Offset: 0x0007D2D4
		// (set) Token: 0x060030EF RID: 12527 RVA: 0x0007F0E7 File Offset: 0x0007D2E7
		[global::System.MonoTODO("Implement support for this")]
		[WebCategory("Behavior")]
		[WebSysDescription("Whether the tree will automatically generate bindings.")]
		[DefaultValue(true)]
		public bool AutoGenerateDataBindings
		{
			get
			{
				return this.ViewState.GetBool("AutoGenerateDataBindings", true);
			}
			set
			{
				this.ViewState["AutoGenerateDataBindings"] = value;
			}
		}

		/// <summary>Gets or sets the URL to a custom image for the collapsible node indicator.</summary>
		/// <returns>The URL to a custom image to display for collapsible nodes. The default is an empty string (""), which displays the default minus sign (-) image.</returns>
		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x060030F0 RID: 12528 RVA: 0x0007F0FF File Offset: 0x0007D2FF
		// (set) Token: 0x060030F1 RID: 12529 RVA: 0x0007F116 File Offset: 0x0007D316
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("The url of the image to show when a node can be collapsed.")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string CollapseImageUrl
		{
			get
			{
				return this.ViewState.GetString("CollapseImageUrl", string.Empty);
			}
			set
			{
				this.ViewState["CollapseImageUrl"] = value;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> objects that define the relationship between a data item and the node that it is binding to.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" /> that represents the relationship between a data item and the node that it is binding to.</returns>
		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x060030F2 RID: 12530 RVA: 0x0007F129 File Offset: 0x0007D329
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("Bindings for tree nodes.")]
		[Editor("System.Web.UI.Design.WebControls.TreeViewBindingsEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(null)]
		[WebCategory("Data")]
		public TreeNodeBindingCollection DataBindings
		{
			get
			{
				if (this.dataBindings == null)
				{
					this.dataBindings = new TreeNodeBindingCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.dataBindings).TrackViewState();
					}
				}
				return this.dataBindings;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.TreeView" /> control renders client-side script to handle expanding and collapsing events.</summary>
		/// <returns>true to render the client-side script on compatible browsers; otherwise, false. The default is true.</returns>
		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x060030F3 RID: 12531 RVA: 0x00045FAB File Offset: 0x000441AB
		// (set) Token: 0x060030F4 RID: 12532 RVA: 0x00045FBE File Offset: 0x000441BE
		[WebSysDescription("Whether the tree view can use client-side script to expand and collapse nodes.")]
		[Themeable(false)]
		[DefaultValue(true)]
		[WebCategory("Behavior")]
		public bool EnableClientScript
		{
			get
			{
				return this.ViewState.GetBool("EnableClientScript", true);
			}
			set
			{
				this.ViewState["EnableClientScript"] = value;
			}
		}

		/// <summary>Gets or sets the number of levels that are expanded when a <see cref="T:System.Web.UI.WebControls.TreeView" /> control is displayed for the first time.</summary>
		/// <returns>The depth to display when the <see cref="T:System.Web.UI.WebControls.TreeView" /> is initially displayed. The default is -1, which displays all the nodes.</returns>
		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x060030F5 RID: 12533 RVA: 0x0007F157 File Offset: 0x0007D357
		// (set) Token: 0x060030F6 RID: 12534 RVA: 0x0007F16A File Offset: 0x0007D36A
		[TypeConverter("System.Web.UI.WebControls.TreeView+TreeViewExpandDepthConverter, System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Behavior")]
		[WebSysDescription("The initial expand depth.")]
		[DefaultValue(-1)]
		public int ExpandDepth
		{
			get
			{
				return this.ViewState.GetInt("ExpandDepth", -1);
			}
			set
			{
				this.ViewState["ExpandDepth"] = value;
			}
		}

		/// <summary>Gets or sets the ToolTip for the image that is displayed for the expandable node indicator.</summary>
		/// <returns>The ToolTip for the image displayed for the expandable node indicator.</returns>
		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x060030F7 RID: 12535 RVA: 0x0007F182 File Offset: 0x0007D382
		// (set) Token: 0x060030F8 RID: 12536 RVA: 0x0007F199 File Offset: 0x0007D399
		[Localizable(true)]
		public string ExpandImageToolTip
		{
			get
			{
				return this.ViewState.GetString("ExpandImageToolTip", "Expand {0}");
			}
			set
			{
				this.ViewState["ExpandImageToolTip"] = value;
			}
		}

		/// <summary>Gets or sets the URL to a custom image for the expandable node indicator.</summary>
		/// <returns>The URL to a custom image to display for expandable nodes. The default is an empty string (""), which displays the default plus sign (+) image.</returns>
		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x060030F9 RID: 12537 RVA: 0x0007F1AC File Offset: 0x0007D3AC
		// (set) Token: 0x060030FA RID: 12538 RVA: 0x0007F1C3 File Offset: 0x0007D3C3
		[UrlProperty]
		[WebSysDescription("The url of the image to show when a node can be expanded.")]
		[WebCategory("Appearance")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string ExpandImageUrl
		{
			get
			{
				return this.ViewState.GetString("ExpandImageUrl", string.Empty);
			}
			set
			{
				this.ViewState["ExpandImageUrl"] = value;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object that allows you to set the appearance of a node when the mouse pointer is positioned over it.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> that represents the style of a node when the mouse pointer is positioned over it.</returns>
		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x060030FB RID: 12539 RVA: 0x0007F1D6 File Offset: 0x0007D3D6
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebCategory("Styles")]
		[DefaultValue(null)]
		public Style HoverNodeStyle
		{
			get
			{
				if (this.hoverNodeStyle == null)
				{
					this.hoverNodeStyle = new Style();
					if (base.IsTrackingViewState)
					{
						this.hoverNodeStyle.TrackViewState();
					}
				}
				return this.hoverNodeStyle;
			}
		}

		/// <summary>Gets or sets the group of images to use for the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TreeViewImageSet" /> values. The default is TreeViewImageSet.Custom.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified image set is not one of the <see cref="T:System.Web.UI.WebControls.TreeViewImageSet" /> values. </exception>
		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x060030FC RID: 12540 RVA: 0x0007F204 File Offset: 0x0007D404
		// (set) Token: 0x060030FD RID: 12541 RVA: 0x0007F217 File Offset: 0x0007D417
		[DefaultValue(TreeViewImageSet.Custom)]
		public TreeViewImageSet ImageSet
		{
			get
			{
				return (TreeViewImageSet)this.ViewState.GetInt("ImageSet", 0);
			}
			set
			{
				if (!Enum.IsDefined(typeof(TreeViewImageSet), value))
				{
					throw new ArgumentOutOfRangeException();
				}
				this.ViewState["ImageSet"] = value;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object that allows you to set the appearance of leaf nodes.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> that represents the style of the leaf nodes in the <see cref="T:System.Web.UI.WebControls.TreeView" />.</returns>
		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x060030FE RID: 12542 RVA: 0x0007F24C File Offset: 0x0007D44C
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[WebCategory("Styles")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TreeNodeStyle LeafNodeStyle
		{
			get
			{
				if (this.leafNodeStyle == null)
				{
					this.leafNodeStyle = new TreeNodeStyle();
					if (base.IsTrackingViewState)
					{
						this.leafNodeStyle.TrackViewState();
					}
				}
				return this.leafNodeStyle;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.Style" /> objects that represent the node styles at the individual levels of the tree.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.StyleCollection" /> that represents the node styles at the individual levels of the tree. </returns>
		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x060030FF RID: 12543 RVA: 0x0007F27A File Offset: 0x0007D47A
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Editor("System.Web.UI.Design.WebControls.TreeNodeStyleCollectionEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Styles")]
		public TreeNodeStyleCollection LevelStyles
		{
			get
			{
				if (this.levelStyles == null)
				{
					this.levelStyles = new TreeNodeStyleCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.levelStyles).TrackViewState();
					}
				}
				return this.levelStyles;
			}
		}

		/// <summary>Gets or sets the path to a folder that contains the line images that are used to connect child nodes to parent nodes.</summary>
		/// <returns>The path to a folder that contains the line images used to connect nodes. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeView.LineImagesFolder" /> property is not set.</returns>
		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x06003100 RID: 12544 RVA: 0x0007F2A8 File Offset: 0x0007D4A8
		// (set) Token: 0x06003101 RID: 12545 RVA: 0x0007F2BF File Offset: 0x0007D4BF
		[DefaultValue("")]
		public string LineImagesFolder
		{
			get
			{
				return this.ViewState.GetString("LineImagesFolder", string.Empty);
			}
			set
			{
				this.ViewState["LineImagesFolder"] = value;
			}
		}

		/// <summary>Gets or sets the maximum number of tree levels to bind to the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		/// <returns>The maximum number of tree levels to bind to the <see cref="T:System.Web.UI.WebControls.TreeView" /> control. The default is -1, which binds all the tree levels in the data source to the control.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than -1.</exception>
		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x06003102 RID: 12546 RVA: 0x0007F2D2 File Offset: 0x0007D4D2
		// (set) Token: 0x06003103 RID: 12547 RVA: 0x0007F2E5 File Offset: 0x0007D4E5
		[DefaultValue(-1)]
		public int MaxDataBindDepth
		{
			get
			{
				return this.ViewState.GetInt("MaxDataBindDepth", -1);
			}
			set
			{
				this.ViewState["MaxDataBindDepth"] = value;
			}
		}

		/// <summary>Gets or sets the indentation amount (in pixels) for the child nodes of the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		/// <returns>The amount of space (in pixels) between a child node's left edge and its parent node's left edge. The default is 20.</returns>
		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x06003104 RID: 12548 RVA: 0x0007F2FD File Offset: 0x0007D4FD
		// (set) Token: 0x06003105 RID: 12549 RVA: 0x0007F311 File Offset: 0x0007D511
		[DefaultValue(20)]
		public int NodeIndent
		{
			get
			{
				return this.ViewState.GetInt("NodeIndent", 20);
			}
			set
			{
				this.ViewState["NodeIndent"] = value;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.TreeNode" /> objects that represents the root nodes in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> that contains the root nodes in the <see cref="T:System.Web.UI.WebControls.TreeView" />.</returns>
		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x06003106 RID: 12550 RVA: 0x0007F329 File Offset: 0x0007D529
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Editor("System.Web.UI.Design.WebControls.TreeNodeCollectionEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(null)]
		[MergableProperty(false)]
		public TreeNodeCollection Nodes
		{
			get
			{
				if (this.nodes == null)
				{
					this.nodes = new TreeNodeCollection(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.nodes).TrackViewState();
					}
				}
				return this.nodes;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object that allows you to set the default appearance of the nodes in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> that represents the default style of a node.</returns>
		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x06003107 RID: 12551 RVA: 0x0007F358 File Offset: 0x0007D558
		[DefaultValue(null)]
		[WebCategory("Styles")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		public TreeNodeStyle NodeStyle
		{
			get
			{
				if (this.nodeStyle == null)
				{
					this.nodeStyle = new TreeNodeStyle();
					if (base.IsTrackingViewState)
					{
						this.nodeStyle.TrackViewState();
					}
				}
				return this.nodeStyle;
			}
		}

		/// <summary>Gets or sets a value indicating whether text wraps in a node when the node runs out of space.</summary>
		/// <returns>true to wrap the text; otherwise, false. The default is false.</returns>
		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x06003108 RID: 12552 RVA: 0x0007F386 File Offset: 0x0007D586
		// (set) Token: 0x06003109 RID: 12553 RVA: 0x0007F399 File Offset: 0x0007D599
		[DefaultValue(false)]
		public bool NodeWrap
		{
			get
			{
				return this.ViewState.GetBool("NodeWrap", false);
			}
			set
			{
				this.ViewState["NodeWrap"] = value;
			}
		}

		/// <summary>Gets or sets the URL to a custom image for the non-expandable node indicator.</summary>
		/// <returns>The URL to a custom image to display for non-expandable nodes. The default is an empty string (""), which displays the default blank image.</returns>
		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x0600310A RID: 12554 RVA: 0x0007F3B1 File Offset: 0x0007D5B1
		// (set) Token: 0x0600310B RID: 12555 RVA: 0x0007F3C8 File Offset: 0x0007D5C8
		[UrlProperty]
		[DefaultValue("")]
		[WebSysDescription("The url of the image to show for leaf nodes.")]
		[WebCategory("Appearance")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string NoExpandImageUrl
		{
			get
			{
				return this.ViewState.GetString("NoExpandImageUrl", string.Empty);
			}
			set
			{
				this.ViewState["NoExpandImageUrl"] = value;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object that allows you to set the appearance of parent nodes in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> that represents the style of the parent nodes in the <see cref="T:System.Web.UI.WebControls.TreeView" />.</returns>
		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x0600310C RID: 12556 RVA: 0x0007F3DB File Offset: 0x0007D5DB
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[WebCategory("Styles")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TreeNodeStyle ParentNodeStyle
		{
			get
			{
				if (this.parentNodeStyle == null)
				{
					this.parentNodeStyle = new TreeNodeStyle();
					if (base.IsTrackingViewState)
					{
						this.parentNodeStyle.TrackViewState();
					}
				}
				return this.parentNodeStyle;
			}
		}

		/// <summary>Gets or sets the character that is used to delimit the node values that are specified by the <see cref="P:System.Web.UI.WebControls.TreeNode.ValuePath" /> property.</summary>
		/// <returns>The character used to delimit the node values specified in the <see cref="P:System.Web.UI.WebControls.TreeNode.ValuePath" /> property. The default is a slash mark (/).</returns>
		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x0600310D RID: 12557 RVA: 0x0007F409 File Offset: 0x0007D609
		// (set) Token: 0x0600310E RID: 12558 RVA: 0x00068CB2 File Offset: 0x00066EB2
		[DefaultValue('/')]
		public char PathSeparator
		{
			get
			{
				return this.ViewState.GetChar("PathSeparator", '/');
			}
			set
			{
				this.ViewState["PathSeparator"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether node data is populated on demand from the client.</summary>
		/// <returns>true to populate tree node data on demand from the client; otherwise, false. The default is true.</returns>
		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x0600310F RID: 12559 RVA: 0x0007F41D File Offset: 0x0007D61D
		// (set) Token: 0x06003110 RID: 12560 RVA: 0x0007F430 File Offset: 0x0007D630
		[DefaultValue(true)]
		public bool PopulateNodesFromClient
		{
			get
			{
				return this.ViewState.GetBool("PopulateNodesFromClient", true);
			}
			set
			{
				this.ViewState["PopulateNodesFromClient"] = value;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object that allows you to set the appearance of the root node in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> that represents the style of the root node in the <see cref="T:System.Web.UI.WebControls.TreeView" />.</returns>
		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x06003111 RID: 12561 RVA: 0x0007F448 File Offset: 0x0007D648
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[WebCategory("Styles")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TreeNodeStyle RootNodeStyle
		{
			get
			{
				if (this.rootNodeStyle == null)
				{
					this.rootNodeStyle = new TreeNodeStyle();
					if (base.IsTrackingViewState)
					{
						this.rootNodeStyle.TrackViewState();
					}
				}
				return this.rootNodeStyle;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object that controls the appearance of the selected node in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> that represents the style of the selected node in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control. The default is null, which indicates that the <see cref="P:System.Web.UI.WebControls.TreeView.SelectedNodeStyle" /> property is not set.</returns>
		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x06003112 RID: 12562 RVA: 0x0007F476 File Offset: 0x0007D676
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[WebCategory("Styles")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TreeNodeStyle SelectedNodeStyle
		{
			get
			{
				if (this.selectedNodeStyle == null)
				{
					this.selectedNodeStyle = new TreeNodeStyle();
					if (base.IsTrackingViewState)
					{
						this.selectedNodeStyle.TrackViewState();
					}
				}
				return this.selectedNodeStyle;
			}
		}

		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x06003113 RID: 12563 RVA: 0x0007F4A4 File Offset: 0x0007D6A4
		private Style ControlLinkStyle
		{
			get
			{
				if (this.controlLinkStyle == null)
				{
					this.controlLinkStyle = new Style();
					this.controlLinkStyle.AlwaysRenderTextDecoration = true;
				}
				return this.controlLinkStyle;
			}
		}

		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x06003114 RID: 12564 RVA: 0x0007F4CB File Offset: 0x0007D6CB
		private Style NodeLinkStyle
		{
			get
			{
				if (this.nodeLinkStyle == null)
				{
					this.nodeLinkStyle = new Style();
				}
				return this.nodeLinkStyle;
			}
		}

		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x06003115 RID: 12565 RVA: 0x0007F4E6 File Offset: 0x0007D6E6
		private Style RootNodeLinkStyle
		{
			get
			{
				if (this.rootNodeLinkStyle == null)
				{
					this.rootNodeLinkStyle = new Style();
				}
				return this.rootNodeLinkStyle;
			}
		}

		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x06003116 RID: 12566 RVA: 0x0007F501 File Offset: 0x0007D701
		private Style ParentNodeLinkStyle
		{
			get
			{
				if (this.parentNodeLinkStyle == null)
				{
					this.parentNodeLinkStyle = new Style();
				}
				return this.parentNodeLinkStyle;
			}
		}

		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x06003117 RID: 12567 RVA: 0x0007F51C File Offset: 0x0007D71C
		private Style SelectedNodeLinkStyle
		{
			get
			{
				if (this.selectedNodeLinkStyle == null)
				{
					this.selectedNodeLinkStyle = new Style();
				}
				return this.selectedNodeLinkStyle;
			}
		}

		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x06003118 RID: 12568 RVA: 0x0007F537 File Offset: 0x0007D737
		private Style LeafNodeLinkStyle
		{
			get
			{
				if (this.leafNodeLinkStyle == null)
				{
					this.leafNodeLinkStyle = new Style();
				}
				return this.leafNodeLinkStyle;
			}
		}

		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x06003119 RID: 12569 RVA: 0x0007F552 File Offset: 0x0007D752
		private Style HoverNodeLinkStyle
		{
			get
			{
				if (this.hoverNodeLinkStyle == null)
				{
					this.hoverNodeLinkStyle = new Style();
				}
				return this.hoverNodeLinkStyle;
			}
		}

		/// <summary>Gets or sets a value indicating which node types will display a check box in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Web.UI.WebControls.TreeNodeTypes" /> values. The default is TreeNodeType.None.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The bitwise combination value is outside the range of the <see cref="T:System.Web.UI.WebControls.TreeNodeTypes" /> enumeration. </exception>
		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x0600311A RID: 12570 RVA: 0x0007F56D File Offset: 0x0007D76D
		// (set) Token: 0x0600311B RID: 12571 RVA: 0x0007F580 File Offset: 0x0007D780
		[DefaultValue(TreeNodeTypes.None)]
		public TreeNodeTypes ShowCheckBoxes
		{
			get
			{
				return (TreeNodeTypes)this.ViewState.GetInt("ShowCheckBoxes", 0);
			}
			set
			{
				if (value > TreeNodeTypes.All)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.ViewState["ShowCheckBoxes"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether expansion node indicators are displayed.</summary>
		/// <returns>true to show the expansion node indicators; otherwise, false. The default is true.</returns>
		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x0600311C RID: 12572 RVA: 0x0007F5A2 File Offset: 0x0007D7A2
		// (set) Token: 0x0600311D RID: 12573 RVA: 0x0007F5B5 File Offset: 0x0007D7B5
		[DefaultValue(true)]
		public bool ShowExpandCollapse
		{
			get
			{
				return this.ViewState.GetBool("ShowExpandCollapse", true);
			}
			set
			{
				this.ViewState["ShowExpandCollapse"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether lines connecting child nodes to parent nodes are displayed.</summary>
		/// <returns>true to display lines connecting nodes; otherwise, false. The default is false.</returns>
		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x0600311E RID: 12574 RVA: 0x0007F5CD File Offset: 0x0007D7CD
		// (set) Token: 0x0600311F RID: 12575 RVA: 0x0007F5E0 File Offset: 0x0007D7E0
		[DefaultValue(false)]
		public bool ShowLines
		{
			get
			{
				return this.ViewState.GetBool("ShowLines", false);
			}
			set
			{
				this.ViewState["ShowLines"] = value;
			}
		}

		/// <summary>Gets or sets a value that is used to render alternate text for screen readers to skip the content for the control. </summary>
		/// <returns>A string that the <see cref="T:System.Web.UI.WebControls.TreeView" /> renders as alternate text with an invisible image as a hint to screen readers. The default is "Skip Navigation Links." </returns>
		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x06003120 RID: 12576 RVA: 0x0007F5F8 File Offset: 0x0007D7F8
		// (set) Token: 0x06003121 RID: 12577 RVA: 0x00069335 File Offset: 0x00067535
		[Localizable(true)]
		public string SkipLinkText
		{
			get
			{
				return this.ViewState.GetString("SkipLinkText", "Skip Navigation Links.");
			}
			set
			{
				this.ViewState["SkipLinkText"] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object that represents the selected node in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TreeNode" /> that represents the selected node in the <see cref="T:System.Web.UI.WebControls.TreeView" />.</returns>
		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x06003122 RID: 12578 RVA: 0x0007F60F File Offset: 0x0007D80F
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TreeNode SelectedNode
		{
			get
			{
				return this.selectedNode;
			}
		}

		/// <summary>Gets the value of the selected node.</summary>
		/// <returns>The value of the selected node.</returns>
		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x06003123 RID: 12579 RVA: 0x0007F617 File Offset: 0x0007D817
		[Browsable(false)]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string SelectedValue
		{
			get
			{
				if (this.selectedNode == null)
				{
					return string.Empty;
				}
				return this.selectedNode.Value;
			}
		}

		/// <summary>Gets or sets the target window or frame in which to display the Web page content that is associated with a node.</summary>
		/// <returns>The target window or frame in which to display the linked Web page content. Values must begin with a letter in the range of A through Z (case insensitive), except for certain special values that begin with an underscore, as shown in the following table.Target value Renders the content in _blankA new window without frames. _parentThe immediate frameset parent. _searchThe search pane._selfThe frame with focus. _topThe full window without frames. NoteCheck your browser documentation to determine if the _search value is supported.  For example, Microsoft Internet Explorer 5.0 and later supports the _search target value.The default is an empty string (""), which refreshes the window or frame with focus.</returns>
		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x06003124 RID: 12580 RVA: 0x00047499 File Offset: 0x00045699
		// (set) Token: 0x06003125 RID: 12581 RVA: 0x000444A2 File Offset: 0x000426A2
		[DefaultValue("")]
		public string Target
		{
			get
			{
				return this.ViewState.GetString("Target", string.Empty);
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control is rendered as UI on the page. </summary>
		/// <returns>true, if the control is visible on the page; otherwise, false. </returns>
		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x06003126 RID: 12582 RVA: 0x00034DBA File Offset: 0x00032FBA
		// (set) Token: 0x06003127 RID: 12583 RVA: 0x00034DC2 File Offset: 0x00032FC2
		[global::System.MonoTODO("why override?")]
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.TreeNode" /> objects that represent the nodes in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control that display a selected check box.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> that contains the nodes in the <see cref="T:System.Web.UI.WebControls.TreeView" /> that display a selected check box.</returns>
		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x06003128 RID: 12584 RVA: 0x0007F634 File Offset: 0x0007D834
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TreeNodeCollection CheckedNodes
		{
			get
			{
				TreeNodeCollection treeNodeCollection = new TreeNodeCollection();
				this.FindCheckedNodes(this.Nodes, treeNodeCollection);
				return treeNodeCollection;
			}
		}

		// Token: 0x06003129 RID: 12585 RVA: 0x0007F658 File Offset: 0x0007D858
		private void FindCheckedNodes(TreeNodeCollection nodeList, TreeNodeCollection result)
		{
			foreach (object obj in nodeList)
			{
				TreeNode treeNode = (TreeNode)obj;
				if (treeNode.Checked)
				{
					result.Add(treeNode, false);
				}
				this.FindCheckedNodes(treeNode.ChildNodes, result);
			}
		}

		/// <summary>Opens every node in the tree.</summary>
		// Token: 0x0600312A RID: 12586 RVA: 0x0007F6C4 File Offset: 0x0007D8C4
		public void ExpandAll()
		{
			foreach (object obj in this.Nodes)
			{
				((TreeNode)obj).ExpandAll();
			}
		}

		/// <summary>Closes every node in the tree.</summary>
		// Token: 0x0600312B RID: 12587 RVA: 0x0007F71C File Offset: 0x0007D91C
		public void CollapseAll()
		{
			foreach (object obj in this.Nodes)
			{
				((TreeNode)obj).CollapseAll();
			}
		}

		/// <summary>Retrieves the <see cref="T:System.Web.UI.WebControls.TreeNode" /> object in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control at the specified value path.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.TreeNode" /> at the specified value path.</returns>
		/// <param name="valuePath">The value path of a node. </param>
		// Token: 0x0600312C RID: 12588 RVA: 0x0007F774 File Offset: 0x0007D974
		public TreeNode FindNode(string valuePath)
		{
			if (valuePath == null)
			{
				throw new ArgumentNullException("valuePath");
			}
			string[] array = valuePath.Split(this.PathSeparator, StringSplitOptions.None);
			int num = 0;
			TreeNodeCollection childNodes = this.Nodes;
			bool flag = true;
			while (childNodes.Count > 0 && flag)
			{
				flag = false;
				foreach (object obj in childNodes)
				{
					TreeNode treeNode = (TreeNode)obj;
					if (treeNode.Value == array[num])
					{
						if (++num == array.Length)
						{
							return treeNode;
						}
						childNodes = treeNode.ChildNodes;
						flag = true;
						break;
					}
				}
			}
			return null;
		}

		// Token: 0x0600312D RID: 12589 RVA: 0x0007F834 File Offset: 0x0007DA34
		private TreeView.ImageStyle GetImageStyle()
		{
			if (this.ImageSet != TreeViewImageSet.Custom)
			{
				return (TreeView.ImageStyle)TreeView.imageStyles[this.ImageSet];
			}
			return null;
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value for the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		/// <returns>Always returns a <see cref="F:System.Web.UI.HtmlTextWriterTag.Div" /> value.</returns>
		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x0600312E RID: 12590 RVA: 0x0006C6CF File Offset: 0x0006A8CF
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		/// <summary>Returns a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> class. The <see cref="M:System.Web.UI.WebControls.TreeView.CreateNode" /> is a helper method.</summary>
		/// <returns>A new instance of the <see cref="T:System.Web.UI.WebControls.TreeNode" />.</returns>
		// Token: 0x0600312F RID: 12591 RVA: 0x0007F85A File Offset: 0x0007DA5A
		protected internal virtual TreeNode CreateNode()
		{
			return new TreeNode(this);
		}

		/// <summary>Calls the <see cref="M:System.Web.UI.WebControls.BaseDataBoundControl.DataBind" /> method of the base class. </summary>
		// Token: 0x06003130 RID: 12592 RVA: 0x00069D6E File Offset: 0x00067F6E
		public sealed override void DataBind()
		{
			base.DataBind();
		}

		/// <summary>Allows a derived class to set whether the specified <see cref="T:System.Web.UI.WebControls.TreeNode" /> control is data-bound.</summary>
		/// <param name="node">The <see cref="T:System.Web.UI.WebControls.TreeNode" /> to set. </param>
		/// <param name="dataBound">true to set the node as data-bound; otherwise, false. </param>
		// Token: 0x06003131 RID: 12593 RVA: 0x0007F862 File Offset: 0x0007DA62
		protected void SetNodeDataBound(TreeNode node, bool dataBound)
		{
			node.SetDataBound(dataBound);
		}

		/// <summary>Allows a derived class to set the data path for the specified <see cref="T:System.Web.UI.WebControls.TreeNode" /> control.</summary>
		/// <param name="node">The <see cref="T:System.Web.UI.WebControls.TreeNode" /> to set. </param>
		/// <param name="dataPath">The data path for the <see cref="T:System.Web.UI.WebControls.TreeNode" />. </param>
		// Token: 0x06003132 RID: 12594 RVA: 0x0007F86B File Offset: 0x0007DA6B
		protected void SetNodeDataPath(TreeNode node, string dataPath)
		{
			node.SetDataPath(dataPath);
		}

		/// <summary>Allows a derived class to set the data item for the specified <see cref="T:System.Web.UI.WebControls.TreeNode" /> control.</summary>
		/// <param name="node">The <see cref="T:System.Web.UI.WebControls.TreeNode" /> to set. </param>
		/// <param name="dataItem">The data item for the <see cref="T:System.Web.UI.WebControls.TreeNode" />. </param>
		// Token: 0x06003133 RID: 12595 RVA: 0x0007F874 File Offset: 0x0007DA74
		protected void SetNodeDataItem(TreeNode node, object dataItem)
		{
			node.SetDataItem(dataItem);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003134 RID: 12596 RVA: 0x00044036 File Offset: 0x00042236
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
		}

		// Token: 0x06003135 RID: 12597 RVA: 0x0007F87D File Offset: 0x0007DA7D
		internal void SetSelectedNode(TreeNode node, bool loading)
		{
			if (this.selectedNode == node)
			{
				return;
			}
			if (this.selectedNode != null)
			{
				this.selectedNode.SelectedFlag = false;
			}
			this.selectedNode = node;
			if (!loading)
			{
				this.OnSelectedNodeChanged(new TreeNodeEventArgs(this.selectedNode));
			}
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x0007F8B8 File Offset: 0x0007DAB8
		internal void NotifyCheckChanged(TreeNode node)
		{
			this.OnTreeNodeCheckChanged(new TreeNodeEventArgs(node));
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x0007F8C8 File Offset: 0x0007DAC8
		internal void NotifyExpandedChanged(TreeNode node)
		{
			if (node.Expanded != null && node.Expanded.Value)
			{
				this.OnTreeNodeExpanded(new TreeNodeEventArgs(node));
				return;
			}
			if (node.Expanded != null && node.IsParentNode)
			{
				this.OnTreeNodeCollapsed(new TreeNodeEventArgs(node));
			}
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x0007F926 File Offset: 0x0007DB26
		internal void NotifyPopulateRequired(TreeNode node)
		{
			this.OnTreeNodePopulate(new TreeNodeEventArgs(node));
		}

		/// <summary>Tracks view-state changes to the <see cref="T:System.Web.UI.WebControls.TreeView" /> control so that they can be stored in the <see cref="T:System.Web.UI.StateBag" /> object for the control. This <see cref="T:System.Web.UI.StateBag" /> is accessible through the <see cref="P:System.Web.UI.Control.ViewState" /> property.</summary>
		// Token: 0x06003139 RID: 12601 RVA: 0x0007F934 File Offset: 0x0007DB34
		protected override void TrackViewState()
		{
			this.EnsureDataBound();
			base.TrackViewState();
			if (this.hoverNodeStyle != null)
			{
				this.hoverNodeStyle.TrackViewState();
			}
			if (this.leafNodeStyle != null)
			{
				this.leafNodeStyle.TrackViewState();
			}
			if (this.levelStyles != null && this.levelStyles.Count > 0)
			{
				((IStateManager)this.levelStyles).TrackViewState();
			}
			if (this.nodeStyle != null)
			{
				this.nodeStyle.TrackViewState();
			}
			if (this.parentNodeStyle != null)
			{
				this.parentNodeStyle.TrackViewState();
			}
			if (this.rootNodeStyle != null)
			{
				this.rootNodeStyle.TrackViewState();
			}
			if (this.selectedNodeStyle != null)
			{
				this.selectedNodeStyle.TrackViewState();
			}
			if (this.dataBindings != null)
			{
				((IStateManager)this.dataBindings).TrackViewState();
			}
			if (this.nodes != null)
			{
				((IStateManager)this.nodes).TrackViewState();
			}
		}

		/// <summary>Saves the state of the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		/// <returns>The server control's current view state; otherwise, null, if there is no view state associated with the control.</returns>
		// Token: 0x0600313A RID: 12602 RVA: 0x0007FA08 File Offset: 0x0007DC08
		protected override object SaveViewState()
		{
			object[] array = new object[]
			{
				base.SaveViewState(),
				(this.hoverNodeStyle == null) ? null : this.hoverNodeStyle.SaveViewState(),
				(this.leafNodeStyle == null) ? null : this.leafNodeStyle.SaveViewState(),
				(this.levelStyles == null) ? null : ((IStateManager)this.levelStyles).SaveViewState(),
				(this.nodeStyle == null) ? null : this.nodeStyle.SaveViewState(),
				(this.parentNodeStyle == null) ? null : this.parentNodeStyle.SaveViewState(),
				(this.rootNodeStyle == null) ? null : this.rootNodeStyle.SaveViewState(),
				(this.selectedNodeStyle == null) ? null : this.selectedNodeStyle.SaveViewState(),
				(this.dataBindings == null) ? null : ((IStateManager)this.dataBindings).SaveViewState(),
				(this.nodes == null) ? null : ((IStateManager)this.nodes).SaveViewState()
			};
			for (int i = array.Length - 1; i >= 0; i--)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		/// <param name="state">A object that contains the saved view state values for the control. </param>
		// Token: 0x0600313B RID: 12603 RVA: 0x0007FB20 File Offset: 0x0007DD20
		protected override void LoadViewState(object state)
		{
			if (state == null)
			{
				return;
			}
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				this.HoverNodeStyle.LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				this.LeafNodeStyle.LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				((IStateManager)this.LevelStyles).LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				this.NodeStyle.LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				this.ParentNodeStyle.LoadViewState(array[5]);
			}
			if (array[6] != null)
			{
				this.RootNodeStyle.LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				this.SelectedNodeStyle.LoadViewState(array[7]);
			}
			if (array[8] != null)
			{
				((IStateManager)this.DataBindings).LoadViewState(array[8]);
			}
			if (array[9] != null)
			{
				((IStateManager)this.Nodes).LoadViewState(array[9]);
			}
		}

		/// <summary>Enables the <see cref="T:System.Web.UI.WebControls.TreeView" /> control to process an event that is raised when a form is posted to the server. The <see cref="M:System.Web.UI.WebControls.TreeView.RaisePostBackEvent(System.String)" /> method is a helper method for the <see cref="M:System.Web.UI.WebControls.TreeView.System#Web#UI#ICallbackEventHandler#RaiseCallbackEvent(System.String)" /> method.</summary>
		/// <param name="eventArgument">A string that represents an optional event argument to pass to the event handler. </param>
		// Token: 0x0600313C RID: 12604 RVA: 0x0007FBF0 File Offset: 0x0007DDF0
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			string[] array = eventArgument.Split('|', StringSplitOptions.None);
			TreeNode treeNode = this.FindNodeByPos(array[1]);
			if (treeNode == null)
			{
				return;
			}
			if (array[0] == "sel")
			{
				this.HandleSelectEvent(treeNode);
				return;
			}
			if (array[0] == "ec")
			{
				this.HandleExpandCollapseEvent(treeNode);
			}
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x0007FC50 File Offset: 0x0007DE50
		private void HandleSelectEvent(TreeNode node)
		{
			switch (node.SelectAction)
			{
			case TreeNodeSelectAction.Select:
				node.Select();
				return;
			case TreeNodeSelectAction.Expand:
				node.Expand();
				return;
			case TreeNodeSelectAction.SelectExpand:
				node.Select();
				node.Expand();
				return;
			default:
				return;
			}
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x0007FC91 File Offset: 0x0007DE91
		private void HandleExpandCollapseEvent(TreeNode node)
		{
			node.ToggleExpandState();
		}

		/// <summary>Signals the <see cref="T:System.Web.UI.WebControls.TreeView" /> control to notify the ASP.NET application that the state of the control has changed.</summary>
		// Token: 0x0600313F RID: 12607 RVA: 0x00003946 File Offset: 0x00001B46
		protected virtual void RaisePostDataChangedEvent()
		{
		}

		// Token: 0x06003140 RID: 12608 RVA: 0x0007FC9C File Offset: 0x0007DE9C
		private TreeNode MakeNodeTree(string[] args)
		{
			string[] array = args[0].Split('_', StringSplitOptions.None);
			TreeNode treeNode = null;
			foreach (string text in array)
			{
				int num = int.Parse(text);
				TreeNode treeNode2 = new TreeNode(text);
				if (treeNode != null)
				{
					treeNode.ChildNodes.Add(treeNode2);
					treeNode2.Index = num;
				}
				treeNode = treeNode2;
			}
			treeNode.Value = args[1].Replace("U+007C", "|");
			treeNode.ImageUrl = args[2].Replace("U+007C", "|");
			treeNode.NavigateUrl = args[3].Replace("U+007C", "|");
			treeNode.Target = args[4].Replace("U+007C", "|");
			treeNode.Tree = this;
			this.NotifyPopulateRequired(treeNode);
			return treeNode;
		}

		/// <summary>Raises the callback event using the specified arguments. </summary>
		/// <param name="eventArgument">A string that represents an optional event argument to pass to the event handler.</param>
		// Token: 0x06003141 RID: 12609 RVA: 0x0007FD60 File Offset: 0x0007DF60
		protected virtual void RaiseCallbackEvent(string eventArgument)
		{
			string[] array = eventArgument.Split('|', StringSplitOptions.None);
			base.RequiresDataBinding = true;
			this.EnsureDataBound();
			TreeNode treeNode = this.MakeNodeTree(array);
			ArrayList arrayList = new ArrayList();
			for (TreeNode treeNode2 = treeNode; treeNode2 != null; treeNode2 = treeNode2.Parent)
			{
				int num = ((treeNode2.Parent != null) ? treeNode2.Parent.ChildNodes.Count : this.Nodes.Count);
				arrayList.Insert(0, (treeNode2.Index < num - 1) ? this : null);
			}
			StringWriter stringWriter = new StringWriter();
			HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
			this.EnsureStylesPrepared();
			treeNode.Expanded = new bool?(true);
			int count = treeNode.ChildNodes.Count;
			for (int i = 0; i < count; i++)
			{
				this.RenderNode(htmlTextWriter, treeNode.ChildNodes[i], treeNode.Depth + 1, arrayList, true, i < count - 1);
			}
			string text = stringWriter.ToString();
			this.callbackResult = ((text.Length > 0) ? text : "*");
		}

		/// <summary>Returns the result of a callback event that targets a control.</summary>
		/// <returns>The results of the callback.</returns>
		// Token: 0x06003142 RID: 12610 RVA: 0x0007FE67 File Offset: 0x0007E067
		protected virtual string GetCallbackResult()
		{
			return this.callbackResult;
		}

		/// <summary>Enables the <see cref="T:System.Web.UI.WebControls.TreeView" /> control to process an event that is raised when a form is posted to the server.</summary>
		/// <param name="eventArgument">A string that represents an optional event argument to pass to the event handler. </param>
		// Token: 0x06003143 RID: 12611 RVA: 0x0007FE6F File Offset: 0x0007E06F
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		/// <summary>Processes postback data for the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		/// <returns>true, if the <see cref="T:System.Web.UI.WebControls.TreeView" /> control's state changes as a result of the postback event; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control. </param>
		/// <param name="postCollection">The collection of all incoming name values. </param>
		// Token: 0x06003144 RID: 12612 RVA: 0x0007FE78 File Offset: 0x0007E078
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>Signals the <see cref="T:System.Web.UI.WebControls.TreeView" /> control to notify the ASP.NET application that the state of the control has changed.</summary>
		// Token: 0x06003145 RID: 12613 RVA: 0x0007FE82 File Offset: 0x0007E082
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		/// <summary>Raises the callback event using the specified arguments.</summary>
		/// <param name="eventArgument">A string that represents an optional event argument to pass to the event handler.</param>
		// Token: 0x06003146 RID: 12614 RVA: 0x0007FE8A File Offset: 0x0007E08A
		void ICallbackEventHandler.RaiseCallbackEvent(string eventArgs)
		{
			this.RaiseCallbackEvent(eventArgs);
		}

		/// <summary>Returns the result of a callback event that targets a control.</summary>
		/// <returns>The results of the callback.</returns>
		// Token: 0x06003147 RID: 12615 RVA: 0x0007FE93 File Offset: 0x0007E093
		string ICallbackEventHandler.GetCallbackResult()
		{
			return this.GetCallbackResult();
		}

		/// <summary>Creates a collection to store child controls.</summary>
		/// <returns>Always returns an <see cref="T:System.Web.UI.EmptyControlCollection" />.</returns>
		// Token: 0x06003148 RID: 12616 RVA: 0x0002FDF9 File Offset: 0x0002DFF9
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		/// <summary>Creates all the nodes based on the data source.</summary>
		// Token: 0x06003149 RID: 12617 RVA: 0x0007FE9C File Offset: 0x0007E09C
		protected internal override void PerformDataBinding()
		{
			base.PerformDataBinding();
			this.InitializeDataBindings();
			HierarchicalDataSourceView data = this.GetData(string.Empty);
			if (data == null)
			{
				return;
			}
			this.Nodes.Clear();
			IHierarchicalEnumerable hierarchicalEnumerable = data.Select();
			this.FillBoundChildrenRecursive(hierarchicalEnumerable, this.Nodes);
		}

		// Token: 0x0600314A RID: 12618 RVA: 0x0007FEE4 File Offset: 0x0007E0E4
		private void FillBoundChildrenRecursive(IHierarchicalEnumerable hEnumerable, TreeNodeCollection nodeCollection)
		{
			if (hEnumerable == null)
			{
				return;
			}
			foreach (object obj in hEnumerable)
			{
				IHierarchyData hierarchyData = hEnumerable.GetHierarchyData(obj);
				TreeNode treeNode = new TreeNode();
				nodeCollection.Add(treeNode);
				treeNode.Bind(hierarchyData);
				this.OnTreeNodeDataBound(new TreeNodeEventArgs(treeNode));
				if ((this.MaxDataBindDepth < 0 || treeNode.Depth != this.MaxDataBindDepth) && hierarchyData != null && hierarchyData.HasChildren)
				{
					IHierarchicalEnumerable children = hierarchyData.GetChildren();
					this.FillBoundChildrenRecursive(children, treeNode.ChildNodes);
				}
			}
		}

		/// <summary>Processes postback data for the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		/// <returns>true, if the <see cref="T:System.Web.UI.WebControls.TreeView" /> control's state changes as a result of the postback event; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control. </param>
		/// <param name="postCollection">The collection of all incoming name values. </param>
		// Token: 0x0600314B RID: 12619 RVA: 0x0007FF94 File Offset: 0x0007E194
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			bool flag = false;
			if (this.EnableClientScript && this.PopulateNodesFromClient)
			{
				string text = postCollection[this.ClientID + "_PopulatedStates"];
				if (text != null)
				{
					foreach (string text2 in text.Split(TreeView.postDataSplitChars, StringSplitOptions.RemoveEmptyEntries))
					{
						TreeNode treeNode = this.FindNodeByPos(text2);
						if (treeNode != null && treeNode.PopulateOnDemand && !treeNode.Populated)
						{
							Page page = this.Page;
							if (page != null && page.IsCallback)
							{
								treeNode.Populated = true;
							}
							else
							{
								treeNode.Populate();
							}
						}
					}
				}
				flag = true;
			}
			this.UnsetCheckStates(this.Nodes, postCollection);
			this.SetCheckStates(postCollection);
			if (this.EnableClientScript)
			{
				string text3 = postCollection[this.ClientID + "_ExpandStates"];
				if (text3 != null)
				{
					string[] array2 = text3.Split(TreeView.postDataSplitChars, StringSplitOptions.RemoveEmptyEntries);
					this.UnsetExpandStates(this.Nodes, array2);
					this.SetExpandStates(array2);
				}
				else
				{
					this.UnsetExpandStates(this.Nodes, new string[0]);
				}
				flag = true;
			}
			return flag;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
		// Token: 0x0600314C RID: 12620 RVA: 0x000800B0 File Offset: 0x0007E2B0
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			Page page = this.Page;
			if (page != null)
			{
				if (base.IsEnabled)
				{
					page.RegisterRequiresPostBack(this);
				}
				if (this.EnableClientScript && !page.ClientScript.IsClientScriptIncludeRegistered(typeof(TreeView), "TreeView.js"))
				{
					string webResourceUrl = page.ClientScript.GetWebResourceUrl(typeof(TreeView), "TreeView.js");
					page.ClientScript.RegisterClientScriptInclude(typeof(TreeView), "TreeView.js", webResourceUrl);
				}
			}
			string text = this.ClientID + "_data";
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("var {0} = new Object ();\n{0}.treeId = {1};\n{0}.uid = {2};\n{0}.showImage = {3};\n", new object[]
			{
				text,
				ClientScriptManager.GetScriptLiteral(this.ClientID),
				ClientScriptManager.GetScriptLiteral(this.UniqueID),
				ClientScriptManager.GetScriptLiteral(this.ShowExpandCollapse)
			});
			if (this.ShowExpandCollapse)
			{
				TreeView.ImageStyle imageStyle = this.GetImageStyle();
				stringBuilder.AppendFormat("{0}.expandImage = {1};\n{0}.collapseImage = {2};\n", text, ClientScriptManager.GetScriptLiteral(this.GetNodeImageUrl("plus", imageStyle)), ClientScriptManager.GetScriptLiteral(this.GetNodeImageUrl("minus", imageStyle)));
				if (this.PopulateNodesFromClient)
				{
					stringBuilder.AppendFormat("{0}.noExpandImage = {1};\n", text, ClientScriptManager.GetScriptLiteral(this.GetNodeImageUrl("noexpand", imageStyle)));
				}
			}
			if (page != null)
			{
				stringBuilder.AppendFormat("{0}.form = {1};\n{0}.PopulateNode = function (nodeId, nodeValue, nodeImageUrl, nodeNavigateUrl, nodeTarget) {{\n\t{2}.__theFormPostData = \"\";\n\t{2}.__theFormPostCollection = new Array ();\n\t{2}.WebForm_InitCallback ();\n\tTreeView_PopulateNode (this.uid, this.treeId, nodeId, nodeValue, nodeImageUrl, nodeNavigateUrl, nodeTarget)\n}};\n", text, page.theForm, page.WebFormScriptReference);
				stringBuilder.AppendFormat("{0}.populateFromClient = {1};\n{0}.expandAlt = {2};\n{0}.collapseAlt = {3};\n", new object[]
				{
					text,
					ClientScriptManager.GetScriptLiteral(this.PopulateNodesFromClient),
					ClientScriptManager.GetScriptLiteral(this.GetNodeImageToolTip(true, null)),
					ClientScriptManager.GetScriptLiteral(this.GetNodeImageToolTip(false, null))
				});
				if (!page.IsPostBack)
				{
					this.SetNodesExpandedToDepthRecursive(this.Nodes);
				}
				bool enableClientScript = this.EnableClientScript;
				if (enableClientScript)
				{
					page.ClientScript.RegisterHiddenField(this.ClientID + "_ExpandStates", this.GetExpandStates());
					page.ClientScript.RegisterWebFormClientScript();
				}
				if (enableClientScript && this.PopulateNodesFromClient)
				{
					page.ClientScript.RegisterHiddenField(this.ClientID + "_PopulatedStates", "|");
				}
				this.EnsureStylesPrepared();
				if (this.hoverNodeStyle != null)
				{
					if (page.Header == null)
					{
						throw new InvalidOperationException("Using TreeView.HoverNodeStyle requires Page.Header to be non-null (e.g. <head runat=\"server\" />).");
					}
					this.RegisterStyle(this.HoverNodeStyle, this.HoverNodeLinkStyle);
					stringBuilder.AppendFormat("{0}.hoverClass = {1};\n{0}.hoverLinkClass = {2};\n", text, ClientScriptManager.GetScriptLiteral(this.HoverNodeStyle.RegisteredCssClass), ClientScriptManager.GetScriptLiteral(this.HoverNodeLinkStyle.RegisteredCssClass));
				}
				page.ClientScript.RegisterStartupScript(typeof(TreeView), this.UniqueID, stringBuilder.ToString(), true);
			}
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x0008035A File Offset: 0x0007E55A
		private void EnsureStylesPrepared()
		{
			if (this.stylesPrepared)
			{
				return;
			}
			this.stylesPrepared = true;
			this.PrepareStyles();
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x00080374 File Offset: 0x0007E574
		private void PrepareStyles()
		{
			this.ControlLinkStyle.CopyTextStylesFrom(base.ControlStyle);
			this.RegisterStyle(this.ControlLinkStyle);
			if (this.nodeStyle != null)
			{
				this.RegisterStyle(this.NodeStyle, this.NodeLinkStyle);
			}
			if (this.rootNodeStyle != null)
			{
				this.RegisterStyle(this.RootNodeStyle, this.RootNodeLinkStyle);
			}
			if (this.parentNodeStyle != null)
			{
				this.RegisterStyle(this.ParentNodeStyle, this.ParentNodeLinkStyle);
			}
			if (this.leafNodeStyle != null)
			{
				this.RegisterStyle(this.LeafNodeStyle, this.LeafNodeLinkStyle);
			}
			if (this.levelStyles != null && this.levelStyles.Count > 0)
			{
				this.levelLinkStyles = new List<Style>(this.levelStyles.Count);
				foreach (object obj in this.levelStyles)
				{
					Style style = (Style)obj;
					Style style2 = new Style();
					this.levelLinkStyles.Add(style2);
					this.RegisterStyle(style, style2);
				}
			}
			if (this.selectedNodeStyle != null)
			{
				this.RegisterStyle(this.SelectedNodeStyle, this.SelectedNodeLinkStyle);
			}
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x000804AC File Offset: 0x0007E6AC
		private void SetNodesExpandedToDepthRecursive(TreeNodeCollection nodes)
		{
			foreach (object obj in nodes)
			{
				TreeNode treeNode = (TreeNode)obj;
				if (treeNode.Expanded == null && (this.ExpandDepth < 0 || treeNode.Depth < this.ExpandDepth))
				{
					treeNode.Expanded = new bool?(true);
				}
				this.SetNodesExpandedToDepthRecursive(treeNode.ChildNodes);
			}
		}

		// Token: 0x06003150 RID: 12624 RVA: 0x00080538 File Offset: 0x0007E738
		private string IncrementStyleClassName()
		{
			this.registeredStylesCounter++;
			return this.ClientID + "_" + this.registeredStylesCounter.ToString();
		}

		// Token: 0x06003151 RID: 12625 RVA: 0x00080563 File Offset: 0x0007E763
		private void RegisterStyle(Style baseStyle, Style linkStyle)
		{
			linkStyle.CopyTextStylesFrom(baseStyle);
			linkStyle.BorderStyle = BorderStyle.None;
			linkStyle.AddCssClass(baseStyle.CssClass);
			baseStyle.Font.Reset();
			this.RegisterStyle(linkStyle);
			this.RegisterStyle(baseStyle);
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x00080598 File Offset: 0x0007E798
		private void RegisterStyle(Style baseStyle)
		{
			if (this.Page.Header == null)
			{
				return;
			}
			string text = this.IncrementStyleClassName().Trim('_');
			baseStyle.SetRegisteredCssClass(text);
			this.Page.Header.StyleSheet.CreateStyleRule(baseStyle, this, "." + text);
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x000694C0 File Offset: 0x000676C0
		private string GetBindingKey(string dataMember, int depth)
		{
			return dataMember + " " + depth.ToString();
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x000805EC File Offset: 0x0007E7EC
		private void InitializeDataBindings()
		{
			if (this.dataBindings != null && this.dataBindings.Count > 0)
			{
				this.bindings = new Hashtable();
				using (IEnumerator enumerator = this.dataBindings.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						TreeNodeBinding treeNodeBinding = (TreeNodeBinding)obj;
						string bindingKey = this.GetBindingKey(treeNodeBinding.DataMember, treeNodeBinding.Depth);
						if (!this.bindings.ContainsKey(bindingKey))
						{
							this.bindings[bindingKey] = treeNodeBinding;
						}
					}
					return;
				}
			}
			this.bindings = null;
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x00080694 File Offset: 0x0007E894
		internal TreeNodeBinding FindBindingForNode(string type, int depth)
		{
			if (this.bindings == null)
			{
				return null;
			}
			TreeNodeBinding treeNodeBinding = (TreeNodeBinding)this.bindings[this.GetBindingKey(type, depth)];
			if (treeNodeBinding != null)
			{
				return treeNodeBinding;
			}
			treeNodeBinding = (TreeNodeBinding)this.bindings[this.GetBindingKey(type, -1)];
			if (treeNodeBinding != null)
			{
				return treeNodeBinding;
			}
			treeNodeBinding = (TreeNodeBinding)this.bindings[this.GetBindingKey(string.Empty, depth)];
			if (treeNodeBinding != null)
			{
				return treeNodeBinding;
			}
			return (TreeNodeBinding)this.bindings[this.GetBindingKey(string.Empty, -1)];
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x00080728 File Offset: 0x0007E928
		internal void DecorateNode(TreeNode node)
		{
			if (node == null)
			{
				return;
			}
			if (node.ImageUrl != null && node.ImageUrl.Length > 0)
			{
				return;
			}
			if (node.IsRootNode && this.rootNodeStyle != null)
			{
				node.ImageUrl = this.rootNodeStyle.ImageUrl;
				return;
			}
			if (node.IsParentNode && this.parentNodeStyle != null)
			{
				node.ImageUrl = this.parentNodeStyle.ImageUrl;
				return;
			}
			if (node.IsLeafNode && this.leafNodeStyle != null)
			{
				node.ImageUrl = this.leafNodeStyle.ImageUrl;
			}
		}

		/// <summary>Renders the nodes in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream used to write content to a Web page. </param>
		// Token: 0x06003157 RID: 12631 RVA: 0x000807B8 File Offset: 0x0007E9B8
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			SiteMapDataSource siteMapDataSource = this.GetDataSource() as SiteMapDataSource;
			if (base.IsBoundUsingDataSourceID && siteMapDataSource != null)
			{
				IHierarchyData currentNode = siteMapDataSource.Provider.CurrentNode;
				if (currentNode != null)
				{
					this.activeSiteMapPath = currentNode.Path;
				}
			}
			ArrayList arrayList = new ArrayList();
			int count = this.Nodes.Count;
			for (int i = 0; i < count; i++)
			{
				this.RenderNode(writer, this.Nodes[i], 0, arrayList, i > 0, i < count - 1);
			}
		}

		/// <summary>Adds HTML attributes and styles that need to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> control.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
		// Token: 0x06003158 RID: 12632 RVA: 0x00064ACD File Offset: 0x00062CCD
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
		}

		/// <summary>Renders the HTML opening tag of the control to the specified writer.  </summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
		// Token: 0x06003159 RID: 12633 RVA: 0x00080844 File Offset: 0x0007EA44
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			string skipLinkText = this.SkipLinkText;
			if (!string.IsNullOrEmpty(skipLinkText))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Href, "#" + this.ClientID + "_SkipLink");
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				ClientScriptManager clientScriptManager = new ClientScriptManager(null);
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, skipLinkText);
				writer.AddAttribute(HtmlTextWriterAttribute.Src, clientScriptManager.GetWebResourceUrl(typeof(SiteMapPath), "transparent.gif"));
				writer.AddAttribute(HtmlTextWriterAttribute.Height, "0");
				writer.AddAttribute(HtmlTextWriterAttribute.Width, "0");
				writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0px");
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			base.RenderBeginTag(writer);
		}

		/// <summary>Renders the HTML closing tag of the control to the specified writer.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
		// Token: 0x0600315A RID: 12634 RVA: 0x000808F4 File Offset: 0x0007EAF4
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			base.RenderEndTag(writer);
			if (!string.IsNullOrEmpty(this.SkipLinkText))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_SkipLink");
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.RenderEndTag();
			}
		}

		// Token: 0x0600315B RID: 12635 RVA: 0x00080930 File Offset: 0x0007EB30
		private void RenderNode(HtmlTextWriter writer, TreeNode node, int level, ArrayList levelLines, bool hasPrevious, bool hasNext)
		{
			this.DecorateNode(node);
			bool flag = this.EnableClientScript && base.Events[TreeView.TreeNodeCollapsedEvent] == null && base.Events[TreeView.TreeNodeExpandedEvent] == null;
			TreeView.ImageStyle imageStyle = this.GetImageStyle();
			bool flag2 = node.Expanded != null && node.Expanded.Value;
			if (flag && !flag2)
			{
				flag2 = !node.PopulateOnDemand || node.Populated;
			}
			bool flag3;
			if (flag2)
			{
				flag3 = node.ChildNodes.Count > 0;
			}
			else
			{
				flag3 = (node.PopulateOnDemand && !node.Populated) || node.ChildNodes.Count > 0;
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0", false);
			writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0", false);
			writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0");
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			string text = this.GetNodeImageUrl("i", imageStyle);
			for (int i = 0; i < level; i++)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.NodeIndent.ToString() + "px");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "1px");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				if (this.ShowLines && levelLines[i] != null)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Src, text);
					writer.AddAttribute(HtmlTextWriterAttribute.Alt, string.Empty, false);
					writer.RenderBeginTag(HtmlTextWriterTag.Img);
					writer.RenderEndTag();
				}
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			bool showExpandCollapse = this.ShowExpandCollapse;
			bool showLines = this.ShowLines;
			if (showExpandCollapse || showLines)
			{
				bool flag4 = false;
				string text2 = string.Empty;
				string text3 = string.Empty;
				if (showLines)
				{
					if (hasPrevious && hasNext)
					{
						text3 = "t";
					}
					else if (hasPrevious && !hasNext)
					{
						text3 = "l";
					}
					else if (!hasPrevious && hasNext)
					{
						text3 = "r";
					}
					else
					{
						text3 = "dash";
					}
				}
				if (showExpandCollapse)
				{
					if (flag3)
					{
						flag4 = true;
						if (node.Expanded != null && node.Expanded.Value)
						{
							text3 += "minus";
						}
						else
						{
							text3 += "plus";
						}
						text2 = this.GetNodeImageToolTip(node.Expanded == null || !node.Expanded.Value, node.Text);
					}
					else if (!showLines)
					{
						text3 = "noexpand";
					}
				}
				if (!string.IsNullOrEmpty(text3))
				{
					text = this.GetNodeImageUrl(text3, imageStyle);
					writer.RenderBeginTag(HtmlTextWriterTag.Td);
					if (flag4)
					{
						if (!flag || (!this.PopulateNodesFromClient && node.PopulateOnDemand && !node.Populated))
						{
							writer.AddAttribute(HtmlTextWriterAttribute.Href, this.GetClientEvent(node, "ec"));
						}
						else
						{
							writer.AddAttribute(HtmlTextWriterAttribute.Href, this.GetClientExpandEvent(node));
						}
						writer.RenderBeginTag(HtmlTextWriterTag.A);
					}
					writer.AddAttribute(HtmlTextWriterAttribute.Alt, text2);
					if (flag4 && flag)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Id, this.GetNodeClientId(node, "img"));
					}
					writer.AddAttribute(HtmlTextWriterAttribute.Src, text);
					if (flag4)
					{
						writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0");
					}
					writer.RenderBeginTag(HtmlTextWriterTag.Img);
					writer.RenderEndTag();
					if (flag4)
					{
						writer.RenderEndTag();
					}
					writer.RenderEndTag();
				}
			}
			string text4 = ((node.ImageUrl.Length > 0) ? base.ResolveClientUrl(node.ImageUrl) : null);
			if (string.IsNullOrEmpty(text4) && imageStyle != null)
			{
				if (imageStyle.RootIcon != null && node.IsRootNode)
				{
					text4 = this.GetNodeIconUrl(imageStyle.RootIcon);
				}
				else if (imageStyle.ParentIcon != null && node.IsParentNode)
				{
					text4 = this.GetNodeIconUrl(imageStyle.ParentIcon);
				}
				else if (imageStyle.LeafIcon != null && node.IsLeafNode)
				{
					text4 = this.GetNodeIconUrl(imageStyle.LeafIcon);
				}
			}
			if (level < this.LevelStyles.Count && this.LevelStyles[level].ImageUrl != null)
			{
				text4 = base.ResolveClientUrl(this.LevelStyles[level].ImageUrl);
			}
			if (!string.IsNullOrEmpty(text4))
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, "-1");
				this.BeginNodeTag(writer, node, flag);
				writer.AddAttribute(HtmlTextWriterAttribute.Src, text4);
				writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0");
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, node.ImageToolTip);
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			if (!this.NodeWrap)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
			}
			bool flag5 = node == this.SelectedNode && this.selectedNodeStyle != null;
			if (!flag5 && this.selectedNodeStyle != null && !string.IsNullOrEmpty(this.activeSiteMapPath))
			{
				flag5 = string.Compare(this.activeSiteMapPath, node.NavigateUrl, RuntimeHelpers.StringComparison) == 0;
			}
			this.AddNodeStyle(writer, node, level, flag5);
			if (this.EnableClientScript)
			{
				writer.AddAttribute("onmouseout", "TreeView_UnhoverNode(this)", false);
				writer.AddAttribute("onmouseover", "TreeView_HoverNode('" + this.ClientID + "', this)");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			if (node.ShowCheckBoxInternal)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Name, this.ClientID + "_cs_" + node.Path);
				writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox", false);
				string toolTip = node.ToolTip;
				if (!string.IsNullOrEmpty(toolTip))
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Title, toolTip);
				}
				if (node.Checked)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked", false);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Input);
				writer.RenderEndTag();
			}
			node.BeginRenderText(writer);
			if (flag)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.GetNodeClientId(node, "txt"));
			}
			this.AddNodeLinkStyle(writer, node, level, flag5);
			this.BeginNodeTag(writer, node, flag);
			writer.Write(node.Text);
			writer.RenderEndTag();
			node.EndRenderText(writer);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
			if (flag3)
			{
				if (level >= levelLines.Count)
				{
					if (hasNext)
					{
						levelLines.Add(this);
					}
					else
					{
						levelLines.Add(null);
					}
				}
				else if (hasNext)
				{
					levelLines[level] = this;
				}
				else
				{
					levelLines[level] = null;
				}
				if (flag)
				{
					if (node.Expanded == null || !node.Expanded.Value)
					{
						writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
					}
					else
					{
						writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "block");
					}
					writer.AddAttribute(HtmlTextWriterAttribute.Id, this.GetNodeClientId(node, null));
					writer.RenderBeginTag(HtmlTextWriterTag.Span);
					if (flag2)
					{
						this.AddChildrenPadding(writer, node);
						int count = node.ChildNodes.Count;
						for (int j = 0; j < count; j++)
						{
							this.RenderNode(writer, node.ChildNodes[j], level + 1, levelLines, true, j < count - 1);
						}
						if (hasNext)
						{
							this.AddChildrenPadding(writer, node);
						}
					}
					writer.RenderEndTag();
					return;
				}
				if (flag2)
				{
					this.AddChildrenPadding(writer, node);
					int count2 = node.ChildNodes.Count;
					for (int k = 0; k < count2; k++)
					{
						this.RenderNode(writer, node.ChildNodes[k], level + 1, levelLines, true, k < count2 - 1);
					}
					if (hasNext)
					{
						this.AddChildrenPadding(writer, node);
					}
				}
			}
		}

		// Token: 0x0600315C RID: 12636 RVA: 0x00081080 File Offset: 0x0007F280
		private void AddChildrenPadding(HtmlTextWriter writer, TreeNode node)
		{
			int depth = node.Depth;
			Unit unit = Unit.Empty;
			if (this.levelStyles != null && depth < this.levelStyles.Count)
			{
				unit = this.levelStyles[depth].ChildNodesPadding;
			}
			if (unit.IsEmpty && this.nodeStyle != null)
			{
				unit = this.nodeStyle.ChildNodesPadding;
			}
			double value;
			if (unit.IsEmpty || (value = unit.Value) == 0.0 || unit.Type != UnitType.Pixel)
			{
				return;
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			writer.AddAttribute(HtmlTextWriterAttribute.Height, ((int)value).ToString(), false);
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x0600315D RID: 12637 RVA: 0x00081144 File Offset: 0x0007F344
		private void RenderMenuItemSpacing(HtmlTextWriter writer, Unit itemSpacing)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x0600315E RID: 12638 RVA: 0x00081164 File Offset: 0x0007F364
		private Unit GetNodeSpacing(TreeNode node)
		{
			if (node.Selected && this.selectedNodeStyle != null && this.selectedNodeStyle.NodeSpacing != Unit.Empty)
			{
				return this.selectedNodeStyle.NodeSpacing;
			}
			if (this.levelStyles != null && node.Depth < this.levelStyles.Count && this.levelStyles[node.Depth].NodeSpacing != Unit.Empty)
			{
				return this.levelStyles[node.Depth].NodeSpacing;
			}
			if (node.IsLeafNode)
			{
				if (this.leafNodeStyle != null && this.leafNodeStyle.NodeSpacing != Unit.Empty)
				{
					return this.leafNodeStyle.NodeSpacing;
				}
			}
			else if (node.IsRootNode)
			{
				if (this.rootNodeStyle != null && this.rootNodeStyle.NodeSpacing != Unit.Empty)
				{
					return this.rootNodeStyle.NodeSpacing;
				}
			}
			else if (node.IsParentNode && this.parentNodeStyle != null && this.parentNodeStyle.NodeSpacing != Unit.Empty)
			{
				return this.parentNodeStyle.NodeSpacing;
			}
			if (this.nodeStyle != null)
			{
				return this.nodeStyle.NodeSpacing;
			}
			return Unit.Empty;
		}

		// Token: 0x0600315F RID: 12639 RVA: 0x000812B0 File Offset: 0x0007F4B0
		private void AddNodeStyle(HtmlTextWriter writer, TreeNode node, int level, bool nodeIsSelected)
		{
			TreeNodeStyle treeNodeStyle = new TreeNodeStyle();
			if (this.Page.Header != null)
			{
				if (this.nodeStyle != null)
				{
					treeNodeStyle.PrependCssClass(this.nodeStyle.RegisteredCssClass);
					treeNodeStyle.PrependCssClass(this.nodeStyle.CssClass);
				}
				if (node.IsLeafNode)
				{
					if (this.leafNodeStyle != null)
					{
						treeNodeStyle.PrependCssClass(this.leafNodeStyle.RegisteredCssClass);
						treeNodeStyle.PrependCssClass(this.leafNodeStyle.CssClass);
					}
				}
				else if (node.IsRootNode)
				{
					if (this.rootNodeStyle != null)
					{
						treeNodeStyle.PrependCssClass(this.rootNodeStyle.RegisteredCssClass);
						treeNodeStyle.PrependCssClass(this.rootNodeStyle.CssClass);
					}
				}
				else if (node.IsParentNode && this.parentNodeStyle != null)
				{
					treeNodeStyle.AddCssClass(this.parentNodeStyle.RegisteredCssClass);
					treeNodeStyle.AddCssClass(this.parentNodeStyle.CssClass);
				}
				if (this.levelStyles != null && this.levelStyles.Count > level)
				{
					treeNodeStyle.PrependCssClass(this.levelStyles[level].RegisteredCssClass);
					treeNodeStyle.PrependCssClass(this.levelStyles[level].CssClass);
				}
				if (nodeIsSelected)
				{
					treeNodeStyle.AddCssClass(this.selectedNodeStyle.RegisteredCssClass);
					treeNodeStyle.AddCssClass(this.selectedNodeStyle.CssClass);
				}
			}
			else
			{
				if (this.nodeStyle != null)
				{
					treeNodeStyle.CopyFrom(this.nodeStyle);
				}
				if (node.IsLeafNode)
				{
					if (this.leafNodeStyle != null)
					{
						treeNodeStyle.CopyFrom(this.leafNodeStyle);
					}
				}
				else if (node.IsRootNode)
				{
					if (this.rootNodeStyle != null)
					{
						treeNodeStyle.CopyFrom(this.rootNodeStyle);
					}
				}
				else if (node.IsParentNode && this.parentNodeStyle != null)
				{
					treeNodeStyle.CopyFrom(this.parentNodeStyle);
				}
				if (this.levelStyles != null && this.levelStyles.Count > level)
				{
					treeNodeStyle.CopyFrom(this.levelStyles[level]);
				}
				if (nodeIsSelected)
				{
					treeNodeStyle.CopyFrom(this.selectedNodeStyle);
				}
			}
			treeNodeStyle.AddAttributesToRender(writer);
		}

		// Token: 0x06003160 RID: 12640 RVA: 0x000814B8 File Offset: 0x0007F6B8
		private void AddNodeLinkStyle(HtmlTextWriter writer, TreeNode node, int level, bool nodeIsSelected)
		{
			Style style = new Style();
			bool flag = false;
			if (this.Page.Header != null)
			{
				style.AddCssClass(this.ControlLinkStyle.RegisteredCssClass);
				if (this.nodeStyle != null)
				{
					style.AddCssClass(this.nodeLinkStyle.CssClass);
					style.AddCssClass(this.nodeLinkStyle.RegisteredCssClass);
				}
				if (this.levelLinkStyles != null && this.levelLinkStyles.Count > level)
				{
					style.AddCssClass(this.levelLinkStyles[level].CssClass);
					style.AddCssClass(this.levelLinkStyles[level].RegisteredCssClass);
					flag = true;
				}
				if (node.IsLeafNode)
				{
					if (this.leafNodeStyle != null)
					{
						style.AddCssClass(this.leafNodeLinkStyle.CssClass);
						style.AddCssClass(this.leafNodeLinkStyle.RegisteredCssClass);
					}
				}
				else if (node.IsRootNode)
				{
					if (this.rootNodeStyle != null)
					{
						style.AddCssClass(this.rootNodeLinkStyle.CssClass);
						style.AddCssClass(this.rootNodeLinkStyle.RegisteredCssClass);
					}
				}
				else if (node.IsParentNode && this.parentNodeStyle != null)
				{
					style.AddCssClass(this.parentNodeLinkStyle.CssClass);
					style.AddCssClass(this.parentNodeLinkStyle.RegisteredCssClass);
				}
				if (nodeIsSelected)
				{
					style.AddCssClass(this.selectedNodeLinkStyle.CssClass);
					style.AddCssClass(this.selectedNodeLinkStyle.RegisteredCssClass);
				}
			}
			else
			{
				style.CopyFrom(this.ControlLinkStyle);
				if (this.nodeStyle != null)
				{
					style.CopyFrom(this.nodeLinkStyle);
				}
				if (this.levelLinkStyles != null && this.levelLinkStyles.Count > level)
				{
					style.CopyFrom(this.levelLinkStyles[level]);
					flag = true;
				}
				if (node.IsLeafNode)
				{
					if (node.IsLeafNode && this.leafNodeStyle != null)
					{
						style.CopyFrom(this.leafNodeLinkStyle);
					}
				}
				else if (node.IsRootNode)
				{
					if (node.IsRootNode && this.rootNodeStyle != null)
					{
						style.CopyFrom(this.rootNodeLinkStyle);
					}
				}
				else if (node.IsParentNode && node.IsParentNode && this.parentNodeStyle != null)
				{
					style.CopyFrom(this.parentNodeLinkStyle);
				}
				if (nodeIsSelected)
				{
					style.CopyFrom(this.selectedNodeLinkStyle);
				}
				style.AlwaysRenderTextDecoration = true;
			}
			if (flag)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, "none");
				writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "1em");
			}
			style.AddAttributesToRender(writer);
		}

		// Token: 0x06003161 RID: 12641 RVA: 0x0008171C File Offset: 0x0007F91C
		private void BeginNodeTag(HtmlTextWriter writer, TreeNode node, bool clientExpand)
		{
			if (node.ToolTip.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Title, node.ToolTip);
			}
			string navigateUrl = node.NavigateUrl;
			if (!string.IsNullOrEmpty(navigateUrl))
			{
				string text = ((node.Target.Length > 0) ? node.Target : this.Target);
				string text2 = base.ResolveClientUrl(navigateUrl);
				writer.AddAttribute(HtmlTextWriterAttribute.Href, text2);
				if (text.Length > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Target, text);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				return;
			}
			if (node.SelectAction != TreeNodeSelectAction.None)
			{
				if (node.SelectAction == TreeNodeSelectAction.Expand && clientExpand)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Href, this.GetClientExpandEvent(node));
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Href, this.GetClientEvent(node, "sel"));
				}
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				return;
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
		}

		// Token: 0x06003162 RID: 12642 RVA: 0x000817E8 File Offset: 0x0007F9E8
		private string GetNodeImageToolTip(bool expand, string txt)
		{
			if (expand)
			{
				string expandImageToolTip = this.ExpandImageToolTip;
				if (!string.IsNullOrEmpty(expandImageToolTip))
				{
					return string.Format(expandImageToolTip, HttpUtility.HtmlAttributeEncode(txt));
				}
				if (txt != null)
				{
					return "Expand " + HttpUtility.HtmlAttributeEncode(txt);
				}
				return "Expand {0}";
			}
			else
			{
				string collapseImageToolTip = this.CollapseImageToolTip;
				if (!string.IsNullOrEmpty(collapseImageToolTip))
				{
					return string.Format(collapseImageToolTip, HttpUtility.HtmlAttributeEncode(txt));
				}
				if (txt != null)
				{
					return "Collapse " + HttpUtility.HtmlAttributeEncode(txt);
				}
				return "Collapse {0}";
			}
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x00081863 File Offset: 0x0007FA63
		private string GetNodeClientId(TreeNode node, string sufix)
		{
			return this.ClientID + "_" + node.Path + ((sufix != null) ? ("_" + sufix) : string.Empty);
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x00081890 File Offset: 0x0007FA90
		private string GetNodeImageUrl(string shape, TreeView.ImageStyle imageStyle)
		{
			if (this.ShowLines)
			{
				if (!string.IsNullOrEmpty(this.LineImagesFolder))
				{
					return base.ResolveClientUrl(this.LineImagesFolder + "/" + shape + ".gif");
				}
			}
			else
			{
				if (imageStyle != null)
				{
					if (shape == "plus")
					{
						if (!string.IsNullOrEmpty(imageStyle.Expand))
						{
							return this.GetNodeIconUrl(imageStyle.Expand);
						}
					}
					else if (shape == "minus")
					{
						if (!string.IsNullOrEmpty(imageStyle.Collapse))
						{
							return this.GetNodeIconUrl(imageStyle.Collapse);
						}
					}
					else if (shape == "noexpand" && !string.IsNullOrEmpty(imageStyle.NoExpand))
					{
						return this.GetNodeIconUrl(imageStyle.NoExpand);
					}
				}
				else if (shape == "plus")
				{
					if (!string.IsNullOrEmpty(this.ExpandImageUrl))
					{
						return base.ResolveClientUrl(this.ExpandImageUrl);
					}
				}
				else if (shape == "minus")
				{
					if (!string.IsNullOrEmpty(this.CollapseImageUrl))
					{
						return base.ResolveClientUrl(this.CollapseImageUrl);
					}
				}
				else if (shape == "noexpand" && !string.IsNullOrEmpty(this.NoExpandImageUrl))
				{
					return base.ResolveClientUrl(this.NoExpandImageUrl);
				}
				if (!string.IsNullOrEmpty(this.LineImagesFolder))
				{
					return base.ResolveClientUrl(this.LineImagesFolder + "/" + shape + ".gif");
				}
			}
			return this.Page.ClientScript.GetWebResourceUrl(typeof(TreeView), "TreeView_" + shape + ".gif");
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x00081A22 File Offset: 0x0007FC22
		private string GetNodeIconUrl(string icon)
		{
			return this.Page.ClientScript.GetWebResourceUrl(typeof(TreeView), icon + ".gif");
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x00081A49 File Offset: 0x0007FC49
		private string GetClientEvent(TreeNode node, string ev)
		{
			return this.Page.ClientScript.GetPostBackClientHyperlink(this, ev + "|" + node.Path, true);
		}

		// Token: 0x06003167 RID: 12647 RVA: 0x00081A70 File Offset: 0x0007FC70
		private string GetClientExpandEvent(TreeNode node)
		{
			return string.Format("javascript:TreeView_ToggleExpand ('{0}','{1}','{2}','{3}','{4}','{5}')", new object[]
			{
				this.ClientID,
				node.Path,
				HttpUtility.HtmlAttributeEncode(node.Value).Replace("'", "\\'").Replace("|", "U+007C"),
				HttpUtility.HtmlAttributeEncode(node.ImageUrl).Replace("'", "\\'").Replace("|", "U+007c"),
				HttpUtility.HtmlAttributeEncode(node.NavigateUrl).Replace("'", "\\'").Replace("|", "U+007C"),
				HttpUtility.HtmlAttributeEncode(node.Target).Replace("'", "\\'").Replace("|", "U+007C")
			});
		}

		// Token: 0x06003168 RID: 12648 RVA: 0x00081B50 File Offset: 0x0007FD50
		private TreeNode FindNodeByPos(string path)
		{
			string[] array = path.Split('_', StringSplitOptions.None);
			TreeNode treeNode = null;
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				int num = int.Parse(array2[i]);
				if (treeNode == null)
				{
					if (num >= this.Nodes.Count)
					{
						return null;
					}
					treeNode = this.Nodes[num];
				}
				else
				{
					if (num >= treeNode.ChildNodes.Count)
					{
						return null;
					}
					treeNode = treeNode.ChildNodes[num];
				}
			}
			return treeNode;
		}

		// Token: 0x06003169 RID: 12649 RVA: 0x00081BC0 File Offset: 0x0007FDC0
		private void UnsetCheckStates(TreeNodeCollection col, NameValueCollection states)
		{
			foreach (object obj in col)
			{
				TreeNode treeNode = (TreeNode)obj;
				if (treeNode.ShowCheckBoxInternal && treeNode.Checked && (states == null || states[this.ClientID + "_cs_" + treeNode.Path] == null))
				{
					treeNode.Checked = false;
				}
				if (treeNode.HasChildData)
				{
					this.UnsetCheckStates(treeNode.ChildNodes, states);
				}
			}
		}

		// Token: 0x0600316A RID: 12650 RVA: 0x00081C5C File Offset: 0x0007FE5C
		private void SetCheckStates(NameValueCollection states)
		{
			if (states == null)
			{
				return;
			}
			string text = this.ClientID + "_cs_";
			foreach (object obj in states)
			{
				string text2 = (string)obj;
				if (text2.StartsWith(text, StringComparison.Ordinal))
				{
					string text3 = text2.Substring(text.Length);
					TreeNode treeNode = this.FindNodeByPos(text3);
					if (treeNode != null && !treeNode.Checked)
					{
						treeNode.Checked = true;
					}
				}
			}
		}

		// Token: 0x0600316B RID: 12651 RVA: 0x00081CF8 File Offset: 0x0007FEF8
		private void UnsetExpandStates(TreeNodeCollection col, string[] states)
		{
			foreach (object obj in col)
			{
				TreeNode treeNode = (TreeNode)obj;
				if (treeNode.Expanded != null && treeNode.Expanded.Value && Array.IndexOf<string>(states, treeNode.Path) == -1)
				{
					treeNode.Expanded = new bool?(false);
				}
				if (treeNode.HasChildData)
				{
					this.UnsetExpandStates(treeNode.ChildNodes, states);
				}
			}
		}

		// Token: 0x0600316C RID: 12652 RVA: 0x00081D9C File Offset: 0x0007FF9C
		private void SetExpandStates(string[] states)
		{
			foreach (string text in states)
			{
				if (!string.IsNullOrEmpty(text))
				{
					TreeNode treeNode = this.FindNodeByPos(text);
					if (treeNode != null)
					{
						treeNode.Expanded = new bool?(true);
					}
				}
			}
		}

		// Token: 0x0600316D RID: 12653 RVA: 0x00081DDC File Offset: 0x0007FFDC
		private string GetExpandStates()
		{
			StringBuilder stringBuilder = new StringBuilder("|");
			foreach (object obj in this.Nodes)
			{
				TreeNode treeNode = (TreeNode)obj;
				this.GetExpandStates(stringBuilder, treeNode);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600316E RID: 12654 RVA: 0x00081E48 File Offset: 0x00080048
		private void GetExpandStates(StringBuilder sb, TreeNode node)
		{
			if (node.Expanded != null && node.Expanded.Value)
			{
				sb.Append(node.Path);
				sb.Append('|');
			}
			if (node.HasChildData)
			{
				foreach (object obj in node.ChildNodes)
				{
					TreeNode treeNode = (TreeNode)obj;
					this.GetExpandStates(sb, treeNode);
				}
			}
		}

		// Token: 0x04001C23 RID: 7203
		private static readonly char[] postDataSplitChars = new char[] { '|' };

		// Token: 0x04001C24 RID: 7204
		private string activeSiteMapPath;

		// Token: 0x04001C25 RID: 7205
		private bool stylesPrepared;

		// Token: 0x04001C26 RID: 7206
		private Style hoverNodeStyle;

		// Token: 0x04001C27 RID: 7207
		private TreeNodeStyle leafNodeStyle;

		// Token: 0x04001C28 RID: 7208
		private TreeNodeStyle nodeStyle;

		// Token: 0x04001C29 RID: 7209
		private TreeNodeStyle parentNodeStyle;

		// Token: 0x04001C2A RID: 7210
		private TreeNodeStyle rootNodeStyle;

		// Token: 0x04001C2B RID: 7211
		private TreeNodeStyle selectedNodeStyle;

		// Token: 0x04001C2C RID: 7212
		private TreeNodeStyleCollection levelStyles;

		// Token: 0x04001C2D RID: 7213
		private TreeNodeCollection nodes;

		// Token: 0x04001C2E RID: 7214
		private TreeNodeBindingCollection dataBindings;

		// Token: 0x04001C2F RID: 7215
		private TreeNode selectedNode;

		// Token: 0x04001C30 RID: 7216
		private Hashtable bindings;

		// Token: 0x04001C31 RID: 7217
		private int registeredStylesCounter = -1;

		// Token: 0x04001C32 RID: 7218
		private List<Style> levelLinkStyles;

		// Token: 0x04001C33 RID: 7219
		private Style controlLinkStyle;

		// Token: 0x04001C34 RID: 7220
		private Style nodeLinkStyle;

		// Token: 0x04001C35 RID: 7221
		private Style rootNodeLinkStyle;

		// Token: 0x04001C36 RID: 7222
		private Style parentNodeLinkStyle;

		// Token: 0x04001C37 RID: 7223
		private Style leafNodeLinkStyle;

		// Token: 0x04001C38 RID: 7224
		private Style selectedNodeLinkStyle;

		// Token: 0x04001C39 RID: 7225
		private Style hoverNodeLinkStyle;

		// Token: 0x04001C40 RID: 7232
		private static Hashtable imageStyles;

		// Token: 0x04001C41 RID: 7233
		private string callbackResult;

		// Token: 0x04001C42 RID: 7234
		private const string _OnPreRender_Script_Preamble = "var {0} = new Object ();\n{0}.treeId = {1};\n{0}.uid = {2};\n{0}.showImage = {3};\n";

		// Token: 0x04001C43 RID: 7235
		private const string _OnPreRender_Script_ShowExpandCollapse = "{0}.expandImage = {1};\n{0}.collapseImage = {2};\n";

		// Token: 0x04001C44 RID: 7236
		private const string _OnPreRender_Script_ShowExpandCollapse_Populate = "{0}.noExpandImage = {1};\n";

		// Token: 0x04001C45 RID: 7237
		private const string _OnPreRender_Script_PopulateCallback = "{0}.form = {1};\n{0}.PopulateNode = function (nodeId, nodeValue, nodeImageUrl, nodeNavigateUrl, nodeTarget) {{\n\t{2}.__theFormPostData = \"\";\n\t{2}.__theFormPostCollection = new Array ();\n\t{2}.WebForm_InitCallback ();\n\tTreeView_PopulateNode (this.uid, this.treeId, nodeId, nodeValue, nodeImageUrl, nodeNavigateUrl, nodeTarget)\n}};\n";

		// Token: 0x04001C46 RID: 7238
		private const string _OnPreRender_Script_CallbackOptions = "{0}.populateFromClient = {1};\n{0}.expandAlt = {2};\n{0}.collapseAlt = {3};\n";

		// Token: 0x04001C47 RID: 7239
		private const string _OnPreRender_Script_HoverStyle = "{0}.hoverClass = {1};\n{0}.hoverLinkClass = {2};\n";

		// Token: 0x0200042F RID: 1071
		private class TreeViewExpandDepthConverter : TypeConverter
		{
			// Token: 0x06003170 RID: 12656 RVA: 0x00081EEF File Offset: 0x000800EF
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || sourceType == typeof(int) || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x06003171 RID: 12657 RVA: 0x00081F1F File Offset: 0x0008011F
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return destinationType == typeof(string) || destinationType == typeof(int) || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x06003172 RID: 12658 RVA: 0x00081F50 File Offset: 0x00080150
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType != typeof(int) && destinationType != typeof(string))
				{
					return base.ConvertTo(context, culture, value, destinationType);
				}
				if (value is string)
				{
					if (destinationType == typeof(int))
					{
						if (string.Compare("FullyExpand", (string)value, StringComparison.OrdinalIgnoreCase) == 0)
						{
							return -1;
						}
						try
						{
							return int.Parse((string)value);
						}
						catch (Exception)
						{
							return -1;
						}
						return value;
					}
					return value;
				}
				int num = (int)value;
				if (!(destinationType == typeof(string)))
				{
					return value;
				}
				if (num == -1)
				{
					return "FullyExpand";
				}
				return num.ToString();
			}

			// Token: 0x06003173 RID: 12659 RVA: 0x00082024 File Offset: 0x00080224
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				if (!(value is string) && !(value is int))
				{
					return base.ConvertFrom(context, culture, value);
				}
				if (value is string)
				{
					if (string.Compare("FullyExpand", (string)value, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return -1;
					}
					try
					{
						return int.Parse((string)value);
					}
					catch (Exception)
					{
						return null;
					}
					return value;
				}
				return value;
			}
		}

		// Token: 0x02000430 RID: 1072
		private class ImageStyle
		{
			// Token: 0x06003175 RID: 12661 RVA: 0x0008209C File Offset: 0x0008029C
			public ImageStyle(string expand, string collapse, string noExpand, string icon, string iconLeaf, string iconRoot)
			{
				this.Expand = expand;
				this.Collapse = collapse;
				this.NoExpand = noExpand;
				this.RootIcon = iconRoot;
				this.ParentIcon = icon;
				this.LeafIcon = iconLeaf;
			}

			// Token: 0x04001C48 RID: 7240
			public string Expand;

			// Token: 0x04001C49 RID: 7241
			public string Collapse;

			// Token: 0x04001C4A RID: 7242
			public string NoExpand;

			// Token: 0x04001C4B RID: 7243
			public string RootIcon;

			// Token: 0x04001C4C RID: 7244
			public string ParentIcon;

			// Token: 0x04001C4D RID: 7245
			public string LeafIcon;
		}
	}
}
