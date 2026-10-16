using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IDesignerEventService.DesignerCreated" /> and <see cref="E:System.ComponentModel.Design.IDesignerEventService.DesignerDisposed" /> events.</summary>
	// Token: 0x02000454 RID: 1108
	public class DesignerEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerEventArgs" /> class.</summary>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> of the document. </param>
		// Token: 0x060023F6 RID: 9206 RVA: 0x000817F2 File Offset: 0x0007F9F2
		public DesignerEventArgs(IDesignerHost host)
		{
			this.Designer = host;
		}

		/// <summary>Gets the host of the document.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> of the document.</returns>
		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x060023F7 RID: 9207 RVA: 0x00081801 File Offset: 0x0007FA01
		public IDesignerHost Designer { get; }
	}
}
