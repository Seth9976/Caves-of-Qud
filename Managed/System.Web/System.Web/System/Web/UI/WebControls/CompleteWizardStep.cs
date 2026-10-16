using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Defines the template of the final step for creating a user account with the <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control.</summary>
	// Token: 0x02000350 RID: 848
	[Browsable(false)]
	public sealed class CompleteWizardStep : TemplatedWizardStep
	{
		/// <summary>Gets or sets the type of user interface (UI) to display for the <see cref="T:System.Web.UI.WebControls.CompleteWizardStep" /> page of a <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control.</summary>
		/// <returns>The <see cref="F:System.Web.UI.WebControls.WizardStepType.Complete" /> enumeration value for the <see cref="T:System.Web.UI.WebControls.WizardStepType" /> enumeration.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to set the <see cref="P:System.Web.UI.WebControls.CompleteWizardStep.StepType" /> property.</exception>
		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06001F6F RID: 8047 RVA: 0x00008B4A File Offset: 0x00006D4A
		// (set) Token: 0x06001F70 RID: 8048 RVA: 0x0004D8E3 File Offset: 0x0004BAE3
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Themeable(false)]
		[Filterable(false)]
		[Browsable(false)]
		public override WizardStepType StepType
		{
			get
			{
				return WizardStepType.Complete;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		/// <summary>Gets or sets the title to display for the final user account creation step of the <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control.</summary>
		/// <returns>The title to use for the final user account creation step of the <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control. The default value is "Complete".</returns>
		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06001F71 RID: 8049 RVA: 0x0004D8EC File Offset: 0x0004BAEC
		// (set) Token: 0x06001F72 RID: 8050 RVA: 0x0004D91E File Offset: 0x0004BB1E
		[Localizable(true)]
		public override string Title
		{
			get
			{
				object obj = this.ViewState["TitleText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Complete");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("TitleText");
					return;
				}
				this.ViewState["TitleText"] = value;
			}
		}
	}
}
