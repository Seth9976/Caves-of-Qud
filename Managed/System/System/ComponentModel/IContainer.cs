using System;

namespace System.ComponentModel
{
	/// <summary>Provides functionality for containers. Containers are objects that logically contain zero or more components.</summary>
	// Token: 0x02000371 RID: 881
	public interface IContainer : IDisposable
	{
		/// <summary>Adds the specified <see cref="T:System.ComponentModel.IComponent" /> to the <see cref="T:System.ComponentModel.IContainer" /> at the end of the list.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to add. </param>
		// Token: 0x06001D19 RID: 7449
		void Add(IComponent component);

		/// <summary>Adds the specified <see cref="T:System.ComponentModel.IComponent" /> to the <see cref="T:System.ComponentModel.IContainer" /> at the end of the list, and assigns a name to the component.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to add. </param>
		/// <param name="name">The unique, case-insensitive name to assign to the component.-or- null that leaves the component unnamed. </param>
		// Token: 0x06001D1A RID: 7450
		void Add(IComponent component, string name);

		/// <summary>Gets all the components in the <see cref="T:System.ComponentModel.IContainer" />.</summary>
		/// <returns>A collection of <see cref="T:System.ComponentModel.IComponent" /> objects that represents all the components in the <see cref="T:System.ComponentModel.IContainer" />.</returns>
		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001D1B RID: 7451
		ComponentCollection Components { get; }

		/// <summary>Removes a component from the <see cref="T:System.ComponentModel.IContainer" />.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to remove. </param>
		// Token: 0x06001D1C RID: 7452
		void Remove(IComponent component);
	}
}
