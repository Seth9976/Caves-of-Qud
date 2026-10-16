using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a basic, component site-specific, key-value pair dictionary through a service that a designer can use to store user-defined data.</summary>
	// Token: 0x02000462 RID: 1122
	public interface IDictionaryService
	{
		/// <summary>Gets the key corresponding to the specified value.</summary>
		/// <returns>The associated key, or null if no key exists.</returns>
		/// <param name="value">The value to look up in the dictionary. </param>
		// Token: 0x0600244A RID: 9290
		object GetKey(object value);

		/// <summary>Gets the value corresponding to the specified key.</summary>
		/// <returns>The associated value, or null if no value exists.</returns>
		/// <param name="key">The key to look up the value for. </param>
		// Token: 0x0600244B RID: 9291
		object GetValue(object key);

		/// <summary>Sets the specified key-value pair.</summary>
		/// <param name="key">An object to use as the key to associate the value with. </param>
		/// <param name="value">The value to store. </param>
		// Token: 0x0600244C RID: 9292
		void SetValue(object key, object value);
	}
}
