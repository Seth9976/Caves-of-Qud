using System;
using System.Collections;

namespace System.Configuration
{
	/// <summary>Provides contextual information that the provider can use when persisting settings.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C7 RID: 455
	[Serializable]
	public class SettingsContext : Hashtable
	{
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x00031DCF File Offset: 0x0002FFCF
		// (set) Token: 0x06000BF1 RID: 3057 RVA: 0x00031DD7 File Offset: 0x0002FFD7
		internal ApplicationSettingsBase CurrentSettings
		{
			get
			{
				return this.current;
			}
			set
			{
				this.current = value;
			}
		}

		// Token: 0x040007B7 RID: 1975
		[NonSerialized]
		private ApplicationSettingsBase current;
	}
}
