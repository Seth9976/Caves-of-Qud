using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Represents HTML elements, text, and any other strings in an ASP.NET page that do not require processing on the server.</summary>
	// Token: 0x020001E0 RID: 480
	[ToolboxItem(false)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class LiteralControl : Control, ITextControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.LiteralControl" /> class that contains a literal string to be rendered on the requested ASP.NET page.</summary>
		// Token: 0x06001362 RID: 4962 RVA: 0x0003292F File Offset: 0x00030B2F
		public LiteralControl()
		{
			this.EnableViewState = false;
			base.AutoID = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.LiteralControl" /> class with the specified text.</summary>
		/// <param name="text">The text to be rendered on the requested Web page. </param>
		// Token: 0x06001363 RID: 4963 RVA: 0x00032945 File Offset: 0x00030B45
		public LiteralControl(string text)
			: this()
		{
			this.Text = text;
		}

		/// <summary>Gets or sets the text content of the <see cref="T:System.Web.UI.LiteralControl" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the text content of the literal control. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001364 RID: 4964 RVA: 0x00032954 File Offset: 0x00030B54
		// (set) Token: 0x06001365 RID: 4965 RVA: 0x0003295C File Offset: 0x00030B5C
		public virtual string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = ((value == null) ? string.Empty : value);
			}
		}

		/// <summary>Writes the content of the <see cref="T:System.Web.UI.LiteralControl" /> object to the ASP.NET page.</summary>
		/// <param name="output">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that renders the content of the <see cref="T:System.Web.UI.LiteralControl" /> to the requesting client. </param>
		// Token: 0x06001366 RID: 4966 RVA: 0x0003296F File Offset: 0x00030B6F
		protected internal override void Render(HtmlTextWriter output)
		{
			output.Write(this._text);
		}

		/// <summary>Creates an <see cref="T:System.Web.UI.EmptyControlCollection" /> object for the current instance of the <see cref="T:System.Web.UI.LiteralControl" /> class.</summary>
		/// <returns>The <see cref="T:System.Web.UI.EmptyControlCollection" /> for the current control.</returns>
		// Token: 0x06001367 RID: 4967 RVA: 0x0002FDF9 File Offset: 0x0002DFF9
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		// Token: 0x0400146B RID: 5227
		private string _text;
	}
}
