using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents a value provider for cookie values.</summary>
	// Token: 0x02000706 RID: 1798
	public sealed class CookieValueProvider : IUnvalidatedValueProvider, IValueProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.CookieValueProvider" /> class.</summary>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		// Token: 0x06004B94 RID: 19348 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public CookieValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns a value that indicates whether the cookie collection contains the specified prefix.</summary>
		/// <returns>true if the cookie collection contains the specified prefix; otherwise, false.</returns>
		/// <param name="prefix">The prefix.</param>
		// Token: 0x06004B95 RID: 19349 RVA: 0x000C8280 File Offset: 0x000C6480
		public bool ContainsPrefix(string prefix)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Retrieves a value object using the specified key.</summary>
		/// <returns>The value object.</returns>
		/// <param name="key">The key.</param>
		// Token: 0x06004B96 RID: 19350 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public ValueProviderResult GetValue(string key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Retrieves a value object using the specified key and a parameter that specifies whether validation should be skipped.</summary>
		/// <returns>The value object.</returns>
		/// <param name="key">The key.</param>
		/// <param name="skipValidation">true to skip validation; otherwise, false.</param>
		// Token: 0x06004B97 RID: 19351 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public ValueProviderResult GetValue(string key, bool skipValidation)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
