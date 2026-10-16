using System;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Defines additional management capabilities for the application and extraction of personalization state. </summary>
	// Token: 0x02000480 RID: 1152
	public interface IPersonalizable
	{
		/// <summary>Gets a value that indicates whether the custom data that a control manages has changed. </summary>
		/// <returns>true if the custom data managed with the <see cref="T:System.Web.UI.WebControls.WebParts.IPersonalizable" /> interface has changed; otherwise, false.</returns>
		// Token: 0x17001077 RID: 4215
		// (get) Token: 0x06003430 RID: 13360
		bool IsDirty { get; }

		/// <summary>Loads custom data into a control. </summary>
		/// <param name="state">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> that contains custom scoped data that was loaded from the underlying data store.</param>
		// Token: 0x06003431 RID: 13361
		void Load(PersonalizationDictionary state);

		/// <summary>Saves custom properties and internal state information in the control's <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> object.</summary>
		/// <param name="state">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> that contains custom scoped data that was loaded from the underlying data store.</param>
		// Token: 0x06003432 RID: 13362
		void Save(PersonalizationDictionary state);
	}
}
