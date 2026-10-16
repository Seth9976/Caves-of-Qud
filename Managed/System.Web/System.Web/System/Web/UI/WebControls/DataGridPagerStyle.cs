using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the style for the pager of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. This class cannot be inherited.</summary>
	// Token: 0x02000378 RID: 888
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DataGridPagerStyle : TableItemStyle
	{
		// Token: 0x060021E5 RID: 8677 RVA: 0x00055176 File Offset: 0x00053376
		internal DataGridPagerStyle()
		{
		}

		/// <summary>Gets or sets a value that specifies whether the pager element displays buttons that link to the next and previous page, or numeric buttons that link directly to a page.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.PagerMode" /> values. The default value is NextPrev.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the <see cref="T:System.Web.UI.WebControls.PagerMode" /> values. </exception>
		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x060021E6 RID: 8678 RVA: 0x0005517E File Offset: 0x0005337E
		// (set) Token: 0x060021E7 RID: 8679 RVA: 0x000551A4 File Offset: 0x000533A4
		[DefaultValue(PagerMode.NextPrev)]
		[WebCategory("Misc")]
		[NotifyParentProperty(true)]
		[WebSysDescription("")]
		public PagerMode Mode
		{
			get
			{
				if (!base.CheckBit(1048576))
				{
					return PagerMode.NextPrev;
				}
				return (PagerMode)base.ViewState["Mode"];
			}
			set
			{
				base.ViewState["Mode"] = value;
				this.SetBit(1048576);
			}
		}

		/// <summary>Gets or sets the text displayed for the next page button.</summary>
		/// <returns>The text to display for the next page button. The default value is "&amp;gt;", which is rendered as the greater than sign (&gt;).</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is less than 1.</exception>
		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x060021E8 RID: 8680 RVA: 0x000551C7 File Offset: 0x000533C7
		// (set) Token: 0x060021E9 RID: 8681 RVA: 0x000551F1 File Offset: 0x000533F1
		[Localizable(true)]
		[DefaultValue("&gt;")]
		[WebCategory("Misc")]
		[WebSysDescription("")]
		[NotifyParentProperty(true)]
		public string NextPageText
		{
			get
			{
				if (!base.CheckBit(2097152))
				{
					return "&gt;";
				}
				return base.ViewState.GetString("NextPageText", "&gt;");
			}
			set
			{
				base.ViewState["NextPageText"] = value;
				this.SetBit(2097152);
			}
		}

		/// <summary>Gets or sets the number of numeric buttons to display concurrently in the pager element of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>The number of numeric buttons to display concurrently in the pager element of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. The default value is 10.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than 1.</exception>
		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x060021EA RID: 8682 RVA: 0x0005520F File Offset: 0x0005340F
		// (set) Token: 0x060021EB RID: 8683 RVA: 0x00055233 File Offset: 0x00053433
		[WebSysDescription("")]
		[WebCategory("Misc")]
		[DefaultValue(10)]
		[NotifyParentProperty(true)]
		public int PageButtonCount
		{
			get
			{
				if (!base.CheckBit(4194304))
				{
					return 10;
				}
				return base.ViewState.GetInt("PageButtonCount", 10);
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("value", "PageButtonCount must be greater than 0");
				}
				base.ViewState["PageButtonCount"] = value;
				this.SetBit(4194304);
			}
		}

		/// <summary>Gets or sets the position of the pager element in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.PagerPosition" /> values. The default value is Bottom.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the <see cref="T:System.Web.UI.WebControls.PagerPosition" /> values. </exception>
		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x060021EC RID: 8684 RVA: 0x0005526A File Offset: 0x0005346A
		// (set) Token: 0x060021ED RID: 8685 RVA: 0x00055290 File Offset: 0x00053490
		[NotifyParentProperty(true)]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		[DefaultValue(PagerPosition.Bottom)]
		public PagerPosition Position
		{
			get
			{
				if (!base.CheckBit(8388608))
				{
					return PagerPosition.Bottom;
				}
				return (PagerPosition)base.ViewState["Position"];
			}
			set
			{
				base.ViewState["Position"] = value;
				this.SetBit(8388608);
			}
		}

		/// <summary>Gets or sets the text displayed for the previous page button.</summary>
		/// <returns>The text to display for the previous page button. The default value is "&amp;lt;", which is rendered as the less than sign (&lt;).</returns>
		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x060021EE RID: 8686 RVA: 0x000552B3 File Offset: 0x000534B3
		// (set) Token: 0x060021EF RID: 8687 RVA: 0x000552DD File Offset: 0x000534DD
		[DefaultValue("&lt;")]
		[NotifyParentProperty(true)]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		[Localizable(true)]
		public string PrevPageText
		{
			get
			{
				if (!base.CheckBit(16777216))
				{
					return "&lt;";
				}
				return base.ViewState.GetString("PrevPageText", "&lt;");
			}
			set
			{
				base.ViewState["PrevPageText"] = value;
				this.SetBit(16777216);
			}
		}

		/// <summary>Gets or sets a value indicating whether the pager is displayed in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>true to display the pager; otherwise, false. The default value is true.</returns>
		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x060021F0 RID: 8688 RVA: 0x000552FB File Offset: 0x000534FB
		// (set) Token: 0x060021F1 RID: 8689 RVA: 0x0005531D File Offset: 0x0005351D
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		public bool Visible
		{
			get
			{
				return !base.CheckBit(33554432) || base.ViewState.GetBool("Visible", true);
			}
			set
			{
				base.ViewState["Visible"] = value;
				this.SetBit(33554432);
			}
		}

		/// <summary>Copies the style of the specified <see cref="T:System.Web.UI.WebControls.Style" /> object into this instance of the <see cref="T:System.Web.UI.WebControls.DataGridPagerStyle" /> class.</summary>
		/// <param name="s">The <see cref="T:System.Web.UI.WebControls.Style" /> to copy from. </param>
		// Token: 0x060021F2 RID: 8690 RVA: 0x00055340 File Offset: 0x00053540
		public override void CopyFrom(Style s)
		{
			base.CopyFrom(s);
			if (s == null || s.IsEmpty)
			{
				return;
			}
			if (s.CheckBit(1048576) && ((DataGridPagerStyle)s).Mode != PagerMode.NextPrev)
			{
				this.Mode = ((DataGridPagerStyle)s).Mode;
			}
			if (s.CheckBit(2097152) && ((DataGridPagerStyle)s).NextPageText != "&gt;")
			{
				this.NextPageText = ((DataGridPagerStyle)s).NextPageText;
			}
			if (s.CheckBit(4194304) && ((DataGridPagerStyle)s).PageButtonCount != 10)
			{
				this.PageButtonCount = ((DataGridPagerStyle)s).PageButtonCount;
			}
			if (s.CheckBit(8388608) && ((DataGridPagerStyle)s).Position != PagerPosition.Bottom)
			{
				this.Position = ((DataGridPagerStyle)s).Position;
			}
			if (s.CheckBit(16777216) && ((DataGridPagerStyle)s).PrevPageText != "&lt;")
			{
				this.PrevPageText = ((DataGridPagerStyle)s).PrevPageText;
			}
			if (s.CheckBit(33554432) && !((DataGridPagerStyle)s).Visible)
			{
				this.Visible = ((DataGridPagerStyle)s).Visible;
			}
		}

		/// <summary>Merges the style of the specified <see cref="T:System.Web.UI.WebControls.Style" /> object with this instance of the <see cref="T:System.Web.UI.WebControls.DataGridPagerStyle" /> class.</summary>
		/// <param name="s">The <see cref="T:System.Web.UI.WebControls.Style" /> to merge with. </param>
		// Token: 0x060021F3 RID: 8691 RVA: 0x00055478 File Offset: 0x00053678
		public override void MergeWith(Style s)
		{
			base.MergeWith(s);
			if (s == null || s.IsEmpty)
			{
				return;
			}
			if (!base.CheckBit(1048576) && s.CheckBit(1048576) && ((DataGridPagerStyle)s).Mode != PagerMode.NextPrev)
			{
				this.Mode = ((DataGridPagerStyle)s).Mode;
			}
			if (!base.CheckBit(2097152) && s.CheckBit(2097152) && ((DataGridPagerStyle)s).NextPageText != "&gt;")
			{
				this.NextPageText = ((DataGridPagerStyle)s).NextPageText;
			}
			if (!base.CheckBit(4194304) && s.CheckBit(4194304) && ((DataGridPagerStyle)s).PageButtonCount != 10)
			{
				this.PageButtonCount = ((DataGridPagerStyle)s).PageButtonCount;
			}
			if (!base.CheckBit(8388608) && s.CheckBit(8388608) && ((DataGridPagerStyle)s).Position != PagerPosition.Bottom)
			{
				this.Position = ((DataGridPagerStyle)s).Position;
			}
			if (!base.CheckBit(16777216) && s.CheckBit(16777216) && ((DataGridPagerStyle)s).PrevPageText != "&lt;")
			{
				this.PrevPageText = ((DataGridPagerStyle)s).PrevPageText;
			}
			if (!base.CheckBit(33554432) && s.CheckBit(33554432) && !((DataGridPagerStyle)s).Visible)
			{
				this.Visible = ((DataGridPagerStyle)s).Visible;
			}
		}

		/// <summary>Restores the <see cref="T:System.Web.UI.WebControls.DataGridPagerStyle" /> object to its default values.</summary>
		// Token: 0x060021F4 RID: 8692 RVA: 0x00055600 File Offset: 0x00053800
		public override void Reset()
		{
			base.ViewState.Remove("Mode");
			base.ViewState.Remove("NextPageText");
			base.ViewState.Remove("PageButtonCount");
			base.ViewState.Remove("Position");
			base.ViewState.Remove("PrevPageText");
			base.ViewState.Remove("Visible");
			base.Reset();
		}

		// Token: 0x02000379 RID: 889
		[Flags]
		private enum DataGridPagerStyles
		{
			// Token: 0x04001900 RID: 6400
			Mode = 1048576,
			// Token: 0x04001901 RID: 6401
			NextPageText = 2097152,
			// Token: 0x04001902 RID: 6402
			PageButtonCount = 4194304,
			// Token: 0x04001903 RID: 6403
			Position = 8388608,
			// Token: 0x04001904 RID: 6404
			PrevPageText = 16777216,
			// Token: 0x04001905 RID: 6405
			Visible = 33554432
		}
	}
}
