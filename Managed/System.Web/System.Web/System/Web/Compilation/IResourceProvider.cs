using System;
using System.Globalization;
using System.Resources;

namespace System.Web.Compilation
{
	/// <summary>Defines the interface a class must implement to act as a resource provider.</summary>
	// Token: 0x02000653 RID: 1619
	public interface IResourceProvider
	{
		/// <summary>Returns a resource object for the key and culture.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the resource value for the <paramref name="resourceKey" /> and <paramref name="culture" />.</returns>
		/// <param name="resourceKey">The key identifying a particular resource.</param>
		/// <param name="culture">The culture identifying a localized value for the resource.</param>
		// Token: 0x06004575 RID: 17781
		object GetObject(string resourceKey, CultureInfo culture);

		/// <summary>Gets an object to read resource values from a source.</summary>
		/// <returns>The <see cref="T:System.Resources.IResourceReader" /> associated with the current resource provider.</returns>
		// Token: 0x170015C2 RID: 5570
		// (get) Token: 0x06004576 RID: 17782
		IResourceReader ResourceReader { get; }
	}
}
