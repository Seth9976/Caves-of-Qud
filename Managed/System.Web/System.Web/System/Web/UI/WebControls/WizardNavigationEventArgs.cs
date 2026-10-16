using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="P:System.Web.UI.WebControls.WizardNavigationEventArgs.CurrentStepIndex" /> property and the <see cref="P:System.Web.UI.WebControls.WizardNavigationEventArgs.NextStepIndex" /> property for navigation in wizard controls.</summary>
	// Token: 0x02000447 RID: 1095
	public class WizardNavigationEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WizardNavigationEventArgs" /> class.</summary>
		/// <param name="currentStepIndex">The index of the <see cref="T:System.Web.UI.WebControls.WizardStep" /> object that is currently displayed in the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</param>
		/// <param name="nextStepIndex">The index of the <see cref="T:System.Web.UI.WebControls.WizardStep" /> object that the <see cref="T:System.Web.UI.WebControls.Wizard" /> control will display next.</param>
		// Token: 0x060032BC RID: 12988 RVA: 0x000869C5 File Offset: 0x00084BC5
		public WizardNavigationEventArgs(int currentStepIndex, int nextStepIndex)
		{
			this.curStepIndex = currentStepIndex;
			this.nxtStepIndex = nextStepIndex;
			this.cancel = false;
		}

		/// <summary>Gets or sets a value indicating whether the navigation to the next step in the wizard should be canceled.</summary>
		/// <returns>true if the navigation to the next step should be canceled; otherwise, false. The default value is false.</returns>
		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x060032BD RID: 12989 RVA: 0x000869E2 File Offset: 0x00084BE2
		// (set) Token: 0x060032BE RID: 12990 RVA: 0x000869EA File Offset: 0x00084BEA
		public bool Cancel
		{
			get
			{
				return this.cancel;
			}
			set
			{
				this.cancel = value;
			}
		}

		/// <summary>Gets the index of the <see cref="T:System.Web.UI.WebControls.WizardStep" /> object currently displayed in the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
		/// <returns>A zero-based index value that represents the <see cref="T:System.Web.UI.WebControls.WizardStep" /> object that is currently displayed in the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</returns>
		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x060032BF RID: 12991 RVA: 0x000869F3 File Offset: 0x00084BF3
		public int CurrentStepIndex
		{
			get
			{
				return this.curStepIndex;
			}
		}

		/// <summary>Gets a value that represents the index of the <see cref="T:System.Web.UI.WebControls.WizardStep" /> object that the <see cref="T:System.Web.UI.WebControls.Wizard" /> control will display next.</summary>
		/// <returns>A zero-based index value that represents the <see cref="T:System.Web.UI.WebControls.WizardStep" /> object that the <see cref="T:System.Web.UI.WebControls.Wizard" /> control will display next.</returns>
		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x060032C0 RID: 12992 RVA: 0x000869FB File Offset: 0x00084BFB
		public int NextStepIndex
		{
			get
			{
				return this.nxtStepIndex;
			}
		}

		// Token: 0x04001CB3 RID: 7347
		private int curStepIndex;

		// Token: 0x04001CB4 RID: 7348
		private int nxtStepIndex;

		// Token: 0x04001CB5 RID: 7349
		private bool cancel;
	}
}
