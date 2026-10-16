using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Serves as the abstract base class for button fields, such as the <see cref="T:System.Web.UI.WebControls.ButtonField" /> or <see cref="T:System.Web.UI.WebControls.CommandField" /> class. The <see cref="T:System.Web.UI.WebControls.ButtonFieldBase" /> class provides the methods and properties that are common to all button fields.</summary>
	// Token: 0x0200033E RID: 830
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class ButtonFieldBase : DataControlField
	{
		/// <summary>Gets or sets the button type to display in the button field.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values. The default is ButtonType.Link.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value for the <see cref="P:System.Web.UI.WebControls.ButtonFieldBase.ButtonType" /> property is not one of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values. </exception>
		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06001DA0 RID: 7584 RVA: 0x00047EBD File Offset: 0x000460BD
		// (set) Token: 0x06001DA1 RID: 7585 RVA: 0x00047ED0 File Offset: 0x000460D0
		[DefaultValue(ButtonType.Link)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual ButtonType ButtonType
		{
			get
			{
				return (ButtonType)base.ViewState.GetInt("ButtonType", 2);
			}
			set
			{
				base.ViewState["ButtonType"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether validation is performed when a button in a <see cref="T:System.Web.UI.WebControls.ButtonFieldBase" /> object is clicked.</summary>
		/// <returns>true to perform validation when a button in a <see cref="T:System.Web.UI.WebControls.ButtonFieldBase" /> is clicked; otherwise, false. The default is false.</returns>
		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06001DA2 RID: 7586 RVA: 0x00047EEE File Offset: 0x000460EE
		// (set) Token: 0x06001DA3 RID: 7587 RVA: 0x00047F01 File Offset: 0x00046101
		[DefaultValue(false)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual bool CausesValidation
		{
			get
			{
				return base.ViewState.GetBool("CausesValidation", false);
			}
			set
			{
				base.ViewState["CausesValidation"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether the header section is displayed in a <see cref="T:System.Web.UI.WebControls.ButtonFieldBase" /> object.</summary>
		/// <returns>true to show the header section; otherwise, false. The default is false.</returns>
		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06001DA4 RID: 7588 RVA: 0x00047F1F File Offset: 0x0004611F
		// (set) Token: 0x06001DA5 RID: 7589 RVA: 0x00047F32 File Offset: 0x00046132
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		[WebSysDescription("")]
		public override bool ShowHeader
		{
			get
			{
				return base.ViewState.GetBool("showHeader", false);
			}
			set
			{
				base.ViewState["showHeader"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the name of the group of validation controls to validate when a button in a <see cref="T:System.Web.UI.WebControls.ButtonFieldBase" /> object is clicked.</summary>
		/// <returns>The name of the validation group to validate when a button in a <see cref="T:System.Web.UI.WebControls.ButtonFieldBase" /> is clicked. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.ButtonFieldBase.ValidationGroup" /> property is not set.</returns>
		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06001DA6 RID: 7590 RVA: 0x00047F50 File Offset: 0x00046150
		// (set) Token: 0x06001DA7 RID: 7591 RVA: 0x00047F67 File Offset: 0x00046167
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		public virtual string ValidationGroup
		{
			get
			{
				return base.ViewState.GetString("ValidationGroup", string.Empty);
			}
			set
			{
				base.ViewState["ValidationGroup"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Copies the properties of the current object that is derived from the <see cref="T:System.Web.UI.WebControls.ButtonFieldBase" /> class to the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
		/// <param name="newField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> to which to copy the properties of the current <see cref="T:System.Web.UI.WebControls.ButtonFieldBase" />.</param>
		// Token: 0x06001DA8 RID: 7592 RVA: 0x00047F80 File Offset: 0x00046180
		protected override void CopyProperties(DataControlField newField)
		{
			base.CopyProperties(newField);
			ButtonFieldBase buttonFieldBase = (ButtonFieldBase)newField;
			buttonFieldBase.ButtonType = this.ButtonType;
			buttonFieldBase.CausesValidation = this.CausesValidation;
			buttonFieldBase.ShowHeader = this.ShowHeader;
			buttonFieldBase.ValidationGroup = this.ValidationGroup;
		}
	}
}
