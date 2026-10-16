using System;

namespace System.ComponentModel
{
	/// <summary>Allows coordination of initialization for a component and its dependent properties.</summary>
	// Token: 0x020003BE RID: 958
	public interface ISupportInitializeNotification : ISupportInitialize
	{
		/// <summary>Gets a value indicating whether the component is initialized.</summary>
		/// <returns>true to indicate the component has completed initialization; otherwise, false. </returns>
		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001F22 RID: 7970
		bool IsInitialized { get; }

		/// <summary>Occurs when initialization of the component is completed.</summary>
		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06001F23 RID: 7971
		// (remove) Token: 0x06001F24 RID: 7972
		event EventHandler Initialized;
	}
}
