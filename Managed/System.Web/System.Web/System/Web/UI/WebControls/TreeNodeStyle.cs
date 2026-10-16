using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the style of a node in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	// Token: 0x0200042B RID: 1067
	public sealed class TreeNodeStyle : Style
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> class.</summary>
		// Token: 0x060030BA RID: 12474 RVA: 0x0006C32C File Offset: 0x0006A52C
		public TreeNodeStyle()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> class with the specified <see cref="T:System.Web.UI.StateBag" /> object information.</summary>
		/// <param name="bag">A <see cref="T:System.Web.UI.StateBag" /> that stores the style information.</param>
		// Token: 0x060030BB RID: 12475 RVA: 0x0006C334 File Offset: 0x0006A534
		public TreeNodeStyle(StateBag bag)
			: base(bag)
		{
		}

		/// <summary>Gets or sets the URL to an image that is displayed next to the node.</summary>
		/// <returns>The URL to a custom image that is displayed next to the node. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeStyle.ImageUrl" /> property is not set.</returns>
		/// <exception cref="T:System.ArgumentNullException">The selected value is null.</exception>
		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x060030BC RID: 12476 RVA: 0x0007E650 File Offset: 0x0007C850
		// (set) Token: 0x060030BD RID: 12477 RVA: 0x0007E67A File Offset: 0x0007C87A
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		public string ImageUrl
		{
			get
			{
				if (!base.CheckBit(262144))
				{
					return string.Empty;
				}
				return base.ViewState.GetString("ImageUrl", string.Empty);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.ViewState["ImageUrl"] = value;
				this.SetBit(262144);
			}
		}

		/// <summary>Gets or sets the amount of space between a parent node and a child node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> class is applied.</summary>
		/// <returns>The amount of space, in pixels, that is above and below the child nodes section of a parent node. The default is 0 pixels.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is of type <see cref="F:System.Web.UI.WebControls.UnitType.Percentage" /> or is less than 0.</exception>
		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x060030BE RID: 12478 RVA: 0x0007E6A8 File Offset: 0x0007C8A8
		// (set) Token: 0x060030BF RID: 12479 RVA: 0x0007E6F7 File Offset: 0x0007C8F7
		[DefaultValue(0)]
		[NotifyParentProperty(true)]
		public Unit ChildNodesPadding
		{
			get
			{
				if (!base.CheckBit(65536))
				{
					return 0;
				}
				if (base.ViewState["ChildNodesPadding"] != null)
				{
					return (Unit)base.ViewState["ChildNodesPadding"];
				}
				return 0;
			}
			set
			{
				base.ViewState["ChildNodesPadding"] = value;
				this.SetBit(65536);
			}
		}

		/// <summary>Gets or sets the amount of space to the left and right of the text in the node.</summary>
		/// <returns>The amount of space, in pixels, that is to the left and right of the node's text. The default is 0 pixels.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is of type <see cref="F:System.Web.UI.WebControls.UnitType.Percentage" /> or is less than 0.</exception>
		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x060030C0 RID: 12480 RVA: 0x0007E71C File Offset: 0x0007C91C
		// (set) Token: 0x060030C1 RID: 12481 RVA: 0x0007E76B File Offset: 0x0007C96B
		[NotifyParentProperty(true)]
		[DefaultValue(0)]
		public Unit HorizontalPadding
		{
			get
			{
				if (!base.CheckBit(131072))
				{
					return 0;
				}
				if (base.ViewState["HorizontalPadding"] != null)
				{
					return (Unit)base.ViewState["HorizontalPadding"];
				}
				return 0;
			}
			set
			{
				base.ViewState["HorizontalPadding"] = value;
				this.SetBit(131072);
			}
		}

		/// <summary>Gets or sets the amount of space above and below the text for a node.</summary>
		/// <returns>The amount of space, in pixels, above and below a node's text. The default is 0 pixels.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is of type <see cref="F:System.Web.UI.WebControls.UnitType.Percentage" /> or is less than 0.</exception>
		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x060030C2 RID: 12482 RVA: 0x0007E790 File Offset: 0x0007C990
		// (set) Token: 0x060030C3 RID: 12483 RVA: 0x0007E7DF File Offset: 0x0007C9DF
		[DefaultValue(0)]
		[NotifyParentProperty(true)]
		public Unit VerticalPadding
		{
			get
			{
				if (!base.CheckBit(1048576))
				{
					return 0;
				}
				if (base.ViewState["VerticalPadding"] != null)
				{
					return (Unit)base.ViewState["VerticalPadding"];
				}
				return 0;
			}
			set
			{
				base.ViewState["VerticalPadding"] = value;
				this.SetBit(1048576);
			}
		}

		/// <summary>Gets or sets the amount of vertical spacing between the node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object is applied and its adjacent nodes.</summary>
		/// <returns>The amount of vertical space, in pixels, between the node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> is applied and its adjacent nodes at the same level. The default is 0 pixels.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is of type <see cref="F:System.Web.UI.WebControls.UnitType.Percentage" /> or is less than 0.</exception>
		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x060030C4 RID: 12484 RVA: 0x0007E804 File Offset: 0x0007CA04
		// (set) Token: 0x060030C5 RID: 12485 RVA: 0x0007E853 File Offset: 0x0007CA53
		[DefaultValue(0)]
		[NotifyParentProperty(true)]
		public Unit NodeSpacing
		{
			get
			{
				if (!base.CheckBit(524288))
				{
					return 0;
				}
				if (base.ViewState["NodeSpacing"] != null)
				{
					return (Unit)base.ViewState["NodeSpacing"];
				}
				return 0;
			}
			set
			{
				base.ViewState["NodeSpacing"] = value;
				this.SetBit(524288);
			}
		}

		/// <summary>Copies the style properties of the specified <see cref="T:System.Web.UI.WebControls.Style" /> object into the current <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object.</summary>
		/// <param name="s">The <see cref="T:System.Web.UI.WebControls.Style" /> to copy. </param>
		// Token: 0x060030C6 RID: 12486 RVA: 0x0007E878 File Offset: 0x0007CA78
		public override void CopyFrom(Style s)
		{
			if (s == null || s.IsEmpty)
			{
				return;
			}
			base.CopyFrom(s);
			TreeNodeStyle treeNodeStyle = s as TreeNodeStyle;
			if (treeNodeStyle == null)
			{
				return;
			}
			if (treeNodeStyle.CheckBit(65536))
			{
				this.ChildNodesPadding = treeNodeStyle.ChildNodesPadding;
			}
			if (treeNodeStyle.CheckBit(131072))
			{
				this.HorizontalPadding = treeNodeStyle.HorizontalPadding;
			}
			if (treeNodeStyle.CheckBit(262144))
			{
				this.ImageUrl = treeNodeStyle.ImageUrl;
			}
			if (treeNodeStyle.CheckBit(524288))
			{
				this.NodeSpacing = treeNodeStyle.NodeSpacing;
			}
			if (treeNodeStyle.CheckBit(1048576))
			{
				this.VerticalPadding = treeNodeStyle.VerticalPadding;
			}
		}

		/// <summary>Combines the style properties of the specified <see cref="T:System.Web.UI.WebControls.Style" /> object with the style properties of the current <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object.</summary>
		/// <param name="s">The <see cref="T:System.Web.UI.WebControls.Style" /> that will merge with the current node's settings. </param>
		// Token: 0x060030C7 RID: 12487 RVA: 0x0007E920 File Offset: 0x0007CB20
		public override void MergeWith(Style s)
		{
			if (s != null && !s.IsEmpty)
			{
				if (this.IsEmpty)
				{
					this.CopyFrom(s);
					return;
				}
				base.MergeWith(s);
				TreeNodeStyle treeNodeStyle = s as TreeNodeStyle;
				if (treeNodeStyle == null)
				{
					return;
				}
				if (treeNodeStyle.CheckBit(65536) && !base.CheckBit(65536))
				{
					this.ChildNodesPadding = treeNodeStyle.ChildNodesPadding;
				}
				if (treeNodeStyle.CheckBit(131072) && !base.CheckBit(131072))
				{
					this.HorizontalPadding = treeNodeStyle.HorizontalPadding;
				}
				if (treeNodeStyle.CheckBit(262144) && !base.CheckBit(262144))
				{
					this.ImageUrl = treeNodeStyle.ImageUrl;
				}
				if (treeNodeStyle.CheckBit(524288) && !base.CheckBit(524288))
				{
					this.NodeSpacing = treeNodeStyle.NodeSpacing;
				}
				if (treeNodeStyle.CheckBit(1048576) && !base.CheckBit(1048576))
				{
					this.VerticalPadding = treeNodeStyle.VerticalPadding;
				}
			}
		}

		/// <summary>Returns the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object to its original state.</summary>
		// Token: 0x060030C8 RID: 12488 RVA: 0x0007EA20 File Offset: 0x0007CC20
		public override void Reset()
		{
			if (base.CheckBit(65536))
			{
				base.ViewState.Remove("ChildNodesPadding");
			}
			if (base.CheckBit(131072))
			{
				base.ViewState.Remove("HorizontalPadding");
			}
			if (base.CheckBit(262144))
			{
				base.ViewState.Remove("ImageUrl");
			}
			if (base.CheckBit(524288))
			{
				base.ViewState.Remove("NodeSpacing");
			}
			if (base.CheckBit(1048576))
			{
				base.ViewState.Remove("VerticalPadding");
			}
			base.Reset();
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x0007EAC4 File Offset: 0x0007CCC4
		protected override void FillStyleAttributes(CssStyleCollection attributes, IUrlResolutionService urlResolver)
		{
			base.FillStyleAttributes(attributes, urlResolver);
			if (base.CheckBit(131072))
			{
				attributes.Add(HtmlTextWriterStyle.PaddingLeft, this.HorizontalPadding.ToString());
				attributes.Add(HtmlTextWriterStyle.PaddingRight, this.HorizontalPadding.ToString());
			}
			if (base.CheckBit(1048576))
			{
				attributes.Add(HtmlTextWriterStyle.PaddingTop, this.VerticalPadding.ToString());
				attributes.Add(HtmlTextWriterStyle.PaddingBottom, this.VerticalPadding.ToString());
			}
		}

		// Token: 0x04001C17 RID: 7191
		private const string CHILD_PADD = "ChildNodesPadding";

		// Token: 0x04001C18 RID: 7192
		private const string HORZ_PADD = "HorizontalPadding";

		// Token: 0x04001C19 RID: 7193
		private const string IMG_URL = "ImageUrl";

		// Token: 0x04001C1A RID: 7194
		private const string SPACING = "NodeSpacing";

		// Token: 0x04001C1B RID: 7195
		private const string VERT_PADD = "VerticalPadding";

		// Token: 0x0200042C RID: 1068
		[Flags]
		private enum TreeNodeStyles
		{
			// Token: 0x04001C1D RID: 7197
			ChildNodesPadding = 65536,
			// Token: 0x04001C1E RID: 7198
			HorizontalPadding = 131072,
			// Token: 0x04001C1F RID: 7199
			ImageUrl = 262144,
			// Token: 0x04001C20 RID: 7200
			NodeSpacing = 524288,
			// Token: 0x04001C21 RID: 7201
			VerticalPadding = 1048576
		}
	}
}
