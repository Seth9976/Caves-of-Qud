using System;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Enables <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls or other server controls to contain collections of verbs.</summary>
	// Token: 0x0200045F RID: 1119
	public interface IWebActionable
	{
		/// <summary>Gets a reference to a collection of custom <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> objects.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerbCollection" /> that contains custom <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> objects.</returns>
		// Token: 0x17001055 RID: 4181
		// (get) Token: 0x060033AB RID: 13227
		WebPartVerbCollection Verbs { get; }
	}
}
