using System;

namespace System.ComponentModel
{
	/// <summary>Provides the abstract base class for all licenses. A license is granted to a specific instance of a component.</summary>
	// Token: 0x020003C9 RID: 969
	public abstract class License : IDisposable
	{
		/// <summary>When overridden in a derived class, gets the license key granted to this component.</summary>
		/// <returns>A license key granted to this component.</returns>
		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001F4E RID: 8014
		public abstract string LicenseKey { get; }

		/// <summary>When overridden in a derived class, disposes of the resources used by the license.</summary>
		// Token: 0x06001F4F RID: 8015
		public abstract void Dispose();
	}
}
