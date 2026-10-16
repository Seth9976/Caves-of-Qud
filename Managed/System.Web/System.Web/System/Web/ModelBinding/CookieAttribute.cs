using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents an attribute that specifies that the values for model binding are provided by a cookie.</summary>
	// Token: 0x02000705 RID: 1797
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class CookieAttribute : ValueProviderSourceAttribute, IUnvalidatedValueProviderSource
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.CookieAttribute" /> class.</summary>
		// Token: 0x06004B8D RID: 19341 RVA: 0x00003946 File Offset: 0x00001B46
		public CookieAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.CookieAttribute" /> class using the specified cookie name.</summary>
		/// <param name="name">The name of the cookie.</param>
		// Token: 0x06004B8E RID: 19342 RVA: 0x00003946 File Offset: 0x00001B46
		public CookieAttribute(string name)
		{
		}

		/// <summary>Gets the name of the cookie.</summary>
		/// <returns>The name of the cookie.</returns>
		// Token: 0x1700176B RID: 5995
		// (get) Token: 0x06004B8F RID: 19343 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string Name
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a value that specifies whether cookie values should be validated.</summary>
		/// <returns>true if cookie values are validated; otherwise, false. The default is true.</returns>
		// Token: 0x1700176C RID: 5996
		// (get) Token: 0x06004B90 RID: 19344 RVA: 0x000C8264 File Offset: 0x000C6464
		// (set) Token: 0x06004B91 RID: 19345 RVA: 0x00003946 File Offset: 0x00001B46
		public bool ValidateInput
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
			}
		}

		/// <summary>Returns the model name.</summary>
		/// <returns>The model name.</returns>
		// Token: 0x06004B92 RID: 19346 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override string GetModelName()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns the value provider.</summary>
		/// <returns>The value provider.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="modelBindingExecutionContext" /> parameter is null.</exception>
		// Token: 0x06004B93 RID: 19347 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
