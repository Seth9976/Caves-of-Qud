using System;

namespace System.Web.ModelBinding
{
	/// <summary>Defines the methods that are required for a value provider. </summary>
	// Token: 0x02000519 RID: 1305
	public interface IValueProvider
	{
		/// <summary>Returns a value that specifies whether the collection contains the specified prefix.</summary>
		/// <returns>true if the collection contains the specified prefix; otherwise, false.</returns>
		/// <param name="prefix">The prefix.</param>
		// Token: 0x060039A9 RID: 14761
		bool ContainsPrefix(string prefix);

		/// <summary>Returns a value object using the specified key.</summary>
		/// <returns>The value object.</returns>
		/// <param name="key">The key.</param>
		// Token: 0x060039AA RID: 14762
		ValueProviderResult GetValue(string key);
	}
}
