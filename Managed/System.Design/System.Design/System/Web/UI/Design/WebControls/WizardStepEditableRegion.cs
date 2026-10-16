using System;
using System.Web.UI.WebControls;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Defines an editable region of content within the design-time markup of a wizard step for a <see cref="T:System.Web.UI.Design.WebControls.WizardDesigner" /> object.</summary>
	// Token: 0x020001B9 RID: 441
	public class WizardStepEditableRegion : EditableDesignerRegion
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.WizardStepEditableRegion" /> class using the given designer and step.</summary>
		/// <param name="designer">A <see cref="T:System.Web.UI.Design.WebControls.WizardDesigner" /> that is the parent designer for the wizard.</param>
		/// <param name="wizardStep">A <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> that defines the step.</param>
		// Token: 0x06000BAE RID: 2990 RVA: 0x00009525 File Offset: 0x00007725
		public WizardStepEditableRegion(WizardDesigner designer, WizardStepBase wizardStep)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the associated design-time wizard step.</summary>
		/// <returns>The associated design-time wizard step.</returns>
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x00009717 File Offset: 0x00007917
		public WizardStepBase Step
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
