using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentAdded" />, <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentAdding" />, <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRemoved" />, and <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRemoving" /> events.</summary>
	// Token: 0x02000447 RID: 1095
	public class ComponentEventArgs : EventArgs
	{
		/// <summary>Gets the component associated with the event.</summary>
		/// <returns>The component associated with the event.</returns>
		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x060023A5 RID: 9125 RVA: 0x0008114D File Offset: 0x0007F34D
		public virtual IComponent Component { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ComponentEventArgs" /> class.</summary>
		/// <param name="component">The component that is the source of the event. </param>
		// Token: 0x060023A6 RID: 9126 RVA: 0x00081155 File Offset: 0x0007F355
		public ComponentEventArgs(IComponent component)
		{
			this.Component = component;
		}
	}
}
