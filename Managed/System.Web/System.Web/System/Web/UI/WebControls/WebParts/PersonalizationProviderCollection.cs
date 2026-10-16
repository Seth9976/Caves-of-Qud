using System;
using System.Configuration.Provider;
using System.Reflection;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Stores references to <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationProvider" /> objects indexed by name. This class cannot be inherited. </summary>
	// Token: 0x020007B1 RID: 1969
	[DefaultMember("Item")]
	public sealed class PersonalizationProviderCollection : ProviderCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationProviderCollection" /> class. </summary>
		// Token: 0x06004F85 RID: 20357 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public PersonalizationProviderCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Copies the personalization providers in the collection to an array, starting at the specified index.</summary>
		/// <param name="array">The array to which the personalization providers are copied.</param>
		/// <param name="index">The location in the array at which to begin copying.</param>
		// Token: 0x06004F86 RID: 20358 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void CopyTo(PersonalizationProvider[] array, int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
