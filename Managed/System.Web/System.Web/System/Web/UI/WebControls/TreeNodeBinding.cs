using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Web.UI.WebControls
{
	/// <summary>Defines the relationship between a data item and the node it is binding to in a <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	// Token: 0x02000428 RID: 1064
	[DefaultProperty("TextField")]
	public sealed class TreeNodeBinding : IStateManager, ICloneable, IDataSourceViewSchemaAccessor
	{
		/// <summary>Gets or sets the value to match against a <see cref="P:System.Web.UI.IHierarchyData.Type" /> property for a data item to determine whether to apply the tree node binding.</summary>
		/// <returns>The value to match against a data item's <see cref="P:System.Web.UI.IHierarchyData.Type" /> property to determine whether to apply the tree node binding. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.DataMember" /> property is not set.</returns>
		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x0600305E RID: 12382 RVA: 0x0007D9E0 File Offset: 0x0007BBE0
		// (set) Token: 0x0600305F RID: 12383 RVA: 0x0007DA0D File Offset: 0x0007BC0D
		[DefaultValue("")]
		public string DataMember
		{
			get
			{
				object obj = this.ViewState["DataMember"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["DataMember"] = value;
			}
		}

		/// <summary>Gets or sets the node depth at which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>The node depth at which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is -1, indicating that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.Depth" /> property is not set.</returns>
		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x06003060 RID: 12384 RVA: 0x0007DA20 File Offset: 0x0007BC20
		// (set) Token: 0x06003061 RID: 12385 RVA: 0x0007DA49 File Offset: 0x0007BC49
		[DefaultValue(-1)]
		public int Depth
		{
			get
			{
				object obj = this.ViewState["Depth"];
				if (obj != null)
				{
					return (int)obj;
				}
				return -1;
			}
			set
			{
				this.ViewState["Depth"] = value;
			}
		}

		/// <summary>Gets or sets the string that specifies the display format for the text of a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>A formatting string that specifies the display format for the text of a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.FormatString" /> property is not set.</returns>
		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x06003062 RID: 12386 RVA: 0x0007DA64 File Offset: 0x0007BC64
		// (set) Token: 0x06003063 RID: 12387 RVA: 0x0007DA91 File Offset: 0x0007BC91
		[Localizable(true)]
		[DefaultValue("")]
		public string FormatString
		{
			get
			{
				object obj = this.ViewState["FormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["FormatString"] = value;
			}
		}

		/// <summary>Gets or sets the ToolTip text for the image that is displayed next to a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>The ToolTip text for the image that is displayed next to a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the P:System.Web.UI.WebControls.TreeNodeBinding.ImageToolTip property is not set.</returns>
		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06003064 RID: 12388 RVA: 0x0007DAA4 File Offset: 0x0007BCA4
		// (set) Token: 0x06003065 RID: 12389 RVA: 0x0007DAD1 File Offset: 0x0007BCD1
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
				return "";
			}
			set
			{
				this.ViewState["ImageToolTip"] = value;
			}
		}

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.ImageToolTip" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.ImageToolTip" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.ImageToolTipField" /> property is not set.</returns>
		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06003066 RID: 12390 RVA: 0x0007DAE4 File Offset: 0x0007BCE4
		// (set) Token: 0x06003067 RID: 12391 RVA: 0x0007DB11 File Offset: 0x0007BD11
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string ImageToolTipField
		{
			get
			{
				object obj = this.ViewState["ImageToolTipField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["ImageToolTipField"] = value;
			}
		}

		/// <summary>Gets or sets the URL to an image that is displayed next to a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>The URL to an image that is displayed next to a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.ImageUrl" /> property is not set.</returns>
		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x06003068 RID: 12392 RVA: 0x0007DB24 File Offset: 0x0007BD24
		// (set) Token: 0x06003069 RID: 12393 RVA: 0x0007DB51 File Offset: 0x0007BD51
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
		[UrlProperty]
		public string ImageUrl
		{
			get
			{
				object obj = this.ViewState["ImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.ImageUrl" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.ImageUrl" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.ImageUrlField" /> property is not set.</returns>
		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x0600306A RID: 12394 RVA: 0x0007DB64 File Offset: 0x0007BD64
		// (set) Token: 0x0600306B RID: 12395 RVA: 0x0007DB91 File Offset: 0x0007BD91
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ImageUrlField
		{
			get
			{
				object obj = this.ViewState["ImageUrlField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["ImageUrlField"] = value;
			}
		}

		/// <summary>Gets or sets the URL to link to when a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied is clicked.</summary>
		/// <returns>The URL to link to when a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied is clicked. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.NavigateUrl" /> property is not set.</returns>
		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x0600306C RID: 12396 RVA: 0x0007DBA4 File Offset: 0x0007BDA4
		// (set) Token: 0x0600306D RID: 12397 RVA: 0x0007DBD1 File Offset: 0x0007BDD1
		[DefaultValue("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string NavigateUrl
		{
			get
			{
				object obj = this.ViewState["NavigateUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["NavigateUrl"] = value;
			}
		}

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.NavigateUrl" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.NavigateUrl" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.NavigateUrlField" /> property is not set.</returns>
		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x0600306E RID: 12398 RVA: 0x0007DBE4 File Offset: 0x0007BDE4
		// (set) Token: 0x0600306F RID: 12399 RVA: 0x0007DC11 File Offset: 0x0007BE11
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string NavigateUrlField
		{
			get
			{
				object obj = this.ViewState["NavigateUrlField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["NavigateUrlField"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied is populated dynamically.</summary>
		/// <returns>true to populate the node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied dynamically; otherwise, false. The default is false.</returns>
		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x06003070 RID: 12400 RVA: 0x0007DC24 File Offset: 0x0007BE24
		// (set) Token: 0x06003071 RID: 12401 RVA: 0x0007DC4D File Offset: 0x0007BE4D
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
			}
		}

		/// <summary>Gets or sets the event or events to raise when a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied is selected.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TreeNodeSelectAction" /> values. The default is TreeNodeSelectAction.Select.</returns>
		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x06003072 RID: 12402 RVA: 0x0007DC68 File Offset: 0x0007BE68
		// (set) Token: 0x06003073 RID: 12403 RVA: 0x0007DC91 File Offset: 0x0007BE91
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

		/// <summary>Gets or sets a value indicating whether a check box is displayed next to a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>true to display a check box next to a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied; otherwise, false. The default is false.</returns>
		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x06003074 RID: 12404 RVA: 0x0007DCA9 File Offset: 0x0007BEA9
		// (set) Token: 0x06003075 RID: 12405 RVA: 0x0007DCC0 File Offset: 0x0007BEC0
		[DefaultValue(false)]
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

		/// <summary>Gets or sets the target window or frame in which to display the Web page content that is associated with a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>The target window or frame in which to display the linked Web page content. Values must begin with a letter in the range of A through Z (case insensitive), except for certain special values that begin with an underscore, as shown in the following table.Target value Description _blankRenders the content in a new window without frames. _parentRenders the content in the immediate frameset parent. _searchRenders the content in the search pane._selfRenders the content in the frame with focus. _topRenders the content in the full window without frames. NoteCheck your browser documentation to determine if the _search value is supported.  For example, Microsoft Internet Explorer version 5.0 and later supports the _search target value.The default is an empty string (""), which refreshes the window or frame with focus.</returns>
		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06003076 RID: 12406 RVA: 0x0007DCD8 File Offset: 0x0007BED8
		// (set) Token: 0x06003077 RID: 12407 RVA: 0x0007DD05 File Offset: 0x0007BF05
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
				return "";
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.Target" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.Target" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.TargetField" /> property is not set.</returns>
		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06003078 RID: 12408 RVA: 0x0007DD18 File Offset: 0x0007BF18
		// (set) Token: 0x06003079 RID: 12409 RVA: 0x0007DD45 File Offset: 0x0007BF45
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string TargetField
		{
			get
			{
				object obj = this.ViewState["TargetField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["TargetField"] = value;
			}
		}

		/// <summary>Gets or sets the text that is displayed for the node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>The text displayed for the node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string ("").</returns>
		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x0600307A RID: 12410 RVA: 0x0007DD58 File Offset: 0x0007BF58
		// (set) Token: 0x0600307B RID: 12411 RVA: 0x0007DD85 File Offset: 0x0007BF85
		[WebSysDescription("The display text of the tree node.")]
		[DefaultValue("")]
		[Localizable(true)]
		public string Text
		{
			get
			{
				object obj = this.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.Text" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.Text" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.TextField" /> property is not set.</returns>
		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x0600307C RID: 12412 RVA: 0x0007DD98 File Offset: 0x0007BF98
		// (set) Token: 0x0600307D RID: 12413 RVA: 0x0007DDC5 File Offset: 0x0007BFC5
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string TextField
		{
			get
			{
				object obj = this.ViewState["TextField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["TextField"] = value;
			}
		}

		/// <summary>Gets or sets the ToolTip text for a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>The ToolTip text for a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.ToolTip" /> property is not set.</returns>
		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x0600307E RID: 12414 RVA: 0x0007DDD8 File Offset: 0x0007BFD8
		// (set) Token: 0x0600307F RID: 12415 RVA: 0x0007DE05 File Offset: 0x0007C005
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
				return "";
			}
			set
			{
				this.ViewState["ToolTip"] = value;
			}
		}

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.ToolTip" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.ToolTip" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.ToolTipField" /> property is not set.</returns>
		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x06003080 RID: 12416 RVA: 0x0007DE18 File Offset: 0x0007C018
		// (set) Token: 0x06003081 RID: 12417 RVA: 0x0007DE45 File Offset: 0x0007C045
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ToolTipField
		{
			get
			{
				object obj = this.ViewState["ToolTipField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["ToolTipField"] = value;
			}
		}

		/// <summary>Gets or sets a displayed value that is not displayed but is used to store any additional data about a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied, such as data used for handling postback events.</summary>
		/// <returns>Supplemental data about a node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied; this data is not displayed. The default is an empty string ("").</returns>
		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06003082 RID: 12418 RVA: 0x0007DE58 File Offset: 0x0007C058
		// (set) Token: 0x06003083 RID: 12419 RVA: 0x0007DE85 File Offset: 0x0007C085
		[Localizable(true)]
		[DefaultValue("")]
		public string Value
		{
			get
			{
				object obj = this.ViewState["Value"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["Value"] = value;
			}
		}

		// Token: 0x06003084 RID: 12420 RVA: 0x0007DE98 File Offset: 0x0007C098
		internal bool HasPropertyValue(string propName)
		{
			return this.ViewState[propName] != null;
		}

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.Value" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied.</summary>
		/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.TreeNode.Value" /> property of a <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to which the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is applied. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.ValueField" /> property is not set.</returns>
		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x06003085 RID: 12421 RVA: 0x0007DEAC File Offset: 0x0007C0AC
		// (set) Token: 0x06003086 RID: 12422 RVA: 0x0007DED9 File Offset: 0x0007C0D9
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ValueField
		{
			get
			{
				object obj = this.ViewState["ValueField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["ValueField"] = value;
			}
		}

		/// <summary>Loads the previously saved view state for the node.</summary>
		/// <param name="state">A <see cref="T:System.Object" /> that contains the saved view state values. </param>
		// Token: 0x06003087 RID: 12423 RVA: 0x0007DEEC File Offset: 0x0007C0EC
		void IStateManager.LoadViewState(object savedState)
		{
			this.ViewState.LoadViewState(savedState);
		}

		/// <summary>Saves the view state changes to an object.</summary>
		/// <returns>The object that contains the view state changes.</returns>
		// Token: 0x06003088 RID: 12424 RVA: 0x0007DEFA File Offset: 0x0007C0FA
		object IStateManager.SaveViewState()
		{
			return this.ViewState.SaveViewState();
		}

		/// <summary>Instructs the <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to track changes to its view state.</summary>
		// Token: 0x06003089 RID: 12425 RVA: 0x0007DF07 File Offset: 0x0007C107
		void IStateManager.TrackViewState()
		{
			this.ViewState.TrackViewState();
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IStateManager.IsTrackingViewState" />.</summary>
		/// <returns>true, if the control is marked to save its state; otherwise, false.</returns>
		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x0600308A RID: 12426 RVA: 0x0007DF14 File Offset: 0x0007C114
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.ViewState.IsTrackingViewState;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataSourceViewSchemaAccessor.DataSourceViewSchema" />.</summary>
		/// <returns>An object that represents the schema that is associated with the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object.</returns>
		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x0600308B RID: 12427 RVA: 0x00003A2B File Offset: 0x00001C2B
		// (set) Token: 0x0600308C RID: 12428 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		object IDataSourceViewSchemaAccessor.DataSourceViewSchema
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Creates a copy of the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object.</summary>
		/// <returns>An object that represents a copy of the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object.</returns>
		// Token: 0x0600308D RID: 12429 RVA: 0x0007DF24 File Offset: 0x0007C124
		object ICloneable.Clone()
		{
			TreeNodeBinding treeNodeBinding = new TreeNodeBinding();
			foreach (object obj in this.ViewState)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				treeNodeBinding.ViewState[(string)dictionaryEntry.Key] = dictionaryEntry.Value;
			}
			return treeNodeBinding;
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x0007DF9C File Offset: 0x0007C19C
		internal void SetDirty()
		{
			foreach (object obj in this.ViewState.Keys)
			{
				string text = (string)obj;
				this.ViewState.SetItemDirty(text, true);
			}
		}

		/// <summary>Returns the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.DataMember" /> property.</summary>
		/// <returns>Returns the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.DataMember" /> property. If the <see cref="P:System.Web.UI.WebControls.TreeNodeBinding.DataMember" /> property is null or an empty string (""), (Empty) is returned.</returns>
		// Token: 0x0600308F RID: 12431 RVA: 0x0007E000 File Offset: 0x0007C200
		public override string ToString()
		{
			if (this.DataMember.Length <= 0)
			{
				return "(Empty)";
			}
			return this.DataMember;
		}

		// Token: 0x04001C10 RID: 7184
		private StateBag ViewState = new StateBag();
	}
}
