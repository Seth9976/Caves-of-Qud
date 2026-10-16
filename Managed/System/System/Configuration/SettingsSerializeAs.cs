using System;

namespace System.Configuration
{
	/// <summary>Determines the serialization scheme used to store application settings.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001DA RID: 474
	public enum SettingsSerializeAs
	{
		/// <summary>The settings property is serialized as plain text.</summary>
		// Token: 0x040007D6 RID: 2006
		String,
		/// <summary>The settings property is serialized as XML using XML serialization.</summary>
		// Token: 0x040007D7 RID: 2007
		Xml,
		/// <summary>The settings property is serialized using binary object serialization.</summary>
		// Token: 0x040007D8 RID: 2008
		Binary,
		/// <summary>The settings provider has implicit knowledge of the property or its type and picks an appropriate serialization mechanism. Often used for custom serialization.</summary>
		// Token: 0x040007D9 RID: 2009
		ProviderSpecific
	}
}
