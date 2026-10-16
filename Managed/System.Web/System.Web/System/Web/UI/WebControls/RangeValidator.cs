using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Checks whether the value of an input control is within a specified range of values.</summary>
	// Token: 0x020003F5 RID: 1013
	[ToolboxData("<{0}:RangeValidator runat=\"server\" ErrorMessage=\"RangeValidator\"></{0}:RangeValidator>")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class RangeValidator : BaseCompareValidator
	{
		/// <summary>Gets or sets the maximum value of the validation range.</summary>
		/// <returns>The maximum value of the validation range. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x06002CC2 RID: 11458 RVA: 0x00074AAF File Offset: 0x00072CAF
		// (set) Token: 0x06002CC3 RID: 11459 RVA: 0x00074AC6 File Offset: 0x00072CC6
		[Themeable(false)]
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public string MaximumValue
		{
			get
			{
				return this.ViewState.GetString("MaximumValue", string.Empty);
			}
			set
			{
				this.ViewState["MaximumValue"] = value;
			}
		}

		/// <summary>Gets or sets the minimum value of the validation range.</summary>
		/// <returns>The minimum value of the validation range. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x06002CC4 RID: 11460 RVA: 0x00074AD9 File Offset: 0x00072CD9
		// (set) Token: 0x06002CC5 RID: 11461 RVA: 0x00074AF0 File Offset: 0x00072CF0
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[Themeable(false)]
		[DefaultValue("")]
		public string MinimumValue
		{
			get
			{
				return this.ViewState.GetString("MinimumValue", string.Empty);
			}
			set
			{
				this.ViewState["MinimumValue"] = value;
			}
		}

		/// <summary>Adds the HTML attributes and styles for the control that need to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object. </summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06002CC6 RID: 11462 RVA: 0x00074B04 File Offset: 0x00072D04
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (base.RenderUplevel)
			{
				base.RegisterExpandoAttribute(this.ClientID, "evaluationfunction", "RangeValidatorEvaluateIsValid");
				base.RegisterExpandoAttribute(this.ClientID, "minimumvalue", this.MinimumValue, true);
				base.RegisterExpandoAttribute(this.ClientID, "maximumvalue", this.MaximumValue, true);
			}
		}

		/// <summary>This is a check of properties to determine any errors made by the developer. </summary>
		/// <returns>true if the control properties are valid; otherwise, false.</returns>
		// Token: 0x06002CC7 RID: 11463 RVA: 0x00074B68 File Offset: 0x00072D68
		protected override bool ControlPropertiesValid()
		{
			if (!BaseCompareValidator.CanConvert(this.MinimumValue, base.Type))
			{
				throw new HttpException("Minimum value cannot be converterd to type " + base.Type.ToString());
			}
			if (!BaseCompareValidator.CanConvert(this.MaximumValue, base.Type))
			{
				throw new HttpException("Maximum value cannot be converterd to type " + base.Type.ToString());
			}
			if (base.Type != ValidationDataType.String && BaseCompareValidator.Compare(this.MinimumValue, this.MaximumValue, ValidationCompareOperator.GreaterThan, base.Type))
			{
				throw new HttpException("Maximum value must be equal or bigger than Minimum value");
			}
			return base.ControlPropertiesValid();
		}

		/// <summary>Determines whether the content in the input control is valid.</summary>
		/// <returns>true if the control is valid; otherwise, false.</returns>
		// Token: 0x06002CC8 RID: 11464 RVA: 0x00074C18 File Offset: 0x00072E18
		protected override bool EvaluateIsValid()
		{
			string text = base.GetControlValidationValue(base.ControlToValidate);
			if (text == null)
			{
				return true;
			}
			text = text.Trim();
			return text.Length == 0 || (BaseCompareValidator.Compare(text, this.MinimumValue, ValidationCompareOperator.GreaterThanEqual, base.Type) && BaseCompareValidator.Compare(text, this.MaximumValue, ValidationCompareOperator.LessThanEqual, base.Type));
		}
	}
}
