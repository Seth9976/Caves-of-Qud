using System;

namespace System.ComponentModel
{
	/// <summary>Provides a base class for the container filter service.</summary>
	// Token: 0x02000392 RID: 914
	public abstract class ContainerFilterService
	{
		/// <summary>Filters the component collection.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.ComponentCollection" /> that represents a modified collection.</returns>
		/// <param name="components">The component collection to filter.</param>
		// Token: 0x06001DF7 RID: 7671 RVA: 0x00003914 File Offset: 0x00001B14
		public virtual ComponentCollection FilterComponents(ComponentCollection components)
		{
			return components;
		}
	}
}
