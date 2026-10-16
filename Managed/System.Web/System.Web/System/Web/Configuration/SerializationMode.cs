using System;

namespace System.Web.Configuration
{
	/// <summary>Determines the serialization method used for a <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object.</summary>
	// Token: 0x02000573 RID: 1395
	public enum SerializationMode
	{
		/// <summary>The <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object is serialized to a simple string.</summary>
		// Token: 0x04002060 RID: 8288
		String,
		/// <summary>The profile <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> is serialized as XML using XML serialization.</summary>
		// Token: 0x04002061 RID: 8289
		Xml,
		/// <summary>The <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object is serialized using binary serialization.</summary>
		// Token: 0x04002062 RID: 8290
		Binary,
		/// <summary>The provider has implicit knowledge of the type and is responsible for deciding how to serialize the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object into the data store.</summary>
		// Token: 0x04002063 RID: 8291
		ProviderSpecific
	}
}
