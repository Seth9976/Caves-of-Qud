using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.TypeDescriptor.Refreshed" /> event.</summary>
	// Token: 0x020003EA RID: 1002
	public class RefreshEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RefreshEventArgs" /> class with the component that has changed.</summary>
		/// <param name="componentChanged">The component that changed. </param>
		// Token: 0x060020D1 RID: 8401 RVA: 0x00071714 File Offset: 0x0006F914
		public RefreshEventArgs(object componentChanged)
		{
			this.ComponentChanged = componentChanged;
			this.TypeChanged = componentChanged.GetType();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RefreshEventArgs" /> class with the type of component that has changed.</summary>
		/// <param name="typeChanged">The <see cref="T:System.Type" /> that changed. </param>
		// Token: 0x060020D2 RID: 8402 RVA: 0x0007172F File Offset: 0x0006F92F
		public RefreshEventArgs(Type typeChanged)
		{
			this.TypeChanged = typeChanged;
		}

		/// <summary>Gets the component that changed its properties, events, or extenders.</summary>
		/// <returns>The component that changed its properties, events, or extenders, or null if all components of the same type have changed.</returns>
		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x060020D3 RID: 8403 RVA: 0x0007173E File Offset: 0x0006F93E
		public object ComponentChanged { get; }

		/// <summary>Gets the <see cref="T:System.Type" /> that changed its properties or events.</summary>
		/// <returns>The <see cref="T:System.Type" /> that changed its properties or events.</returns>
		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x060020D4 RID: 8404 RVA: 0x00071746 File Offset: 0x0006F946
		public Type TypeChanged { get; }
	}
}
