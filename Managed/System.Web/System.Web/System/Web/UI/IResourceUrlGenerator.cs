using System;

namespace System.Web.UI
{
	/// <summary>Defines the method that a designer-host must implement to provide URL reference look-up for embedded resources.</summary>
	// Token: 0x02000179 RID: 377
	public interface IResourceUrlGenerator
	{
		/// <summary>Returns a URL reference to an embedded resource in an assembly that is used at design time.</summary>
		/// <returns>The URL reference to the resource.</returns>
		/// <param name="type">The type in the assembly that contains the embedded resource.</param>
		/// <param name="resourceName">The name of the resource to retrieve.</param>
		// Token: 0x06000F46 RID: 3910
		string GetResourceUrl(Type type, string resourceName);
	}
}
