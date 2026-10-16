using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Web.Configuration;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Serves as the abstract base class for validation controls.</summary>
	// Token: 0x02000335 RID: 821
	[DefaultProperty("ErrorMessage")]
	[Designer("System.Web.UI.Design.WebControls.BaseValidatorDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class BaseValidator : Label, IValidator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.BaseValidator" /> class.</summary>
		// Token: 0x06001CD9 RID: 7385 RVA: 0x00045EEF File Offset: 0x000440EF
		protected BaseValidator()
		{
			this.valid = true;
			this.ForeColor = Color.Red;
		}

		/// <summary>This property is not supported.</summary>
		/// <returns>This property is not supported and always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to set this property.</exception>
		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06001CDA RID: 7386 RVA: 0x00045F09 File Offset: 0x00044109
		// (set) Token: 0x06001CDB RID: 7387 RVA: 0x00045F11 File Offset: 0x00044111
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override string AssociatedControlID
		{
			get
			{
				return base.AssociatedControlID;
			}
			set
			{
				base.AssociatedControlID = value;
			}
		}

		/// <summary>Gets or sets the name of the validation group to which this validation control belongs.</summary>
		/// <returns>The name of the validation group to which this validation control belongs. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06001CDC RID: 7388 RVA: 0x0003EF36 File Offset: 0x0003D136
		// (set) Token: 0x06001CDD RID: 7389 RVA: 0x0003EF4D File Offset: 0x0003D14D
		[DefaultValue("")]
		[Themeable(false)]
		public virtual string ValidationGroup
		{
			get
			{
				return this.ViewState.GetString("ValidationGroup", string.Empty);
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether focus is set to the control specified by the <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> property when validation fails.</summary>
		/// <returns>true to set focus on the control specified by <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> when validation fails; otherwise, false. The default is false.</returns>
		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06001CDE RID: 7390 RVA: 0x00045F1A File Offset: 0x0004411A
		// (set) Token: 0x06001CDF RID: 7391 RVA: 0x00045F2D File Offset: 0x0004412D
		[DefaultValue(false)]
		[Themeable(false)]
		public bool SetFocusOnError
		{
			get
			{
				return this.ViewState.GetBool("SetFocusOnError", false);
			}
			set
			{
				this.ViewState["SetFocusOnError"] = value;
			}
		}

		/// <summary>Gets or sets the text displayed in the validation control when validation fails.</summary>
		/// <returns>The text displayed in the validation control when validation fails. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06001CE0 RID: 7392 RVA: 0x00045F45 File Offset: 0x00044145
		// (set) Token: 0x06001CE1 RID: 7393 RVA: 0x00045F4D File Offset: 0x0004414D
		[DefaultValue("")]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[global::System.MonoTODO("Why override?")]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		/// <summary>Gets or sets the input control to validate.</summary>
		/// <returns>The input control to validate. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06001CE2 RID: 7394 RVA: 0x00045F56 File Offset: 0x00044156
		// (set) Token: 0x06001CE3 RID: 7395 RVA: 0x00045F6D File Offset: 0x0004416D
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[DefaultValue("")]
		[TypeConverter(typeof(ValidatedControlConverter))]
		[Themeable(false)]
		[IDReferenceProperty(typeof(Control))]
		public string ControlToValidate
		{
			get
			{
				return this.ViewState.GetString("ControlToValidate", string.Empty);
			}
			set
			{
				this.ViewState["ControlToValidate"] = value;
			}
		}

		/// <summary>Gets or sets the display behavior of the error message in a validation control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ValidatorDisplay" /> values. The default value is Static.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the <see cref="T:System.Web.UI.WebControls.ValidatorDisplay" /> values. </exception>
		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06001CE4 RID: 7396 RVA: 0x00045F80 File Offset: 0x00044180
		// (set) Token: 0x06001CE5 RID: 7397 RVA: 0x00045F93 File Offset: 0x00044193
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[DefaultValue(ValidatorDisplay.Static)]
		[Themeable(false)]
		public ValidatorDisplay Display
		{
			get
			{
				return (ValidatorDisplay)this.ViewState.GetInt("Display", 1);
			}
			set
			{
				this.ViewState["Display"] = (int)value;
			}
		}

		/// <summary>Gets or sets a value indicating whether client-side validation is enabled.</summary>
		/// <returns>true if client-side validation is enabled; otherwise, false. The default value is true.</returns>
		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06001CE6 RID: 7398 RVA: 0x00045FAB File Offset: 0x000441AB
		// (set) Token: 0x06001CE7 RID: 7399 RVA: 0x00045FBE File Offset: 0x000441BE
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[DefaultValue(true)]
		public bool EnableClientScript
		{
			get
			{
				return this.ViewState.GetBool("EnableClientScript", true);
			}
			set
			{
				this.ViewState["EnableClientScript"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the validation control is enabled.</summary>
		/// <returns>true if the validation control is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06001CE8 RID: 7400 RVA: 0x00045FD6 File Offset: 0x000441D6
		// (set) Token: 0x06001CE9 RID: 7401 RVA: 0x00045FE9 File Offset: 0x000441E9
		public override bool Enabled
		{
			get
			{
				return this.ViewState.GetBool("BaseValidatorEnabled", true);
			}
			set
			{
				this.ViewState["BaseValidatorEnabled"] = value;
			}
		}

		/// <summary>Gets or sets the text for the error message displayed in a <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> control when validation fails.</summary>
		/// <returns>The error message displayed in a <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> control when validation fails. The default value is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06001CEA RID: 7402 RVA: 0x00046001 File Offset: 0x00044201
		// (set) Token: 0x06001CEB RID: 7403 RVA: 0x00046018 File Offset: 0x00044218
		[WebSysDescription("")]
		[DefaultValue("")]
		[Localizable(true)]
		[WebCategory("Appearance")]
		public string ErrorMessage
		{
			get
			{
				return this.ViewState.GetString("ErrorMessage", string.Empty);
			}
			set
			{
				this.ViewState["ErrorMessage"] = value;
			}
		}

		/// <summary>Gets or sets the color of the message displayed when validation fails.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color of the message displayed when validation fails. The default is <see cref="P:System.Drawing.Color.Red" />.</returns>
		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06001CEC RID: 7404 RVA: 0x0004602B File Offset: 0x0004422B
		// (set) Token: 0x06001CED RID: 7405 RVA: 0x00046033 File Offset: 0x00044233
		[DefaultValue(typeof(Color), "Red")]
		public override Color ForeColor
		{
			get
			{
				return this.forecolor;
			}
			set
			{
				this.forecolor = value;
				base.ForeColor = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the associated input control passes validation.</summary>
		/// <returns>true if the associated input control passes validation; otherwise, false. The default value is true.</returns>
		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06001CEE RID: 7406 RVA: 0x00046043 File Offset: 0x00044243
		// (set) Token: 0x06001CEF RID: 7407 RVA: 0x0004604B File Offset: 0x0004424B
		[Browsable(false)]
		[Themeable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(true)]
		[WebCategory("Misc")]
		[WebSysDescription("")]
		public bool IsValid
		{
			get
			{
				return this.valid;
			}
			set
			{
				this.valid = value;
			}
		}

		/// <summary>Gets a value that indicates whether the control specified by the <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> property is a valid control.</summary>
		/// <returns>true if the control specified by <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> is a valid control; otherwise, false.</returns>
		/// <exception cref="T:System.Web.HttpException">No value is specified in the <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> property.- or -The input control specified by the <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> property is not found on the page. </exception>
		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x00046054 File Offset: 0x00044254
		protected bool PropertiesValid
		{
			get
			{
				return this.NamingContainer.FindControl(this.ControlToValidate) != null;
			}
		}

		/// <summary>Gets a value that indicates whether the client's browser supports "uplevel" rendering.</summary>
		/// <returns>true if the browser supports "uplevel" rendering; otherwise, false.</returns>
		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06001CF1 RID: 7409 RVA: 0x0004606C File Offset: 0x0004426C
		protected bool RenderUplevel
		{
			get
			{
				return this.render_uplevel;
			}
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x0004606C File Offset: 0x0004426C
		internal bool GetRenderUplevel()
		{
			return this.render_uplevel;
		}

		/// <summary>Adds the HTML attributes and styles that need to be rendered for the control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06001CF3 RID: 7411 RVA: 0x00046074 File Offset: 0x00044274
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (this.render_uplevel)
			{
				if (this.ID == null)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
				}
				if (this.ControlToValidate != string.Empty)
				{
					this.RegisterExpandoAttribute(this.ClientID, "controltovalidate", this.GetControlRenderID(this.ControlToValidate));
				}
				if (this.ErrorMessage != string.Empty)
				{
					this.RegisterExpandoAttribute(this.ClientID, "errormessage", this.ErrorMessage, true);
				}
				if (this.ValidationGroup != string.Empty)
				{
					this.RegisterExpandoAttribute(this.ClientID, "validationGroup", this.ValidationGroup, true);
				}
				if (this.SetFocusOnError)
				{
					this.RegisterExpandoAttribute(this.ClientID, "focusOnError", "t");
				}
				bool isEnabled = base.IsEnabled;
				if (!isEnabled)
				{
					this.RegisterExpandoAttribute(this.ClientID, "enabled", "False");
				}
				if (isEnabled && !this.IsValid)
				{
					this.RegisterExpandoAttribute(this.ClientID, "isvalid", "False");
				}
				else if (this.Display == ValidatorDisplay.Static)
				{
					writer.AddStyleAttribute("visibility", "hidden");
				}
				else
				{
					writer.AddStyleAttribute("display", "none");
				}
				if (this.Display != ValidatorDisplay.Static)
				{
					this.RegisterExpandoAttribute(this.ClientID, "display", this.Display.ToString());
				}
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x000461E4 File Offset: 0x000443E4
		internal void RegisterExpandoAttribute(string controlId, string attributeName, string attributeValue)
		{
			this.RegisterExpandoAttribute(controlId, attributeName, attributeValue, false);
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x000461F0 File Offset: 0x000443F0
		internal void RegisterExpandoAttribute(string controlId, string attributeName, string attributeValue, bool encode)
		{
			Page page = this.Page;
			if (page.ScriptManager != null)
			{
				page.ScriptManager.RegisterExpandoAttributeExternal(this, controlId, attributeName, attributeValue, encode);
				return;
			}
			page.ClientScript.RegisterExpandoAttribute(controlId, attributeName, attributeValue, encode);
		}

		/// <summary>Verifies whether the specified control is on the page and contains validation properties.</summary>
		/// <param name="name">The control to verify. </param>
		/// <param name="propertyName">Additional text to describe the source of the exception, if an exception is thrown from using this method. </param>
		/// <exception cref="T:System.Web.HttpException">The specified control is not found.- or -The specified control does not have a <see cref="T:System.Web.UI.ValidationPropertyAttribute" /> attribute associated with it; therefore, it cannot be validated with a validation control. </exception>
		// Token: 0x06001CF6 RID: 7414 RVA: 0x0004622E File Offset: 0x0004442E
		protected void CheckControlValidationProperty(string name, string propertyName)
		{
			Control control = this.NamingContainer.FindControl(name);
			if (control == null)
			{
				throw new HttpException(string.Format("Unable to find control id '{0}'.", name));
			}
			if (BaseValidator.GetValidationProperty(control) == null)
			{
				throw new HttpException(string.Format("Unable to find ValidationProperty attribute '{0}' on control '{1}'", propertyName, name));
			}
		}

		/// <summary>Determines whether the control specified by the <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> property is a valid control.</summary>
		/// <returns>true if the control specified by <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> is a valid control; otherwise, false.</returns>
		/// <exception cref="T:System.Web.HttpException">No value is specified for the <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> property.- or -The input control specified by the <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> property is not found on the page.- or -The input control specified by the <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> property does not have a <see cref="T:System.Web.UI.ValidationPropertyAttribute" /> attribute associated with it; therefore, it cannot be validated with a validation control.</exception>
		// Token: 0x06001CF7 RID: 7415 RVA: 0x00046269 File Offset: 0x00044469
		protected virtual bool ControlPropertiesValid()
		{
			if (this.ControlToValidate.Length == 0)
			{
				throw new HttpException(string.Format("ControlToValidate property of '{0}' cannot be blank.", this.ID));
			}
			this.CheckControlValidationProperty(this.ControlToValidate, string.Empty);
			return true;
		}

		/// <summary>Determines whether the validation control can perform client-side validation.</summary>
		/// <returns>true if the validation control can perform client-side validation; otherwise, false.</returns>
		// Token: 0x06001CF8 RID: 7416 RVA: 0x000462A0 File Offset: 0x000444A0
		protected virtual bool DetermineRenderUplevel()
		{
			return this.EnableClientScript && UplevelHelper.IsUplevel(HttpCapabilitiesBase.GetUserAgentForDetection(HttpContext.Current.Request));
		}

		/// <summary>When overridden in a derived class, this method contains the code to determine whether the value in the input control is valid.</summary>
		/// <returns>true if the value in the input control is valid; otherwise, false.</returns>
		// Token: 0x06001CF9 RID: 7417
		protected abstract bool EvaluateIsValid();

		/// <summary>Gets the client ID of the specified control.</summary>
		/// <returns>The client ID of the specified control.</returns>
		/// <param name="name">The name of the control to get the client ID from. </param>
		// Token: 0x06001CFA RID: 7418 RVA: 0x000462C0 File Offset: 0x000444C0
		protected string GetControlRenderID(string name)
		{
			Control control = this.NamingContainer.FindControl(name);
			if (control == null)
			{
				return null;
			}
			return control.ClientID;
		}

		/// <summary>Gets the value associated with the specified input control.</summary>
		/// <returns>The value associated with the specified input control.</returns>
		/// <param name="name">The name of the input control to get the value from. </param>
		// Token: 0x06001CFB RID: 7419 RVA: 0x000462E8 File Offset: 0x000444E8
		protected string GetControlValidationValue(string name)
		{
			Control control = this.NamingContainer.FindControl(name);
			if (control == null)
			{
				return null;
			}
			PropertyDescriptor validationProperty = BaseValidator.GetValidationProperty(control);
			if (validationProperty == null)
			{
				return null;
			}
			object value = validationProperty.GetValue(control);
			if (value == null)
			{
				return string.Empty;
			}
			if (value is ListItem)
			{
				return ((ListItem)value).Value;
			}
			return value.ToString();
		}

		/// <summary>Determines the validation property of a control (if it exists).</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that contains the validation property of the control.</returns>
		/// <param name="component">A <see cref="T:System.Object" /> that represents the control to get the validation property of. </param>
		// Token: 0x06001CFC RID: 7420 RVA: 0x00046340 File Offset: 0x00044540
		public static PropertyDescriptor GetValidationProperty(object component)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component);
			foreach (object obj in TypeDescriptor.GetAttributes(component))
			{
				ValidationPropertyAttribute validationPropertyAttribute = ((Attribute)obj) as ValidationPropertyAttribute;
				if (validationPropertyAttribute != null && validationPropertyAttribute.Name != null)
				{
					return properties[validationPropertyAttribute.Name];
				}
			}
			return null;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001CFD RID: 7421 RVA: 0x000463C0 File Offset: 0x000445C0
		protected internal override void OnInit(EventArgs e)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.Validators.Add(this);
				page.GetValidators(this.ValidationGroup).Add(this);
			}
			base.OnInit(e);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001CFE RID: 7422 RVA: 0x000463FC File Offset: 0x000445FC
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.pre_render_called = true;
			this.ControlPropertiesValid();
			this.render_uplevel = this.DetermineRenderUplevel();
			if (this.render_uplevel)
			{
				this.RegisterValidatorCommonScript();
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Unload" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001CFF RID: 7423 RVA: 0x00046430 File Offset: 0x00044630
		protected internal override void OnUnload(EventArgs e)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.Validators.Remove(this);
				if (!string.IsNullOrEmpty(this.ValidationGroup))
				{
					page.GetValidators(this.ValidationGroup).Remove(this);
				}
			}
			base.OnUnload(e);
		}

		/// <summary>Registers code on the page for client-side validation.</summary>
		// Token: 0x06001D00 RID: 7424 RVA: 0x0004647C File Offset: 0x0004467C
		protected void RegisterValidatorCommonScript()
		{
			Page page = this.Page;
			if (page != null)
			{
				if (page.ScriptManager != null)
				{
					page.ScriptManager.RegisterClientScriptResourceExternal(this, typeof(BaseValidator), "WebUIValidation_2.0.js");
					page.ScriptManager.RegisterClientScriptBlockExternal(this, typeof(BaseValidator), "ValidationInitializeScript", page.ValidationInitializeScript, true);
					page.ScriptManager.RegisterOnSubmitStatementExternal(this, typeof(BaseValidator), "ValidationOnSubmitStatement", page.ValidationOnSubmitStatement);
					page.ScriptManager.RegisterStartupScriptExternal(this, typeof(BaseValidator), "ValidationStartupScript", page.ValidationStartupScript, true);
					return;
				}
				if (!page.ClientScript.IsClientScriptIncludeRegistered(typeof(BaseValidator), "Mono-System.Web-ValidationClientScriptBlock"))
				{
					page.ClientScript.RegisterClientScriptInclude(typeof(BaseValidator), "Mono-System.Web-ValidationClientScriptBlock", page.ClientScript.GetWebResourceUrl(typeof(BaseValidator), "WebUIValidation_2.0.js"));
					page.ClientScript.RegisterClientScriptBlock(typeof(BaseValidator), "Mono-System.Web-ValidationClientScriptBlock.Initialize", page.ValidationInitializeScript, true);
					page.ClientScript.RegisterOnSubmitStatement(typeof(BaseValidator), "Mono-System.Web-ValidationOnSubmitStatement", page.ValidationOnSubmitStatement);
					page.ClientScript.RegisterStartupScript(typeof(BaseValidator), "Mono-System.Web-ValidationStartupScript", page.ValidationStartupScript, true);
				}
			}
		}

		/// <summary>Registers an ECMAScript array declaration using the array name Page_Validators.</summary>
		// Token: 0x06001D01 RID: 7425 RVA: 0x000465D8 File Offset: 0x000447D8
		protected virtual void RegisterValidatorDeclaration()
		{
			Page page = this.Page;
			if (page != null)
			{
				if (page.ScriptManager != null)
				{
					page.ScriptManager.RegisterArrayDeclarationExternal(this, "Page_Validators", "document.getElementById ('" + this.ClientID + "')");
					page.ScriptManager.RegisterStartupScriptExternal(this, typeof(BaseValidator), this.ClientID + "DisposeScript", string.Concat(new string[] { "\ndocument.getElementById('", this.ClientID, "').dispose = function() {\n    Array.remove(Page_Validators, document.getElementById('", this.ClientID, "'));\n}\n" }), true);
					return;
				}
				page.ClientScript.RegisterArrayDeclaration("Page_Validators", "document.getElementById ('" + this.ClientID + "')");
			}
		}

		/// <summary>Displays the control on the client.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream for rendering on the client. </param>
		// Token: 0x06001D02 RID: 7426 RVA: 0x000466A8 File Offset: 0x000448A8
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (!base.IsEnabled && !this.EnableClientScript)
			{
				return;
			}
			if (this.render_uplevel)
			{
				this.RegisterValidatorDeclaration();
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool isValid = this.IsValid;
			if (!this.pre_render_called)
			{
				flag = true;
				flag2 = true;
			}
			else if (this.render_uplevel)
			{
				flag = true;
				flag2 = this.Display > ValidatorDisplay.None;
			}
			else if (this.Display != ValidatorDisplay.None)
			{
				flag = !isValid;
				flag2 = !isValid;
				flag3 = isValid && this.Display == ValidatorDisplay.Static;
			}
			if (flag)
			{
				this.AddAttributesToRender(writer);
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
			}
			if (flag2 || flag3)
			{
				string text;
				if (flag2)
				{
					text = this.Text;
					if (string.IsNullOrEmpty(text))
					{
						text = this.ErrorMessage;
					}
				}
				else
				{
					text = "&nbsp;";
				}
				writer.Write(text);
			}
			if (flag)
			{
				writer.RenderEndTag();
			}
		}

		/// <summary>Performs validation on the associated input control and updates the <see cref="P:System.Web.UI.WebControls.BaseValidator.IsValid" /> property.</summary>
		// Token: 0x06001D03 RID: 7427 RVA: 0x00046774 File Offset: 0x00044974
		public void Validate()
		{
			if (base.IsEnabled && this.Visible)
			{
				this.IsValid = this.ControlPropertiesValid() && this.EvaluateIsValid();
				return;
			}
			this.IsValid = true;
		}

		/// <summary>Gets a value that indicates whether the control generates unobtrusive JavaScript.</summary>
		/// <returns>true if the control generates unobtrusive JavaScript; otherwise, false.</returns>
		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06001D04 RID: 7428 RVA: 0x000467A8 File Offset: 0x000449A8
		protected bool IsUnobtrusive
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		// Token: 0x04001817 RID: 6167
		private bool render_uplevel;

		// Token: 0x04001818 RID: 6168
		private bool valid;

		// Token: 0x04001819 RID: 6169
		private Color forecolor;

		// Token: 0x0400181A RID: 6170
		private bool pre_render_called;
	}
}
