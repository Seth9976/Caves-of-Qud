using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML &lt;input type=reset&gt; element on the server.</summary>
	// Token: 0x02000264 RID: 612
	[DefaultEvent("")]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlInputReset : HtmlInputButton
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputReset" /> class using default values.</summary>
		// Token: 0x06001907 RID: 6407 RVA: 0x0004133B File Offset: 0x0003F53B
		public HtmlInputReset()
			: base("reset")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputReset" /> class using the specified input type.</summary>
		/// <param name="type">The input type.</param>
		// Token: 0x06001908 RID: 6408 RVA: 0x00041348 File Offset: 0x0003F548
		public HtmlInputReset(string type)
			: base(type)
		{
		}

		/// <summary>Gets or sets a value that indicates whether validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlInputReset" /> control is clicked. </summary>
		/// <returns>true if validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlInputReset" /> control is clicked; otherwise, false. The default value is true. </returns>
		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06001909 RID: 6409 RVA: 0x0003EF0B File Offset: 0x0003D10B
		// (set) Token: 0x0600190A RID: 6410 RVA: 0x0003EF1E File Offset: 0x0003D11E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool CausesValidation
		{
			get
			{
				return this.ViewState.GetBool("CausesValidation", true);
			}
			set
			{
				this.ViewState["CausesValidation"] = value;
			}
		}

		/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlInputReset" /> control causes validation when it posts back to the server. </summary>
		/// <returns>The group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlInputReset" /> control causes validation when it posts back to the server. The default value is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x0600190B RID: 6411 RVA: 0x0003F11F File Offset: 0x0003D31F
		// (set) Token: 0x0600190C RID: 6412 RVA: 0x0003EF4D File Offset: 0x0003D14D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ValidationGroup
		{
			get
			{
				return this.ViewState.GetString("ValidationGroup", "");
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		/// <summary>Occurs when an <see cref="T:System.Web.UI.HtmlControls.HtmlInputReset" /> control is clicked on the Web page. </summary>
		// Token: 0x1400003D RID: 61
		// (add) Token: 0x0600190D RID: 6413 RVA: 0x00041351 File Offset: 0x0003F551
		// (remove) Token: 0x0600190E RID: 6414 RVA: 0x00041364 File Offset: 0x0003F564
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public new event EventHandler ServerClick
		{
			add
			{
				base.Events.AddHandler(HtmlInputReset.ServerClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlInputReset.ServerClickEvent, value);
			}
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x00041377 File Offset: 0x0003F577
		// Note: this type is marked as 'beforefieldinit'.
		static HtmlInputReset()
		{
			HtmlInputReset.ServerClickEvent = new object();
		}
	}
}
