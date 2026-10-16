using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="P:System.ComponentModel.Design.IDesignerEventService.ActiveDesigner" /> event.</summary>
	// Token: 0x0200043F RID: 1087
	public class ActiveDesignerEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ActiveDesignerEventArgs" /> class.</summary>
		/// <param name="oldDesigner">The document that is losing activation. </param>
		/// <param name="newDesigner">The document that is gaining activation. </param>
		// Token: 0x06002382 RID: 9090 RVA: 0x00080FA1 File Offset: 0x0007F1A1
		public ActiveDesignerEventArgs(IDesignerHost oldDesigner, IDesignerHost newDesigner)
		{
			this.OldDesigner = oldDesigner;
			this.NewDesigner = newDesigner;
		}

		/// <summary>Gets the document that is losing activation.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that represents the document losing activation.</returns>
		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06002383 RID: 9091 RVA: 0x00080FB7 File Offset: 0x0007F1B7
		public IDesignerHost OldDesigner { get; }

		/// <summary>Gets the document that is gaining activation.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that represents the document gaining activation.</returns>
		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06002384 RID: 9092 RVA: 0x00080FBF File Offset: 0x0007F1BF
		public IDesignerHost NewDesigner { get; }
	}
}
