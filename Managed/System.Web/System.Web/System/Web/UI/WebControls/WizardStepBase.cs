using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Implements the basic functionality required by a step in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
	// Token: 0x02000449 RID: 1097
	[Bindable(false)]
	[ControlBuilder(typeof(WizardStepControlBuilder))]
	[ToolboxItem("")]
	public abstract class WizardStepBase : View
	{
		/// <summary>Gets or sets a value indicating whether the user is allowed to return to the current step from a subsequent step in a <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection. </summary>
		/// <returns>true if the user is allowed to return to the current step; otherwise, false. The default value is true.</returns>
		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x060032C5 RID: 12997 RVA: 0x00086A04 File Offset: 0x00084C04
		// (set) Token: 0x060032C6 RID: 12998 RVA: 0x00051A28 File Offset: 0x0004FC28
		[Filterable(false)]
		[Themeable(false)]
		[DefaultValue(true)]
		public virtual bool AllowReturn
		{
			get
			{
				object obj = this.ViewState["AllowReturn"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["AllowReturn"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether themes apply to this control.</summary>
		/// <returns>true to use themes; otherwise, false. The default is false.</returns>
		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x060032C7 RID: 12999 RVA: 0x00086A2D File Offset: 0x00084C2D
		// (set) Token: 0x060032C8 RID: 13000 RVA: 0x00086A35 File Offset: 0x00084C35
		[Browsable(true)]
		public override bool EnableTheming
		{
			get
			{
				return base.EnableTheming;
			}
			set
			{
				base.EnableTheming = value;
			}
		}

		/// <summary>Gets or sets the programmatic identifier assigned to the server control.</summary>
		/// <returns>The programmatic identifier assigned to the control.</returns>
		/// <exception cref="T:System.ArgumentException">The property was set to an invalid identifier string at design time.-or-The property was set to the same identifier as the containing <see cref="P:System.Web.UI.WebControls.WizardStepBase.Wizard" /> control at design time.-or- The property was set to the same identifier as another step in the containing <see cref="P:System.Web.UI.WebControls.WizardStepBase.Wizard" /> control at design time.</exception>
		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x060032C9 RID: 13001 RVA: 0x00034797 File Offset: 0x00032997
		// (set) Token: 0x060032CA RID: 13002 RVA: 0x0003479F File Offset: 0x0003299F
		public override string ID
		{
			get
			{
				return base.ID;
			}
			set
			{
				base.ID = value;
			}
		}

		/// <summary>Gets the name associated with a step in a control that acts as a wizard.</summary>
		/// <returns>The name associated with a step in a control that acts as a wizard.</returns>
		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x060032CB RID: 13003 RVA: 0x00086A3E File Offset: 0x00084C3E
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string Name
		{
			get
			{
				if (this.Title != null && this.Title.Length > 0)
				{
					return this.Title;
				}
				if (this.ID != null && this.ID.Length > 0)
				{
					return this.ID;
				}
				return null;
			}
		}

		/// <summary>Gets or sets the type of navigation user interface (UI) to display for a step in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.WizardStepType" /> enumeration values. The default value is WizardStepType.Auto.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.UI.WebControls.WizardStepType" /> enumeration values.</exception>
		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x060032CC RID: 13004 RVA: 0x00086A7C File Offset: 0x00084C7C
		// (set) Token: 0x060032CD RID: 13005 RVA: 0x00086AA5 File Offset: 0x00084CA5
		[DefaultValue(WizardStepType.Auto)]
		public virtual WizardStepType StepType
		{
			get
			{
				object obj = this.ViewState["StepType"];
				if (obj == null)
				{
					return WizardStepType.Auto;
				}
				return (WizardStepType)obj;
			}
			set
			{
				this.ViewState["StepType"] = value;
			}
		}

		/// <summary>Gets or sets the title to use for a step in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control when the sidebar feature is enabled.</summary>
		/// <returns>The title to use for a step in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control when the sidebar feature is enabled. The default value is an empty string ("").</returns>
		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x060032CE RID: 13006 RVA: 0x00086AC0 File Offset: 0x00084CC0
		// (set) Token: 0x060032CF RID: 13007 RVA: 0x00086AED File Offset: 0x00084CED
		[Localizable(true)]
		[DefaultValue("")]
		public virtual string Title
		{
			get
			{
				object obj = this.ViewState["Title"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["Title"] = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.Wizard" /> control that is the parent of the object derived from <see cref="T:System.Web.UI.WebControls.WizardStepBase" />.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.Wizard" /> control that is the parent of the object derived from <see cref="T:System.Web.UI.WebControls.WizardStepBase" />.</returns>
		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x060032D0 RID: 13008 RVA: 0x00086B00 File Offset: 0x00084D00
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Wizard Wizard
		{
			get
			{
				return this.wizard;
			}
		}

		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> control to restore.</param>
		// Token: 0x060032D1 RID: 13009 RVA: 0x00086B08 File Offset: 0x00084D08
		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
		}

		/// <summary>Raises the <see cref="M:System.Web.UI.Control.OnLoad(System.EventArgs)" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x060032D2 RID: 13010 RVA: 0x00086B11 File Offset: 0x00084D11
		protected internal override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
		}

		/// <summary>Outputs the content of the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> control's child controls to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to be rendered on the client.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x060032D3 RID: 13011 RVA: 0x00086B1A File Offset: 0x00084D1A
		protected internal override void RenderChildren(HtmlTextWriter writer)
		{
			base.RenderChildren(writer);
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x00086B23 File Offset: 0x00084D23
		internal void SetWizard(Wizard w)
		{
			this.wizard = w;
		}

		// Token: 0x04001CB6 RID: 7350
		private Wizard wizard;
	}
}
