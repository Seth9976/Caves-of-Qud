using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRename" /> event.</summary>
	// Token: 0x02000449 RID: 1097
	public class ComponentRenameEventArgs : EventArgs
	{
		/// <summary>Gets the component that is being renamed.</summary>
		/// <returns>The component that is being renamed.</returns>
		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x060023AB RID: 9131 RVA: 0x00081164 File Offset: 0x0007F364
		public object Component { get; }

		/// <summary>Gets the name of the component before the rename event.</summary>
		/// <returns>The previous name of the component.</returns>
		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x060023AC RID: 9132 RVA: 0x0008116C File Offset: 0x0007F36C
		public virtual string OldName { get; }

		/// <summary>Gets the name of the component after the rename event.</summary>
		/// <returns>The name of the component after the rename event.</returns>
		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x060023AD RID: 9133 RVA: 0x00081174 File Offset: 0x0007F374
		public virtual string NewName { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ComponentRenameEventArgs" /> class.</summary>
		/// <param name="component">The component to be renamed. </param>
		/// <param name="oldName">The old name of the component. </param>
		/// <param name="newName">The new name of the component. </param>
		// Token: 0x060023AE RID: 9134 RVA: 0x0008117C File Offset: 0x0007F37C
		public ComponentRenameEventArgs(object component, string oldName, string newName)
		{
			this.OldName = oldName;
			this.NewName = newName;
			this.Component = component;
		}
	}
}
