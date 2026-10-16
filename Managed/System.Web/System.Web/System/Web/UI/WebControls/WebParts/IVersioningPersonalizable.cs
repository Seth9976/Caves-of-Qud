using System;
using System.Collections;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Represents an interface that can manage personalization data belonging to a previous version of a Web Parts control.</summary>
	// Token: 0x0200045E RID: 1118
	public interface IVersioningPersonalizable
	{
		/// <summary>Loads personalization data to a Web Parts control that does not have a corresponding personalized property for the data due to a version change.</summary>
		/// <param name="unknownProperties">A dictionary of personalization data that could not be applied to a control.</param>
		// Token: 0x060033AA RID: 13226
		void Load(IDictionary unknownProperties);
	}
}
