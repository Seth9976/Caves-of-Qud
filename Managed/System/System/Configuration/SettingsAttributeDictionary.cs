using System;
using System.Collections;

namespace System.Configuration
{
	/// <summary>Represents a collection of key/value pairs used to describe a configuration object as well as a <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
	// Token: 0x020001C5 RID: 453
	[Serializable]
	public class SettingsAttributeDictionary : Hashtable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsAttributeDictionary" /> class. </summary>
		// Token: 0x06000BE0 RID: 3040 RVA: 0x00031A1D File Offset: 0x0002FC1D
		public SettingsAttributeDictionary()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsAttributeDictionary" /> class. </summary>
		/// <param name="attributes">A collection of key/value pairs that are related to configuration settings.</param>
		// Token: 0x06000BE1 RID: 3041 RVA: 0x00031A25 File Offset: 0x0002FC25
		public SettingsAttributeDictionary(SettingsAttributeDictionary attributes)
			: base(attributes)
		{
		}
	}
}
