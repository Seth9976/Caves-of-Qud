using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Validates whether the value of an associated input control matches the pattern specified by a regular expression.</summary>
	// Token: 0x020003F7 RID: 1015
	[ToolboxData("<{0}:RegularExpressionValidator runat=\"server\" ErrorMessage=\"RegularExpressionValidator\"></{0}:RegularExpressionValidator>")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class RegularExpressionValidator : BaseValidator
	{
		/// <summary>Adds to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object the HTML attributes and styles that need to be rendered for the control. </summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06002CD5 RID: 11477 RVA: 0x00074E04 File Offset: 0x00073004
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (base.RenderUplevel)
			{
				base.RegisterExpandoAttribute(this.ClientID, "evaluationfunction", "RegularExpressionValidatorEvaluateIsValid");
				if (this.ValidationExpression.Length > 0)
				{
					base.RegisterExpandoAttribute(this.ClientID, "validationexpression", this.ValidationExpression, true);
				}
			}
			base.AddAttributesToRender(writer);
		}

		/// <summary>Indicates whether the value in the input control is valid.</summary>
		/// <returns>true if the value in the input control is valid; otherwise, false.</returns>
		// Token: 0x06002CD6 RID: 11478 RVA: 0x00074E5C File Offset: 0x0007305C
		protected override bool EvaluateIsValid()
		{
			if (base.GetControlValidationValue(base.ControlToValidate).Trim() == "")
			{
				return true;
			}
			StringBuilder stringBuilder = new StringBuilder(this.ValidationExpression);
			if (stringBuilder.Length == 0 || stringBuilder[0] != '^')
			{
				stringBuilder.Insert(0, '^');
			}
			if (stringBuilder[stringBuilder.Length - 1] != '$')
			{
				stringBuilder.Append('$');
			}
			return Regex.IsMatch(base.GetControlValidationValue(base.ControlToValidate), stringBuilder.ToString());
		}

		/// <summary>Gets or sets the regular expression that determines the pattern used to validate a field.</summary>
		/// <returns>A string that specifies the regular expression used to validate a field for format. The default is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.Web.HttpException">The regular expression is not properly formed. </exception>
		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x06002CD7 RID: 11479 RVA: 0x00074EE3 File Offset: 0x000730E3
		// (set) Token: 0x06002CD8 RID: 11480 RVA: 0x00074EFA File Offset: 0x000730FA
		[Themeable(false)]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.WebControls.RegexTypeEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public string ValidationExpression
		{
			get
			{
				return this.ViewState.GetString("ValidationExpression", "");
			}
			set
			{
				this.ViewState["ValidationExpression"] = value;
			}
		}

		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x06002CD9 RID: 11481 RVA: 0x00074F10 File Offset: 0x00073110
		// (set) Token: 0x06002CDA RID: 11482 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public int? MatchTimeout
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
