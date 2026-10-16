using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Represents the design-time version of the <see cref="T:System.Web.UI.DataBoundLiteralControl" /> control. This class cannot be inherited.</summary>
	// Token: 0x020001C6 RID: 454
	[ToolboxItem(false)]
	[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DesignerDataBoundLiteralControl : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DesignerDataBoundLiteralControl" /> class.</summary>
		// Token: 0x0600127A RID: 4730 RVA: 0x0003066D File Offset: 0x0002E86D
		public DesignerDataBoundLiteralControl()
		{
			base.AutoID = false;
		}

		/// <summary>Gets or sets the text content of the <see cref="T:System.Web.UI.DataBoundLiteralControl" /> control.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the text in the &lt;%# … %&gt; data-binding expression.</returns>
		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x0600127B RID: 4731 RVA: 0x00030687 File Offset: 0x0002E887
		// (set) Token: 0x0600127C RID: 4732 RVA: 0x0003068F File Offset: 0x0002E88F
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				if (value == null)
				{
					this.text = string.Empty;
					return;
				}
				this.text = value;
			}
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x0002FDF9 File Offset: 0x0002DFF9
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x000306A7 File Offset: 0x0002E8A7
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				this.text = (string)savedState;
			}
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x000306B8 File Offset: 0x0002E8B8
		protected internal override void Render(HtmlTextWriter output)
		{
			output.Write(this.text);
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x00030687 File Offset: 0x0002E887
		protected override object SaveViewState()
		{
			return this.text;
		}

		// Token: 0x04001427 RID: 5159
		private string text = string.Empty;
	}
}
