using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a step in a wizard control that can be customized through the use of templates.</summary>
	// Token: 0x02000424 RID: 1060
	[Themeable(true)]
	[Bindable(false)]
	[PersistChildren(false)]
	[ParseChildren(true)]
	[ToolboxItem(false)]
	[ControlBuilder(typeof(WizardStepControlBuilder))]
	public class TemplatedWizardStep : WizardStepBase
	{
		/// <summary>Gets or sets the template for displaying the content of a step in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> object that contains the template for displaying the content of a step in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</returns>
		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x06002FCD RID: 12237 RVA: 0x0007BFCB File Offset: 0x0007A1CB
		// (set) Token: 0x06002FCE RID: 12238 RVA: 0x0007BFD3 File Offset: 0x0007A1D3
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[TemplateContainer(typeof(Wizard))]
		public virtual ITemplate ContentTemplate
		{
			get
			{
				return this._contentTemplate;
			}
			set
			{
				this._contentTemplate = value;
			}
		}

		/// <summary>Gets the container that a <see cref="T:System.Web.UI.WebControls.Wizard" /> control uses to create a <see cref="P:System.Web.UI.WebControls.TemplatedWizardStep.ContentTemplate" /> template for a step.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Control" /> that contains the <see cref="P:System.Web.UI.WebControls.TemplatedWizardStep.ContentTemplate" /> template for a step.</returns>
		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x06002FCF RID: 12239 RVA: 0x0007BFDC File Offset: 0x0007A1DC
		// (set) Token: 0x06002FD0 RID: 12240 RVA: 0x0007BFE4 File Offset: 0x0007A1E4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Control ContentTemplateContainer
		{
			get
			{
				return this._contentTemplateContainer;
			}
			internal set
			{
				this._contentTemplateContainer = value;
			}
		}

		/// <summary>Gets or sets the template for displaying the navigation user interface (UI) of a step in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> object that contains the template for displaying the navigation UI of a step in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</returns>
		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x06002FD1 RID: 12241 RVA: 0x0007BFED File Offset: 0x0007A1ED
		// (set) Token: 0x06002FD2 RID: 12242 RVA: 0x0007BFF5 File Offset: 0x0007A1F5
		[TemplateContainer(typeof(Wizard))]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate CustomNavigationTemplate
		{
			get
			{
				return this._customNavigationTemplate;
			}
			set
			{
				this._customNavigationTemplate = value;
			}
		}

		/// <summary>Gets the container that a <see cref="T:System.Web.UI.WebControls.Wizard" /> control uses to create a <see cref="P:System.Web.UI.WebControls.TemplatedWizardStep.CustomNavigationTemplate" /> template for a step.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Control" /> that contains the <see cref="P:System.Web.UI.WebControls.TemplatedWizardStep.CustomNavigationTemplate" /> template for a step.</returns>
		/// <exception cref="T:System.NullReferenceException">If <see cref="P:System.Web.UI.WebControls.TemplatedWizardStep.CustomNavigationTemplate" /> has no content.</exception>
		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x06002FD3 RID: 12243 RVA: 0x0007BFFE File Offset: 0x0007A1FE
		// (set) Token: 0x06002FD4 RID: 12244 RVA: 0x0007C006 File Offset: 0x0007A206
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Bindable(false)]
		public Control CustomNavigationTemplateContainer
		{
			get
			{
				return this._customNavigationTemplateContainer;
			}
			internal set
			{
				this._customNavigationTemplateContainer = value;
			}
		}

		/// <summary>Gets the skin to apply to the <see cref="T:System.Web.UI.WebControls.TemplatedWizardStep" />.</summary>
		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x06002FD5 RID: 12245 RVA: 0x0003003F File Offset: 0x0002E23F
		// (set) Token: 0x06002FD6 RID: 12246 RVA: 0x00030047 File Offset: 0x0002E247
		[Browsable(true)]
		[global::System.MonoTODO("Why override?")]
		public override string SkinID
		{
			get
			{
				return base.SkinID;
			}
			set
			{
				base.SkinID = value;
			}
		}

		// Token: 0x04001BFB RID: 7163
		private ITemplate _contentTemplate;

		// Token: 0x04001BFC RID: 7164
		private Control _contentTemplateContainer;

		// Token: 0x04001BFD RID: 7165
		private ITemplate _customNavigationTemplate;

		// Token: 0x04001BFE RID: 7166
		private Control _customNavigationTemplateContainer;
	}
}
