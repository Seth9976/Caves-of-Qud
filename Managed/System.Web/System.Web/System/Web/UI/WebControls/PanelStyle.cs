using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the style for a <see cref="T:System.Web.UI.WebControls.Panel" /> control.</summary>
	// Token: 0x020003E0 RID: 992
	public class PanelStyle : Style
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.PanelStyle" /> class.</summary>
		/// <param name="bag">A <see cref="T:System.Web.UI.StateBag" /> object that represents the state bag in which to store style information.</param>
		// Token: 0x06002B93 RID: 11155 RVA: 0x0006C334 File Offset: 0x0006A534
		public PanelStyle(StateBag bag)
			: base(bag)
		{
		}

		/// <summary>Gets or sets the URL of the background image for the panel control.</summary>
		/// <returns>The URL of the background image for the panel control. The default value is an empty string ("").</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Web.UI.WebControls.PanelStyle.BackImageUrl" /> property is null.</exception>
		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x06002B94 RID: 11156 RVA: 0x00071B8A File Offset: 0x0006FD8A
		// (set) Token: 0x06002B95 RID: 11157 RVA: 0x00071BB4 File Offset: 0x0006FDB4
		[DefaultValue("")]
		[UrlProperty]
		public virtual string BackImageUrl
		{
			get
			{
				if (!base.CheckBit(65536))
				{
					return string.Empty;
				}
				return base.ViewState.GetString("BackImageUrl", string.Empty);
			}
			set
			{
				base.ViewState["BackImageUrl"] = value;
				this.SetBit(65536);
			}
		}

		/// <summary>Gets or sets the direction in which to display controls that include text in a panel control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ContentDirection" /> values. The default is <see cref="F:System.Web.UI.WebControls.ContentDirection.NotSet" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The direction is not one of the <see cref="T:System.Web.UI.WebControls.ContentDirection" /> values.</exception>
		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x06002B96 RID: 11158 RVA: 0x00071BD2 File Offset: 0x0006FDD2
		// (set) Token: 0x06002B97 RID: 11159 RVA: 0x00071BF8 File Offset: 0x0006FDF8
		[DefaultValue(ContentDirection.NotSet)]
		public virtual ContentDirection Direction
		{
			get
			{
				if (!base.CheckBit(131072))
				{
					return ContentDirection.NotSet;
				}
				return (ContentDirection)base.ViewState["Direction"];
			}
			set
			{
				base.ViewState["Direction"] = value;
				this.SetBit(131072);
			}
		}

		/// <summary>Gets or sets the horizontal alignment of the contents within a panel control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> values. The default is <see cref="F:System.Web.UI.WebControls.HorizontalAlign.NotSet" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The horizontal alignment is not one of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> values.</exception>
		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x06002B98 RID: 11160 RVA: 0x00071C1B File Offset: 0x0006FE1B
		// (set) Token: 0x06002B99 RID: 11161 RVA: 0x00071C41 File Offset: 0x0006FE41
		[DefaultValue(HorizontalAlign.NotSet)]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				if (!base.CheckBit(262144))
				{
					return HorizontalAlign.NotSet;
				}
				return (HorizontalAlign)base.ViewState["HorizontalAlign"];
			}
			set
			{
				base.ViewState["HorizontalAlign"] = value;
				this.SetBit(262144);
			}
		}

		/// <summary>Gets or sets the visibility and position of scroll bars in a panel control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ScrollBars" /> values. The default is <see cref="F:System.Web.UI.WebControls.ScrollBars.None" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.PanelStyle.ScrollBars" /> property is not one of the <see cref="T:System.Web.UI.WebControls.ScrollBars" /> values.</exception>
		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x06002B9A RID: 11162 RVA: 0x00071C64 File Offset: 0x0006FE64
		// (set) Token: 0x06002B9B RID: 11163 RVA: 0x00071C8A File Offset: 0x0006FE8A
		[DefaultValue(ScrollBars.None)]
		public virtual ScrollBars ScrollBars
		{
			get
			{
				if (!base.CheckBit(524288))
				{
					return ScrollBars.None;
				}
				return (ScrollBars)base.ViewState["ScrollBars"];
			}
			set
			{
				base.ViewState["ScrollBars"] = value;
				this.SetBit(524288);
			}
		}

		/// <summary>Gets or sets a value indicating whether the content wraps within the panel.</summary>
		/// <returns>true if the content wraps within the panel; otherwise, false. The default is true.</returns>
		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x06002B9C RID: 11164 RVA: 0x00071CAD File Offset: 0x0006FEAD
		// (set) Token: 0x06002B9D RID: 11165 RVA: 0x00071CD3 File Offset: 0x0006FED3
		[DefaultValue(true)]
		public virtual bool Wrap
		{
			get
			{
				return !base.CheckBit(1048576) || (bool)base.ViewState["Wrap"];
			}
			set
			{
				base.ViewState["Wrap"] = value;
				this.SetBit(1048576);
			}
		}

		/// <summary>Duplicates the style properties of the specified <see cref="T:System.Web.UI.WebControls.Style" /> object for the current instance of the <see cref="T:System.Web.UI.WebControls.PanelStyle" /> class.</summary>
		/// <param name="s">A <see cref="T:System.Web.UI.WebControls.Style" /> object that represents the style settings to copy.</param>
		// Token: 0x06002B9E RID: 11166 RVA: 0x00071CF8 File Offset: 0x0006FEF8
		public override void CopyFrom(Style s)
		{
			if (s == null || s.IsEmpty)
			{
				return;
			}
			base.CopyFrom(s);
			PanelStyle panelStyle = s as PanelStyle;
			if (panelStyle == null)
			{
				return;
			}
			if (s.CheckBit(65536))
			{
				this.BackImageUrl = panelStyle.BackImageUrl;
			}
			if (s.CheckBit(131072))
			{
				this.Direction = panelStyle.Direction;
			}
			if (s.CheckBit(262144))
			{
				this.HorizontalAlign = panelStyle.HorizontalAlign;
			}
			if (s.CheckBit(524288))
			{
				this.ScrollBars = panelStyle.ScrollBars;
			}
			if (s.CheckBit(1048576))
			{
				this.Wrap = panelStyle.Wrap;
			}
		}

		/// <summary>Combines the style settings of the specified <see cref="T:System.Web.UI.WebControls.Style" /> object with the current instance of the <see cref="T:System.Web.UI.WebControls.PanelStyle" /> class.</summary>
		/// <param name="s">A <see cref="T:System.Web.UI.WebControls.Style" /> object that represents the style settings to combine with the <see cref="T:System.Web.UI.WebControls.PanelStyle" /> object.</param>
		// Token: 0x06002B9F RID: 11167 RVA: 0x00071DA0 File Offset: 0x0006FFA0
		public override void MergeWith(Style s)
		{
			if (s == null || s.IsEmpty)
			{
				return;
			}
			base.MergeWith(s);
			PanelStyle panelStyle = s as PanelStyle;
			if (panelStyle == null)
			{
				return;
			}
			if (!base.CheckBit(65536) && s.CheckBit(65536))
			{
				this.BackImageUrl = panelStyle.BackImageUrl;
			}
			if (!base.CheckBit(131072) && s.CheckBit(131072))
			{
				this.Direction = panelStyle.Direction;
			}
			if (!base.CheckBit(262144) && s.CheckBit(262144))
			{
				this.HorizontalAlign = panelStyle.HorizontalAlign;
			}
			if (!base.CheckBit(524288) && s.CheckBit(524288))
			{
				this.ScrollBars = panelStyle.ScrollBars;
			}
			if (!base.CheckBit(1048576) && s.CheckBit(1048576))
			{
				this.Wrap = panelStyle.Wrap;
			}
		}

		/// <summary>Removes any defined style settings from the <see cref="T:System.Web.UI.WebControls.PanelStyle" /> class.</summary>
		// Token: 0x06002BA0 RID: 11168 RVA: 0x00071E8C File Offset: 0x0007008C
		public override void Reset()
		{
			base.Reset();
			base.ViewState.Remove("BackImageUrl");
			base.ViewState.Remove("Direction");
			base.ViewState.Remove("HorizontalAlign");
			base.ViewState.Remove("ScrollBars");
			base.ViewState.Remove("Wrap");
		}

		// Token: 0x020003E1 RID: 993
		[Flags]
		private enum PanelStyles
		{
			// Token: 0x04001B26 RID: 6950
			BackImageUrl = 65536,
			// Token: 0x04001B27 RID: 6951
			Direction = 131072,
			// Token: 0x04001B28 RID: 6952
			HorizontalAlign = 262144,
			// Token: 0x04001B29 RID: 6953
			ScrollBars = 524288,
			// Token: 0x04001B2A RID: 6954
			Wrap = 1048576
		}
	}
}
