using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface for adding and removing extender providers at design time.</summary>
	// Token: 0x02000465 RID: 1125
	public interface IExtenderProviderService
	{
		/// <summary>Adds the specified extender provider.</summary>
		/// <param name="provider">The extender provider to add. </param>
		// Token: 0x06002456 RID: 9302
		void AddExtenderProvider(IExtenderProvider provider);

		/// <summary>Removes the specified extender provider.</summary>
		/// <param name="provider">The extender provider to remove. </param>
		// Token: 0x06002457 RID: 9303
		void RemoveExtenderProvider(IExtenderProvider provider);
	}
}
