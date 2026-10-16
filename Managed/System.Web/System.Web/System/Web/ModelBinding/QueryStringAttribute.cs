using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents an attribute that specifies that model binding values are provided by a query string value.</summary>
	// Token: 0x02000729 RID: 1833
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class QueryStringAttribute : ValueProviderSourceAttribute, IUnvalidatedValueProviderSource
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.QueryStringAttribute" /> class.</summary>
		// Token: 0x06004C19 RID: 19481 RVA: 0x00003946 File Offset: 0x00001B46
		public QueryStringAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.QueryStringAttribute" /> class using the specified query string key.</summary>
		/// <param name="key">The key.</param>
		// Token: 0x06004C1A RID: 19482 RVA: 0x00003946 File Offset: 0x00001B46
		public QueryStringAttribute(string key)
		{
		}

		/// <summary>Gets the key of the query string value.</summary>
		/// <returns>The key of the query string value.</returns>
		// Token: 0x17001784 RID: 6020
		// (get) Token: 0x06004C1B RID: 19483 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string Key
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a value that indicates whether query string values are validated.</summary>
		/// <returns>true if query string values are validated; otherwise, false. The default is true.</returns>
		// Token: 0x17001785 RID: 6021
		// (get) Token: 0x06004C1C RID: 19484 RVA: 0x000C84B0 File Offset: 0x000C66B0
		// (set) Token: 0x06004C1D RID: 19485 RVA: 0x00003946 File Offset: 0x00001B46
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

		/// <summary>Returns the name of the model.</summary>
		/// <returns>The name of the model.</returns>
		// Token: 0x06004C1E RID: 19486 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override string GetModelName()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns the value provider.</summary>
		/// <returns>The value provider.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="modelBindingExecutionContext" /> parameter is null.</exception>
		// Token: 0x06004C1F RID: 19487 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
