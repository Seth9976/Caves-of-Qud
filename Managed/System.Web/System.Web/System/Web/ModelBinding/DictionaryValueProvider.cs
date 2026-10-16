using System;
using System.Collections.Generic;
using System.Globalization;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents the base class for value providers whose values come from a collection that implements the <see cref="T:System.Collections.Generic.IDictionary`2" /> interface.</summary>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	// Token: 0x02000712 RID: 1810
	public class DictionaryValueProvider<TValue> : IValueProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.DictionaryValueProvider`1" /> class.</summary>
		/// <param name="dictionary">The dictionary.</param>
		/// <param name="culture">The culture information.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dictionary" /> parameter is null.</exception>
		// Token: 0x06004BC1 RID: 19393 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public DictionaryValueProvider(IDictionary<string, TValue> dictionary, CultureInfo culture)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns a value that indicates whether the dictionary contains the specified prefix.</summary>
		/// <returns>true if the dictionary contains the specified prefix; otherwise, false.</returns>
		/// <param name="prefix">The prefix.</param>
		// Token: 0x06004BC2 RID: 19394 RVA: 0x000C8328 File Offset: 0x000C6528
		public virtual bool ContainsPrefix(string prefix)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Retrieves a value object using the specified key.</summary>
		/// <returns>The value object.</returns>
		/// <param name="key">The key.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is null.</exception>
		// Token: 0x06004BC3 RID: 19395 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public virtual ValueProviderResult GetValue(string key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
