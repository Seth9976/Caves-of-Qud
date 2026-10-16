using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays the value of the System.Web.UI.Page.User.Identity.Name property. </summary>
	// Token: 0x020003C5 RID: 965
	[Designer("System.Web.UI.Design.WebControls.LoginNameDesigner,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultProperty("FormatString")]
	[Bindable(false)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class LoginName : WebControl
	{
		/// <summary>Provides a format item string to display.</summary>
		/// <returns>A string containing format items for displaying the user's name. The default value is "{0}".</returns>
		/// <exception cref="T:System.FormatException">The format string is not valid. </exception>
		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x0600286A RID: 10346 RVA: 0x00067864 File Offset: 0x00065A64
		// (set) Token: 0x0600286B RID: 10347 RVA: 0x00067891 File Offset: 0x00065A91
		[DefaultValue("{0}")]
		[Localizable(true)]
		public virtual string FormatString
		{
			get
			{
				object obj = this.ViewState["FormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "{0}";
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("FormatString");
					return;
				}
				this.ViewState["FormatString"] = value;
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x0600286C RID: 10348 RVA: 0x00044E29 File Offset: 0x00043029
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.WebControls.LoginName" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> control. </summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered output.</param>
		// Token: 0x0600286D RID: 10349 RVA: 0x000678B8 File Offset: 0x00065AB8
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (!this.Anonymous)
			{
				this.RenderBeginTag(writer);
				this.RenderContents(writer);
				this.RenderEndTag(writer);
			}
		}

		/// <summary>Renders the HTML opening tag of the control to the specified writer.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> object that represents the output stream that renders HTML content to the client.</param>
		// Token: 0x0600286E RID: 10350 RVA: 0x000678D7 File Offset: 0x00065AD7
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			if (!this.Anonymous)
			{
				base.RenderBeginTag(writer);
			}
		}

		/// <summary>Renders the contents of the control to the specified writer. This method is used primarily by control developers.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> object that represents the output stream that renders HTML content to the client.</param>
		/// <exception cref="T:System.FormatException">The <see cref="P:System.Web.UI.WebControls.LoginName.FormatString" /> property is not set to a valid format string.</exception>
		// Token: 0x0600286F RID: 10351 RVA: 0x000678E8 File Offset: 0x00065AE8
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			if (!this.Anonymous)
			{
				string text = (string)this.ViewState["FormatString"];
				if (text == null || text.Length == 0)
				{
					writer.Write(this.User);
					return;
				}
				writer.Write(text, this.User);
			}
		}

		/// <summary>Renders the HTML closing tag of the control to the specified writer.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> object that represents the output stream that renders HTML content to the client.</param>
		// Token: 0x06002870 RID: 10352 RVA: 0x00067938 File Offset: 0x00065B38
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			if (!this.Anonymous)
			{
				base.RenderEndTag(writer);
			}
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x06002871 RID: 10353 RVA: 0x00067949 File Offset: 0x00065B49
		private bool Anonymous
		{
			get
			{
				return this.User.Length == 0;
			}
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x06002872 RID: 10354 RVA: 0x00067959 File Offset: 0x00065B59
		private string User
		{
			get
			{
				if (this.Page == null || this.Page.User == null)
				{
					return string.Empty;
				}
				return this.Page.User.Identity.Name;
			}
		}
	}
}
