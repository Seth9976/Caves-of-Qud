using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML &lt;title&gt; element on the server.</summary>
	// Token: 0x02000274 RID: 628
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlTitle : HtmlControl
	{
		/// <summary>Notifies the <see cref="T:System.Web.UI.HtmlControls.HtmlTitle" /> control that an XML or HTML element was parsed and adds that element to the <see cref="T:System.Web.UI.ControlCollection" /> collection of the control.</summary>
		/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element.</param>
		// Token: 0x060019F1 RID: 6641 RVA: 0x00042ED4 File Offset: 0x000410D4
		protected override void AddParsedSubObject(object obj)
		{
			LiteralControl literalControl = obj as LiteralControl;
			if (literalControl != null)
			{
				this.text = literalControl.Text;
				return;
			}
			base.AddParsedSubObject(obj);
		}

		/// <summary>Creates a new <see cref="T:System.Web.UI.ControlCollection" /> collection for the <see cref="T:System.Web.UI.HtmlControls.HtmlTitle" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> object to contain the current server control's child server controls.</returns>
		// Token: 0x060019F2 RID: 6642 RVA: 0x0002C74C File Offset: 0x0002A94C
		protected override ControlCollection CreateControlCollection()
		{
			return new ControlCollection(this);
		}

		/// <summary>Gets or sets the text of the HTML &lt;title&gt; element.</summary>
		/// <returns>The text of the HTML &lt;title&gt; element. The default value is an empty string ("").</returns>
		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x060019F3 RID: 6643 RVA: 0x00042EFF File Offset: 0x000410FF
		// (set) Token: 0x060019F4 RID: 6644 RVA: 0x00042F07 File Offset: 0x00041107
		[Localizable(true)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		public virtual string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlTitle" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
		// Token: 0x060019F5 RID: 6645 RVA: 0x00042F10 File Offset: 0x00041110
		protected internal override void Render(HtmlTextWriter writer)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Title);
			if (this.HasControls() || base.HasRenderMethodDelegate())
			{
				this.RenderChildren(writer);
			}
			else
			{
				writer.Write(this.text);
			}
			writer.RenderEndTag();
		}

		// Token: 0x04001643 RID: 5699
		private string text;
	}
}
