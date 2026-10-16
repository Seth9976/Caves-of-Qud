using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.Serialization.IDesignerSerializationManager.ResolveName" /> event.</summary>
	// Token: 0x02000493 RID: 1171
	public class ResolveNameEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.ResolveNameEventArgs" /> class.</summary>
		/// <param name="name">The name to resolve. </param>
		// Token: 0x06002557 RID: 9559 RVA: 0x0008325C File Offset: 0x0008145C
		public ResolveNameEventArgs(string name)
		{
			this.Name = name;
			this.Value = null;
		}

		/// <summary>Gets the name of the object to resolve.</summary>
		/// <returns>The name of the object to resolve.</returns>
		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06002558 RID: 9560 RVA: 0x00083272 File Offset: 0x00081472
		public string Name { get; }

		/// <summary>Gets or sets the object that matches the name.</summary>
		/// <returns>The object that the name is associated with.</returns>
		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06002559 RID: 9561 RVA: 0x0008327A File Offset: 0x0008147A
		// (set) Token: 0x0600255A RID: 9562 RVA: 0x00083282 File Offset: 0x00081482
		public object Value { get; set; }
	}
}
