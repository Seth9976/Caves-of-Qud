using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Makes the associated input control a required field.</summary>
	// Token: 0x020003FC RID: 1020
	[ToolboxData("<{0}:RequiredFieldValidator runat=\"server\" ErrorMessage=\"RequiredFieldValidator\"></{0}:RequiredFieldValidator>")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class RequiredFieldValidator : BaseValidator
	{
		/// <summary>Adds the HTML attributes and styles that need to be rendered for the control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06002D44 RID: 11588 RVA: 0x0007612A File Offset: 0x0007432A
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (base.RenderUplevel)
			{
				base.RegisterExpandoAttribute(this.ClientID, "evaluationfunction", "RequiredFieldValidatorEvaluateIsValid");
				base.RegisterExpandoAttribute(this.ClientID, "initialvalue", this.InitialValue, true);
			}
			base.AddAttributesToRender(writer);
		}

		/// <summary>Called during the validation stage when ASP.NET processes a Web Form.</summary>
		/// <returns>true if the value in the input control is valid; otherwise, false.</returns>
		// Token: 0x06002D45 RID: 11589 RVA: 0x00076169 File Offset: 0x00074369
		protected override bool EvaluateIsValid()
		{
			return base.GetControlValidationValue(base.ControlToValidate) != this.InitialValue;
		}

		/// <summary>Gets or sets the initial value of the associated input control.</summary>
		/// <returns>A string that specifies the initial value of the associated input control. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06002D46 RID: 11590 RVA: 0x00076182 File Offset: 0x00074382
		// (set) Token: 0x06002D47 RID: 11591 RVA: 0x00076199 File Offset: 0x00074399
		[DefaultValue("")]
		[WebSysDescription("")]
		[Themeable(false)]
		[WebCategory("Behavior")]
		public string InitialValue
		{
			get
			{
				return this.ViewState.GetString("InitialValue", "");
			}
			set
			{
				this.ViewState["InitialValue"] = value;
			}
		}
	}
}
