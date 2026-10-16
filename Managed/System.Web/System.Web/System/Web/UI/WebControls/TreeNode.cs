using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a node in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	// Token: 0x02000427 RID: 1063
	[ParseChildren(true, "ChildNodes")]
	public class TreeNode : IStateManager, ICloneable
	{
		// Token: 0x06003002 RID: 12290 RVA: 0x0007C80A File Offset: 0x0007AA0A
		internal TreeNode(TreeView tree)
		{
			this.Tree = tree;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> class without text or a value.</summary>
		// Token: 0x06003003 RID: 12291 RVA: 0x0007C82B File Offset: 0x0007AA2B
		public TreeNode()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> class using the specified text.</summary>
		/// <param name="text">The text that is displayed in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control for the node. </param>
		// Token: 0x06003004 RID: 12292 RVA: 0x0007C845 File Offset: 0x0007AA45
		public TreeNode(string text)
		{
			this.Text = text;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> class using the specified text and value.</summary>
		/// <param name="text">The text that is displayed in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control for the node. </param>
		/// <param name="value">The supplemental data associated with the node, such as data used for handling postback events. </param>
		// Token: 0x06003005 RID: 12293 RVA: 0x0007C866 File Offset: 0x0007AA66
		public TreeNode(string text, string value)
		{
			this.Text = text;
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> class using the specified text, value, and image URL.</summary>
		/// <param name="text">The text that is displayed in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control for the node. </param>
		/// <param name="value">The supplemental data associated with the node, such as data used for handling postback events. </param>
		/// <param name="imageUrl">The URL to an image that is displayed next to the node. </param>
		// Token: 0x06003006 RID: 12294 RVA: 0x0007C88E File Offset: 0x0007AA8E
		public TreeNode(string text, string value, string imageUrl)
		{
			this.Text = text;
			this.Value = value;
			this.ImageUrl = imageUrl;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> class using the specified text, value, image URL, navigation URL, and target.</summary>
		/// <param name="text">The text that is displayed in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control for the node. </param>
		/// <param name="value">The supplemental data associated with the node, such as data used for handling postback events. </param>
		/// <param name="imageUrl">The URL to an image that is displayed next to the node. </param>
		/// <param name="navigateUrl">The URL to link to when the node is clicked. </param>
		/// <param name="target">The target window or frame in which to display the Web page content linked to when the node is clicked. </param>
		// Token: 0x06003007 RID: 12295 RVA: 0x0007C8BD File Offset: 0x0007AABD
		public TreeNode(string text, string value, string imageUrl, string navigateUrl, string target)
		{
			this.Text = text;
			this.Value = value;
			this.ImageUrl = imageUrl;
			this.NavigateUrl = navigateUrl;
			this.Target = target;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> class using the specified owner.</summary>
		/// <param name="owner">The <see cref="T:System.Web.UI.WebControls.TreeView" /> that will contain the new <see cref="T:System.Web.UI.WebControls.TreeNode" />.</param>
		/// <param name="isRoot">true if the <see cref="T:System.Web.UI.WebControls.TreeNode" /> is a root node; otherwise, false.</param>
		// Token: 0x06003008 RID: 12296 RVA: 0x0007C8FC File Offset: 0x0007AAFC
		[global::System.MonoTODO("Not implemented")]
		protected TreeNode(TreeView owner, bool isRoot)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the depth of the node.</summary>
		/// <returns>The depth of the node.</returns>
		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x06003009 RID: 12297 RVA: 0x0007C91C File Offset: 0x0007AB1C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int Depth
		{
			get
			{
				if (this.depth != -1)
				{
					return this.depth;
				}
				this.depth = 0;
				for (TreeNode treeNode = this.parent; treeNode != null; treeNode = treeNode.parent)
				{
					this.depth++;
				}
				return this.depth;
			}
		}

		// Token: 0x0600300A RID: 12298 RVA: 0x0007C968 File Offset: 0x0007AB68
		private void ResetPathData()
		{
			this.path = null;
			this.depth = -1;
			this.gotBinding = false;
			if (this.nodes != null)
			{
				foreach (object obj in this.nodes)
				{
					((TreeNode)obj).ResetPathData();
				}
			}
		}

		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x0600300B RID: 12299 RVA: 0x0007C9DC File Offset: 0x0007ABDC
		// (set) Token: 0x0600300C RID: 12300 RVA: 0x0007C9E4 File Offset: 0x0007ABE4
		internal TreeView Tree
		{
			get
			{
				return this.tree;
			}
			set
			{
				if (this.SelectedFlag && value != null)
				{
					value.SetSelectedNode(this, false);
				}
				this.tree = value;
				if (this.nodes != null)
				{
					this.nodes.SetTree(this.tree);
				}
				this.ResetPathData();
				if (this.PopulateOnDemand && !this.Populated && this.Expanded != null && this.Expanded.Value)
				{
					this.Populate();
				}
			}
		}

		/// <summary>Gets a value that indicates whether the node was created through data binding.</summary>
		/// <returns>true if the node was created through data binding; otherwise, false.</returns>
		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x0600300D RID: 12301 RVA: 0x0007CA60 File Offset: 0x0007AC60
		// (set) Token: 0x0600300E RID: 12302 RVA: 0x0007CA8B File Offset: 0x0007AC8B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(false)]
		[Browsable(false)]
		public bool DataBound
		{
			get
			{
				return this.ViewState["DataBound"] != null && (bool)this.ViewState["DataBound"];
			}
			private set
			{
				this.ViewState["DataBound"] = value;
			}
		}

		/// <summary>Gets the data item that is bound to the control.</summary>
		/// <returns>A <see cref="T:System.Object" /> that represents the data item that is bound to the control. The default value is null, which indicates that the node is not bound to any data item.</returns>
		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x0600300F RID: 12303 RVA: 0x0007CAA3 File Offset: 0x0007ACA3
		[DefaultValue(null)]
		[Browsable(false)]
		public object DataItem
		{
			get
			{
				return this.dataItem;
			}
		}

		/// <summary>Gets the path to the data bound to the node.</summary>
		/// <returns>The path to the data bound to the node. This value comes from the hierarchical data source control to which the <see cref="T:System.Web.UI.WebControls.TreeView" /> control is bound. The default value is an empty string ("").</returns>
		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x06003010 RID: 12304 RVA: 0x0007CAAB File Offset: 0x0007ACAB
		// (set) Token: 0x06003011 RID: 12305 RVA: 0x0007CADA File Offset: 0x0007ACDA
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		[Browsable(false)]
		public string DataPath
		{
			get
			{
				if (this.ViewState["DataPath"] != null)
				{
					return (string)this.ViewState["DataPath"];
				}
				return string.Empty;
			}
			private set
			{
				this.ViewState["DataPath"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the node's check box is selected.</summary>
		/// <returns>true if the node's check box is selected; otherwise, false. The default is false.</returns>
		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x06003012 RID: 12306 RVA: 0x0007CAF0 File Offset: 0x0007ACF0
		// (set) Token: 0x06003013 RID: 12307 RVA: 0x0007CB19 File Offset: 0x0007AD19
		[DefaultValue(false)]
		public bool Checked
		{
			get
			{
				object obj = this.ViewState["Checked"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["Checked"] = value;
				if (this.tree != null)
				{
					this.tree.NotifyCheckChanged(this);
				}
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> collection that contains the first-level child nodes of the current node.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> that contains the first-level child nodes of the current node.</returns>
		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x06003014 RID: 12308 RVA: 0x0007CB45 File Offset: 0x0007AD45
		[DefaultValue(null)]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[Browsable(false)]
		public TreeNodeCollection ChildNodes
		{
			get
			{
				if (this.nodes == null)
				{
					this.nodes = new TreeNodeCollection(this);
					if (this.IsTrackingViewState)
					{
						((IStateManager)this.nodes).TrackViewState();
					}
				}
				return this.nodes;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the node is expanded.</summary>
		/// <returns>true if the node is expanded, false if the node is not expanded, or null.</returns>
		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x06003015 RID: 12309 RVA: 0x0007CB74 File Offset: 0x0007AD74
		// (set) Token: 0x06003016 RID: 12310 RVA: 0x0007CB8C File Offset: 0x0007AD8C
		[DefaultValue(null)]
		public bool? Expanded
		{
			get
			{
				return (bool?)this.ViewState["Expanded"];
			}
			set
			{
				bool? flag = (bool?)this.ViewState["Expanded"];
				bool? flag2 = value;
				if ((flag.GetValueOrDefault() == flag2.GetValueOrDefault()) & (flag != null == (flag2 != null)))
				{
					return;
				}
				this.ViewState["Expanded"] = value;
				if (this.tree != null)
				{
					this.tree.NotifyExpandedChanged(this);
				}
				if (this.PopulateOnDemand && !this.Populated && value != null && value.Value)
				{
					this.Populate();
				}
			}
		}

		/// <summary>Gets or sets the ToolTip text for the image displayed next to a node.</summary>
		/// <returns>The ToolTip text for the image displayed next to a node. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06003017 RID: 12311 RVA: 0x0007CC28 File Offset: 0x0007AE28
		// (set) Token: 0x06003018 RID: 12312 RVA: 0x0007CC55 File Offset: 0x0007AE55
		[Localizable(true)]
		[DefaultValue("")]
		public string ImageToolTip
		{
			get
			{
				object obj = this.ViewState["ImageToolTip"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ImageToolTip"] = value;
			}
		}

		/// <summary>Gets or sets the URL to an image that is displayed next to the node.</summary>
		/// <returns>The URL to a custom image that is displayed next to the node. The default value is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06003019 RID: 12313 RVA: 0x0007CC68 File Offset: 0x0007AE68
		// (set) Token: 0x0600301A RID: 12314 RVA: 0x0007CC95 File Offset: 0x0007AE95
		[DefaultValue("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string ImageUrl
		{
			get
			{
				object obj = this.ViewState["ImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the URL to navigate to when the node is clicked.</summary>
		/// <returns>The URL to navigate to when the node is clicked. The default value is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x0600301B RID: 12315 RVA: 0x0007CCA8 File Offset: 0x0007AEA8
		// (set) Token: 0x0600301C RID: 12316 RVA: 0x0007CCD5 File Offset: 0x0007AED5
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
		[UrlProperty]
		public string NavigateUrl
		{
			get
			{
				object obj = this.ViewState["NavigateUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["NavigateUrl"] = value;
			}
		}

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x0600301D RID: 12317 RVA: 0x0007CCE8 File Offset: 0x0007AEE8
		// (set) Token: 0x0600301E RID: 12318 RVA: 0x0007CCF0 File Offset: 0x0007AEF0
		internal bool HadChildrenBeforePopulating
		{
			get
			{
				return this.hadChildrenBeforePopulating;
			}
			set
			{
				if (this.populating)
				{
					return;
				}
				this.hadChildrenBeforePopulating = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the node is populated dynamically.</summary>
		/// <returns>true to populate the node dynamically; otherwise, false. The default is false.</returns>
		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x0600301F RID: 12319 RVA: 0x0007CD04 File Offset: 0x0007AF04
		// (set) Token: 0x06003020 RID: 12320 RVA: 0x0007CD2D File Offset: 0x0007AF2D
		[DefaultValue(false)]
		public bool PopulateOnDemand
		{
			get
			{
				object obj = this.ViewState["PopulateOnDemand"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["PopulateOnDemand"] = value;
				if (value && this.nodes != null && this.nodes.Count > 0)
				{
					this.HadChildrenBeforePopulating = true;
					return;
				}
				this.HadChildrenBeforePopulating = false;
			}
		}

		/// <summary>Gets or sets the event or events to raise when a node is selected.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TreeNodeSelectAction" /> values. The default is TreeNodeSelectAction.Select.</returns>
		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x06003021 RID: 12321 RVA: 0x0007CD70 File Offset: 0x0007AF70
		// (set) Token: 0x06003022 RID: 12322 RVA: 0x0007CD99 File Offset: 0x0007AF99
		[DefaultValue(TreeNodeSelectAction.Select)]
		public TreeNodeSelectAction SelectAction
		{
			get
			{
				object obj = this.ViewState["SelectAction"];
				if (obj != null)
				{
					return (TreeNodeSelectAction)obj;
				}
				return TreeNodeSelectAction.Select;
			}
			set
			{
				this.ViewState["SelectAction"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether a check box is displayed next to the node.</summary>
		/// <returns>true to display the check box; otherwise, false.</returns>
		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x06003023 RID: 12323 RVA: 0x0007CDB1 File Offset: 0x0007AFB1
		// (set) Token: 0x06003024 RID: 12324 RVA: 0x0007CDC8 File Offset: 0x0007AFC8
		[DefaultValue(null)]
		public bool? ShowCheckBox
		{
			get
			{
				return (bool?)this.ViewState["ShowCheckBox"];
			}
			set
			{
				this.ViewState["ShowCheckBox"] = value;
			}
		}

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x06003025 RID: 12325 RVA: 0x0007CDE0 File Offset: 0x0007AFE0
		internal bool ShowCheckBoxInternal
		{
			get
			{
				if (this.ShowCheckBox != null)
				{
					return this.ShowCheckBox.Value;
				}
				return this.Tree.ShowCheckBoxes == TreeNodeTypes.All || ((this.Tree.ShowCheckBoxes & TreeNodeTypes.Leaf) > TreeNodeTypes.None && this.IsLeafNode) || ((this.Tree.ShowCheckBoxes & TreeNodeTypes.Parent) > TreeNodeTypes.None && this.IsParentNode && this.Parent != null) || ((this.Tree.ShowCheckBoxes & TreeNodeTypes.Root) > TreeNodeTypes.None && this.Parent == null && this.ChildNodes.Count > 0);
			}
		}

		/// <summary>Gets or sets the target window or frame in which to display the Web page content associated with a node.</summary>
		/// <returns>The target window or frame in which to display the linked Web page content. Values must begin with a letter in the range of A through Z (case-insensitive), except for certain special values that begin with an underscore, as shown in the following table.Target value Description _blankRenders the content in a new window without frames. _parentRenders the content in the immediate frameset parent. _searchRenders the content in the search pane._selfRenders the content in the frame with focus. _topRenders the content in the full window without frames. NoteCheck your browser documentation to determine whether the _search value is supported. For example, Microsoft Internet Explorer 5.0 and later support the _search target value.The default value is an empty string (""), which refreshes the window or frame with focus.</returns>
		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06003026 RID: 12326 RVA: 0x0007CE7C File Offset: 0x0007B07C
		// (set) Token: 0x06003027 RID: 12327 RVA: 0x0007CEA9 File Offset: 0x0007B0A9
		[DefaultValue("")]
		public string Target
		{
			get
			{
				object obj = this.ViewState["Target"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		/// <summary>Gets or sets the text displayed for the node in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		/// <returns>The text displayed for the node in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control. The default is an empty string ("").</returns>
		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06003028 RID: 12328 RVA: 0x0007CEBC File Offset: 0x0007B0BC
		// (set) Token: 0x06003029 RID: 12329 RVA: 0x0007CEFD File Offset: 0x0007B0FD
		[WebSysDescription("The display text of the tree node.")]
		[Localizable(true)]
		[DefaultValue("")]
		public string Text
		{
			get
			{
				object obj = this.ViewState["Text"];
				if (obj == null)
				{
					obj = this.ViewState["Value"];
				}
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		/// <summary>Gets or sets the ToolTip text for the node.</summary>
		/// <returns>The ToolTip text for the node. The default is an empty string ("").</returns>
		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x0600302A RID: 12330 RVA: 0x0007CF10 File Offset: 0x0007B110
		// (set) Token: 0x0600302B RID: 12331 RVA: 0x0007CF3D File Offset: 0x0007B13D
		[Localizable(true)]
		[DefaultValue("")]
		public string ToolTip
		{
			get
			{
				object obj = this.ViewState["ToolTip"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ToolTip"] = value;
			}
		}

		/// <summary>Gets or sets a non-displayed value used to store any additional data about the node, such as data used for handling postback events.</summary>
		/// <returns>Supplemental data about the node that is not displayed. The default value is an empty string ("").</returns>
		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x0600302C RID: 12332 RVA: 0x0007CF50 File Offset: 0x0007B150
		// (set) Token: 0x0600302D RID: 12333 RVA: 0x0007CF91 File Offset: 0x0007B191
		[DefaultValue("")]
		[Localizable(true)]
		public string Value
		{
			get
			{
				object obj = this.ViewState["Value"];
				if (obj == null)
				{
					obj = this.ViewState["Text"];
				}
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["Value"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the node is selected.</summary>
		/// <returns>true if the node is selected; otherwise, false. The default is false.</returns>
		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x0600302E RID: 12334 RVA: 0x0007CFA4 File Offset: 0x0007B1A4
		// (set) Token: 0x0600302F RID: 12335 RVA: 0x0007CFAC File Offset: 0x0007B1AC
		[DefaultValue(false)]
		public bool Selected
		{
			get
			{
				return this.SelectedFlag;
			}
			set
			{
				this.SelectedFlag = value;
				if (this.tree != null)
				{
					if (!value && this.tree.SelectedNode == this)
					{
						this.tree.SetSelectedNode(null, false);
						return;
					}
					if (value)
					{
						this.tree.SetSelectedNode(this, false);
					}
				}
			}
		}

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x06003030 RID: 12336 RVA: 0x0007CFEC File Offset: 0x0007B1EC
		// (set) Token: 0x06003031 RID: 12337 RVA: 0x0007D015 File Offset: 0x0007B215
		internal virtual bool SelectedFlag
		{
			get
			{
				object obj = this.ViewState["Selected"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["Selected"] = value;
			}
		}

		/// <summary>Gets the parent node of the current node.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TreeNode" /> that represents the parent node of the current node.</returns>
		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x06003032 RID: 12338 RVA: 0x0007D02D File Offset: 0x0007B22D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public TreeNode Parent
		{
			get
			{
				return this.parent;
			}
		}

		/// <summary>Gets the path from the root node to the current node.</summary>
		/// <returns>A delimiter-separated list of node values that form a path from the root node to the current node.</returns>
		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x06003033 RID: 12339 RVA: 0x0007D038 File Offset: 0x0007B238
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string ValuePath
		{
			get
			{
				if (this.tree == null)
				{
					return this.Value;
				}
				StringBuilder stringBuilder = new StringBuilder(this.Value);
				for (TreeNode treeNode = this.parent; treeNode != null; treeNode = treeNode.Parent)
				{
					stringBuilder.Insert(0, this.tree.PathSeparator);
					stringBuilder.Insert(0, treeNode.Value);
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x06003034 RID: 12340 RVA: 0x0007D09A File Offset: 0x0007B29A
		// (set) Token: 0x06003035 RID: 12341 RVA: 0x0007D0A2 File Offset: 0x0007B2A2
		internal int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
				this.ResetPathData();
			}
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x0007D0B1 File Offset: 0x0007B2B1
		internal void SetParent(TreeNode node)
		{
			this.parent = node;
			this.ResetPathData();
		}

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x06003037 RID: 12343 RVA: 0x0007D0C0 File Offset: 0x0007B2C0
		internal string Path
		{
			get
			{
				if (this.path != null)
				{
					return this.path;
				}
				StringBuilder stringBuilder = new StringBuilder(this.index.ToString());
				for (TreeNode treeNode = this.parent; treeNode != null; treeNode = treeNode.Parent)
				{
					stringBuilder.Insert(0, '_');
					stringBuilder.Insert(0, treeNode.Index.ToString());
				}
				this.path = stringBuilder.ToString();
				return this.path;
			}
		}

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x06003038 RID: 12344 RVA: 0x0007D134 File Offset: 0x0007B334
		// (set) Token: 0x06003039 RID: 12345 RVA: 0x0007D15D File Offset: 0x0007B35D
		internal bool Populated
		{
			get
			{
				object obj = this.ViewState["Populated"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["Populated"] = value;
			}
		}

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x0600303A RID: 12346 RVA: 0x0007D175 File Offset: 0x0007B375
		internal bool HasChildData
		{
			get
			{
				return this.nodes != null;
			}
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x0007D180 File Offset: 0x0007B380
		internal void Populate()
		{
			if (this.tree == null)
			{
				return;
			}
			this.populating = true;
			this.tree.NotifyPopulateRequired(this);
			this.populating = false;
			this.Populated = true;
		}

		/// <summary>Collapses the current tree node.</summary>
		// Token: 0x0600303C RID: 12348 RVA: 0x0007D1AC File Offset: 0x0007B3AC
		public void Collapse()
		{
			this.Expanded = new bool?(false);
		}

		/// <summary>Collapses the current node and all its child nodes.</summary>
		// Token: 0x0600303D RID: 12349 RVA: 0x0007D1BA File Offset: 0x0007B3BA
		public void CollapseAll()
		{
			this.SetExpandedRec(false, -1);
		}

		/// <summary>Expands the current tree node.</summary>
		// Token: 0x0600303E RID: 12350 RVA: 0x0007D1C4 File Offset: 0x0007B3C4
		public void Expand()
		{
			this.Expanded = new bool?(true);
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x0007D1D2 File Offset: 0x0007B3D2
		internal void Expand(int depth)
		{
			this.SetExpandedRec(true, depth);
		}

		/// <summary>Expands the current node and all its child nodes.</summary>
		// Token: 0x06003040 RID: 12352 RVA: 0x0007D1DC File Offset: 0x0007B3DC
		public void ExpandAll()
		{
			this.SetExpandedRec(true, -1);
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x0007D1E8 File Offset: 0x0007B3E8
		private void SetExpandedRec(bool expanded, int depth)
		{
			this.Expanded = new bool?(expanded);
			if (depth == 0)
			{
				return;
			}
			foreach (object obj in this.ChildNodes)
			{
				((TreeNode)obj).SetExpandedRec(expanded, depth - 1);
			}
		}

		/// <summary>Selects the current node in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
		// Token: 0x06003042 RID: 12354 RVA: 0x0007D254 File Offset: 0x0007B454
		public void Select()
		{
			this.Selected = true;
		}

		/// <summary>Alternates between the expanded and collapsed state of the node.</summary>
		// Token: 0x06003043 RID: 12355 RVA: 0x0007D260 File Offset: 0x0007B460
		public void ToggleExpandState()
		{
			this.Expanded = new bool?(!this.Expanded.GetValueOrDefault(false));
		}

		/// <summary>Loads the node's previously saved view state.</summary>
		/// <param name="state">A <see cref="T:System.Object" /> that contains the saved view state values. </param>
		// Token: 0x06003044 RID: 12356 RVA: 0x0007D28A File Offset: 0x0007B48A
		void IStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		/// <summary>Loads the previously saved view state of the node. </summary>
		/// <param name="state">An <see cref="T:System.Object" /> that represents the state of the node.</param>
		// Token: 0x06003045 RID: 12357 RVA: 0x0007D294 File Offset: 0x0007B494
		protected virtual void LoadViewState(object state)
		{
			if (state == null)
			{
				return;
			}
			object[] array = (object[])state;
			this.ViewState.LoadViewState(array[0]);
			if (this.tree != null && this.SelectedFlag)
			{
				this.tree.SetSelectedNode(this, true);
			}
			if (!this.PopulateOnDemand || this.Populated)
			{
				((IStateManager)this.ChildNodes).LoadViewState(array[1]);
			}
		}

		/// <summary>Saves the view state changes to a <see cref="T:System.Object" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> that contains the view state changes.</returns>
		// Token: 0x06003046 RID: 12358 RVA: 0x0007D2F5 File Offset: 0x0007B4F5
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		/// <summary>Saves the current view state of the node. </summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the saved state of the node. </returns>
		// Token: 0x06003047 RID: 12359 RVA: 0x0007D300 File Offset: 0x0007B500
		protected virtual object SaveViewState()
		{
			object[] array = new object[]
			{
				this.ViewState.SaveViewState(),
				(this.nodes == null) ? null : ((IStateManager)this.nodes).SaveViewState()
			};
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		/// <summary>Instructs the <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to track changes to its view state.</summary>
		// Token: 0x06003048 RID: 12360 RVA: 0x0007D351 File Offset: 0x0007B551
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		/// <summary>Marks the starting point at which to begin tracking and saving view state changes to the node. </summary>
		// Token: 0x06003049 RID: 12361 RVA: 0x0007D359 File Offset: 0x0007B559
		protected void TrackViewState()
		{
			if (this.marked)
			{
				return;
			}
			this.marked = true;
			this.ViewState.TrackViewState();
			if (this.nodes != null)
			{
				((IStateManager)this.nodes).TrackViewState();
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IStateManager.IsTrackingViewState" />.</summary>
		/// <returns>A value that indicates whether the node is saving changes to its view state. </returns>
		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x0600304A RID: 12362 RVA: 0x0007D389 File Offset: 0x0007B589
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		/// <summary>Gets a value that indicates whether the node is saving changes to its view state. </summary>
		/// <returns>true if the control is marked to save its state; otherwise, false. </returns>
		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x0600304B RID: 12363 RVA: 0x0007D391 File Offset: 0x0007B591
		protected bool IsTrackingViewState
		{
			get
			{
				return this.marked;
			}
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x0007D399 File Offset: 0x0007B599
		internal void SetDirty()
		{
			this.ViewState.SetDirty(true);
			if (this.nodes != null)
			{
				this.nodes.SetDirty();
			}
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> class with the properties of the current <see cref="T:System.Web.UI.WebControls.TreeNode" /> instance.</summary>
		/// <returns>A new instance of <see cref="T:System.Web.UI.WebControls.TreeNode" /> with the properties of the current <see cref="T:System.Web.UI.WebControls.TreeNode" /> instance.</returns>
		// Token: 0x0600304D RID: 12365 RVA: 0x0007D3BC File Offset: 0x0007B5BC
		public virtual object Clone()
		{
			TreeNode treeNode = ((this.tree != null) ? this.tree.CreateNode() : new TreeNode());
			foreach (object obj in this.ViewState)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				treeNode.ViewState[(string)dictionaryEntry.Key] = ((StateItem)dictionaryEntry.Value).Value;
			}
			foreach (object obj2 in this.ChildNodes)
			{
				TreeNode treeNode2 = (TreeNode)obj2;
				treeNode.ChildNodes.Add((TreeNode)treeNode2.Clone());
			}
			return treeNode;
		}

		/// <summary>Creates a copy of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents a copy of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> object.</returns>
		// Token: 0x0600304E RID: 12366 RVA: 0x0007D4B0 File Offset: 0x0007B6B0
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x0600304F RID: 12367 RVA: 0x0007D4B8 File Offset: 0x0007B6B8
		internal void Bind(IHierarchyData hierarchyData)
		{
			this.hierarchyData = hierarchyData;
			this.DataBound = true;
			this.DataPath = hierarchyData.Path;
			this.dataItem = hierarchyData.Item;
			TreeNodeBinding treeNodeBinding = this.GetBinding();
			if (treeNodeBinding != null)
			{
				if (treeNodeBinding.ImageToolTipField.Length > 0)
				{
					this.ImageToolTip = Convert.ToString(this.GetBoundPropertyValue(treeNodeBinding.ImageToolTipField));
					if (this.ImageToolTip.Length == 0)
					{
						this.ImageToolTip = treeNodeBinding.ImageToolTip;
					}
				}
				else if (treeNodeBinding.ImageToolTip.Length > 0)
				{
					this.ImageToolTip = treeNodeBinding.ImageToolTip;
				}
				if (treeNodeBinding.ImageUrlField.Length > 0)
				{
					this.ImageUrl = Convert.ToString(this.GetBoundPropertyValue(treeNodeBinding.ImageUrlField));
					if (this.ImageUrl.Length == 0)
					{
						this.ImageUrl = treeNodeBinding.ImageUrl;
					}
				}
				else if (treeNodeBinding.ImageUrl.Length > 0)
				{
					this.ImageUrl = treeNodeBinding.ImageUrl;
				}
				if (treeNodeBinding.NavigateUrlField.Length > 0)
				{
					this.NavigateUrl = Convert.ToString(this.GetBoundPropertyValue(treeNodeBinding.NavigateUrlField));
					if (this.NavigateUrl.Length == 0)
					{
						this.NavigateUrl = treeNodeBinding.NavigateUrl;
					}
				}
				else if (treeNodeBinding.NavigateUrl.Length > 0)
				{
					this.NavigateUrl = treeNodeBinding.NavigateUrl;
				}
				if (treeNodeBinding.HasPropertyValue("PopulateOnDemand"))
				{
					this.PopulateOnDemand = treeNodeBinding.PopulateOnDemand;
				}
				if (treeNodeBinding.HasPropertyValue("SelectAction"))
				{
					this.SelectAction = treeNodeBinding.SelectAction;
				}
				if (treeNodeBinding.HasPropertyValue("ShowCheckBox"))
				{
					this.ShowCheckBox = treeNodeBinding.ShowCheckBox;
				}
				if (treeNodeBinding.TargetField.Length > 0)
				{
					this.Target = Convert.ToString(this.GetBoundPropertyValue(treeNodeBinding.TargetField));
					if (this.Target.Length == 0)
					{
						this.Target = treeNodeBinding.Target;
					}
				}
				else if (treeNodeBinding.Target.Length > 0)
				{
					this.Target = treeNodeBinding.Target;
				}
				string text = null;
				if (treeNodeBinding.TextField.Length > 0)
				{
					text = Convert.ToString(this.GetBoundPropertyValue(treeNodeBinding.TextField));
					if (treeNodeBinding.FormatString.Length > 0)
					{
						text = string.Format(treeNodeBinding.FormatString, text);
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					if (treeNodeBinding.Text.Length > 0)
					{
						text = treeNodeBinding.Text;
					}
					else if (treeNodeBinding.Value.Length > 0)
					{
						text = treeNodeBinding.Value;
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					this.Text = text;
				}
				if (treeNodeBinding.ToolTipField.Length > 0)
				{
					this.ToolTip = Convert.ToString(this.GetBoundPropertyValue(treeNodeBinding.ToolTipField));
					if (this.ToolTip.Length == 0)
					{
						this.ToolTip = treeNodeBinding.ToolTip;
					}
				}
				else if (treeNodeBinding.ToolTip.Length > 0)
				{
					this.ToolTip = treeNodeBinding.ToolTip;
				}
				string text2 = null;
				if (treeNodeBinding.ValueField.Length > 0)
				{
					text2 = Convert.ToString(this.GetBoundPropertyValue(treeNodeBinding.ValueField));
				}
				if (string.IsNullOrEmpty(text2))
				{
					if (treeNodeBinding.Value.Length > 0)
					{
						text2 = treeNodeBinding.Value;
					}
					else if (treeNodeBinding.Text.Length > 0)
					{
						text2 = treeNodeBinding.Text;
					}
				}
				if (!string.IsNullOrEmpty(text2))
				{
					this.Value = text2;
				}
			}
			else
			{
				this.Text = (this.Value = this.GetDefaultBoundText());
			}
			INavigateUIData navigateUIData = hierarchyData as INavigateUIData;
			if (navigateUIData != null)
			{
				this.SelectAction = TreeNodeSelectAction.None;
				this.Text = navigateUIData.ToString();
				this.NavigateUrl = navigateUIData.NavigateUrl;
				this.ToolTip = navigateUIData.Description;
			}
		}

		// Token: 0x06003050 RID: 12368 RVA: 0x0007D838 File Offset: 0x0007BA38
		internal void SetDataItem(object item)
		{
			this.dataItem = item;
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x0007D841 File Offset: 0x0007BA41
		internal void SetDataPath(string path)
		{
			this.DataPath = path;
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x0007D84A File Offset: 0x0007BA4A
		internal void SetDataBound(bool bound)
		{
			this.DataBound = bound;
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x0007D853 File Offset: 0x0007BA53
		private string GetDefaultBoundText()
		{
			if (this.hierarchyData != null)
			{
				return this.hierarchyData.ToString();
			}
			if (this.dataItem != null)
			{
				return this.dataItem.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x0007D882 File Offset: 0x0007BA82
		private string GetDataItemType()
		{
			if (this.hierarchyData != null)
			{
				return this.hierarchyData.Type;
			}
			if (this.dataItem != null)
			{
				return this.dataItem.GetType().ToString();
			}
			return string.Empty;
		}

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x06003055 RID: 12373 RVA: 0x0007D8B6 File Offset: 0x0007BAB6
		internal bool IsParentNode
		{
			get
			{
				return this.ChildNodes.Count > 0 || (this.PopulateOnDemand && !this.Populated);
			}
		}

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x06003056 RID: 12374 RVA: 0x0007D8DB File Offset: 0x0007BADB
		internal bool IsLeafNode
		{
			get
			{
				return !this.IsParentNode;
			}
		}

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x06003057 RID: 12375 RVA: 0x0007D8E6 File Offset: 0x0007BAE6
		internal bool IsRootNode
		{
			get
			{
				return this.Depth == 0;
			}
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x0007D8F4 File Offset: 0x0007BAF4
		private TreeNodeBinding GetBinding()
		{
			if (this.tree == null)
			{
				return null;
			}
			if (this.gotBinding)
			{
				return this.binding;
			}
			this.binding = this.tree.FindBindingForNode(this.GetDataItemType(), this.Depth);
			this.gotBinding = true;
			return this.binding;
		}

		// Token: 0x06003059 RID: 12377 RVA: 0x0007D944 File Offset: 0x0007BB44
		private object GetBoundPropertyValue(string name)
		{
			if (this.boundProperties == null)
			{
				if (this.hierarchyData != null)
				{
					this.boundProperties = TypeDescriptor.GetProperties(this.hierarchyData);
				}
				else
				{
					this.boundProperties = TypeDescriptor.GetProperties(this.dataItem);
				}
			}
			PropertyDescriptor propertyDescriptor = this.boundProperties.Find(name, true);
			if (propertyDescriptor == null)
			{
				throw new InvalidOperationException("Property '" + name + "' not found in data bound item");
			}
			if (this.hierarchyData != null)
			{
				return propertyDescriptor.GetValue(this.hierarchyData);
			}
			return propertyDescriptor.GetValue(this.dataItem);
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x0007D9CD File Offset: 0x0007BBCD
		internal void BeginRenderText(HtmlTextWriter writer)
		{
			this.RenderPreText(writer);
		}

		// Token: 0x0600305B RID: 12379 RVA: 0x0007D9D6 File Offset: 0x0007BBD6
		internal void EndRenderText(HtmlTextWriter writer)
		{
			this.RenderPostText(writer);
		}

		/// <summary>Allows control developers to add additional rendering to the node.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream used to write content to a Web page.</param>
		// Token: 0x0600305C RID: 12380 RVA: 0x00003946 File Offset: 0x00001B46
		protected virtual void RenderPreText(HtmlTextWriter writer)
		{
		}

		/// <summary>Allows control developers to add additional rendering to the node.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream used to write content to a Web page. </param>
		// Token: 0x0600305D RID: 12381 RVA: 0x00003946 File Offset: 0x00001B46
		protected virtual void RenderPostText(HtmlTextWriter writer)
		{
		}

		// Token: 0x04001C01 RID: 7169
		private StateBag ViewState = new StateBag();

		// Token: 0x04001C02 RID: 7170
		private TreeNodeCollection nodes;

		// Token: 0x04001C03 RID: 7171
		private bool marked;

		// Token: 0x04001C04 RID: 7172
		private TreeView tree;

		// Token: 0x04001C05 RID: 7173
		private TreeNode parent;

		// Token: 0x04001C06 RID: 7174
		private int index;

		// Token: 0x04001C07 RID: 7175
		private string path;

		// Token: 0x04001C08 RID: 7176
		private int depth = -1;

		// Token: 0x04001C09 RID: 7177
		private object dataItem;

		// Token: 0x04001C0A RID: 7178
		private IHierarchyData hierarchyData;

		// Token: 0x04001C0B RID: 7179
		private bool gotBinding;

		// Token: 0x04001C0C RID: 7180
		private TreeNodeBinding binding;

		// Token: 0x04001C0D RID: 7181
		private PropertyDescriptorCollection boundProperties;

		// Token: 0x04001C0E RID: 7182
		private bool populating;

		// Token: 0x04001C0F RID: 7183
		private bool hadChildrenBeforePopulating;
	}
}
