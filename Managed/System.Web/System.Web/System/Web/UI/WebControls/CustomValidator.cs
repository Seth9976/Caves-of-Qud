using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Performs user-defined validation on an input control.</summary>
	// Token: 0x02000365 RID: 869
	[DefaultEvent("ServerValidate")]
	[ToolboxData("<{0}:CustomValidator runat=\"server\" ErrorMessage=\"CustomValidator\"></{0}:CustomValidator>")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class CustomValidator : BaseValidator
	{
		/// <summary>Occurs when validation is performed on the server.</summary>
		// Token: 0x14000063 RID: 99
		// (add) Token: 0x06002088 RID: 8328 RVA: 0x00051A72 File Offset: 0x0004FC72
		// (remove) Token: 0x06002089 RID: 8329 RVA: 0x00051A85 File Offset: 0x0004FC85
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public event ServerValidateEventHandler ServerValidate
		{
			add
			{
				this.events.AddHandler(CustomValidator.serverValidateEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(CustomValidator.serverValidateEvent, value);
			}
		}

		/// <summary>Gets or sets the name of the custom client-side script function used for validation.</summary>
		/// <returns>The name of the custom client script function used for validation. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.NoteThe function name should not include any parentheses or parameters.</returns>
		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x0600208B RID: 8331 RVA: 0x00051AAB File Offset: 0x0004FCAB
		// (set) Token: 0x0600208C RID: 8332 RVA: 0x00051AC2 File Offset: 0x0004FCC2
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[Themeable(false)]
		public string ClientValidationFunction
		{
			get
			{
				return this.ViewState.GetString("ClientValidationFunction", string.Empty);
			}
			set
			{
				this.ViewState["ClientValidationFunction"] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value indicating whether empty text should be validated.</summary>
		/// <returns>true if empty text should be validated; otherwise, false.</returns>
		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x0600208D RID: 8333 RVA: 0x00051AD5 File Offset: 0x0004FCD5
		// (set) Token: 0x0600208E RID: 8334 RVA: 0x00051AE8 File Offset: 0x0004FCE8
		[DefaultValue(false)]
		[Themeable(false)]
		public bool ValidateEmptyText
		{
			get
			{
				return this.ViewState.GetBool("ValidateEmptyText", false);
			}
			set
			{
				this.ViewState["ValidateEmptyText"] = value;
			}
		}

		/// <summary>Adds the properties of the <see cref="T:System.Web.UI.WebControls.CustomValidator" /> control to the output stream for rendering on the client.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream for rendering on the client. </param>
		// Token: 0x0600208F RID: 8335 RVA: 0x00051B00 File Offset: 0x0004FD00
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (base.RenderUplevel)
			{
				base.RegisterExpandoAttribute(this.ClientID, "evaluationfunction", "CustomValidatorEvaluateIsValid");
				if (this.ValidateEmptyText)
				{
					base.RegisterExpandoAttribute(this.ClientID, "validateemptytext", "true");
				}
				string clientValidationFunction = this.ClientValidationFunction;
				if (!string.IsNullOrEmpty(clientValidationFunction))
				{
					base.RegisterExpandoAttribute(this.ClientID, "clientvalidationfunction", clientValidationFunction, true);
				}
			}
		}

		/// <summary>Checks the properties of the control for valid values.</summary>
		/// <returns>true if the control properties are valid; otherwise, false.</returns>
		// Token: 0x06002090 RID: 8336 RVA: 0x00051B72 File Offset: 0x0004FD72
		protected override bool ControlPropertiesValid()
		{
			return string.IsNullOrEmpty(base.ControlToValidate) || base.ControlPropertiesValid();
		}

		/// <summary>Overrides the <see cref="M:System.Web.UI.MobileControls.BaseValidator.EvaluateIsValid" /> method.</summary>
		/// <returns>true if the value in the input control is valid; otherwise, false.</returns>
		// Token: 0x06002091 RID: 8337 RVA: 0x00051B8C File Offset: 0x0004FD8C
		protected override bool EvaluateIsValid()
		{
			string controlToValidate = base.ControlToValidate;
			if (!string.IsNullOrEmpty(controlToValidate))
			{
				string controlValidationValue = base.GetControlValidationValue(controlToValidate);
				return (string.IsNullOrEmpty(controlValidationValue) && !this.ValidateEmptyText) || this.OnServerValidate(controlValidationValue);
			}
			return this.OnServerValidate(string.Empty);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.CustomValidator.ServerValidate" /> event for the <see cref="T:System.Web.UI.WebControls.CustomValidator" /> control.</summary>
		/// <returns>true if the value specified by the <paramref name="value" /> parameter passes validation; otherwise, false.</returns>
		/// <param name="value">The value to validate. </param>
		// Token: 0x06002092 RID: 8338 RVA: 0x00051BD8 File Offset: 0x0004FDD8
		protected virtual bool OnServerValidate(string value)
		{
			ServerValidateEventHandler serverValidateEventHandler = this.events[CustomValidator.serverValidateEvent] as ServerValidateEventHandler;
			if (serverValidateEventHandler != null)
			{
				ServerValidateEventArgs e = new ServerValidateEventArgs(value, true);
				serverValidateEventHandler(this, e);
				return e.IsValid;
			}
			return true;
		}

		// Token: 0x040018A4 RID: 6308
		private static readonly object serverValidateEvent = new object();

		// Token: 0x040018A5 RID: 6309
		private EventHandlerList events = new EventHandlerList();
	}
}
