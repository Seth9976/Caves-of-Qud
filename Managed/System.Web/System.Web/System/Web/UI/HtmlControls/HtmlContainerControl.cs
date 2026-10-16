using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Serves as the abstract base class for HTML server controls that map to HTML elements that are required to have an opening and a closing tag.</summary>
	// Token: 0x02000251 RID: 593
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class HtmlContainerControl : HtmlControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl" /> class using default values.</summary>
		// Token: 0x06001828 RID: 6184 RVA: 0x0003F2C5 File Offset: 0x0003D4C5
		protected HtmlContainerControl()
			: this("span")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl" /> class using the specified tag name.</summary>
		/// <param name="tag">A string that specifies the tag name of the control. </param>
		// Token: 0x06001829 RID: 6185 RVA: 0x0003F2D2 File Offset: 0x0003D4D2
		public HtmlContainerControl(string tag)
			: base(tag)
		{
		}

		/// <summary>Gets or sets the content found between the opening and closing tags of the specified HTML server control.</summary>
		/// <returns>The HTML content between opening and closing tags of an HTML server control.</returns>
		/// <exception cref="T:System.Web.HttpException">There is more than one HTML server control.- or -The HTML server control is not a <see cref="T:System.Web.UI.LiteralControl" /> or a <see cref="T:System.Web.UI.DataBoundLiteralControl" />. </exception>
		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x0003F2DC File Offset: 0x0003D4DC
		// (set) Token: 0x0600182B RID: 6187 RVA: 0x0003F344 File Offset: 0x0003D544
		[Browsable(false)]
		[HtmlControlPersistable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string InnerHtml
		{
			get
			{
				if (this.Controls.Count == 0)
				{
					return string.Empty;
				}
				if (this.Controls.Count == 1)
				{
					Control control = this.Controls[0];
					LiteralControl literalControl = control as LiteralControl;
					if (literalControl != null)
					{
						return literalControl.Text;
					}
					DataBoundLiteralControl dataBoundLiteralControl = control as DataBoundLiteralControl;
					if (dataBoundLiteralControl != null)
					{
						return dataBoundLiteralControl.Text;
					}
				}
				throw new HttpException("There is no literal content!");
			}
			set
			{
				this.Controls.Clear();
				this.Controls.Add(new LiteralControl(value));
				if (value == null)
				{
					this.ViewState.Remove("innerhtml");
					return;
				}
				this.ViewState["innerhtml"] = value;
			}
		}

		/// <summary>Gets or sets the text between the opening and closing tags of the specified HTML server control.</summary>
		/// <returns>The text between the opening and closing tags of an HTML server control.</returns>
		/// <exception cref="T:System.Web.HttpException">There is more than one HTML server control.- or - The HTML server control is not a <see cref="T:System.Web.UI.LiteralControl" /> or a <see cref="T:System.Web.UI.DataBoundLiteralControl" />. </exception>
		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x0600182C RID: 6188 RVA: 0x0003F392 File Offset: 0x0003D592
		// (set) Token: 0x0600182D RID: 6189 RVA: 0x0003F39F File Offset: 0x0003D59F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[HtmlControlPersistable(false)]
		[Browsable(false)]
		public virtual string InnerText
		{
			get
			{
				return HttpUtility.HtmlDecode(this.InnerHtml);
			}
			set
			{
				this.InnerHtml = HttpUtility.HtmlEncode(value);
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl" /> content.</param>
		// Token: 0x0600182E RID: 6190 RVA: 0x0003F3AD File Offset: 0x0003D5AD
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.RenderBeginTag(writer);
			this.RenderChildren(writer);
			this.RenderEndTag(writer);
		}

		/// <summary>Renders the closing tag for the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
		// Token: 0x0600182F RID: 6191 RVA: 0x0003F3C4 File Offset: 0x0003D5C4
		protected virtual void RenderEndTag(HtmlTextWriter writer)
		{
			writer.WriteEndTag(this.TagName);
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> instance that receives the rendered content.</param>
		// Token: 0x06001830 RID: 6192 RVA: 0x0003F3D2 File Offset: 0x0003D5D2
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			this.ViewState.Remove("innerhtml");
			base.RenderAttributes(writer);
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x0002C74C File Offset: 0x0002A94C
		protected override ControlCollection CreateControlCollection()
		{
			return new ControlCollection(this);
		}

		/// <summary>Restores the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl" /> control's view state from a previous page request that was saved by the <see cref="M:System.Web.UI.Control.SaveViewState" /> method.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the control state to be restored.</param>
		// Token: 0x06001832 RID: 6194 RVA: 0x0003F3EC File Offset: 0x0003D5EC
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				base.LoadViewState(savedState);
				string text = this.ViewState["innerhtml"] as string;
				if (text != null)
				{
					this.InnerHtml = text;
				}
			}
		}
	}
}
