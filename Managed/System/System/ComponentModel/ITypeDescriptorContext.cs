using System;

namespace System.ComponentModel
{
	/// <summary>Provides contextual information about a component, such as its container and property descriptor.</summary>
	// Token: 0x020003BF RID: 959
	public interface ITypeDescriptorContext : IServiceProvider
	{
		/// <summary>Gets the container representing this <see cref="T:System.ComponentModel.TypeDescriptor" /> request.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.IContainer" /> with the set of objects for this <see cref="T:System.ComponentModel.TypeDescriptor" />; otherwise, null if there is no container or if the <see cref="T:System.ComponentModel.TypeDescriptor" /> does not use outside objects.</returns>
		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001F25 RID: 7973
		IContainer Container { get; }

		/// <summary>Gets the object that is connected with this type descriptor request.</summary>
		/// <returns>The object that invokes the method on the <see cref="T:System.ComponentModel.TypeDescriptor" />; otherwise, null if there is no object responsible for the call.</returns>
		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001F26 RID: 7974
		object Instance { get; }

		/// <summary>Gets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is associated with the given context item.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> that describes the given context item; otherwise, null if there is no <see cref="T:System.ComponentModel.PropertyDescriptor" /> responsible for the call.</returns>
		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001F27 RID: 7975
		PropertyDescriptor PropertyDescriptor { get; }

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanging" /> event.</summary>
		/// <returns>true if this object can be changed; otherwise, false.</returns>
		// Token: 0x06001F28 RID: 7976
		bool OnComponentChanging();

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanged" /> event.</summary>
		// Token: 0x06001F29 RID: 7977
		void OnComponentChanged();
	}
}
