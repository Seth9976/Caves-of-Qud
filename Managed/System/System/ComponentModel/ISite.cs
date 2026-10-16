using System;

namespace System.ComponentModel
{
	/// <summary>Provides functionality required by sites.</summary>
	// Token: 0x02000372 RID: 882
	public interface ISite : IServiceProvider
	{
		/// <summary>Gets the component associated with the <see cref="T:System.ComponentModel.ISite" /> when implemented by a class.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IComponent" /> instance associated with the <see cref="T:System.ComponentModel.ISite" />.</returns>
		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001D1D RID: 7453
		IComponent Component { get; }

		/// <summary>Gets the <see cref="T:System.ComponentModel.IContainer" /> associated with the <see cref="T:System.ComponentModel.ISite" /> when implemented by a class.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IContainer" /> instance associated with the <see cref="T:System.ComponentModel.ISite" />.</returns>
		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001D1E RID: 7454
		IContainer Container { get; }

		/// <summary>Determines whether the component is in design mode when implemented by a class.</summary>
		/// <returns>true if the component is in design mode; otherwise, false.</returns>
		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001D1F RID: 7455
		bool DesignMode { get; }

		/// <summary>Gets or sets the name of the component associated with the <see cref="T:System.ComponentModel.ISite" /> when implemented by a class.</summary>
		/// <returns>The name of the component associated with the <see cref="T:System.ComponentModel.ISite" />; or null, if no name is assigned to the component.</returns>
		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001D20 RID: 7456
		// (set) Token: 0x06001D21 RID: 7457
		string Name { get; set; }
	}
}
