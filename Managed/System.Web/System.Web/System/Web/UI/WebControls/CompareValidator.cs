using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Compares the value entered by the user in an input control with the value entered in another input control, or with a constant value.</summary>
	// Token: 0x0200034F RID: 847
	[ToolboxData("<{0}:CompareValidator runat=\"server\" ErrorMessage=\"CompareValidator\"></{0}:CompareValidator>")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class CompareValidator : BaseCompareValidator
	{
		/// <summary>Adds the attributes of this control to the output stream for rendering on the client.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream for rendering on the client. </param>
		// Token: 0x06001F65 RID: 8037 RVA: 0x0004D67C File Offset: 0x0004B87C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (base.RenderUplevel)
			{
				base.RegisterExpandoAttribute(this.ClientID, "evaluationfunction", "CompareValidatorEvaluateIsValid");
				if (this.ControlToCompare.Length > 0)
				{
					base.RegisterExpandoAttribute(this.ClientID, "controltocompare", base.GetControlRenderID(this.ControlToCompare));
				}
				if (this.ValueToCompare.Length > 0)
				{
					base.RegisterExpandoAttribute(this.ClientID, "valuetocompare", this.ValueToCompare, true);
				}
				base.RegisterExpandoAttribute(this.ClientID, "operator", this.Operator.ToString());
			}
			base.AddAttributesToRender(writer);
		}

		/// <summary>Checks the properties of the control for valid values.</summary>
		/// <returns>true if the control properties are valid; otherwise, false.</returns>
		/// <exception cref="T:System.Web.HttpException">
		///   <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> and <see cref="P:System.Web.UI.WebControls.CompareValidator.ControlToCompare" /> have the same <see cref="P:System.Web.UI.Control.ID" />.</exception>
		/// <exception cref="T:System.Web.HttpException">The value of a target property cannot be converted to the expected <see cref="T:System.Type" />.</exception>
		// Token: 0x06001F66 RID: 8038 RVA: 0x0004D728 File Offset: 0x0004B928
		protected override bool ControlPropertiesValid()
		{
			if (this.Operator != ValidationCompareOperator.DataTypeCheck && this.ControlToCompare.Length == 0 && !BaseCompareValidator.CanConvert(this.ValueToCompare, base.Type, base.CultureInvariantValues))
			{
				throw new HttpException(string.Format("Unable to convert the value: {0} as a {1}", this.ValueToCompare, Enum.GetName(typeof(ValidationDataType), base.Type)));
			}
			if (this.ControlToCompare.Length > 0)
			{
				if (string.CompareOrdinal(this.ControlToCompare, base.ControlToValidate) == 0)
				{
					throw new HttpException(string.Format("Control '{0}' cannot have the same value '{1}' for both ControlToValidate and ControlToCompare.", this.ID, this.ControlToCompare));
				}
				base.CheckControlValidationProperty(this.ControlToCompare, string.Empty);
			}
			return base.ControlPropertiesValid();
		}

		/// <summary>When overridden in a derived class, this method contains the code to determine whether the value in the input control is valid.</summary>
		/// <returns>true if the value in the input control is valid; otherwise, false.</returns>
		// Token: 0x06001F67 RID: 8039 RVA: 0x0004D7E8 File Offset: 0x0004B9E8
		protected override bool EvaluateIsValid()
		{
			string text = base.GetControlValidationValue(base.ControlToValidate);
			if (text == null)
			{
				return true;
			}
			text = text.Trim();
			if (text.Length == 0)
			{
				return true;
			}
			string controlToCompare = this.ControlToCompare;
			string text2 = ((!string.IsNullOrEmpty(controlToCompare)) ? base.GetControlValidationValue(controlToCompare) : this.ValueToCompare);
			return BaseCompareValidator.Compare(base.GetControlValidationValue(base.ControlToValidate), false, text2, base.CultureInvariantValues, this.Operator, base.Type);
		}

		/// <summary>Gets or sets the input control to compare with the input control being validated.</summary>
		/// <returns>The input control to compare with the input control being validated. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06001F68 RID: 8040 RVA: 0x0004D85C File Offset: 0x0004BA5C
		// (set) Token: 0x06001F69 RID: 8041 RVA: 0x0004D873 File Offset: 0x0004BA73
		[DefaultValue("")]
		[TypeConverter(typeof(ValidatedControlConverter))]
		[WebSysDescription("")]
		[Themeable(false)]
		[WebCategory("Behavior")]
		public string ControlToCompare
		{
			get
			{
				return this.ViewState.GetString("ControlToCompare", string.Empty);
			}
			set
			{
				this.ViewState["ControlToCompare"] = value;
			}
		}

		/// <summary>Gets or sets the comparison operation to perform.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ValidationCompareOperator" /> values. The default value is Equal.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified comparison operator is not one of the <see cref="T:System.Web.UI.WebControls.ValidationCompareOperator" /> values. </exception>
		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06001F6A RID: 8042 RVA: 0x0004D886 File Offset: 0x0004BA86
		// (set) Token: 0x06001F6B RID: 8043 RVA: 0x0004D899 File Offset: 0x0004BA99
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[DefaultValue(ValidationCompareOperator.Equal)]
		public ValidationCompareOperator Operator
		{
			get
			{
				return (ValidationCompareOperator)this.ViewState.GetInt("Operator", 0);
			}
			set
			{
				this.ViewState["Operator"] = (int)value;
			}
		}

		/// <summary>Gets or sets a constant value to compare with the value entered by the user in the input control being validated.</summary>
		/// <returns>The constant value to compare with the value entered by the user in the input control being validated. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06001F6C RID: 8044 RVA: 0x0004D8B1 File Offset: 0x0004BAB1
		// (set) Token: 0x06001F6D RID: 8045 RVA: 0x0004D8C8 File Offset: 0x0004BAC8
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[Themeable(false)]
		[DefaultValue("")]
		public string ValueToCompare
		{
			get
			{
				return this.ViewState.GetString("ValueToCompare", string.Empty);
			}
			set
			{
				this.ViewState["ValueToCompare"] = value;
			}
		}
	}
}
