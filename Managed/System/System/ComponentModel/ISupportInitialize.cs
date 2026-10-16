using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that this object supports a simple, transacted notification for batch initialization.</summary>
	// Token: 0x02000373 RID: 883
	public interface ISupportInitialize
	{
		/// <summary>Signals the object that initialization is starting.</summary>
		// Token: 0x06001D22 RID: 7458
		void BeginInit();

		/// <summary>Signals the object that initialization is complete.</summary>
		// Token: 0x06001D23 RID: 7459
		void EndInit();
	}
}
